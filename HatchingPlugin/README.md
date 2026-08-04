# HatchingPlugin

Adds a custom hatching algorithm to DMC's hatching dropdown, plus two hatch processors that clean up
the geometry before and after hatching.

## What it shows

- Registering a new hatching type so it appears wherever DMC offers a choice of hatching.
- Implementing the hatch itself, including honouring contour and hatch offsets.
- Writing pre- and post-processors that run around any hatching type, not just this one.
- Walking a nested `ActionCommandList` safely, skipping non-geometry entries.
- Auto-generating a parameter GUI with `Core.ICommandGUI.GetGUIForm`.
- The clone contract that DMC requires of hatchers and processors.

## Project layout

| File | Purpose |
| --- | --- |
| `HatchingPlugin.cs` | Entry point. The constructor registers the hatcher and both processors — that is all it does. |
| `CustomHatching.cs` | The `IHatcher` implementation. Parameters (spacing, contour offset, hatch offset), validation, and the `Hatch` entry point. |
| `SimpleHatching.cs` | A standalone scanline hatch algorithm, provided as-is without warranty. |
| `HatchingPrePostProcessing.cs` | Two `IHatchProcessor`s: exclude small contours (pre) and exclude short polylines (post). |
| `CustomHatchingGUI.cs` | Hand-built WinForms editor for the hatcher's parameters. |
| `Line Hatching.png` | The thumbnail shown next to the hatching type in the picker. |

## Key DMC interfaces used

- **`Core.Commands.IHatcher`** — a hatching type. `GetHatchingType()` supplies the display name
  ("My Hatching"), `GetHatchingTypeImage()` the thumbnail, `Compile()` validates parameters,
  `Hatch(...)` does the work, and `CloneIHatcher()` / `CloneObj()` produce independent copies.
- **`Core.Commands.IHatchProcessor`** — a processing step that runs around hatching.
  `IsPreProcessor` decides the phase, `Run(hatch, prms)` does the work, `GetData()` returns the short
  summary shown in the UI, and `CloneObj()` copies it.
- **`Base.MultiParameter`** — both bases derive from it, so any parameter added with `Add(...)` is
  saved, loaded and rendered automatically.

## How it hooks into DMC

Three lines in the plugin constructor:

- `Core.Commands.Hatching.hatchers.Add(new CustomHatching())` adds "My Hatching" to the hatching type
  list.
- `IHatchProcessor.AddIHatchProcessor(new HatchingPreProcessing())` and the matching post-processor
  call register the two optional cleanup steps. Users enable them per hatch.

Note that `IsEnabled()` returns `false` and `GetSettings()` returns `null`: this plugin has no
hardware and no settings page. Registration in the constructor is the whole integration.

### The hatch

`CustomHatching.Hatch` runs in three stages:

1. If a contour offset is set, `Hatching.MakeOffset` shrinks the input contours in place.
2. If a hatch offset is set, the contours are cloned and offset again, so the hatch lines can inset
   further than the contour itself.
3. `SimpleHatching.Hatch` generates the lines.

`SimpleHatching` is a textbook scanline fill: convert every closed command to a polyline, then for
each Y from the bounding box minimum upwards by `spacing`, find all edge intersections, sort them,
and emit a line between each consecutive pair. It checks `State.is_cancel` each row so a long hatch
can be interrupted.

### The processors

`HatchingPreProcessing` runs **before** hatching and removes closed contours whose area (via
`Geometry.PolygonArea`, absolute value, because winding direction flips the sign) is below a
threshold — useful for dropping specks in a traced bitmap.

`HatchingPostProcessing` runs **after** hatching and removes result trajectories shorter than a
threshold, which cleans up the slivers left at the tips of narrow shapes.

Both walk the command list recursively, recursing into nested `ActionCommandList`s but not into a
`JoinedCommandList`, and skipping anything without a location, not closed (pre-processor only), or
marked `IFakeMotion` (jumps, positioning and similar non-geometry entries).

## Building

Targets **.NET Framework 4.8**, output type Library.

1. Re-add `Base.dll`, `Core.dll` and `DMC.exe` from your installed DMC version. The checked-in paths
   point at a build tree (`..\..\..\bin\Release\`) and will not resolve.
2. Set **Project → Properties → Build → Output path** to your DMC `Plugins` directory.
3. Build.

## Notes and limitations

- `SimpleHatching` is deliberately simple and is stated in its own header comment to be provided
  "as is" without warranty. It always hatches horizontally — it ignores the `hatching_angle_offset`,
  `hatching_shift_in_x` and `hatching_shift_in_y` arguments that `Hatch` receives — and it flattens
  everything to Z = 0. It also assumes non-self-intersecting contours and does not handle nested
  islands specially beyond even/odd pairing.
- `CustomHatching.Hatch` mutates the caller's command list when a contour offset is applied
  (`MakeOffset(commands, ...)` without cloning first), unlike the hatch offset path which clones.
- `CloneIHatcher` and `CloneObj` must both be implemented and must copy parameter values with
  `AssignValuesFrom` — DMC clones hatchers freely.
