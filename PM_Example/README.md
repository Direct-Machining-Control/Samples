# PM_Example

A power meter device plugin. It simulates a power reading instead of talking to real hardware, so it
is safe to load and use as a template.

## What it shows

- Implementing a power meter that the DMC PowerMeter plugin will discover and load.
- Exposing device settings as a parameter with a range and step (`DoubleExtParameter`).
- A continuous measurement loop on a background `Task`, with a blocking accessor that waits for a
  genuinely new reading rather than returning a stale one.

## Project layout

| File | Purpose |
| --- | --- |
| `DeviceSettings.cs` | `Base.IPowerMeterSettings`. Declares the settings page (`pm_example`, "Power Meter Example") and acts as the factory that hands DMC the actual device. |
| `Device.cs` | `Base.IPowerMeter`. Connect/disconnect, start/stop the measurement loop, report the latest power. |
| `Readme.txt` | The original three-line note, superseded by this file. |

## Key DMC interfaces used

- **`Base.IPowerMeterSettings`** — the discovery point. `GetPowerMeter()` lazily creates and returns
  the device; `CanPerformAutomaticDetection()` tells DMC whether it can probe for the hardware
  without user configuration (this sample returns `false`).
- **`Base.IPowerMeter`** — the device contract: `Connect`, `Disconnect`, `Start`, `Stop`,
  `IsConnected`, `LastMeasured`, `GetPower`, `AveragingTime`, `SetWavelength`, `GetName`.

## How it hooks into DMC

**This plugin is loaded by `PowerMeterPlugin.dll`, not by DMC directly**, and discovery has two hard
requirements:

1. The built assembly must be placed in the DMC `Plugins` folder.
2. Its file name must start with **`PM_`**.

Any such DLL exposing a type that implements `IPowerMeterSettings` is picked up, and its settings
page appears alongside the other power meters.

Note the two-tier structure: the *settings* object is what DMC discovers, and it owns the *device*
object. `DeviceSettings` also calls `Parameters.Clear()` before adding its own parameter, so only
`max_power_to_show` appears on the settings page.

## The simulated measurement

`Start()` spins up a `Task` that writes a new value into `lastMeasured` every 100 ms — the elapsed
seconds wrapped modulo the configured maximum, so the reading ramps and resets. Each write bumps a
`measurementID` counter.

`GetPower` uses that counter to block until a *fresh* reading arrives, giving up after 5 seconds and
returning `-1`. Replace the body of the task with your instrument's read call and the rest of the
pattern still works. `Stop()` clears the active flag and waits for the task to finish.

## Building

Targets **.NET Framework 4.8**, output type Library.

1. Add references to `Base.dll` and `PowerMeter.dll` from your installed DMC version.
2. Set **Project → Properties → Build → Output path** to your DMC `Plugins` directory.
3. Confirm the assembly name still begins with `PM_` — this is what makes it discoverable.
4. Build.

## Notes and limitations

- `SetWavelength` accepts any value and does nothing.
- `AveragingTime` is stored but never applied to the measurement.
- `Connect()` unconditionally reports success; there is no hardware to fail against.
