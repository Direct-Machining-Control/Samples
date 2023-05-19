using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base;

namespace Custom3DSupportsPlugin
{
    public class Plugin : IDevice
    {
        public Plugin()
        {
            // add "CustomSupportGeneration" to support generator list
            Core.Commands.SupportGenerator.support_generators.Add(new CustomSupportGeneration());
        }

        public bool Connect() { return true; }
        public void Stop() { }
        public string GetName() { return "3D supports"; }
        public bool ApplySettings() { return true; }
        public void Disconnect() { }
        public bool IsConnected() { return false; }
        public bool IsEnabled() { return false; }
        public bool OnRecipeStart() { return true; }
        public void OnRecipeFinish() { }
        public IDeviceSettings GetSettings() { return null; }
        public string GetErrorMessage() { return Base.Functions.GetLastErrorMessage(); }
    }

    
}
