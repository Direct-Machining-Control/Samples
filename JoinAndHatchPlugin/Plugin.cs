using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinAndHatchPlugin
{
    public class JoinAndHatchPlugin : IDevice
    {
        #region VARIABLES AND PARAMETERS
        string _UniqueName_ = "JoinAndHatchPlugin";

        //JoinAndHatchPluginSettings settings = new JoinAndHatchPluginSettings();

        public JoinAndHatchPlugin()
        {
            JoinAndHatch.AddCommandToCommandList();
        }

        #endregion

        #region IDEVICE Implementation
        public bool ApplySettings() { return true; }
        public bool Connect() { return true; }
        public void Disconnect() { }
        public string GetErrorMessage() { return Functions.GetLastErrorMessage(); }
        public string GetName() { return _UniqueName_; }
        public IDeviceSettings GetSettings() { return null; }
        public bool IsConnected() { return true; }
        public bool IsEnabled() { return false; }
        public void OnRecipeFinish() { }
        public bool OnRecipeStart() { return true; }
        public void Stop() { }
        #endregion
    }
}
