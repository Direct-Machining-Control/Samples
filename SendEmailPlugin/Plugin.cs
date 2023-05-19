using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base;

namespace SendEmailPlugin
{
    // Class name must be public, contain Plugin word, must inherit IDevice interface, can't be abstract
    public class MyPlugin : IDevice
    {
        public MyPlugin()
        {
            DMC.Helpers.AddTool(DMC.ToolLocation.HomeTab, Core.ICommand.AddCreator(typeof(SendEmailCommand), SendEmailCommand.UN, "group_recipe_flow"), SendEmailCommand.FN).SetImage(Properties.Resources.envelope_32, false);
            
        }

        // Action when user clicks Connect to hardware and IsEnabled is true
        public bool Connect() { return true; }

        // Action when user clicks Disconnect from hardware
        public void Disconnect() { }

        // Action when user clicks Stop button
        public void Stop() { }

        public string GetName() { return "Send Email"; }

        // Action when changes to settings are confirmed or loaded during DMC startup
        public bool ApplySettings() { return true; }

        // Needs to return if device is connected 
        public bool IsConnected() { return false; }

        // Is device is enabled 
        public bool IsEnabled() { return false; }

        // Called before starting recipe
        public bool OnRecipeStart() { return true; }

        // Called after recipe is finished or stopped
        public void OnRecipeFinish() { }

        // Get device settings
        public IDeviceSettings GetSettings() { return null; }

        // Get error message ( is called if Connect returns false )
        public string GetErrorMessage() { return Base.Functions.GetLastErrorMessage(); }
    }
}
