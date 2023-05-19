using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base;
using Core;
using VisionPlugin;

namespace CVSamplePlugin
{
    public class Plugin : IDevice
    {
        static readonly string FN = "CV Sample Plugin";

        public Plugin()
        {
            DMC.Helpers.AddTool(DMC.ToolLocation.HomeTab, Core.ICommand.AddCreator(typeof(Command), Command.unique_name, "group_devices"), Command.friendly_name)
                .SetImage(Properties.Resources.eye_open_16, false);
        }

        public bool ApplySettings()
        {
            return true;
        }

        public bool Connect()
        {
            return true;
        }

        public void Disconnect()
        {
            return;
        }

        public string GetErrorMessage()
        {
            return Functions.GetLastErrorMessage();
        }

        public string GetName()
        {
            return FN;
        }

        public IDeviceSettings GetSettings()
        {
            return null;
        }

        public bool IsConnected()
        {
            return false;
        }

        public bool IsEnabled()
        {
            return true;
        }

        public void OnRecipeFinish()
        {
            return;
        }

        public bool OnRecipeStart()
        {
            return true;
        }

        public void Stop()
        {
            return;
        }
    }

    public class Command : ICommand
    {
        public static new string unique_name = "cv_sample_command";
        public static new string friendly_name = "CV Sample Command";
        public static new string description = "CV Sample Command";

        public ParamSD position_x = new ParamSD("position_x", "Position X", 0);
        public ParamSD position_y = new ParamSD("position_y", "Position Y", 0);
        public ParamSD position_z = new ParamSD("position_z", "Position Z", 0);

        public StringParameter export_variable = new StringParameter("export_variable", "Export Values to Variable", "detected_point");
        bool set_value_to_global_variables = false;

        public Command() : base(unique_name, friendly_name, description)
        {
            Add(position_x); Add(position_y); Add(position_z);
            Add(export_variable);
        }
        public static ICommand Create() { return new Command(); }

        public override System.Drawing.Bitmap GetIcon() { return Properties.Resources.eye_open_16; }

        public override bool IsControlCommand { get { return true; } }

        public override bool Compile()
        {
            // configure export variable
            if (export_variable.Value.Length > 0)
            {
                VariableEx output_variable = new VariableEx(export_variable.Value);
                output_variable.variables.Add(new Variable("x", 0));
                output_variable.variables.Add(new Variable("y", 0));
                if (set_value_to_global_variables)
                    Variables.global_variables.Add(output_variable);
                else 
                    Recipe.variables.Add(output_variable);
            }
            return true;
        }

        public override bool Run()
        {
            // prepare command
            Compile();
            // get command parameters
            ParseAll();

            try
            {
                // get camera
                CameraPlugin cam = CameraPlugin.selected_camera;
                if (cam == null) return Functions.ErrorF("Camera is not selected. ");

                // get camera's motion device
                Hardware.PositioningDevice motion_dev = cam.GetPositioningDevice();

                // calculate target position
                Vector3 pos = new Vector3(position_x.number, position_y.number, position_z.number);
                pos -= cam.Offset;

                // move camera to position
                if (!Hardware.Actions.IsPositionValid(motion_dev, pos)) return Functions.ErrorF("Position out of bounds. ");
                if (!Hardware.Actions.JumpTo(motion_dev, pos, true)) return Functions.ErrorF("Motion failed. ");
                cam.DoDelayAfterMotion(motion_dev);

                // get image
                int width = 0, height = 0, bytes_per_pixel = 0;
                byte[] img = cam.GetImageBytes(ref width, ref height, ref bytes_per_pixel);

                // process the image
                int x = 0, y = 0;
                CVSampleWrap.find_point(width, height, bytes_per_pixel, img, ref x, ref y);
                Vector3 detected_point = new Vector3(x, y, 0);

                // convert result pixel coordinates to system coordinates
                Vector3 point_coordinates = cam.GetViewCenterPosition();
                cam.GetPoint(x, y, ref point_coordinates.x, ref point_coordinates.y);

                // set result to a preconfigured variable
                if (export_variable.Value.Length > 0)
                {
                    Variable output_variable = set_value_to_global_variables ? Variables.global_variables.Get(export_variable.Value) : Recipe.variables.Get(export_variable.Value);
                    ((VariableEx)output_variable).variables.Get("x").SetValue(point_coordinates.x);
                    ((VariableEx)output_variable).variables.Get("y").SetValue(point_coordinates.y);
                }
            }
            catch (Exception ex)
            {
                return Functions.ErrorF(ex.Message);
            }

            return true;
        }
    }
}