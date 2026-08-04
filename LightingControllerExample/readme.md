# LightingControllerExample

An illumination controller plugin. It registers four light channels with DMC's LightingPlugin, which
then renders sliders or on/off buttons for them in the ribbon. Intensity get/set are simulated, so
the plugin is safe to load as-is.

> To create a lighting controller device plugin, **LightingPlugin must be installed** and
> `LightingPlugin.dll` must be referenced by your project. Contact our support or sales department if
> you do not have LightingPlugin.

## What it shows

- Contributing light channels to LightingPlugin rather than building your own UI.
- Letting a channel choose its own control style: a slider between `Minimum` and `Maximum`, or a
  simple on/off toggle.
- Optional colour control, advertised per channel via `HasColor()`.
- Building a settings page automatically from parameters with `ICommandGUI.GetStaticGUI`.
- Showing and hiding dependent parameters in the settings GUI as the user changes a checkbox.
- Adding and removing channels from the host plugin when settings are re-read.

## Project layout

| File | Purpose |
| --- | --- |
| `Plugin.cs` | Entry point (`Base.IDevice`). Holds the singleton used by the channels, and the connect/get-intensity/set-intensity/get-colour/set-colour methods you would replace with real device calls. |
| `PluginSettings.cs` | `IDeviceSettings` + `IGUIManagerParameter`. Holds the IP address parameter and creates the four `Light` channels. |
| `Light.cs` | One channel. `MultiParameter` + `ILightingControl` + `IGUIManagerParameter`. |

## Key DMC interfaces used

- **`Base.IDevice`** — the standard plugin contract.
- **`LightingPlugin.ILightingControl`** — what makes an object a light. Supplies `Name`, `Minimum`,
  `Maximum`, `Step`, `IsToggle`, `IsConnected`, `GetValue`/`SetValue`, and `HasColor`/`GetColor`/
  `SetColor`.
- **`Base.MultiParameter`** — lets a light be a settings parameter in its own right, with children,
  so it saves and loads automatically.
- **`Core.IGUIManagerParameter`** — the mechanism for conditional GUI. `GetDependencies(prm)` returns
  the parameters whose visibility depends on `prm`; `IsIParameterVisible` / `IsIParameterEnabled` then
  decide what happens when it changes.

## How it hooks into DMC

`PluginSettings` constructs four `Light` objects in its constructor (`exampleLight1`…`exampleLight4`,
channel IDs 1–4) and adds each one as a parameter, so their state persists with the rest of DMC's
settings.

The registration itself happens in `PluginSettings.EndSettingsRead()`, which DMC calls after loading
settings. It compares each channel's `Enabled` flag against
`LightingPlugin.Plugin.Instance.AvailableLights` and calls `AddLightDevice` or `RemoveLightDevice`
accordingly. `Plugin.ApplySettings()` calls `EndSettingsRead()` explicitly so that toggling a channel
in the settings dialog takes effect immediately.

Once a channel is in `AvailableLights`, LightingPlugin owns the UI. Each channel decides how it is
rendered:

- `IsToggle == true` — a check button; on writes `Maximum`, off writes `Minimum`.
- `IsToggle == false` — a slider between `Minimum` (0) and `Maximum` (the configured voltage).
- `HasColor() == true` — LightingPlugin also offers a colour picker and calls `GetColor`/`SetColor`.
  This sample returns `false`; remove the colour methods entirely if your device has no colour
  support.

In the settings GUI, `IGUIManagerParameter` hides a channel's voltage and toggle-mode fields until
the channel is enabled, and greys out the IP address and all channels when the device itself is
disabled.

## Building

Targets **.NET Framework 4.8**, output type Library.

1. This project references DLLs through a `$(DMCPath)` MSBuild property. Either define `DMCPath`
   (pointing at your DMC install directory, with a trailing separator) or re-add
   `Plugins\LightingPlugin.dll`, `Base.dll`, `Core.dll` and `GUI.dll` by hand.
2. Set **Project → Properties → Build → Output path** to your DMC `Plugins` directory.
3. Build.

## Notes and limitations

- `SetLightIntensity` just records the value on the channel and `GetLightIntensity` reads it back —
  there is no device. Replace both with your controller's protocol (TCP/IP, a COM DLL, serial, …).
  The comments mark the exact spots.
- `Connect()` and `IsConnected()` always report success, and `Disconnect()` is empty.
- The `ipAddress` parameter is declared and displayed but never used.
- `Step` (0.2) is currently ignored by the host UI.
