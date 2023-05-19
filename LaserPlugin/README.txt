Demonstration of laser control plugin

To run this plugin:
1. Remove old references: Base.dll, Core.dll, GUI.dll and add these dll's from installed version of the software (e.g. C:\Program Files (x86)\DMC\DMC 1.2.31). For debugging, use 32bit dll's if possible (Visual Studio User Interface Designer problems might appear on 64bit version).
2. Go to project properties -> Build tab -> Output group and define Output path to correct location (dll should appear in Plugins directory)
3. Build dll. 