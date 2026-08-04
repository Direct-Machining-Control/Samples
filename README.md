# DMC Plugin Development Samples

Sample projects for extending **DMC**, our laser machine and process control software. Each folder is
a self-contained Visual Studio project with its own `README.md` explaining what it demonstrates, which
DMC interfaces it uses, and how to build it.

[How to make DMC plugin.pdf](How%20to%20make%20DMC%20plugin.pdf) — short description of the plugin
creation procedure. Read this first.

## Where to start

- **New to DMC plugins?** Read the PDF above, then [TestPlugin](TestPlugin/) — it states the naming
  contract DMC uses to discover plugins and collects a dozen small, independent techniques.
- **Adding a recipe step?** [SendEmailPlugin](SendEmailPlugin/) is the smallest complete example.
- **Integrating hardware?** [LaserPlugin](LaserPlugin/) is the most thorough device sample;
  [StageSamplePlugin](StageSamplePlugin/) is the motion-controller equivalent.
- **Building your own operator interface?** [Custom GUI Demo](Custom%20GUI%20Demo/) hosts DMC inside
  your application instead of extending it.
- **Driving DMC from another program?** [RemoteControl](RemoteControl/) wraps the whole TCP remote
  control API.

## Sample list

| Sample | Kind | What it demonstrates |
| --- | --- | --- |
| [CVSamplePlugin](CVSamplePlugin/) | Command + native interop | Calling a native C++ machine-vision DLL from a recipe command: move the camera, grab a frame, detect a point, write it to a recipe variable. |
| [Custom GUI Demo](Custom%20GUI%20Demo/) | Host application | Hosting the DMC engine and 3D view inside your own WinForms HMI, with controls for recipes, axes, IO, joystick, power meters, camera and vision. |
| [CustomDLPPlugin](CustomDLPPlugin/) | DLP device extension | Two ways to intervene in DLP frame output — replacing the display manager, or post-processing each frame. |
| [HatchingPlugin](HatchingPlugin/) | Hatching | A custom hatching type plus pre- and post-processors that strip small contours and short hatch lines. |
| [JoinAndHatchPlugin](JoinAndHatchPlugin/) | Command | A container command that collects its children's geometry into one outline and hatches it as a whole. |
| [LaserPlugin](LaserPlugin/) | Device | A full laser device: settings, ribbon command, popup panel, live status parameters, recipe-run control and marking-parameter extensions. |
| [LightingControllerExample](LightingControllerExample/) | Lighting device | Registering illumination channels with LightingPlugin, as sliders or on/off toggles. Requires LightingPlugin. |
| [PM_Example](PM_Example/) | Power meter | A power meter discovered by PowerMeterPlugin, with a simulated continuous measurement loop. |
| [PointImport](PointImport/) | Import + view tool | Registering a custom file importer, and an interactive Jog & Teach tool that builds lines and arcs from jogged machine positions. |
| [PythonCommandPlugin](PythonCommandPlugin/) | Commands | Two recipe commands that run an external Python script or executable, passing recipe data in and capturing output. |
| [RemoteControl](RemoteControl/) | Client application | A documented TCP client for the Remote Control Module covering recipes, variables, motion, IO, galvo, geometry and file upload, plus an MJPEG preview reader. |
| [SendEmailPlugin](SendEmailPlugin/) | Command | The smallest end-to-end custom command: send an SMTP email from a recipe, with variable expansion and an encrypted password. |
| [StageSamplePlugin](StageSamplePlugin/) | Motion controller | A controller and axis skeleton, including buffered trajectory generation for synchronised XY/XYZ motion. |
| [TestPlugin](TestPlugin/) | Assorted | Remote control functions, a focus/distance sensor, custom view drawing, in-view toolbars, dynamic ribbon menus, a serial device, and more. |
| [ViewWindowPlugin](ViewWindowPlugin/) | UI | A floating always-on-top preview window that borrows the DMC 3D view for background or kiosk operation. |

## Building any sample

Unless a sample's own README says otherwise:

1. **Target framework.** All projects target .NET Framework 4.8. Some older samples may still be set
   to 4.5 — change the reference to 4.8 to build against the latest DMC version.
2. **Fix the DMC references.** Every project references `Base.dll`, `Core.dll` and usually `GUI.dll`
   (sometimes `CADImport.dll` or `DMC.exe`). The checked-in paths point at our build machines and
   will not resolve. Remove them and re-add the same DLLs from your installed DMC directory, for
   example `C:\Program Files (x86)\DMC\DMC 1.2.31\`. When debugging, prefer the 32-bit assemblies —
   the Visual Studio forms designer can misbehave with the 64-bit build.
3. **Set the output path.** In **Project → Properties → Build → Output path**, point the build at your
   DMC `Plugins` directory so the DLL lands where DMC will find it. The two application samples
   (Custom GUI Demo, RemoteControl) are exceptions — see their READMEs.
4. **Build.** DMC loads any DLL in `Plugins` that exposes a public, non-abstract class whose name
   contains the word `Plugin` and which implements `Base.IDevice`. A single DLL may contain several.

Some samples extend another plugin and need its assembly as well: CVSamplePlugin needs
`VisionPlugin.dll`, CustomDLPPlugin needs `DLPPlugin.dll`, LightingControllerExample needs
`LightingPlugin.dll`, and PM_Example needs `PowerMeter.dll` and must keep its `PM_` filename prefix.
Contact our support or sales department if you do not have these plugins.
