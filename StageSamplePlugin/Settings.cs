using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Base;

namespace StageSamplePlugin
{
    // General settings (one controller can control many axes)
    public class ControllerSettings : IDeviceSettings
    {
        public List<AxisSettings> axes = new List<AxisSettings>();

        // custom controller parameters
        public StringListParameter port = new StringListParameter("port", "COM Port", "", null, true);


        public ControllerSettings()
            : base("MyStageController", AxisSettings.controller_name, "My controller settings")
        {
            PriorityInSettings = (int)IDeviceSettings.Priorities.Top;
            
            for (int i = 0; i < Base.Settings.Axes.Count; i++)
            {
                if (!(Base.Settings.Axes[i] is Stage)) continue;
                AxisSettings axis_settings = new AxisSettings((Stage)Base.Settings.Axes[i]);
                axes.Add(axis_settings);
                Add(axis_settings); // add axis settings for automatic parameter saving/loading
            }

            // add all 'custom controller parameters' to parameter list
            Add(port); // add parameter for automatic parameter saving/loading, assigning to GUI field, ...
        }

        public override System.Windows.Forms.UserControl GetGUI()
        {
            SettingsGUI gui = new SettingsGUI();
            gui.Set(this);
            return gui;
        }
    }


    // 
    public class AxisSettings : MultiParameter, IAxisAdditionalSettings
    {
        public IntParameter axis_index = new IntParameter("axis_index", "", 0);
        public DoubleParameter steps_per_mm = new DoubleParameter("steps_per_mm", "", 1000);

        public const string controller_name = "My Controller";
        public string GetControllerName() { return controller_name; }
        public Stage stage;

        public AxisSettings(Stage stage)
            : base(stage.UniqueName, stage.FriendlyName, stage.Description)
        {
            this.stage = stage;
            stage.controllers.Add(this); // Add this controller int controller list
            Add(axis_index); Add(steps_per_mm);
        }

        public UserControl GetAxisGUI()
        {
            return new AxisGUI(this);
        }

        // Is axis controller is enabled. Axis controller will be visible/invisible to the user
        public bool IsEnabled { get { return Plugin.plugin.IsEnabled(); } }
    }
}
