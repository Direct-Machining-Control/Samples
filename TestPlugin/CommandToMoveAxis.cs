using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base;
using Core;
using Core.Commands;

namespace TestPlugin
{
    public class CommandToMoveAxis : ICommand
    {
        public static void AddCommandToCommandList()
        {
            DMC.Helpers.AddTool(DMC.ToolLocation.HomeTab, Core.ICommand.AddCreator(typeof(CommandToMoveAxis), UN, "Devices"), FN).SetImage(Properties.Resources.distance_sensor, false);
        }

        public ParamSD pos = new ParamSD("pos", "Position (mm)", 0);
        const string UN = "CommandToMoveAxis";
        const string FN = "Command To Move Axis";
        const string DESC = "Command To Move Axis";

        public CommandToMoveAxis()
            : base(UN, FN, DESC)
        {
            Add(pos);
        }

        public static ICommand Create() { return new CommandToMoveAxis(); }
        public override bool IsControlCommand { get { return true; } }

        public override bool Compile()
        {
            if (!ParseAll()) return false;
            return true;
        }

        public override bool Run()
        {
            if (!Compile()) return false;

            // Take axes list
            var list = Base.Settings.Axes;
            if (list.Count < 1) return true;

            // Take first axis
            var axis_settings = list[0];

            if (axis_settings.Axis == null) return Functions.ErrorF("Not connected to axis");
            var axis = axis_settings.Axis;

            if (!axis.Move(pos.number, true)) return false;

            return true;
        }
    }
}
