# PointImport

Two distinct techniques for getting geometry into a recipe from outside the normal CAD import path:

- **`Import3DFile`** — registering your own file importer so a new file type appears in DMC's import
  dialog.
- **`JogAndTeach`** — an interactive view tool that builds lines and arcs from positions the operator
  jogs the machine to, with live preview in the 3D view.

The commented-out `Tool` and `ToolImportHatch` classes in `Plugin.cs` add two more worked examples:
parsing points out of a STEP file, and importing tab-separated line coordinates.

## What it shows

- Adding a file importer to `Core.Commands.FileImport.FileImporters`.
- Writing an interactive `IViewTool`: keyboard handling, live geometry preview, custom drawing.
- Reading the current laser focus position (`Base.View.LaserFocus()`) as the operator jogs.
- Constructing an arc from three points.
- Wrapping an arbitrary `ActionCommandList` into a `Core.Commands.Cad` command and adding it to the
  active recipe.
- Building geometry programmatically (lines, circles, polylines) and computing its bounding cube.

## Project layout

| File | Purpose |
| --- | --- |
| `Plugin.cs` | Entry point. Registers the importer and the Jog & Teach tool. Also holds `MakeCommand` (the reusable list-to-command wrapper), `AddNewShape` (programmatic geometry), and the two commented-out import tools. |
| `Import3DFile.cs` | `Core.Commands.IFile3DImporter` — registers a `*.xml` importer. |
| `JogAndTeach.cs` | The interactive teach tool. |

## Key DMC interfaces used

- **`Base.IDevice`** — the plugin contract; `IsEnabled()` is `false`, so this is registration only.
- **`Core.Commands.IFile3DImporter`** — a file importer. Supplies `Filter` / `FilterFull` for the
  dialog, `ImportFile(fileName)` to do the work, and `GetCommand()` / `GetSize()` to hand back the
  result.
- **`Base.IViewTool`** — an interactive tool that owns the 3D view. Override `PreviewKeyDown`,
  `JoystickPreviewKeyDown`, `Draw(IView)`, `Start()` and `Stop()`.
- **`DMC.ITool`** — a ribbon button's action. `JogAndTeach` implements both, so the same object is
  the button and the tool it activates.

## Import3DFile

`AddImporter()` appends an instance to `Core.Commands.FileImport.FileImporters`, which is all it takes
for `*.xml` to become an importable type.

The sample's `ImportFile` ignores the file and generates a cone mesh with
`CADImport._3D._3DSupport.MakeCone`, wrapping it in an `STLActionCommand` sized by
`STLReader.UpdateSize`. Replace that with your own parser; the surrounding plumbing — filter strings,
`GetCommand`, `GetSize`, and `SetICommand` for the display title — stays the same.

## JogAndTeach

Activated from a toggle button on the Edit tab. On start it saves the current default view tool,
installs itself as both default and active tool, clears its buffer, opens the joystick panel
(`DMC.Actions.ShowJoystick()`), and starts a 20 ms thread that keeps the in-progress segment tracking
the machine position.

Operator controls:

| Key | Action |
| --- | --- |
| Enter | Commit the current laser focus position as a point |
| L | Draw lines |
| A | Draw arcs |
| Shift | Toggle between lines and arcs |

In line mode each new point closes the previous segment and starts the next. In arc mode three points
are consumed — start, middle, end — and `ArcCommand.SetFrom3Points` builds the arc; the middle and
end points are only accepted if they are far enough from the previous one
(`Settings.max_deviation`, `Settings.Epsilon`). `Draw(IView)` renders the committed geometry plus the
in-progress segment and its endpoints in the active-item colour.

Clicking the button again stops the tool, restores the previous view tool, and — if anything was
taught — commits it to the recipe through `Plugin.MakeCommand`.

## MakeCommand

The reusable bit worth lifting: given an `ActionCommandList` and a file name, it creates a
`Core.Commands.Cad`, assigns the commands and their bounding cube, stamps the file hash with
`FileImport.GetFileStamp`, disables reload-when-running, and compiles. The result can be handed
straight to `Core.Recipes.ActiveRecipe.AddCommand`.

## Building

Targets **.NET Framework 4.8**, output type Library.

1. Re-add `Base.dll`, `Core.dll`, `GUI.dll`, `CADImport.dll` and `DMC.exe` from your installed DMC
   version.
2. Set **Project → Properties → Build → Output path** to your DMC `Plugins` directory.
3. Build.

## Notes and limitations

- `Import3DFile` claims the `*.xml` extension but never reads the file — it always produces a cone.
- Three ribbon tools in `Plugin.cs` (Import Points, Import Lines, Add new shape) are commented out.
  Uncomment them to reach the STEP point parser (`Tool`), the tab-separated line importer
  (`ToolImportHatch`) and `AddNewShape`.
- The STEP parser is naive: it string-matches `CARTESIAN_POINT('',(` and keeps only the points at the
  highest Z, which suits picking the top face of a part and little else.
- `JogAndTeach`'s monitor thread mutates the same `line` and `arc` objects the draw code reads, with
  no synchronisation.
- `MakeCommand` is called with an empty file name from the teach path, so the file stamp is
  meaningless there.
