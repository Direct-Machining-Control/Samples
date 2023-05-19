using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomDLPPlugin
{
    public class CustomDLPPlugin : IDevice
    {
        #region VARIABLES AND PARAMETERS
        string _UniqueName_ = "CustomDLPPlugin";

        CustomDLPPluginSettings settings = new CustomDLPPluginSettings();
        UniqueProcess proc = new UniqueProcess();
        AddBorder proc_border = new AddBorder();

        internal DLPPlugin.Plugin DEV { get { return DLPPlugin.Plugin.plugin; } }

        #endregion

        #region IDEVICE Implementation

        public CustomDLPPlugin()
        {
        }

        public bool ApplySettings() {

            bool use_unique_view = settings.Enabled && settings.ModeUniqueView;
            bool add_border = settings.Enabled && settings.ModeAddBorder;

            if (use_unique_view)
            {
                // Activate unique view
                DEV.view_manager = proc;
            }
            else
            {
                // Deactivate unique view
                if (DEV.view_manager != null && DEV.view_manager is UniqueProcess)
                    DEV.view_manager = null;
            }

            if (add_border)
            {
                // Add view processor
                DLPPlugin.Plugin.view_processors.Add(proc_border);
            }
            else
            {
                // Remove view processor
                if (DLPPlugin.Plugin.view_processors.Contains(proc_border))
                    DLPPlugin.Plugin.view_processors.Remove(proc_border);
            }
            return true;

        }
        public bool Connect() { return true; }
        public void Disconnect() { }
        public string GetErrorMessage() { return Functions.GetLastErrorMessage(); }
        public string GetName() { return _UniqueName_; }
        public IDeviceSettings GetSettings() { return settings; }
        public bool IsConnected() { return true; }
        public bool IsEnabled() { return settings.Enabled; }
        public void OnRecipeFinish() { }
        public bool OnRecipeStart() { return true; }
        public void Stop() { }
        #endregion
    }
}
