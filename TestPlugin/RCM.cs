using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPlugin
{
    class RCM
    {
    }

    class RC_IMPORT : IRCFunction
    {
        public string GetName() { return "IMPORT"; }
        public int GetArgCount() { return 0; }
        public bool Run(string[] arguments, ref string res)
        {
            Base.Settings.main_window.BeginInvoke(new MethodInvoker(() => {
                DMC.Actions.ImportCAD(Core.Commands.FileImportType.Any);
            }));



            return true;
        }
    }

    class RC_IS_CONNECED : Base.IRCFunction
    {
        public string GetName() { return "IS_CONNECED"; }
        public int GetArgCount() { return 0; }
        public bool Run(string[] arguments, ref string res)
        {
            res = Base.State.is_connected_to_hardware ? "1" : "0";
            return true;
        }
    }

    class RC_PICK_AND_PLACE : Base.IRCFunction
    {
        public string GetName() { return "PICK_AND_PLACE"; }
        public int GetArgCount() { return 6; }
        public bool Run(string[] arguments, ref string res)
        {
            if (arguments.Length < 7) { res = "More arguments are needed. "; return false; }
            if (!State.is_connected_to_hardware) { res = "Not connected to hardware. "; return false; }
            if (State.is_running) { res = "Unable to move axis. Recipe is running. "; return false; }
            if (State.InAction) { res = "Unable to move axis. Wrong state for moving axis. "; return false; }

            double x0 = 0, y0 = 0, z0 = 0;
            double x1 = 0, y1 = 0, z1 = 0;
            if (!Core.Evaluation.Parse(arguments[1], ref x0)) { res = Functions.GetLastErrorMessage(); return false; }
            if (!Core.Evaluation.Parse(arguments[2], ref y0)) { res = Functions.GetLastErrorMessage(); return false; }
            if (!Core.Evaluation.Parse(arguments[3], ref z0)) { res = Functions.GetLastErrorMessage(); return false; }
            if (!Core.Evaluation.Parse(arguments[4], ref x1)) { res = Functions.GetLastErrorMessage(); return false; }
            if (!Core.Evaluation.Parse(arguments[5], ref y1)) { res = Functions.GetLastErrorMessage(); return false; }
            if (!Core.Evaluation.Parse(arguments[6], ref z1)) { res = Functions.GetLastErrorMessage(); return false; }

            // move to pick position
            if (!Base.Hardware.Actions.JumpTo(Hardware.PositioningDevice.Stage1, x0, y0, z0, true)) { res = Functions.GetLastErrorMessage(); return false; }

            // pick
            IOTool digital_output = Base.Settings.IOTools.GetOutput("pick"); // get preconfigured(in Settings->IO Tools) digital output tool
            if (digital_output != null && !digital_output.Run(true)) { res = Functions.GetLastErrorMessage(); return false; } // set digital output to high

            // move to place position
            if (!Base.Hardware.Actions.JumpTo(Hardware.PositioningDevice.Stage1, x1, y1, z1, true)) { res = Functions.GetLastErrorMessage(); return false; }

            if (digital_output != null && !digital_output.Run(false)) { res = Functions.GetLastErrorMessage(); return false; } // set digital output to low

            return true;
        }
    }
}
