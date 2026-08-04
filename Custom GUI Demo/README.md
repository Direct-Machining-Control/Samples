# Custom GUI Demo

This sample is the inverse of every other one here: instead of adding a plugin to DMC, it **hosts DMC
inside your own application**. You get a WinForms app that owns the window, the layout and the
branding, while DMC provides the recipe engine, the 3D view, motion, IO and vision underneath.

Use this when you are building a dedicated machine HMI and the standard DMC interface is more than
your operators need.

The bundled **`DMC sample HMI.pdf`** (and its `.docx` source) walks through the same material with
screenshots.

## What it shows

- Booting the DMC core from your own `Main` — licence check, initialisation, settings load, device
  connection.
- Rendering the DMC 3D view into a panel in your own form.
- Switching to the full DMC UI and back, for maintenance or setup.
- Driving each subsystem from your own controls: recipes, axes, IO, joystick, power meters, camera,
  vision alignment.
- Creating geometry interactively with DMC's built-in view tools.
- Restricting the UI (disabling object dragging, hiding scale/rotate options).

## Project layout

Solution: `Sample.sln`, output type **WinExe** (an application, not a plugin).

| File | Purpose |
| --- | --- |
| `Program.cs` | Standard WinForms entry point. |
| `Helper.cs` | The important file. DMC startup/shutdown, view handover, and worked examples for geometry tools, global variables, marking-parameter presets and vision alignment. |
| `Form1.cs` | The shell: connect, run, cancel, settings, zoom, and switching to the DMC main form. |
| `RecipeUC.cs` | Recipe command tree — list, skip, delete, select (showing each command's own GUI), compile, import CAD, open `.rcp` files. |
| `AxisUC.cs` | Per-axis status, enable and home. |
| `InputOutputUC.cs` | Lists `Base.Settings.IOTools` and shows/sets their state. |
| `JoystickUC.cs` | Jog buttons wired to the axis list. |
| `PowerMeterUC.cs` | Iterates `Base.Settings.PowerMeters` and displays readings. |
| `CamWindow.cs` | Picks the first enabled camera from `Base.Devices.Camera.camera_devices` and renders frames. |
| `VisionUC.cs` | Alignment pattern create/edit/run, preset load and save, result readout. |

## Starting and stopping DMC

`Helper.InitDMC(form, panel)` is the whole boot sequence, in order:

1. `Base.Functions.SetThreadCulture()` — decimal separators must be predictable.
2. `Base.Settings.main_window = main_form` — DMC needs a window to marshal UI work to.
3. `DMC.Helpers.CheckLicense()`.
4. `Core.Actions.Init()`.
5. `DMC.Actions.GetDisplay(view_panel)` — returns the `IView` and parents the 3D display into your
   panel.
6. `Base.Settings.LoadSettings()` then `Base.SystemDevices.ApplySettings()` — loads hardware
   configuration and applies it to the devices.
7. `Base.View.Update()`.

`Helper.CloseDMC()` reverses it: clear `main_window`, `SystemDevices.Disconnect()`, set
`State.is_exit = true`.

`Helper.ShowDMC()` swaps to the real DMC interface — it fetches the main form with
`DMC.Form1.GetMainForm()`, sets `HideOnClose` so closing it returns rather than exits, re-points
`Settings.main_window` at it, shows it and hides your form. The `FormClosing` handler reverses the
swap and re-acquires the display panel. This is how you keep a full-featured setup mode behind a
simplified operator UI.

## Driving DMC from your own controls

The pattern throughout is: call a `DMC.Actions.*` method for the action, read `Base.State.*` for
status, and subscribe to events for change notifications.

- **Actions** — `ConnectDisconnect`, `RecipeRun`, `RecipeCancel`, `RecipeCompile`, `OpenRecipe`,
  `ImportCAD`, `ShowSettings`, `ViewFitScreen`, `ShowJoystick`.
- **State** — `is_running`, `is_connected_to_hardware`, `is_error`, polled from a timer to enable and
  disable buttons.
- **Events** — `Base.State.StateChangedEvent`, `Core.Recipe.RecipeChanged`,
  `Core.Recipes.RecipeLoaded`, `Base.SystemDevices.SettingsApplied`,
  `Base.StatusBar.StatusBarMessageReceived`.

`RecipeUC` is the fullest example. It rebuilds a `TreeView` from `Recipes.ActiveRecipe.Childs`, maps
checkbox state to `ICommand.IsSkipped`, deletes through
`OperationForSelectedCommands(RecipeCommandOperation.Delete)`, and — the useful trick — hosts each
command's **own** editor by calling `command.GetGUI()` and adding the returned control to a panel. You
get DMC's per-command UI without rebuilding it.

`Helper` also contains standalone examples for things that are awkward to discover: adding and
reading global variables (`Base.Variables.global_variables`), applying a marking-parameters preset by
name from `MarkingParamsLibrary`, and creating, configuring, running and saving a
`VisionPlugin.AlignCommand` pattern.

`Helper.CreateGeometry(GeometryTool)` maps an enum to DMC's interactive view tools — `ToolLineSE`,
`ToolArcSER`, `ToolCircleCRSE`, `ToolRectangleSE`, `ToolPolyline`, `ToolEllipse`, `ToolSpiral`,
`ToolSpline`, `ToolText`, `ToolBarcode` — and activates one with
`Base.View.CurrentView.SetActiveTool(tool)` followed by `tool.Start()`.

## Building and running

Targets **.NET Framework 4.8**, output type WinExe.

1. Re-add `Base.dll`, `Core.dll`, `GUI.dll`, `CADImport.dll` and `Vision\VisionPlugin.dll` from your
   installed DMC version — the checked-in paths point at a build tree
   (`..\..\..\..\..\DMC\bin\Debug\`) and will not resolve.
2. Because this is an application rather than a plugin, its **output directory must be the DMC
   installation directory**, so the DMC assemblies, plugins, settings and licence resolve at run
   time. Set **Build → Output path** accordingly, or copy the built `.exe` there.
3. Build and run.

## Notes and limitations

- A valid DMC licence is required; `CheckLicense()` reports failure but the sample continues anyway.
- The vision controls reference hard-coded names and paths — an alignment command called `test`, and
  presets under `<PathParent>\Process\MV\abc.mv` / `save_test.mv`. Change them or the buttons will
  fail.
- `VisionUC` requires VisionPlugin; `CamWindow` requires a configured camera; `PowerMeterUC` requires
  at least one power meter. Each degrades quietly to an empty control if the hardware is absent.
- Status is polled on a `Timer` rather than pushed, so button states lag by up to one tick.
- `ToolPositioning.CanMoveObject = false` and `ShowAdditionalTools = false` are set to lock down the
  operator view; remove them if you want full editing.
