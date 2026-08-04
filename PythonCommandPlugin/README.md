# PythonCommandPlugin

Adds two recipe commands that hand control to an external program: **Python** runs a `.py` script
through a configured interpreter, and **Executable File** runs any `.exe`. Both pass recipe data to
the child process and can capture its output back into a file.

This is the sample to copy when you need to call out to code that lives outside DMC.

## What it shows

- Registering more than one command from a single plugin.
- A plugin settings page with a custom GUI (browse for the Python interpreter).
- Reusing `Core.Commands.ExportDataCommand` to serialise selected recipe values into a temporary
  arguments file, including embedding its parameters in your own command's parameter list.
- Launching a process with `ProcessStartInfo`, redirecting stdout, and choosing between blocking and
  fire-and-forget execution.
- Failing the recipe on a non-zero exit code.
- File browse dialogs inside a command GUI.

## Project layout

| File | Purpose |
| --- | --- |
| `Plugin.cs` | Entry point. Registers both commands and declares `PluginSettings` (the interpreter path). |
| `CommandGUI.cs` | The Python command GUI **and** the `Command` class itself (both live in this file). |
| `ExeFile_CommandGUI.cs` | The Executable File command GUI and the `ExecutableFileCommand` class. |
| `SettingsGUI.cs` | The plugin settings page. |

## Key DMC interfaces used

- **`Base.IDevice`** — the plugin contract. `GetSettings()` returns `PluginSettings` so the
  interpreter path gets a settings page.
- **`Base.IDeviceSettings`** — the settings page. `GetGUI()` returns the custom `SettingsGUI`.
- **`Core.ICommand`** — both commands derive from it. `IsControlCommand` is `true` (flow steps, not
  geometry).
- **`Core.Commands.ExportDataCommand`** — DMC's existing data-export machinery. Rather than
  reimplementing "which variables do I send", the sample creates one, sets `Parent`, points it at a
  temp file with `SetupWritter`, and copies its parameters into its own parameter list so they render
  in the same GUI.

## How the two commands work

Both follow the same shape:

1. **Compile** — parse parameters, expand variables in the file paths with `TextCommand.ParseText`,
   check the target file exists, and compile the embedded `ExportDataCommand`.
2. **Run** — call `exportDataCommand.Run()` to write the arguments file, then start the process.

**Python** (`Command`, unique name `python`) additionally verifies that the plugin is enabled and
that the configured interpreter exists, then launches it as
`<interpreter> "<script.py>" "<argsfile>"`. The script receives the temp file path as `sys.argv[1]`
and reads its data from there. Arguments land in `%TEMP%\python_arguments.tmp` (via
`Settings.PathTEMP`).

**Executable File** (`ExecutableFileCommand`) reads the first line of the arguments file, converts
tabs to spaces and triples every quote — the escaping needed to pass a quoted argument through
`ProcessStartInfo.Arguments` — then passes it on the command line. It also adds a `stop_on_error`
parameter: when set and the child exits non-zero, the command fails and stops the recipe with the
exit code in the message.

Both have a `wait_for_finish` flag. When set, the command blocks and writes the child's stdout to the
result file; when clear, the process is started on a background thread and the recipe continues
immediately.

## Building

Targets **.NET Framework 4.8**, output type Library. Solution: `PythonCommandPlugin.sln`.

1. Re-add `Base.dll`, `Core.dll` and `GUI.dll` from your installed DMC version.
2. Set **Project → Properties → Build → Output path** to your DMC `Plugins` directory.
3. Build.

## Notes and limitations

- The `arguments` parameter exists on both commands, but for Python it is never passed to the
  process — only the script path and the temp file are. Arguments reach the script through the temp
  file instead.
- Both commands share the pattern of writing to a fixed temp filename, so two of them running
  concurrently on different threads would collide.
- When `wait_for_finish` is off, `stop_on_error` cannot apply and the GUI hides it, but stdout is
  still written to the result file from the background thread.
- Nothing validates that the selected `.py` file is valid Python or that the interpreter is really a
  Python interpreter.
