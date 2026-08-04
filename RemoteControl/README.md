# RemoteControl

A client for the DMC **Remote Control Module (RCM)** — the TCP interface that lets an external
program drive DMC over the network. This is not a plugin; it is a standalone WinForms application
that connects to a running DMC instance.

The reusable deliverable is **`RCMClient.cs`**: a single, fully XML-documented file wrapping the
whole RCM command set. Drop it into your own project (WinForms, WPF, console or service) and you have
a typed API instead of raw socket strings. `Form1` is a test harness that exercises it.

The bundled **`Remote Control Module.docx`** documents the wire protocol itself.

## What it shows

- Connecting to DMC over TCP on port 23, with optional CRC32 checksums and message IDs.
- Loading, activating, running, pausing, cancelling and saving recipes — including synchronous
  variants that block until the run or compile completes.
- Reading and writing recipe variables, global variables and queue variables.
- Moving, homing, enabling, jogging and stopping axes.
- Reading and writing IO tools.
- Creating geometry remotely with typed parameter objects.
- Uploading files to the DMC machine.
- Consuming DMC's MJPEG preview stream and displaying it live.

## Project layout

Output type **WinExe**.

| File | Purpose |
| --- | --- |
| `RCMClient.cs` | The API. `RCMClient` plus the geometry parameter classes and enums. ~2300 lines, all documented. |
| `Classes/MJPEGStreamReader.cs` | Reads DMC's MJPEG preview stream. Documented with both WinForms and WPF consumption examples. |
| `Classes/FrameReceivedEventArgs.cs`, `ConnectionStatusEventArgs.cs` | Event payloads for the stream reader. |
| `Form1.cs` | The test harness: connect, load and run recipes, jog and move two axes, read positions and galvo settings, autorun loop, live preview. |
| `Remote Control Module.docx` | Protocol documentation. |

## Using RCMClient

The class-level XML comment carries a minimal console example. In outline:

```
RCMClient client = new RCMClient();
client.Connect(IPAddress.Loopback, out string error, use_message_id: true, use_checksum: true);
client.ConnectToHardware(ref error);
client.RecipeLoad(@"C:\Recipes\Recipe1.rcp", ref error);
client.RecipeRunSync(ref error);
client.Disconnect();
```

Most methods follow the same shape: they return `bool` and take `ref string error_message`. Property
getters (`IsConnectedToHardware`, `IsRecipeRunning`, `IsRecipePaused`, `IsInErrorState`,
`IsInProgress`, `IsRecipeIdle`) each perform a round trip, so cache them if you poll in a tight loop.
Every call goes through one lock, so the client is safe to share across threads but will serialise.

### Command coverage

| Area | Methods |
| --- | --- |
| Connection | `Connect`, `Disconnect`, `ConnectToHardware`, `DisconnectFromHardware`, `IsConnectedToHardware` |
| Recipes | `RecipeLoad`, `RecipeUnload`, `RecipeUnloadAll`, `ActivateRecipe`, `GetActiveRecipe`, `GetRecipes`, `RecipeSave`, `RecipeSaveAs` |
| Execution | `RecipeRun`, `RecipeRunSync`, `RecipeCompileSync`, `RecipePause`, `RecipeContinue`, `RecipeCancel`, `Estimate`, `Remaining`, `GetStatus` |
| Variables | `GetVar`, `SetVar`, `GetGVar`, `SetGVar`, `GetVariables`, `GetValue`, `SetVarValue`, `Queue`, `QueueCount` |
| Commands | `GetPrm`, `GetPrmList`, `SetPrm`, `CommandSetSkip` |
| Motion | `Move`, `HomeAxis`, `Enable`, `Disable`, `ClearAxisError`, `AxisFreemove`, `StopMotion`, `GetPosition` |
| IO and laser | `GetInput`, `SetOutput`, `LaserON`, `LaserOFF` |
| Galvo | `GetGalvo`, `SetGalvo`, `GalvoApplySettings` |
| Geometry | `AddLine`, `AddCircle`, `AddRectangle`, `AddText`, `AddBarcode`, `AddWrapping4D`, `SetTransform`, `SetWorkingZone`, `RemoveWorkingZone` |
| Application | `View`, `ShowApp`, `HideApp`, `CloseDMC`, `LoadSettings`, `SaveSettings`, `SendFile`, `ClearError`, `GetLastError`, `Send` |

`Send(cmd)` is the escape hatch for any command the wrapper does not cover.

### Geometry parameter classes

`AddLine`, `AddCircle`, `AddRectangle`, `AddText`, `AddBarcode` and `AddWrapping4D` take typed objects
(`LineParameters`, `CircleParameters`, `RectangleParameters`, `TextParameters`, `BarcodeParameters`
with `Barcode_CODE_128_Parameters` / `Barcode_DATA_MATRIX_Parameters` / `Barcode_QR_CODE_Parameters`
subclasses, and `Wrapping4d_Parameters`) whose `ToString()` renders the protocol's
`key:value key:value` syntax. Required values are constructor arguments; optional ones are nullable
properties that are only emitted when set. Supporting enums: `Sorting`, `RenderPointAs`,
`ReferencePoint`, `QRCodeVersion`, `RCMClient.GalvoSettings`.

### Checksums and message IDs

When `UseChecksum` is on, outgoing messages become `[id|]message|CRC32` with the checksum as eight
uppercase hex digits, and responses are parsed and verified against the same regex before being
returned. `UseMessageID` adds the incrementing counter (wrapping at 999999). Both are off by default
and are negotiated per connection in `Connect`.

Subscribe to the `RCMEvent` delegate to log every message in both directions — `Form1` uses this for
its traffic log.

## The MJPEG preview

`MJPEGStreamReader.ConnectAsync(url)` opens a TCP connection, sends a hand-built HTTP GET requesting
`multipart/x-mixed-replace`, and starts a background thread that splits the stream on the
`--myboundary` marker, reads each part's `Content-Length` and raises `FrameReceived` with the JPEG
bytes. `ConnectionStatusChanged` reports connect, disconnect and errors.

The XML docs on the `FrameReceived` event contain complete handler examples for both WinForms
(marshalling with `Invoke`, disposing the previous `Image` to avoid a leak) and WPF (building a frozen
`BitmapImage` on the dispatcher).

## Building and running

Targets **.NET Framework 4.8**, output type WinExe. No DMC assembly references — it talks over the
network, so it can be built and run on a different machine from DMC.

1. Build.
2. Start DMC with the Remote Control Module enabled.
3. Run this application. It connects to `IPAddress.Loopback` by default; change that in `Form1` to
   reach DMC on another host.

## Notes and limitations

- The port (23) is hard-coded in `Connect`.
- `Form1` hard-codes `C:\Recipes\Recipe1.rcp` in one of its buttons; change it or the button fails.
- `PickAndPlace` is marked internal and its comment states it only works when the
  [TestPlugin](../TestPlugin/) sample is loaded in DMC — that plugin is what registers the
  `PICK_AND_PLACE` remote function.
- `RecipeRunSync` and `RecipeCompileSync` poll `IS_IN_PROGRESS` in a 1 ms loop with no timeout, so a
  hung recipe blocks the caller forever.
- `RecipePause` with `wait_for_paused_state` inverts its wait condition — it returns as soon as
  `IS_PAUSED 1` is *not* matched.
- The MJPEG parser converts the whole buffer to a UTF-8 string to locate boundaries, which is
  workable but not efficient for high frame rates.
- `Connect` reads and discards the greeting banner without checking it.
