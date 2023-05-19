using System;
using System.Collections.Generic;
//using System.Windows.Forms;
using System.Text;
using System.IO;
using Base;
using Base.Shapes;
using Core;

namespace SelectTrianglePlugin
{
    public class Plugin : IDevice
    {
        public Plugin()
        {
            // add "CustomSupportGeneration" to support generator list
            //Core.Commands.SupportGenerator.support_generators.Add(new CustomSupportGeneration());
            DMC.Helpers.AddTool(DMC.ToolLocation.HomeTab, "View", "Select triangle", new Tool());
        }

        public bool Connect() { return true; }
        public void Stop() { }
        public string GetName() { return "select_triangle"; }
        public bool ApplySettings() { return true; }
        public void Disconnect() { }
        public bool IsConnected() { return false; }
        public bool IsEnabled() { return false; }
        public bool OnRecipeStart() { return true; }
        public void OnRecipeFinish() { }
        public IDeviceSettings GetSettings() { return null; }
        public string GetErrorMessage() { return Base.Functions.GetLastErrorMessage(); }
    }


    public class Tool : DMC.ITool
    {
        public Tool()
        {
        }
        public void Run()
        {
            ToolSurfaceEdit tool = new ToolSurfaceEdit();
            View.CurrentView.SetActiveTool(tool);
            tool.Start();
        }
    }
}
