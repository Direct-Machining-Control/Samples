# ViewWindowPlugin

A small always-on-top window that shows the DMC 3D preview when the main application is hidden.
Collapsed it is a single button; expanded it becomes a 600×500 preview with zoom controls and camera
tracking. It is aimed at background or kiosk operation, where DMC runs minimised but the operator
still needs to see what is being marked.

## What it shows

- Moving DMC's 3D display into your own window and handing it back.
- Driving the preview refresh loop yourself, adapting the interval to how long a frame takes.
- Persisting window position through a hidden `IDeviceSettings` page.
- A borderless, drag-anywhere window with an expand/collapse toggle.
- Reacting to recipe state: auto-expanding when a particular command starts running.
- Theming the 3D view and the WinForms controls.

## Project layout

| File | Purpose |
| --- | --- |
| `Plugin.cs` | Entry point (`Base.IDevice`) plus `SettingsEMotion : IDeviceSettings`. Sets the view colour scheme and shows the window on connect. |
| `ViewWindow.cs` | The window itself: expand/collapse, refresh timer, drag handling, zoom buttons. Also contains `StateMonitoring`. |
| `ViewWindow.Designer.cs` | Layout. |

## Key DMC interfaces used

- **`Base.IDevice`** — the plugin contract. `Connect()` is repurposed to show the window.
- **`Base.IDeviceSettings`** — stores the window's X/Y position and a "show external preview window"
  flag. `VisibleInSettings` is overridden to `false` and `GetGUI()` returns `null`, so the page is
  hidden from the user but the values still persist. This is the pattern for plugin state that
  should be saved but not edited.
- **`DMC.Actions`** — `GetDisplay(panel)` re-parents the 3D view, and `ViewFitScreen` / `ViewReset` /
  `ZoomIn` / `ZoomOut` drive the zoom buttons.

## How the view is borrowed and returned

There is only one 3D display, so expanding and collapsing moves it:

- **Expand** — `Actions.GetDisplay(panel)` attaches the view to the window's panel, sets
  `Base.Settings.UpdateViewInBackgroundMode = true` so it keeps rendering while DMC is hidden,
  switches the STL view to slice mode, and enables camera tracking if a camera exists.
- **Collapse** — if DMC is not in background mode, `RecipeListUC.CreateViewPanel()` gives the display
  back to the main window.

The same handover runs on `FormClosing`.

`timer1_Tick` is the refresh loop. It syncs its own interval to `Base.View.ViewRefreshInterval`, calls
`Base.View.CurrentView.Refresh()` only if enough time has passed since the last frame, and then
adjusts the global interval based on how long the refresh took — under 40 ms drops it to 50 ms,
otherwise 100 ms. It also intercepts a maximise on this window and maximises the DMC main window
instead.

## Interaction details

- The window has no title bar. `btnExpand`'s mouse down/move/up handlers implement dragging; if the
  window did not actually move, mouse-up is treated as a click and toggles expansion. A move saves
  the new position through the settings object.
- `ViewWindow_Paint` draws the one-pixel grey outline.
- `StateMonitoring.UpdateState`, called every tick, polls
  `Core.Recipes.ActiveRecipe.current_running_command`. When a command whose friendly name is
  "Alignment" starts, the window expands automatically and collapses again when it finishes.

## Building

Targets **.NET Framework 4.8**, output type Library.

1. Re-add `Base.dll`, `Core.dll`, `GUI.dll`, `CADImport.dll` and `DMC.exe` from your installed DMC
   version.
2. Set **Project → Properties → Build → Output path** to your DMC `Plugins` directory.
3. Build.

## Notes and limitations

- The plugin is named for the "EMotion" system it was written for — `GetName()` returns
  "EMotion Plugin" and the settings unique name is `emotion`. Rename both before shipping.
- `IsEnabled()` is hard-coded to `true` and `ApplySettings()` does nothing, so the window cannot be
  turned off from settings; only the persisted `show_external_view` flag gates `ShowForm()`, and
  nothing in the sample lets a user change it.
- The auto-expand trigger matches the command's friendly name as a literal string ("Alignment"),
  which will break under localisation or renaming.
- The expanded size (600×500) is hard-coded in static fields.
- `PostCompile` is defined but never subscribed to anything.
- Colours are hard-coded to a particular brand palette in `Plugin`'s static fields.
