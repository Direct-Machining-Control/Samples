using Base;
using Base.UIBase.Classes;
using Base.UIBase.Interfaces;
using Core;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace LaserPlugin
{
    public class Plugin : IDevice, IRecipeControl, IMarkingParametersControl, IStatusProvider
    {
        public static Plugin plugin = null;
        public Settings settings = new Settings(); // Settings that are visible in File->Settings->My Laser
        List<IFormTool> tools = new List<IFormTool>(); // Add GUI tools
        ToolGUI tool_gui = null;
        StatusGUI tool_status = null;
        Laser laser;
        internal Laser Laser => laser;

        public Plugin()
        {
            plugin = this;

            // Add option to add command into recipe 
            IFormTool cmd = DMC.Helpers.AddTool(DMC.ToolLocation.HomeTab, ICommand.AddCreator(typeof(Command), "my_laser", "Devices"), "My Laser");
            cmd.SetImage(Properties.Resources.laser16, false);

            Add(cmd);

            laser = new Laser(settings);

            Core.Commands.MarkingParams.AdditionalControls.Add(new Laser_MarkingParameters(laser, settings));
            Base.SystemMotion.Active.marking_parameters_control.Add(this);
            Base.IParameter.AddType(new Laser_MarkingParameters(laser, settings));
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
                return laser.Connect();
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
                statusParameters.Add(laser.LaserStatus); // add status parameters
            }
            else
            {
                SetTools(false); // hide tools
                statusParameters.Clear(); // clear status parameters
            }
            if (IsEnabled() && State.is_connected_to_hardware) return Connect();
            return true;
        }
        public void Disconnect()
        {
            try
            {
                laser.Disconnect();
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

        #region IStatusProvider Implementation
        List<StatusParameters> statusParameters = new List<StatusParameters>();

        public IReadOnlyList<StatusParameters> GetStatusParameters()
        {
            return statusParameters;
        }
        #endregion

        #region IRecipeControl
        public bool CanContinueRecipe() { if (!IsConnected()) return Functions.Error(this, $"{Settings.deviceName} is not connected. "); else return true; }
        public bool CanPauseRecipe() { return true; }
        public bool CanRunRecipe() { if (!IsConnected()) return Functions.Error(this, $"{Settings.deviceName} is not connected. "); else return true; }
        #endregion

        #region IMarkingParametersControl
        internal static Laser_MPExtState currentLaser_MPState = null;
        public bool NeedToRunCommandList(IMotionDevice selected_motion_device, MarkingParameters marking_parameters)
        {
            if (!State.is_connected_to_hardware || !IsEnabled() || laser == null || !laser.IsConnected) return false;

            Laser_MPExtState laser_MPtoSet = Get_MP(marking_parameters);
            if (laser_MPtoSet == null || !laser_MPtoSet.NeedToSet || laser_MPtoSet.IsSame(currentLaser_MPState))
                return false;
            return true;
        }
        public bool Set(MarkingParameters marking_parameters)
        {
            if (!State.is_connected_to_hardware) return Err.NotConnected(this);
            if (laser == null || !laser.IsConnected) return true;

            Laser_MPExtState laser_MPtoSet = Get_MP(marking_parameters);
            if (laser_MPtoSet == null || !laser_MPtoSet.NeedToSet || laser_MPtoSet.IsSame(currentLaser_MPState))
                return true;
            if (!laser_MPtoSet.Run(Base.SystemMotion.Active))
                return false;
            currentLaser_MPState = laser_MPtoSet;
            return true;
        }

        private Laser_MPExtState Get_MP(MarkingParameters marking_parameters)
        {
            if (marking_parameters.additional == null)
                return null;
            for (int i = 0; i < marking_parameters.additional.Count; i++)
                if (marking_parameters.additional[i] is Laser_MPExtState)
                    return (Laser_MPExtState)marking_parameters.additional[i];
            return null;
        }
        #endregion
    }
}
