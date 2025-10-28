using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Base;

namespace StageSamplePlugin
{
    public class Plugin : Base.IDeviceEx
    {
        internal static Plugin plugin = null;
        internal ControllerSettings settings = new ControllerSettings();
        internal IMotionDevice xyz;

        bool is_connected = false;
        Thread monitor_thread = null;

        public Plugin() { plugin = this; }

        public bool Connect()
        {
            try
            {
                if (!IsEnabled()) { if (IsConnected()) Disconnect(); return true; }
                if (is_connected) Disconnect();

                is_connected = false;

                // TODO: implement connect method to controller(s)

                try
                {
                    is_connected = true;

                    InitAxes();
                    if (!EnableAxes()) { Disconnect(); return false; }


                    // We can move XY axes at the same time if controller is the same
                    if (IsValidAxis(Settings.StageX) && IsValidAxis(Settings.StageY)) 
                    {
                        // create XYZ motion 
                        bool isSimplePointToPointMotion = true;
                        if (isSimplePointToPointMotion)
                            xyz = new GeneralXYZ(
                                IsValidAxis(Settings.StageX) ? Settings.StageX : null,
                                IsValidAxis(Settings.StageY) ? Settings.StageY : null,
                                IsValidAxis(Settings.StageZ) ? Settings.StageZ : null); // use general implementation where we just need to move from one point to another
                        else
                            xyz = new XYZ(); // use sophisticated motion where we can move any trajectory even with triggering
                    }

                    // used for monitoring axes positions
                    // this can be removed and axis position assigned in Axis.Move method
                    monitor_thread = new Thread(new ThreadStart(Monitor));
                    monitor_thread.Start(); 

                }
                catch (Exception ex2)
                {
                    Disconnect();
                    return Base.Err.CanNotConnect(this, GetName(), null, ex2);
                }
                return is_connected;
            }
            catch (Exception ex)
            {
                Disconnect();
                return Base.Err.CanNotConnect(this, GetName(), null, ex);
            }
        }

        public void Disconnect()
        {
            if (is_connected)
            {
                is_connected = false;
                try
                {
                    // TODO: disconnect from controller 
                }
                catch (Exception ex) { Functions.Error(this, "Unable to disconnect from " + GetName(), ex); }
                try
                {
                    if (monitor_thread != null && !monitor_thread.Join(1000)) monitor_thread.Abort();
                    monitor_thread = null;
                }
                catch (Exception) { }
            }
        }

        // Stop running program and any motion
        public void Stop()
        {
            if (!IsConnected()) return;
            try
            {
                // TODO: Stop running program and any motion
            }
            catch (Exception) { }
        }

        public bool CanDoHoming() { return true; }

        public bool Home()
        {
            if (!is_connected) return Functions.Error(GetName() + " not initialized");
            try
            {

            }
            catch (Exception ex) { return Functions.Error(this, "Unable to home " + GetName(), ex); }
            return true;
        }

        // Monitors positions, states, ...
        void Monitor()
        {
            while (IsConnected() && !State.is_exit)
            {
                UpdatatePositions();
                Thread.Sleep(500); 
            }
        }

        // Might be called once after Connect method and later positions updated after Move method
        void UpdatatePositions()
        {
            for (int i = 0; i < Settings.Axes.Count; i++)
            {
                if (!IsValidAxis(Settings.Axes[i])) continue;
                Axis axis = (Axis)Settings.Axes[i].Axis;
                axis.UpdateState();
            }
            if (IsValidAxis(Base.Settings.StageX)) State.current_stage_x = Base.Settings.StageX.Axis.GetPosition();
            if (IsValidAxis(Base.Settings.StageY)) State.current_stage_y = Base.Settings.StageY.Axis.GetPosition();
            if (IsValidAxis(Base.Settings.StageZ)) State.current_stage_z = Base.Settings.StageZ.Axis.GetPosition();
        }







        // Is iAxisSettings is enabled in settings and iAxisSettings is My Controller axis
        internal static bool IsValidAxis(IAxisSettings iAxisSettings)
        {
            return iAxisSettings.Enabled && iAxisSettings.Axis != null && iAxisSettings.Axis is Axis;
        }

        // Create(assign) all enabled(in settings) axes
        private void InitAxes()
        {
            for (int i = 0; i < settings.axes.Count; i++)
            {
                AxisSettings axis_settings = settings.axes[i];
                if (!axis_settings.stage.Enabled || axis_settings.stage.ControllerName != AxisSettings.controller_name) continue;
                axis_settings.stage.Axis = new Axis(axis_settings.stage, axis_settings);
            }
        }

        // Enable all XPS axes
        private bool EnableAxes()
        {
            for (int i = 0; i < Settings.Axes.Count; i++)
            {
                IAxisSettings axis_settings = Settings.Axes[i];
                if (!IsValidAxis(axis_settings)) continue;
                if (!axis_settings.Axis.Enable()) return false;
            }
            return true;
        }

        // Apply settings (settings might be changed by the user)
        public bool ApplySettings()
        {
            if (IsConnected())
            {
                Disconnect();
                Connect();
            }
            return true;
        }

        
        public IDeviceSettings GetSettings() { return settings; }
        public string GetName() { return settings.FriendlyName; }
        public bool IsConnected() { return is_connected; }
        public bool IsEnabled() { return settings.Enabled; }
        public string GetErrorMessage() { return Functions.GetLastErrorMessage(); }
        public bool OnRecipeStart() { return true; }
        public void OnRecipeFinish() { }

    }
}
