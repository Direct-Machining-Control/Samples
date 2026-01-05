using Base;
using System;
using System.Text;

namespace LightingControllerExamplePlugin
{
    /// <summary>
    /// Your Light controller implementation
    /// </summary>
    public class Plugin : IDevice
    {
        /// <summary>
        /// A singleton instance for this controller, to access communication functions
        /// </summary>
        public static Plugin plugin;

        /// <summary>
        /// IDeviceSettings for this instance
        /// </summary>
        public PluginSettings settings = new PluginSettings();

        public Plugin()
        {
            plugin = this;
        }

        /// <summary>
        /// Sends the intensity set command for channel. Pass it in voltage
        /// </summary>
        public bool SetLightIntensity(Light light,double intensity)
        {
            try
            {
                // we "simulate" that we set the intensity.
                light.lastIntensity = intensity;
                 
                // in reality, you would have to integrate this with your lighting controller device, for example send a tcp/ip message or call a COM dll function
                return true;
            }
            catch (Exception e)
            {
                return Functions.Error(this, "An error occurred when trying to set light intensity to example controller", e);
            }
            
        }

        /// <summary>
        /// Gets the current set intensity level from controller for specifiec channel. Returns value in voltage
        /// </summary>
        public double GetLightIntensity(Light light)
        {
            try
            {
                // we return the "simulated intensity.
                // in reality, you would have to integrate this with your lighting controller device, for example send a tcp/ip message or call a COM dll function
                return light.lastIntensity;
            }
            catch (Exception e)
            {
                Functions.Error(this, "An error occurred when trying to get light intensity from example controller", e);
                return light.lastIntensity;
            }
        }

        public bool ApplySettings()
        {
            if (!settings.Enabled)
                return true;
            try
            {
                if (IsConnected())
                    Disconnect();

                if (Base.State.is_connected_to_hardware)
                    Connect();

                settings.EndSettingsRead(); // updates the channel devices to lighting plugin 
            }
            catch(Exception e)
            {
                return Functions.Error(this,"Unable to apply example controller settings. ", e);
            }
            return true;
        }

        public bool Connect()
        {
            if (!settings.Enabled)
                return true;
            // we simulate that we have connected
            // in reality, you should try to establish connection to your device using tcp/ip, COM dll or whatever your light controller needs to connect to it
            return true;
        }

        public void Disconnect()
        {
            //Add disconnection logic with your device
            return;
        }


        /// <summary>
        /// Add logic to get the color set on the controller device. You can remove this if you device doesnt support color functionality
        /// </summary>
        public void GetColor(Light light, out double r, out double g, out double b)
        {
            r = g = b = 0;
            //here you would add logic to get the color for the light from the controller device
            return;
        }

        /// <summary>
        /// Add logic to get the color set on the controller device. You can remove this if you device doesnt support color functionality
        /// </summary>
        public void SetColor(Light light,double r, double g, double b)
        {
            //here you would add logic to set the color for the light from the controller device
            return;
        }


        public string GetErrorMessage()
        {
            return Functions.GetLastErrorMessage();
        }

        public string GetName()
        {
            return "Example Light Controller";
        }

        public IDeviceSettings GetSettings()
        {
            return settings;
        }

        public bool IsConnected()
        {
            // Add logic to check if device is disconnected or not
            return true;
        }

        public bool IsEnabled()
        {
            return settings.Enabled;
        }

        public void OnRecipeFinish()
        {
            
        }

        public bool OnRecipeStart()
        {
            return true;
        }

        public void Stop()
        {
        }
        
    }
}
