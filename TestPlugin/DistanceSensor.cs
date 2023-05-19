using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Base;

namespace TestPlugin
{
    // Class name must be public, contain Plugin word, must inherit IDevice interface, can't be abstract
    public class DistanceSensorPlugin : Base.IDevice
    {
        public const string DEVICE_NAME = "My Distance Sensor";
        public DistanceSensor device = null;
        List<IFormTool> tools = new List<IFormTool>();
        public static DistanceSensorPlugin plugin = null;
        DistanceSensorGUI status_gui = null;
        DistanceSensorSettings settings = new DistanceSensorSettings();

        public DistanceSensorPlugin() { plugin = this; }

        // Action when user clicks Connect to hardware and IsEnabled is true
        public bool Connect() {

            device = new DistanceSensor();
            // TODO: connect to device

            return true; 
        }

        // Action when user clicks Disconnect from hardware
        public void Disconnect() { }

        // Action when user clicks Stop button
        public void Stop() { }

        public string GetName() { return DEVICE_NAME; }

        // Action when changes to settings are confirmed or loaded during DMC startup
        public bool ApplySettings()
        {
            if (IsEnabled())
            {
                AddToolsToGUI();
                if (device == null) device = new DistanceSensor();
                if (!Core.Commands.FindFocus.devices.Contains(device))
                    Core.Commands.FindFocus.devices.Add(device);
                SetTools(true);
            }
            else
            {
                if (device != null && Core.Commands.FindFocus.devices.Contains(device))
                    Core.Commands.FindFocus.devices.Remove(device);
                device = null;
                SetTools(false);
            }
            return true;
        }

        bool tools_added = false;
        private void AddToolsToGUI() 
        {
            if (tools_added) return;

            DMC.Helpers.AddTool(DMC.ToolLocation.HomeTab, Core.ICommand.AddCreator(typeof(DistanceSensorCommand), "distance_sensor", "Devices"), DistanceSensorPlugin.DEVICE_NAME).SetImage(Properties.Resources.distance_sensor, false);
            
            Add(DMC.Helpers.AddTool(DMC.ToolLocation.ControlTab, "Distance Sensor", "Laser To Distance Sensor", new Tool(Tools.LaserToKeyence), null, false, false, null));
            Add(DMC.Helpers.AddTool(DMC.ToolLocation.ControlTab, "Distance Sensor", "Distance Sensor To Laser", new Tool(Tools.KeyenceToLaser), null, false, false, null));
            SetStatusGUI();
            tools_added = true;
        }

        // Create status gui and add to the ribbon
        private void SetStatusGUI()
        {
            if (status_gui == null)
            {
                status_gui = new DistanceSensorGUI();
                DMC.Helpers.AddTool(DMC.ToolLocation.HomeTab, "Status", status_gui);
            }
        }

        private void Add(IFormTool tool) { tools.Add(tool); }
        private void SetTools(bool visible) {
            for (int i = 0; i < tools.Count; i++)
                tools[i].SetVisible(visible);
        }

        public bool IsConnected() {
            // TODO: needs to return if device is connected 
            return false; 
        }

        // Is device is enabled 
        public bool IsEnabled() { return settings.Enabled; }

        // Called before starting recipe
        public bool OnRecipeStart() { return true; }

        // Called after recipe is finished or stopped
        public void OnRecipeFinish() { }

        // Get device settings
        public Base.IDeviceSettings GetSettings() { return settings; }

        // Get error message ( is called if Connect returns false )
        public string GetErrorMessage() { return Base.Functions.GetLastErrorMessage(); }

        // return distance from laser spot to focus device spot
        internal Base.Vector3 GetOffset()
        {
            return settings.GetOffset();
        }
    }







    /// <summary>
    /// Distance sensor device 
    /// </summary>
    public class DistanceSensor : Core.Commands.IFocusDevice
    {
        bool is_found = false;
        double max_focus_value = 0;
        double max_focus_position = 0;
        bool is_stop = false;

        public DistanceSensor() { }


        public string GetName() { return DistanceSensorPlugin.plugin.GetName(); }

        public bool Compile(Core.Commands.FindFocus command)
        {
            if (!DistanceSensorPlugin.plugin.IsEnabled()) return Base.Functions.Error("'" + GetName() + "' is not enabled");
            return true;
        }

        public void Stop()
        {
            is_stop = true;
        }

        // Return distance from laser spot to focus device spot
        public Base.Vector3 GetShiftFromLaserToFocusDevice()
        {
            return DistanceSensorPlugin.plugin.GetOffset();
        }

        // Iterate to reach better quality
        private bool Iterate(double initial_position, double range, double step, bool go_up, Base.IAxisSettings settings)
        {
            if (step < 0.01) step = 0.01;
            initial_position = Math.Min(initial_position, settings.MaxPosition);
            initial_position = Math.Max(initial_position, settings.MinPosition);
            double max_position = initial_position + range; max_position = Math.Min(max_position, settings.MaxPosition);
            double min_position = initial_position - range; min_position = Math.Max(min_position, settings.MinPosition);

            double difference = go_up ? max_position - initial_position : initial_position - min_position;
            difference = Math.Min(difference, range);
            double mul = go_up ? 1 : -1;

            int steps = (int)(difference / step) + 1;
            for (int i = 1; i <= steps; i++)
            {

                double position = initial_position + (step * i * mul);
                if (position > settings.MaxPosition) position = settings.MaxPosition;
                else if (position < settings.MinPosition) position = settings.MinPosition;

                if (!settings.Axis.Move(position, true)) return false;
                if (ReadPosition(position)) return true;

                if (is_stop) return true;
            }
            return true;
        }



        public bool ReadPosition(double axis_position)
        {
            double p = 0;
            // TODO: assign device position to p

            if (p >= 0.3) return false; // out range
            else if (p <= -0.3) return false; // out range

            max_focus_position = axis_position + p;
            is_found = true;
            return true;
        }

        public bool Run(Core.Commands.FindFocus command)
        {
            is_stop = false;
            max_focus_value = 0; max_focus_position = 0;
            is_found = false;
            if (!Compile(command)) return false;

            return Run(command.axis_settings, command.start_position, command.scanning_range.number, 0.2);
        }

        public bool Run(Base.IAxisSettings axis_settings, Base.Vector3 start_position, double scanning_range, double step_size)
        {
            is_stop = false;
            max_focus_value = 0; max_focus_position = 0;
            is_found = false;

            double position = 0;
            if (axis_settings == Base.Settings.StageZ) position = start_position.z;
            else if (axis_settings == Base.Settings.StageX) position = start_position.x;
            else if (axis_settings == Base.Settings.StageY) position = start_position.y;
            else position = axis_settings.Axis.GetPosition();

            if (!ReadPosition(position))
            {
                if (is_stop) return true;
                Iterate(position, scanning_range, step_size, true, axis_settings);
                if (!IsFound())
                {
                    if (is_stop) return true;
                    Iterate(position, scanning_range, step_size, false, axis_settings);
                }
            }

            if (IsFound() && !is_stop)
            {
                if (!axis_settings.Axis.Move(max_focus_position, true)) return false;
                System.Threading.Thread.Sleep(100);
                ReadPosition(max_focus_position);
            }

            if (is_stop) return true;
            return true;
        }

        public double GetPosition() { return max_focus_position; }
        public bool IsFound() { return is_found; }
        public string[] GetModes() { return null; }

        public UserControl GetGUI()
        {
            return null;
        }

        public string GetFocusAxisName()
        {
            return "Z";
        }

        public void SetThreshold(double value) { }
    }













    public enum Tools : int
    {
        KeyenceToLaser,
        LaserToKeyence
    }


    public class Tool : DMC.ITool
    {
        public static bool is_profiling = false;
        public Tools key;

        public Tool(Tools key)
        {
            this.key = key;
        }
        public void Run()
        {
            switch (key)
            {
                case Tools.LaserToKeyence: Move(true); break;
                case Tools.KeyenceToLaser: Move(false); break;
            }
        }

        public static void Move(bool laser_to_keyence)
        {
            try
            {
                if (!DistanceSensorPlugin.plugin.IsEnabled()) { Base.Functions.Error("Device is not enabled."); Base.Functions.ShowLastError(); return; }
                if (!DistanceSensorPlugin.plugin.IsConnected()) { Base.Functions.Error("Not connected to device."); Base.Functions.ShowLastError(); return; }

                Base.Vector3 offset = DistanceSensorPlugin.plugin.GetOffset();

                if (laser_to_keyence)
                {
                    if (!Base.Hardware.Actions.JumpTo(Base.State.CurrentStage - offset, Base.Settings.StageZ.Enabled))
                        Base.Functions.ShowLastError();
                }
                else
                {
                    if (!Base.Hardware.Actions.JumpTo(Base.State.CurrentStage + offset, Base.Settings.StageZ.Enabled))
                        Base.Functions.ShowLastError();
                }
            }
            catch (Exception ex)
            {
                if (laser_to_keyence) Base.Functions.Error("Unable to move laser position to Keyence.", ex);
                else Base.Functions.Error("Unable to move Keyence to laser position.", ex);
                Base.Functions.ShowLastError();
            }
        }

    }




    public class DistanceSensorSettings : Base.IDeviceSettings
    {
        /// </summary>
        public Base.DoubleParameter offset_x = new Base.DoubleParameter("offset_x", "Offset X (mm)", 0);
        public Base.DoubleParameter offset_y = new Base.DoubleParameter("offset_y", "Offset Y (mm)", 0);
        public Base.DoubleParameter offset_z = new Base.DoubleParameter("offset_z", "Offset Z (mm)", 0);

        public DistanceSensorSettings()
            : base("distance_sensor", DistanceSensorPlugin.DEVICE_NAME, DistanceSensorPlugin.DEVICE_NAME)
        {
            Add(offset_x); Add(offset_y); Add(offset_z);
        }

        public override System.Windows.Forms.UserControl GetGUI()
        {
            return null;
            //Base.SettingsGUI gui = new Base.SettingsGUI();
            //gui.Set(this); return gui;
        }

        public override bool IsValid()
        {
            if (!Enabled) return true;
            return true;
        }

        internal Base.Vector3 GetOffset()
        {
            return new Base.Vector3(offset_x.value, offset_y.value, offset_z.value);
        }
    }
}
