using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Base;

namespace StageSamplePlugin
{
    class Axis : IAxis, IAxisFreemove
    {
        IAxisSettings settings = null;
        AxisSettings axis_settings = null;
        double scale = 1;
        private bool enabled;
        double last_position; // last position taken from controller

        public Axis(IAxisSettings settings, AxisSettings axis_settings)
        {
            //this.controller = controller;
            this.axis_settings = axis_settings;
            enabled = true;
            //SetName(settings);
            scale = settings.InvertDirection ? -1 : 1;
            this.settings = settings;

            settings.Axis = this;
            //axis_index = axis_settings.axis_index.value;

        }

        public bool Move(double position, bool waitForMotionDone)
        {
            return Move(position, settings.DefaultSpeed, waitForMotionDone);
        }

        /// <param name="position">Absolute position in mm</param>
        /// <param name="speed">Speed in mm/s</param>
        /// <returns>True if motion is successful</returns>
        public bool Move(double position, double speed, bool waitForMotionDone)
        {
            if (!Enabled) return Functions.Error("Axis '" + GetName() + "' not enabled");
            if (!IsPositionValid(position)) return false;
            if (!IsSpeedValid(speed)) return false;

            try
            {
                // TODO: send position to controller
                

                if (waitForMotionDone) { WaitForMoveDone(); settings.ModeledPosition = position; last_position = position;}
            }
            catch (Exception ex) { return Functions.Error("Unable to do motion. ", ex); }
            return true;
        }

        public bool WaitForMoveDone()
        {
            // TODO: wait until motion is done
            return true;
        }

        private double GetPosition(bool update_from_controller)
        {
            if (!update_from_controller) return last_position;
            try
            {
                // TODO: update last_position variable with current axis position

                return last_position;
            }
            catch (Exception ex) { Functions.Error("Unable to get position. ", ex); return 0.0; }
        }

        /// <summary>
        /// This method might be called in 20ms intervals!!!
        /// </summary>
        /// <returns>Absolute axis position in mm</returns>
        public double GetPosition() { return GetPosition(false); }

        public void UpdateState()
        {
            // TODO: might need to sinchronize threads
            GetPosition(true);

            // check other states
        }

        bool is_homing = false;

        public bool IsHoming { get { return is_homing; } }
        public bool SetAcceleration(double acceleration_mm_per_ss) { return true; }

        public bool Home()
        {
            if (!CanHome) return Functions.Error("Homing for axis is disabled in settings.");
            if (!Enabled) return Functions.Error("Axis '" + GetName() + "' not enabled");

            // TODO: home the axis
            is_homing = true;
            System.Threading.Thread.Sleep(1000);
            is_homing = false;

            return true;
        }

        private bool EnableDisable(bool enable)
        {
            if (!CanDisableEnable) return true;

            try
            {
                // TODO: enable/disable the axis

                enabled = enable;
                return true;
            }
            catch (Exception) { }
            return false;
        }

        public void Stop()
        {
            try
            {
                // TODO: stop any axis motion
            }
            catch (Exception) { }
        }


        public bool StartFreemove(double speed)
        {
            if (!Enabled) return Functions.Error("Axis '" + GetName() + "' not enabled");
            try
            {
                // TODO: start axis free move(JOG) at speed (positive if speed > 0, negative if speed < 0)
            }
            catch (Exception ex) { return Functions.Error("Unable to start freemove. "); }
            return true;
        }

        public void StopFreemove()
        {
            if (!Enabled) return;
            try
            {
                // TODO: stop axis free move(JOG) 
            }
            catch (Exception) { }
        }

        // is axis enabled (power is on)
        public bool Enabled
        {
            get
            {
                if (!Plugin.plugin.IsConnected()) return false;
                return enabled;
            }
        }
        public AxisState GetAxisState()
        {
            AxisState state = AxisState.None;
            //if (IsError()) state |= AxisState.Error;
            //if (IsHomed) state |= AxisState.Homed;
            if (Enabled) state |= AxisState.Enabled;
            return state;
        }

        double currentModeledSpeed = 0;
        public double CurrentModeledSpeed
        {
            set { currentModeledSpeed = value; }
            get { return currentModeledSpeed; }
        }
        public bool CanHome 
        {
            get { if (settings == null) return false; return settings.CanHome; }
        }

        public bool IsPositionValid(double position) { return settings.IsPositionValid(position); }
        public bool IsSpeedValid(double speed) { return settings.IsSpeedValid(speed); }
        //public string GetLetter() { return settings.Letter; }
        public bool IsRotationAxis { get { return settings.IsRotary; } }
        public bool HasSpeedControl() { return true; }
        public bool Enable() { return EnableDisable(true); }
        public bool Disable() { return EnableDisable(false); }
        public bool IsDominant() { return settings.IsDominant; }
        public bool CanDisableEnable { get { return true; } }
        public string GetErrorMessage() { return Functions.GetLastErrorMessage(); }
        public IAxisSettings Settings { get { return settings; } }
        public string GetUnits() { return (settings.IsRotary ? "deg" : "mm"); }
        public string GetName() { return settings.GetFullName(); }
    }
}
