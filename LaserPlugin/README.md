# LaserPlugin

A complete laser device plugin. This is the most thorough device sample in the collection — if you
are writing a plugin that controls a piece of hardware, start here.

The plugin adds a "My Laser" device to DMC, gives it a settings page, a ribbon command, a popup
control panel, a live status readout, and the ability to inject laser state changes into marking
parameters. All actual hardware I/O is left as commented-out stubs for you to fill in.

## What it shows

- Implementing four DMC interfaces on a single plugin class.
- Adding a recipe command to the ribbon (`DMC.Helpers.AddTool` + `ICommand.AddCreator`).
- Adding a popup tool panel and a status tool to the Home tab, and showing/hiding them when the
  device is enabled or disabled in settings.
- Publishing live device values (power, frequency, error code, state) as global status parameters.
- Extending DMC's marking parameters so laser settings can change between marking blocks, with
  de-duplication so identical states are not re-sent.
- Vetoing recipe run/pause/continue when the device is not connected.
- Building a parameter GUI automatically instead of hand-drawing one.
- A background monitoring task with a `CancellationToken`.

## Project layout

| File | Purpose |
| --- | --- |
| `Plugin.cs` | Entry point. Implements `IDevice`, `IRecipeControl`, `IMarkingParametersControl`, `IStatusProvider`. Registers tools and marking-parameter extensions in its constructor. |
| `Laser.cs` | The device abstraction. Connect/disconnect plus a background monitoring task. All serial/TCP calls are stubs. |
| `LaserStatus.cs` | `StatusParameters` subclass publishing trigger frequency, power, error code and a `LaserState` enum. |
| `Settings.cs` | `IDeviceSettings` + `ILaserSettings`. COM port, baud rate, timeout. Shown at the bottom of the Laser Control tab when this device is selected. |
| `Command.cs` | The "My Laser" recipe command. Validates ranges at compile time, applies laser state at run time. |
| `CommandParameters.cs` | The shared parameter block (shutter, LD on/off, PP frequency, MOD frequency/width/delay/efficiency) reused by both the command and the marking-parameter extension. |
| `Marking.cs` | `Laser_MarkingParameters : MarkingParamsEx` and `Laser_MPExtState : ActionCommand` — how to attach device state to a marking-parameters block. |
| `SettingsGUI`, `ToolGUI`, `StatusGUI`, `CommandGUI` | WinForms user controls for the settings page, popup panel, status readout and (optional) hand-built command GUI. |
| `README.txt` | The original two-line build note, superseded by this file. |

## Key DMC interfaces used

- **`Base.IDevice`** — the base plugin contract: connect, disconnect, enable, apply settings, recipe
  start/finish hooks, error message.
- **`Base.UIBase.Interfaces.IRecipeControl`** — lets the device block a recipe from running, pausing
  or continuing. Here it refuses to run when the laser is not connected.
- **`IMarkingParametersControl`** — called by the motion system before a marking block.
  `NeedToRunCommandList` decides whether anything must change; `Set` applies it. The `IsSame`
  comparison on `Laser_MPExtState` avoids re-sending identical state.
- **`IStatusProvider`** — returns a list of `StatusParameters` that DMC surfaces as global variables
  and status displays.
- **`Base.Devices.LaserParameters.ILaserParameterSetter`** and **`ILaserSettings`** — let DMC
  interrogate the command about the laser's frequency capability.
- **`Core.Commands.MarkingParamsEx`** — the extension point for adding your own fields to a marking
  parameters block.

## How it hooks into DMC

The constructor does four things:

1. Registers the `Command` type under the unique name `my_laser` in the ribbon's "Devices" group and
   gives it an icon.
2. Creates the `Laser` object from the settings.
3. Adds a `Laser_MarkingParameters` instance to `Core.Commands.MarkingParams.AdditionalControls` and
   to `Base.IParameter`, so the extra laser fields appear inside every marking-parameters block.
4. Registers itself with `Base.SystemMotion.Active.marking_parameters_control`.

`ApplySettings()` is where enable/disable is handled: it lazily creates the popup and status tools
the first time the device is enabled, toggles their visibility, adds or clears the status
parameters, and reconnects if DMC is already connected to hardware.

At run time, the recipe can change laser state two ways: explicitly, by dropping a "My Laser" command
into the recipe, or implicitly, through the extra fields the plugin adds to marking parameters.

## Building

Targets **.NET Framework 4.8**, output type Library.

1. Remove the existing `Base.dll`, `Core.dll` and `GUI.dll` references and re-add them from your
   installed DMC version. The checked-in paths point at
   `C:\Program Files (x86)\DMC\DMC 1.2.31\` and will not resolve on your machine. For debugging,
   prefer the 32-bit DLLs — the Visual Studio forms designer can misbehave with the 64-bit build.
2. Set **Project → Properties → Build → Output path** to your DMC `Plugins` directory.
3. Build. DMC loads any DLL in `Plugins` that exposes a public, non-abstract class whose name
   contains "Plugin" and which implements `Base.IDevice`.

## Notes and limitations

- No real hardware communication is implemented. `Laser.IsConnected` always returns `true`, and every
  `SetLD` / `Shutter` / `SetPPFrequency` / `SetMOD2*` call site is commented out with a
  `//Your implementation` marker. Fill these in against your laser's protocol.
- `LaserStatus` still carries the unique name `edgewave_state` from the original device it was
  derived from — rename it if you ship this.
- If your laser can report its actual pulse frequency, the monitoring thread contains a worked
  example of pushing that value back into `Base.Settings.LaserControls[i].LaserFrequencyActual`.
- Parameter validation ranges in `Command.Compile` (200–10000 kHz, 0–100 %) are placeholders.
