# CustomDLPPlugin

Extends DMC's DLP projector plugin with custom image handling. It offers two mutually exclusive
modes, selectable in settings:

- **Unique Process** — take over frame display completely, deciding what to show and for how long.
- **Add Border** — leave DMC's display logic alone and just post-process each frame before it is
  projected.

Both modes do the same trivial thing (draw a white border) so you can see the difference between the
two hook points.

## What it shows

- Registering with another plugin's extension points rather than DMC core.
- The two levels at which you can intervene in DLP frame output.
- Swapping an extension in and out live from `ApplySettings()`.
- A `BoolParameter` rendered as a two-option choice rather than a checkbox.

## Project layout

| File | Purpose |
| --- | --- |
| `Plugin.cs` | Entry point. `ApplySettings()` installs or removes the two extensions based on the current mode. |
| `CustomView.cs` | `UniqueProcess : IDLPViewManager` and `AddBorder : IDLPViewProcessor` — the two hooks, plus the shared `DrawBorder` helper. |
| `PluginSettings.cs` | `IDeviceSettings` with the single mode parameter. |
| `PluginSettingsGUI.cs` | The settings page. |

## Key DMC interfaces used

- **`Base.IDevice`** — the plugin contract. `IsConnected()` always returns `true` and `Connect()` is
  a no-op, since there is no hardware of its own.
- **`DLPPlugin.IDLPViewManager`** — the *display* hook. Its `Show(IDLPView view, Bitmap image,
  ulong frame)` is called instead of the plugin's own display routine, and is responsible for calling
  `view.Show(...)` itself. Only one view manager can be active.
- **`DLPPlugin.IDLPViewProcessor`** — the *processing* hook. Its `Process(Bitmap image, ulong frame)`
  returns the image to display; DMC's normal display path continues afterwards. Several processors
  can be chained.

## How it hooks into DMC

All the integration is in `ApplySettings()`, which DMC calls when settings are confirmed and at
startup:

- For **Unique Process**, it assigns `DLPPlugin.Plugin.plugin.view_manager = proc`, and clears it
  again (only if the current manager is its own) when the mode is switched off or the plugin is
  disabled.
- For **Add Border**, it adds or removes its processor from the static
  `DLPPlugin.Plugin.view_processors` list.

Because both paths check the plugin's `Enabled` flag first, disabling the plugin in settings cleanly
uninstalls both hooks.

`UniqueProcess.Show` draws the border and then calls `view.Show(image, 0.1)` — that second argument is
the display duration in seconds, and choosing it is the point of implementing a view manager rather
than a processor.

## Building

Targets **.NET Framework 4.8**, output type Library.

1. Add a reference to `DLPPlugin.dll` from your DMC `Plugins` folder, plus `Base.dll` (and `Core.dll`
   / `GUI.dll` if you extend the sample) from your installed DMC version.
2. Set **Project → Properties → Build → Output path** to your DMC `Plugins` directory.
3. Build.

## Notes and limitations

- Requires the DLP plugin to be installed. Without `DLPPlugin.dll` the project will not compile and
  the plugin will not load.
- The mode is a single `BoolParameter` with two labels ("Unique Process" / "Add Border"), so the two
  modes are exclusive by construction. If you want both, change it to two independent flags.
- `DrawBorder` uses `image.Height - 2` for the rectangle height while using `image.Width - 1` for the
  width, so the bottom edge is drawn one pixel short. Copy the intent, not the arithmetic.
- Each call to `DrawBorder` allocates a new `Pen`; in a per-frame path you would cache it.
