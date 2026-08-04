# CVSamplePlugin

Shows how to bolt a native (C++) machine-vision library onto a DMC recipe command: move the camera to
a position, grab a frame, hand the raw pixels to unmanaged code, and write the detected point back
into a recipe variable.

The detection itself is a stub — the native function always returns the point at one quarter of the
image width and height — so you can run the whole pipeline before writing any real image processing.

## What it shows

- A two-project solution: a native C++ DLL and a managed C# plugin that P/Invokes into it.
- Getting the currently selected camera from VisionPlugin and its associated positioning device.
- Applying the camera's offset, validating the target position and jumping to it.
- Pulling raw image bytes out of the camera without going through a `Bitmap`.
- Converting detected pixel coordinates back into system coordinates.
- Declaring a structured recipe variable (`VariableEx` with `x` and `y` children) at compile time and
  filling it at run time.

## Project layout

Solution: `CVSamplePlugin.sln`, two projects.

| File | Purpose |
| --- | --- |
| `CVSampleDll/CVLibrary.h` | The exported native API: `count_pixels` and `find_point`, both `extern "C"` with `__declspec(dllexport)`. |
| `CVSampleDll/CVLibrary.cpp` | The stub implementations. |
| `CVSampleDll/dllmain.cpp`, `pch.*`, `framework.h` | Standard Visual C++ DLL scaffolding. |
| `CVSamplePlugin/Plugin.cs` | The managed plugin **and** the `Command` class. |
| `CVSamplePlugin/CVSampleWrap.cs` | The P/Invoke declarations. |

## Key DMC interfaces used

- **`Base.IDevice`** — the plugin contract. `IsEnabled()` returns `true` but `GetSettings()` returns
  `null`; the plugin exists to register the command.
- **`Core.ICommand`** — the recipe command (`cv_sample_command`, "CV Sample Command"),
  `IsControlCommand = true`.
- **`VisionPlugin.CameraPlugin`** — the camera abstraction. Used for `selected_camera`,
  `GetPositioningDevice()`, `Offset`, `GetImageBytes`, `DoDelayAfterMotion`,
  `GetViewCenterPosition()` and `GetPoint()`.
- **`Base.Hardware.Actions`** — `IsPositionValid` and `JumpTo` for the motion.
- **`Core.VariableEx`** — a variable with named sub-values, so a single recipe variable can carry
  both x and y.

## How the interop is wired

`CVSampleWrap` declares the two native functions with
`[DllImport("CVSampleDll.dll", CallingConvention = CallingConvention.Cdecl)]`. The C++ side takes the
image as `char image[]` and returns the detected point through `int &pointX, int &pointY`; the C#
side passes a `byte[]` and `ref int`, which marshals correctly.

The native DLL must sit somewhere Windows will find it when the managed plugin loads — in practice,
next to the plugin DLL in the DMC `Plugins` folder.

## What the command does at run time

1. Take `CameraPlugin.selected_camera`; fail with a clear message if none is selected.
2. Compute the target as the configured position minus the camera's offset, so the *camera* ends up
   over the point of interest rather than the laser.
3. Validate the target with `Hardware.Actions.IsPositionValid`, jump there, then honour the camera's
   configured settling delay via `DoDelayAfterMotion`.
4. Grab the frame as raw bytes with width, height and bytes-per-pixel returned by reference.
5. Call `CVSampleWrap.find_point` to get pixel coordinates.
6. Convert those pixels to system coordinates with `cam.GetPoint`, seeded from
   `cam.GetViewCenterPosition()`.
7. Write the result into the recipe variable named by the `export_variable` parameter (default
   `detected_point`), as `detected_point.x` and `detected_point.y`.

`Compile()` creates that variable so later commands can reference it even before the first run. The
`set_value_to_global_variables` field switches between recipe-scoped and global variables; it is
hard-coded to `false`.

## Building

Both projects target the DMC platform; the managed one is **.NET Framework 4.8**, output type Library.

1. Build `CVSampleDll` first. Match its platform (x86/x64) to your DMC installation.
2. In `CVSamplePlugin`, re-add `Base.dll`, `Core.dll`, `GUI.dll` and `Vision\VisionPlugin.dll` from
   your installed DMC version — the checked-in paths point at a build tree
   (`..\..\..\..\bin\Debug\`) and will not resolve.
3. Set the managed project's **Build → Output path** to your DMC `Plugins` directory, and make sure
   `CVSampleDll.dll` is copied there too.
4. Build.

## Notes and limitations

- `find_point` ignores the image entirely and returns `(width/4, height/4)`. `count_pixels` returns
  `width * height`. Replace both with your OpenCV/Halcon/proprietary detection.
- `count_pixels` is declared and wrapped but never called from C#.
- There is no check that the detection succeeded — the command always reports success and always
  writes a coordinate. Add a return code to `find_point` for real use.
- The image is passed as a flat byte array; your native code must interpret the layout using the
  `bytes_per_pixel` argument.
- Requires VisionPlugin to be installed and a camera to be configured and selected.
