using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base;
using Core;
using Core.Commands;

namespace TestPlugin
{
    public class DistanceSensorCommand : ICommand
    {
        public StringParameter variable_name = new StringParameter("variable_name", "Result Variable Name", "distance");
        public Core.Commands.Positioning position = new Core.Commands.Positioning(false);
        Vector3 measuring_position = Vector3.Zero;

        public static ICommand Create() { return new DistanceSensorCommand(); }
        public override System.Drawing.Bitmap GetIcon() { return Properties.Resources.distance_sensor; }

        public DistanceSensorCommand()
            : base("distance_sensor", DistanceSensorPlugin.DEVICE_NAME, DistanceSensorPlugin.DEVICE_NAME)
        {
            Add(variable_name);
            Add(position); 
        }

        public override string GetInfo()
        {
            return variable_name.Value;
        }

        public override bool IsControlCommand { get { return true; } }
        public override ICommandGUI GetGUI()
        {
            return DistanceSensorCommandGUI.Get(this);
        }

        public override bool Compile()
        {
            is_cancel = false;
            if (!ParseAll()) return false;
            measuring_position = position.GetShift(Recipe.processor, Base.Shapes.Cube.Zero);
            if (!Core.Commands.Positioning.IsPositionValid(measuring_position)) return false;

            Recipe.processor.Add(new Base.Shapes.FakeMotionCommand(Recipe.processor.LastPosition, measuring_position)); // to change last position in the recipe

            if (variable_name.Value != null && variable_name.Value.Length > 0)
                Recipe.variables.Add(new Variable(variable_name.Value, 0));

            return true;
        }

        bool is_cancel = false;

        public override bool Run()
        {
            if (!Compile()) return false;

            if (is_cancel) return true;
            if (!Hardware.Actions.JumpTo(measuring_position.x, measuring_position.y, measuring_position.z, true)) return false;
            if (is_cancel) return true;

            if (!DistanceSensorPlugin.plugin.device.Run(Settings.StageZ, measuring_position, 1, 0.1)) return false;
            if (!DistanceSensorPlugin.plugin.device.IsFound()) return Functions.Error("Unable to measure the distance with '" + DistanceSensorPlugin.plugin.GetName() + "'");
            
            if (variable_name.Value != null && variable_name.Value.Length > 0)
                Recipe.variables.Add(new Variable(variable_name.Value, DistanceSensorPlugin.plugin.device.GetPosition()));

            return true;
        }

        public override void Stop()
        {
            is_cancel = true;
            DistanceSensorPlugin.plugin.device.Stop();
            System.Threading.Thread.Sleep(100);
            is_cancel = true;
        }

    }
}
