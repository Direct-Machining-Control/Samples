# TestPlugin

A scratchpad of independent techniques rather than one coherent plugin. Most of the demos are
commented out in `Plugin.cs`; you enable the one you want to study by uncommenting a line.

This folder also carries the canonical **`How to make DMC plugin.pdf`** (and the `.docx` source),
which is the document to read before anything else.

## The plugin contract

The comment at the top of `Plugin.cs` states the rule DMC uses to find plugins:

> Class name must be public, contain the word "Plugin", must inherit the `IDevice` interface, and
> cannot be abstract.

Note that a single DLL can contain several such classes — this project ships `MyPlugin` and
`DistanceSensorPlugin` side by side, and both load.

## What is in here

### Remote control extensions — `RCM.cs`

Adds custom commands to the Remote Control Module protocol by implementing `Base.IRCFunction` and
registering with `Base.Settings.AddRC`. This is the only demo enabled by default. Three examples:

- `IMPORT` — no arguments; marshals to the UI thread with `main_window.BeginInvoke` and opens the CAD
  import dialog. Any RC function that touches the UI must do this.
- `IS_CONNECED` — returns `1` or `0` from `State.is_connected_to_hardware` (the typo is in the
  original).
- `PICK_AND_PLACE` — six arguments; a full worked example that guards on connection and machine
  state, parses each argument through `Core.Evaluation.Parse` (so callers can pass expressions),
  jumps to the pick position, drives a preconfigured digital output named `pick` via
  `Settings.IOTools.GetOutput`, jumps to the place position and releases.

The matching client side is in the [RemoteControl](../RemoteControl/) sample, whose
`RCMClient.PickAndPlace` is documented as only working when this plugin is loaded.

### A focus/distance sensor device — `DistanceSensor.cs`

A second `IDevice` in the same DLL, plus `DistanceSensor : Core.Commands.IFocusDevice` registered
into `Core.Commands.FindFocus.devices` — that is what makes it selectable in DMC's Find Focus
command. The device implements a scan: read at the current position, and if nothing is in range,
step upwards through the scanning range and then downwards, moving the axis each step, until a
reading lands within ±0.3.

It also shows lazily creating ribbon tools the first time the device is enabled, keeping them in a
list, and toggling their visibility from `ApplySettings()`. The two Control-tab tools
("Laser To Distance Sensor" and back) jump the stage by the configured XYZ offset between the laser
spot and the sensor spot.

### A command that uses the device — `DistanceSensorCommand.cs`

Shows `Core.Commands.Positioning` as a parameter, `position.GetShift(...)` to resolve the target,
inserting a `FakeMotionCommand` at compile time so the recipe's notion of "last position" advances
without emitting real motion, and writing the measurement into a named recipe variable. Also
implements `Stop()` so a long measurement can be cancelled.

### Geometry generation and hatching — `MyPoly.cs`

A container command (`CanContainChilds`) that generates a five-corner polygon at a radius read from a
recipe variable via `Evaluation.Parse`, collects its children's geometry with the same
`Make2DCommands` collector pattern used by [JoinAndHatchPlugin](../JoinAndHatchPlugin/), centres them
inside the polygon, hatches the result with `Hatching.HatchLines`, then mirrors and translates the
hatch. `Run()` demonstrates forcing the CAD processor to generate and execute motion repeatedly
(`cad.Run()` in a loop) rather than once at the end.

### A minimal command — `CommandToMoveAxis.cs`

About as small as a working command gets: one numeric parameter, then take
`Base.Settings.Axes[0]` and call `axis.Move(pos, true)`.

### Drawing into the 3D view — `DrawShape.cs`, `ViewButton.cs`

`MyArea : ActionCommand` overrides `Draw(IView)` to render a red rectangle, and installs itself with
`Base.View.CurrentView.AddToBase(active, ObjectDrawingOrder.BEGIN)`. `ViewButton` does the same for a
bitmap, converting it with `CADImport.ImageReader.GetArrayARGB` and drawing via `view.DrawImage`.
`ObjectDrawingOrder` controls whether your overlay sits behind or in front of the geometry.

### Toolbars and menus — `ViewToolbar.cs`, `PopupTools.cs`

`ViewToolbar` adds floating buttons inside the 3D view itself (`Base.View.CurrentView.AddViewToolbar`)
wired to `Actions.ViewFitScreen` / `ViewReset`. `PopupTools` builds a ribbon dropdown whose contents
are rebuilt every time it opens: `DMC.Helpers.AddPopupMenuTool` takes a `FillDropDown` callback that
removes the old items (`Helpers.RemoveTool(tool.GetKey())`) and adds one per entry in
`Settings.LaserControls`. Use this when the menu contents depend on current configuration.

### A serial-port device — `SerialControl.cs`

A device that mirrors machine state to an external controller: on connect it opens the first
available serial port, sends `CONNECT`, then polls a digital input named `DoorOpened` (configured in
Settings → IO Tools) once a second and sends `DOOR OPENED` / `DOOR CLOSED` on change. Sends `STOP` on
stop and `DISCONNECT` on disconnect. The class deliberately does *not* implement `IDevice` (the
declaration is commented out) so it does not load unless you want it.

### A developer utility — `TakeImages.cs`

Not a plugin feature. A floating window that walks the control tree of whichever DMC form is active,
shows it as a tree, and screenshots the selected control to `Settings.PathTEMP` via GDI `StretchBlt`.
It was used to capture documentation images.

## Building

Targets **.NET Framework 4.8**, output type Library. Solution: `TestPlugin.sln`.

1. Re-add `Base.dll`, `Core.dll`, `GUI.dll`, `CADImport.dll` and `DMC.exe` from your installed DMC
   version.
2. Set **Project → Properties → Build → Output path** to your DMC `Plugins` directory.
3. Build.

## Notes and limitations

- Most demos are commented out in `MyPlugin`'s constructor. Only the three RC functions and
  `DMC.Actions.ShowRunError = false` are active. Uncomment what you want to try — including the
  startup-settings example, the main-window title change, `MyPoly`, `PopupTools` and `TakeImages`.
- `MyPlugin.Connect()`, `OnRecipeStart()` and the state-changed handlers are empty shells left in
  place to show where your code goes; `OnRecipeStart` contains a commented axis-move example.
- The distance sensor has no hardware behind it. `ReadPosition` has a `// TODO: assign device
  position to p` and always reads zero, so a scan always "finds" focus at the first position.
- `DistanceSensorSettings.GetGUI()` returns `null`, so the offsets can only be edited by hand in the
  settings file unless you uncomment the default GUI.
- `PopupTools.AddTools` references the static `tool` field before `Create()` assigns it on the first
  pass; it works only because the dropdown is filled after creation.
- `MyArea.Deactivate()` returns early when `active != null`, inverting the intended guard.
- `TakeImages` uses P/Invoke into `gdi32`/`user32` and hard-codes a five-second start delay.
