using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base;
using Core;
using System.IO.Ports;
using System.Threading;

namespace LaserPlugin
{
    public class Plugin : IDevice, IRecipeControl
    {
        public static Plugin plugin = null;
        public Settings settings = new Settings(); // Settings that are visible in File->Settings->My Laser
        List<IFormTool> tools = new List<IFormTool>(); // Add GUI tools
        ToolGUI tool_gui = null;
        StatusGUI tool_status = null;

        public Plugin()
        {
            plugin = this;

            // Add option to add command into recipe 
            IFormTool cmd = DMC.Helpers.AddTool(DMC.ToolLocation.HomeTab, ICommand.AddCreator(typeof(Command), "my_laser", "Devices"), "My Laser");
            cmd.SetImage(Properties.Resources.laser16, false);

            Add(cmd);
        }

        private void Add(IFormTool tool) 
        { 
            if (tool != null) tools.Add(tool); 
        }

        private void SetTools(bool visible)
        {
            for (int i = 0; i < tools.Count; i++)
                tools[i].SetVisible(visible);
        }

        bool tools_added = false;
        private void AddToolsToGUI()
        {
            if (tools_added) return;
            tool_gui = new ToolGUI();
            tool_status = new StatusGUI();
            Add(DMC.Helpers.AddPopupTool(DMC.ToolLocation.HomeTab, "Laser", "My Laser Control", tool_gui, true, Properties.Resources.laser_settings));
            Add(DMC.Helpers.AddTool(DMC.ToolLocation.HomeTab, "Laser", tool_status));
            tools_added = true;
        }

        public string GetName() { return "My Laser"; }

        // return last error
        public string GetErrorMessage() { return Functions.GetLastErrorMessage(); }

        public bool Connect()
        {
            try
            {
                // Connect to device using user defined settings
                return true;
            }
            catch (Exception ex) { return Functions.Error(this, "Unable to connect to " + GetName(), ex); }
        }

        public bool ApplySettings()
        {
            if (IsConnected()) Disconnect();
            if (IsEnabled())
            {
                AddToolsToGUI();
                SetTools(true); // set tools visible
            }
            else
            {
                SetTools(false); // hide tools
            }
            if (IsEnabled() && State.is_connected_to_hardware) return Connect();
            return true;
        }
        public void Disconnect()
        {
            try
            {

            }
            catch (Exception) { }
        }
        public bool IsConnected() { return true; }
        public bool IsEnabled() { return settings.Enabled; }

        // user clicked Run recipe
        public bool OnRecipeStart()
        {
            if (!IsEnabled()) return true;
            // we can open shutter automatically
            return true;
        }

        // recipe finished
        public void OnRecipeFinish() 
        { 
            if (!IsEnabled()) return; 
            // we can close shutter automatically
        }

        // user clicked Stop
        public void Stop() { }
        public IDeviceSettings GetSettings() { return settings; }

        public bool CanRunRecipe()
        {
            return true;
        }

        public bool CanPauseRecipe()
        {
            return true;
        }

        public bool CanContinueRecipe()
        {
            return true;
        }
    }
}
