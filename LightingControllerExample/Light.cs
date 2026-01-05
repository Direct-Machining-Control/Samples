using Base;
using Core;
using LightingPlugin;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LightingControllerExamplePlugin
{
    /// <summary>
    /// Light (single channel) instance on your light controller device
    /// </summary>
    public class Light : MultiParameter, ILightingControl, IGUIManagerParameter
    {
        public const string FN = "ExampleLight";
        public const string DESC = "Example Light";

        /// <summary>
        /// Set if this channel is enabled or disabled for your light controller device
        /// </summary>
        private BoolParameter enabled = new BoolParameter("enabled", "Enabled", "Is this pin enabled", false);

        /// <summary>
        /// Let the user defined the maximum voltage for this channel.
        /// </summary>
        private DoubleExtParameter voltage = new DoubleExtParameter("voltage", "Voltage (V)", "Maximum voltage", 24, 12, 36, 0.5);

        /// <summary>
        /// Let the user decide if the light is on/off or should be controlled with slider
        /// </summary>
        private BoolParameter toggleMode = new BoolParameter("toggleMode", "Operate in On/Off mode", "When this is turned on, the lighting controls will use this light in on/off mode, on being 100%, off being 0%", true);


        /// <summary>
        /// Is the light enabled?
        /// </summary>
        public bool Enabled => enabled.value;

        /// <summary>
        /// Voltage on the controller. Automatically set on connect
        /// </summary>
        public double Voltage => voltage.value;

        /// <summary>
        /// channel ID on the controller for this light
        /// </summary>
        public int channelID = 1;

        /// <summary>
        /// last intensity value we got from the controller
        /// </summary>
        public double lastIntensity = 0;

        public Light(string unique_name, int pinID) : base(unique_name,FN,DESC)
        {
            channelID = pinID;
            
            //add a header for gui
            HeaderGroupParameter header = new HeaderGroupParameter(Name);
            Add(header);

            Add(enabled);
            Add(voltage);
            Add(toggleMode);
        }

        /// <summary>
        /// Is the light connected (usually just means is the controller connected)
        /// </summary>
        public bool IsConnected => Plugin.plugin.IsConnected();

        /// <summary>
        /// name of the light to show in LightingPlugin settings
        /// </summary>
        public string Name => "Example light (channel " + channelID + ")";

        /// <summary>
        /// Minimum voltage value
        /// </summary>
        public double Minimum => 0;

        /// <summary>
        /// Maximum voltage value
        /// </summary>
        public double Maximum => Voltage;

        /// <summary>
        /// Currently unused. Usually would indicate the step value on the slider
        /// </summary>
        public double Step => 0.2f;

        /// <summary>
        /// If this is true, then instead of a slider there will be a check button to turn the light on or off.
        /// the Maximum voltage will be used for on, and Minimum for off.
        /// </summary>
        public bool IsToggle => toggleMode.value;

        /// <summary>
        /// Return the current set color on the light
        /// </summary>
        public void GetColor(out double r, out double g, out double b)
        {
            Plugin.plugin.GetColor(this,out r, out g, out b);
            return;
        }

        /// <summary>
        /// Return the current set voltage value (should be between Minimum and Maximum you provided)
        /// </summary>
        /// <returns></returns>
        public double GetValue()
        {
            lastIntensity = Plugin.plugin.GetLightIntensity(this);
            return lastIntensity;
        }

        /// <summary>
        /// If this is true, in ribbon tool there will be an option for the user to select the color, then
        /// the GetColor() and SetColor() will be called.
        /// If it is false, all these options will be ignored.
        /// </summary>
        /// <returns></returns>
        public bool HasColor()
        {
            return false;
        }

        /// <summary>
        /// This function is called, when a new color has been selected in ribbon.
        /// if HasColor() is false, this will never be called
        /// </summary>
        public void SetColor(double r, double g, double b)
        {
            Plugin.plugin.SetColor(this, r, g, b);
            return;
        }

        /// <summary>
        /// Function that gets called when we need to set new voltage to the controller.
        /// </summary>
        /// <param name="value">Voltage value between Minimum and Maximum you provided</param>
        /// <returns></returns>
        public bool SetValue(double value)
        {
            return Plugin.plugin.SetLightIntensity(this, value);
        }




        // create a gui here for the light in the settings 
        public override Control CreateField()
        {
            var gui = ICommandGUI.GetStaticGUI(unique_name, parameters, null, this);
            gui.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            return gui;
        }

        //IGUIManagerparameter interface needed to set enabled or visibl state on a parameter based on other parameter values
        #region IGUIManagerParameter
        public bool IsIParameterVisible(IParameter prm)
        {
            if (prm == voltage || prm == toggleMode)
                return enabled.value;
            return true;
        }

        public bool IsIParameterEnabled(IParameter prm)
        {
            return true;
        }

        public List<IParameter> GetDependencies(IParameter prm)
        {
            if (prm == enabled)
            {
                return new List<IParameter>() { voltage, toggleMode };
            }
            return null;
        }
        #endregion
    }
}
