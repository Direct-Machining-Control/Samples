using Base;
using Core;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LightingControllerExamplePlugin
{
    /// <summary>
    /// Settings displayed in hardware settings for your light controller device implementation
    /// </summary>
    public class PluginSettings : IDeviceSettings, IGUIManagerParameter
    {
        /// <summary>
        /// example parameter for information needed to connect to device
        /// </summary>
        public StringParameter ipAddress = new StringParameter("ipAddress", "IP Address", "IP Address of light controller","127.0.0.1");
        /// <summary>
        /// Each Light instance
        /// </summary>
        public List<Light> Lights = new List<Light>();

        public PluginSettings() : base("lightingExampleSettings", "Lighting Example", "Settings for lighting example plugin")
        {
            Add(ipAddress);
          
            //Lets say our device has 4 channels for the lights, so we add 4 lights.
            for (int i = 0; i < 4; i++)
            {
                Lights.Add(new Light("exampleLight" + (i + 1), i + 1));
                Add(Lights[i]);
            }
        }

        
        public override UserControl GetGUI()
        {
            // creates a gui for the lights and other parameters added using Add() to these settings
            var gui = ICommandGUI.GetStaticGUI(UniqueName, Parameters, null,this);
            gui.AutoScroll = true;
            return gui;
        }


        //After reading the settings, we add/remove the lights from the lighting plugin
        public override void EndSettingsRead() 
        {
            foreach (var channel in Lights)
            {
                if (channel.Enabled && !LightingPlugin.Plugin.Instance.AvailableLights.Contains(channel))
                {
                    LightingPlugin.Plugin.Instance.AddLightDevice(channel);
                }
                else if (!channel.Enabled && LightingPlugin.Plugin.Instance.AvailableLights.Contains(channel))
                {
                    LightingPlugin.Plugin.Instance.RemoveLightDevice(channel);
                }
            }
        }



        //IGUIManagerparameter interface needed to set enabled or visibl state on a parameter based on other parameter values
        #region IGUIManagerParameter

        public List<IParameter> GetDependencies(IParameter prm)
        {
            if (prm == enabled)
            {
                List<IParameter> parameters = new List<IParameter>() { ipAddress };
                for (int i = 0; i < Lights.Count; i++)
                {
                    parameters.Add(Lights[i]);
                }
                return parameters;
            }
            
            return null;
        }

        public bool IsIParameterEnabled(IParameter prm)
        {
            if (prm == ipAddress || Lights.Contains(prm))
            {
                return enabled.value;
            }
            return true;
        }

        public bool IsIParameterVisible(IParameter prm)
        {
            return true;
        }
        #endregion

    }
}
