using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Base;

namespace StageSamplePlugin
{
    class Axis : IAxis, IAxisFreemove, IAxisError
    {
        IAxisSettings settings = null;
        AxisSettings axis_settings = null;
        double scale = 1;
        private bool enabled;
        private bool is_moving;
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
            is_moving = false;
        }

        public bool Move(double position, bool waitForMotionDone)
        {
            return Move(position, settings.DefaultSpeed, waitForMotionDone);
        }

        /// <summary>
        /// Move axis
        /// </summary>
        /// <param name="position">Position in units (mm or deg)</param>
        /// <param name="speed">Speed in units/s</param>
        /// <param name="waitForMotionDone">true if wait for motion done, false if don't wait for motion done</param>
        /// <returns>True if motion successful, false if error occured</returns>
        public bool Move(double position, double speed, bool waitForMotionDone)
        {
            if (!Enabled) return Functions.Error(this, "Axis '" + GetName() + "' not enabled");
            if (!IsPositionValid(position)) return false;
            if (!IsSpeedValid(speed)) return false;

            try
            {
                is_moving = true;
                // TODO: send position to controller


                if (waitForMotionDone) { WaitForMoveDone(); settings.ModeledPosition = position; last_position = position;}
            }
            catch (Exception ex) { return Functions.Error(this, "Unable to do motion. ", ex); }
            return true;
        }

        /// <summary>
        /// Wait for axis motion done can be called after Move function is called without waiting for motion to finish
        /// </summary>
        /// <returns>True if motion successful, false if error occured</returns>
        public bool WaitForMoveDone()
        {
            // TODO: wait until motion is done
            is_moving = false;
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
            catch (Exception ex) { Functions.Error(this, "Unable to get position. ", ex); return 0.0; }
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
            if (!CanHome) return Functions.Error(this, "Homing for axis is disabled in settings.");
            if (!Enabled) return Functions.Error(this, "Axis '" + GetName() + "' not enabled");

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
            if (!Enabled) return Functions.Error(this, "Axis '" + GetName() + "' not enabled");
            try
            {
                // TODO: start axis free move(JOG) at speed (positive if speed > 0, negative if speed < 0)
            }
            catch (Exception ex) { return Functions.Error(this, "Unable to start freemove. ", ex); }
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

        // is axis enabled (power to motor is on)
        public bool Enabled
        {
            get
            {
                if (!Plugin.plugin.IsConnected()) return false;
                return enabled;
            }
        }

        /// <summary>
        /// Get current axis state
        /// </summary>
        public AxisState GetAxisState()
        {
            AxisState state = AxisState.None;
            //if (IsError()) state |= AxisState.Error;
            if (is_moving) state |= AxisState.Moving;
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

        /// <summary>
        /// Is axis in error state
        /// </summary>
        /// <returns>True if axis in error</returns>
        bool IAxisError.IsError()
        {
            if (!Plugin.plugin.IsConnected()) return true;
            return false; // no error
        }

        /// <summary>
        /// Is axis in warning state
        /// </summary>
        /// <returns>True if axis in warning state</returns>
        bool IAxisError.IsWarning()
        {
            return false; // no warning
        }

        /// <summary>
        /// Is axis has option to clean error
        /// </summary>
        /// <returns>True is axis has option to clear or reset error</returns>
        bool IAxisError.IsCanClearError()
        {
            return true;
        }

        /// <summary>
        /// Clear/reset axis error
        /// </summary>
        /// <returns>True if error is reseted</returns>
        bool IAxisError.ClearError()
        {
            return true;
        }

        /// <summary>
        /// Get axis state information
        /// </summary>
        /// <returns>List with axis states(error messages)</returns>
        string[] IAxisError.GetInfoList()
        {
            if (!Plugin.plugin.IsConnected()) return new string[] { "No connection to controller" };

            return null;
        }
    }
}
