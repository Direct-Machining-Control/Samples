using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Base;
using Core;

namespace PythonCommandPlugin
{
    public class Plugin : IDevice
    {
        static readonly string UN = "python_plugin";
        static readonly string FN = "Python";
        static readonly string DS = "Python Plugin";

        public static PluginSettings settings = new PluginSettings(UN, FN, DS);

        public Plugin()
        {
            DMC.Helpers.AddTool(DMC.ToolLocation.HomeTab, Core.ICommand.AddCreator(typeof(Command), Command.unique_name, "group_devices"), Command.friendly_name)
                .SetImage(Properties.Resources.computing_16, false);

            DMC.Helpers.AddTool(DMC.ToolLocation.HomeTab, Core.ICommand.AddCreator(typeof(ExecutableFileCommand), ExecutableFileCommand.unique_name, "group_devices"), ExecutableFileCommand.friendly_name)
                .SetImage(Properties.Resources.computing_16, false);
        }

        public bool ApplySettings()
        {
            return true;
        }

        public bool Connect()

        {
            return true;
        }

        public void Disconnect()
        {
            return;
        }

        public string GetErrorMessage()
        {
            return Functions.GetLastErrorMessage();
        }

        public string GetName()
        {
            return FN;
        }

        public IDeviceSettings GetSettings()
        {
            return settings;
        }

        public bool IsConnected()
        {
            return false;
        }

        public bool IsEnabled()
        {
            return true;
        }

        public void OnRecipeFinish()
        {
            return;
        }

        public bool OnRecipeStart()
        {
            return true;
        }

        public void Stop()
        {
            return;
        }
    }

    public class PluginSettings : IDeviceSettings
    {
        public StringParameter executable_path = new StringParameter("executable_path", "Python Executable Path", "");

        public PluginSettings(string unique_name, string title, string description) : base(unique_name, title, description)
        {
            Add(executable_path);
        }

        public override UserControl GetGUI()
        {
            SettingsGUI gui = new SettingsGUI();
            gui.Set(this);
            return gui;
        }
    }
}