using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base;

namespace HatchingPlugin
{
    public class HatchingPlugin : IDevice
    {
        public HatchingPlugin()
        {
            // add "My Hatching" to hatching list
            Core.Commands.Hatching.hatchers.Add(new CustomHatching());

            // add hatching pre/post processors
            Core.Commands.IHatchProcessor.AddIHatchProcessor(new HatchingPreProcessing());
            Core.Commands.IHatchProcessor.AddIHatchProcessor(new HatchingPostProcessing());

        }

        public bool Connect() { return true; }

        public void Stop() { }

        public string GetName() { return "My Hatching Plugin"; }

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
