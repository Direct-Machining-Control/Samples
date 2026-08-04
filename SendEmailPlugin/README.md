# SendEmailPlugin

The smallest end-to-end custom command sample. It adds a "Send Email" step to the recipe flow that
sends a mail over SMTP when the recipe reaches it — useful for notifying an operator that a long job
finished or that a batch needs attention.

If you want to learn how to add a recipe command, read this one first.

## What it shows

- The minimum viable plugin: a class that satisfies `Base.IDevice` and does nothing except register
  a command.
- Defining a command with typed parameters that save, load and appear in the UI automatically.
- Expanding recipe variables and math expressions inside a text parameter at run time.
- Storing a password parameter encrypted rather than in clear text.
- Reporting errors through the status bar as well as the error dialog.

## Project layout

| File | Purpose |
| --- | --- |
| `Plugin.cs` | Entry point. The constructor's single job is to register `SendEmailCommand` in the ribbon. |
| `SendEmailCommand.cs` | The command: parameters, compile-time validation, and the SMTP send in `Run()`. |
| `SendEmaiGUI.cs` / `.Designer.cs` | The hand-built WinForms editor shown when the user selects the command in the recipe. |

## Key DMC interfaces used

- **`Base.IDevice`** — required even for a plugin that controls no hardware. Note the comment at the
  top of `Plugin.cs`: the class must be public, must contain the word "Plugin" in its name, must
  implement `IDevice`, and must not be abstract. `IsEnabled()` returns `false` here, which keeps the
  plugin out of the hardware connect/disconnect cycle while still letting it register its command.
- **`Core.ICommand`** — the recipe command base class. `Compile()` validates before the run,
  `Run()` executes, `GetGUI()` supplies the editor, `GetInfo()` provides the one-line summary shown in
  the recipe tree, and `IsControlCommand` marks it as a flow step rather than geometry.

## How it hooks into DMC

`Plugin`'s constructor calls:

```
DMC.Helpers.AddTool(DMC.ToolLocation.HomeTab,
                    Core.ICommand.AddCreator(typeof(SendEmailCommand), UN, "group_recipe_flow"),
                    FN)
```

`ICommand.AddCreator` registers the type under its unique name so recipes can serialise and
reconstruct it; `AddTool` puts a button in the "group_recipe_flow" group of the Home tab.
`.SetImage(...)` attaches the envelope icon.

At run time the command builds a `MailMessage` from its parameters and sends it with
`System.Net.Mail.SmtpClient`. Two details worth copying:

- **Variable expansion.** `TextCommand.ParseText(body.Value, ref output)` runs the body through
  DMC's text parser first, so an operator can embed recipe variables and expressions in the message.
- **Password storage.** The `Password` property wraps the raw parameter in
  `Functions.Encrypt` / `Functions.Decrypt`, so the stored recipe does not contain the plain
  password.

When `use_login` is set the client switches to port 587 with SSL and explicit credentials; otherwise
it falls back to `CredentialCache.DefaultNetworkCredentials`.

## Building

Targets **.NET Framework 4.8**, output type Library.

1. Re-add `Base.dll`, `Core.dll` and `GUI.dll` from your installed DMC version.
2. Set **Project → Properties → Build → Output path** to your DMC `Plugins` directory.
3. Build.

## Notes and limitations

- The encryption key is hard-coded in `SendEmailCommand.cs`. This obscures the password in the saved
  recipe; it is not real secret management.
- `Run()` catches send failures, reports them and then returns `true` — a failed email does **not**
  stop the recipe. If you want the opposite, return `false` from the catch block.
- Port 587 with SSL is hard-coded for the authenticated path. Many providers now also require an app
  password rather than the account password.
- There is no retry and no timeout override; a slow SMTP host will block the recipe.
