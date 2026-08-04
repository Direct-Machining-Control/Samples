# StageSamplePlugin

A skeleton for a motion-controller plugin: one controller driving several axes, with optional
synchronised XY/XYZ trajectory generation. Every hardware call is a `// TODO:` marker, so the
project compiles and loads but moves nothing.

Use this as the starting point for integrating a stage, gantry or rotary controller that DMC does not
support out of the box.

## What it shows

- Registering a controller that owns multiple axes, and letting the user assign DMC's logical axes
  (X, Y, Z, …) to it in settings.
- Implementing a single axis: absolute moves with and without waiting, homing, jogging (free move),
  enable/disable, speed and position validation, and error reporting.
- A background monitoring thread that keeps axis positions fresh in `Base.State`.
- Two levels of motion integration — the simple point-to-point path and the full trajectory path.
- Generating a controller command buffer (G-code style) and splitting it when it approaches the
  controller's command limit.

## Project layout

| File | Purpose |
| --- | --- |
| `Plugin.cs` | Entry point, implements `Base.IDeviceEx`. Connect, axis creation, axis enabling, homing, and the 500 ms position-monitoring thread. |
| `Axis.cs` | A single axis. Implements `IAxis`, `IAxisFreemove`, `IAxisError`. |
| `XYZ.cs` | Synchronised multi-axis motion device (`Base.IMotionDevice`). Buffered trajectory generation. |
| `Settings.cs` | `ControllerSettings : IDeviceSettings` (COM port and the list of axes) and `AxisSettings : MultiParameter, IAxisAdditionalSettings` (per-axis index and steps/mm). |
| `SettingsGUI`, `AxisGUI` | WinForms controls for the controller settings page and per-axis settings. |

## Key DMC interfaces used

- **`Base.IDeviceEx`** — `IDevice` plus homing support (`CanDoHoming`, `Home`).
- **`Base.IAxis`** — the core axis contract: `Move`, `WaitForMoveDone`, `GetPosition`, `Home`,
  `Enable`/`Disable`, `Stop`, units, speed control.
- **`Base.IAxisFreemove`** — adds `StartFreemove(speed)` / `StopFreemove()` so the axis can be jogged
  from the joystick panel.
- **`Base.IAxisError`** — error and warning state, whether the error can be cleared, and a list of
  human-readable state strings.
- **`Base.IAxisAdditionalSettings`** — per-axis settings contributed by your controller, plus
  `GetAxisGUI()` and `IsEnabled`.
- **`Base.IMotionDevice`** — the trajectory interface: `StartList`/`EndList`/`RunList`, `Mark`,
  `MarkArc`, `MarkCircle`, `Jump`, `MakeMargin`, `Delay`, `FireContinuous`, `FirePulse`.

## How it hooks into DMC

`ControllerSettings` walks `Base.Settings.Axes`, and for each logical `Stage` axis creates an
`AxisSettings` and registers it via `stage.controllers.Add(this)`. That is what makes "My Controller"
appear as a selectable controller in each axis's settings.

On `Connect()`, `InitAxes()` instantiates an `Axis` for every axis whose `ControllerName` matches, and
`EnableAxes()` powers them on. If both X and Y belong to this controller, the plugin creates a motion
device so DMC can move them together:

- `GeneralXYZ` — DMC's built-in wrapper. Use this when your controller can only go from point to
  point; DMC handles the rest.
- `XYZ` (this sample's own class) — use this when your controller can execute a full trajectory,
  including laser triggering along the path. `XYZ` registers itself as
  `SystemMotion.device_stage`, so selecting "Stage" as the device on the MARKING tab routes marking
  through it.

The `isSimplePointToPointMotion` flag in `Plugin.Connect` picks between the two.

`XYZ` accumulates commands into a `StringBuilder` between `StartList()` and `EndList()`, then
`RunList()` sends the batch. `CheckToSplit` runs and restarts the list when it approaches
`MAX_COMMANDS` (1000), preferring to split at a jump rather than mid-contour.

## Building

Targets **.NET Framework 4.8**, output type Library.

1. Re-add `Base.dll`, `Core.dll` and `GUI.dll` from your installed DMC version.
2. Set **Project → Properties → Build → Output path** to your DMC `Plugins` directory.
3. Build.

## Notes and limitations

- Every controller interaction is a `// TODO:` comment. The places you must fill in are: connect and
  disconnect in `Plugin`, `Move` / `WaitForMoveDone` / `GetPosition(true)` / `Home` / `EnableDisable`
  / `Stop` / `StartFreemove` in `Axis`, and the `G0` / `G1` / `G02` / `G04` emitters plus `RunList` in
  `XYZ`.
- `Axis.GetPosition()` is documented as possibly being called every 20 ms — do not talk to hardware
  there. The sample returns a cached value and refreshes it from the monitoring thread instead. If
  your `Move` implementation already knows the final position, you can drop the monitoring thread.
- `Axis.UpdateState()` notes that thread synchronisation may be required, since the monitor thread
  and the motion thread can both touch the controller.
- `AxisSettings.controller_name` ("My Controller") is the string users will see; the settings unique
  name is `MyStageController`. Both need changing for a real plugin.
