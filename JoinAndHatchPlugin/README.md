# JoinAndHatchPlugin

A container command. Drop geometry commands inside it, and instead of each child being marked
separately, their combined outline is treated as one shape and hatched as a whole.

The valuable part of this sample is the technique: how to collect the geometry your child commands
produce, so you can transform it before it reaches the recipe.

## What it shows

- A recipe command that accepts children (`CanContainChilds`).
- Redirecting compilation into a temporary `CADProcessor` so child geometry is captured instead of
  emitted — the core "flatten my children" pattern.
- Embedding DMC's standard `Hatching` parameter block in your own command, so users get the full
  hatching UI for free.
- Hatching a collected outline and combining hatch lines with contours.

## Project layout

| File | Purpose |
| --- | --- |
| `Plugin.cs` | Entry point. Registers the command; nothing else. |
| `GUI.cs` | Contains both the `GUI : ICommandGUI` editor and the `JoinAndHatch : ICommand` command class. |
| `PluginSettings.cs`, `PluginSettingsGUI.cs` | A settings-page scaffold, currently unused (the plugin's `GetSettings()` returns `null`). |

## Key DMC interfaces used

- **`Base.IDevice`** — the plugin contract. `IsEnabled()` returns `false`; this plugin exists only to
  register a command.
- **`Core.ICommand`** — the command. `CanContainChilds()` returns `true`, which is what lets users
  nest geometry inside it in the recipe tree.
- **`Core.ICommandGUI`** — the editor. It hosts a `hatching` control and calls `Set` on it, so the
  standard hatching UI renders inside the command's own editor.
- **`Core.Commands.Hatching`** — added as a parameter in the constructor, giving the command all of
  DMC's hatching types, spacing, angle and processor options without extra code.

## The collect-children pattern

`Make2DCommands` is the piece worth reading, and it recurs in `TestPlugin\MyPoly.cs`:

1. Create a fresh `CADProcessor` with `CollectGeometryOnly = true`.
2. Carry over `last_marking_parameters` from the current processor so the collected geometry keeps
   its marking settings.
3. Swap it in with `Recipe.SetNewProcessor(collector, false)`.
4. Run the children with `CompileRunChilds(false)` followed by `collector.PostCompileRun()`. Their
   output lands in the collector instead of the recipe.
5. Remove the collector's preview (`RemoveFromView`), restore the original processor, clone the
   collected commands out, and reset the collector.
6. If the result does not begin with a `MarkingParameters` entry, insert the original one at the
   front.

`Compile()` then either hatches the collected outline — `hatching.Compile()`, `hatching.Hatch(...)`,
`hatching.Combine(hatch, shape_2d, ...)`, `hatch.SetICommand(this)` so the result is attributed back
to this command — or, if hatching is disabled, just pushes the collected geometry straight through.

## Building

Targets **.NET Framework 4.8**, output type Library.

1. Re-add `Base.dll`, `Core.dll` and `GUI.dll` from your installed DMC version.
2. Set **Project → Properties → Build → Output path** to your DMC `Plugins` directory.
3. Build.

## Notes and limitations

- The command is registered into a ribbon group literally named `"Recipe Flow"`. Other samples use
  the internal group keys (`group_recipe_flow`, `group_devices`, `Geometry`) — check which convention
  your DMC version expects.
- The command has no icon; the `SetImage` call is commented out.
- `PluginSettings` and `PluginSettingsGUI` are dead code in this sample — `Plugin.GetSettings()`
  returns `null` and the settings field is commented out. They are left in as a starting point if you
  need a settings page.
- `Run()` simply calls `Compile()`; all the work happens at compile time, which is normal for a
  geometry-producing command.
- The "join" in the name refers to treating the children as a single outline for hatching purposes;
  there is no boolean union of overlapping contours.
