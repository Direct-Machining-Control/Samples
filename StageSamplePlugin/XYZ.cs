using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base;

namespace StageSamplePlugin
{
    /// <summary>
    /// XYZ class is used for making XY and XYZ sinchronous motion
    /// </summary>
    internal class XYZ:Base.IMotionDevice
    {
        bool is_connected = false;
        Axis axisX = null;
        Axis axisY = null;
        Axis axisZ = null;
        double last_x = 0, last_y = 0, last_z = 0; 

        bool is_laser_on = false;
        bool is_list_started = false;
        const int MAX_COMMANDS = 1000;
        int commands_in_list = 0;
        StringBuilder code = new StringBuilder(); // the code that will be send to controller
        MarkingParameters prm = new MarkingParameters();

        public XYZ() {
            // We might need to access axis settings or X,Y,Z axis directly
            if (Plugin.IsValidAxis(Base.Settings.StageX)) axisX = (Axis)Base.Settings.StageX.Axis; 
            if (Plugin.IsValidAxis(Base.Settings.StageY)) axisY = (Axis)Base.Settings.StageY.Axis;
            if (Plugin.IsValidAxis(Base.Settings.StageZ)) axisZ = (Axis)Base.Settings.StageZ.Axis;

            // Assign this device as default stage motion control device 
            // If user will select 'Stage' as Device in MARKING tab, this device will be called to perform trajectory
            SystemMotion.device_stage = this; 
            SystemMotion.Active.SelectDevice(this);
        }

        // Start command list buffer and collect all commands to it
        public override bool StartList()
        {
            is_list_started = true;
            code.Clear();
            commands_in_list = 0;
            return true;
        }

        public override bool EndList()
        {
            SetJumpParameters(); // laser off

            code.AppendLine("M02"); // TODO: 

            is_list_started = false;
            return true;
        }

        public override bool RunList()
        {
            if (commands_in_list < 1) return true;
            if (is_list_started) EndList();
            try
            {
                // TODO: send generated code to controller, run code, wait until the code is finished
                return true;
            }
            catch (Exception ex)
            {
                if (Base.State.is_cancel) return true;
                return Functions.Error("Unable to run command list!", ex);
            }
        }

        public override bool CanWorkWith(IAxisSettings axis_settings)
        {
            return axis_settings != null && axis_settings.Axis is Axis; // this XYZ can only move axis that depends for this controller
        }

        // New motion speeds, laser parameters, ...
        public override bool SetMarkingParameters(MarkingParameters markingParameters)
        {
            prm = markingParameters;
            return true;
        }

        private bool SetMarkParameters() {

            if (prm.is_triggering_on)
            {
                // TODO: add laser on code
                is_laser_on = true;
            }
            else
            {
                // TODO: add laser off code
                is_laser_on = false;
            }
            
            return true;
        }

        private bool SetJumpParameters() 
        {
            // TODO: add laser off code
            is_laser_on = false;

            return true;
        }

        // run list and start list again
        private void SplitCode()
        {
            RunList();
            StartList();
        }

        // Check if command buffer is not full. If almost full, split code(run)
        private void CheckToSplit(bool isJump)
        {
            if (!is_list_started) return;
            if (commands_in_list < MAX_COMMANDS*0.9) return;
            if (isJump) SplitCode();
            else if (commands_in_list > (MAX_COMMANDS-5)) SplitCode();
        }
        
        public override bool Mark(double x, double y)
        {
            if (!CanMove(true, true, false)) return false;
            if (!SetMarkParameters()) return false;
            commands_in_list++;
            
            G1(x, y, 0, prm.mark_speed, true, true, false);
            
            last_x = x; last_y = y;
            CheckToSplit(false);
            return true;
        }

        public override bool Mark(double x, double y, double z)
        {
            if (!CanMove(true, true, true)) return false;
            if (!SetMarkParameters()) return false;
            commands_in_list++;

            G1(x, y, z, prm.mark_speed, true, true, true);

            last_x = x; last_y = y; last_z = z;
            CheckToSplit(false);
            return true;
        }

        public override bool MarkArc(Vector3 center, Vector3 start_point, Vector3 end_point, double radius, double central_angle_deg, bool clockwise, double start_angle, double end_angle)
        {
            bool move_z = Math.Abs(start_point.z - end_point.z) > Settings.Epsilon;
            if (!CanMove(true, true, move_z)) return false;
            if (!SetMarkParameters()) return false;
            commands_in_list++;

            code.AppendLine("G02"); // TODO: 

            last_x = end_point.x; last_y = end_point.y; last_z = end_point.z;
            CheckToSplit(false);
            return true;
        }

        public override bool MarkCircle(Vector3 center, Vector3 startPoint, double radius, bool clockwise, uint times_to_repeat)
        {
            if (!CanMove(true, true, false)) return false;
            double start_angle = Geometry.AngleAt(center, startPoint.x, startPoint.y);
            if (!SetMarkParameters()) return false;
            commands_in_list++;

            code.AppendLine("G02"); // TODO: 

            CheckToSplit(false); 
            return true;
        }

        public override bool Jump(double x, double y)
        {
            if (!CanMove(true, true, false)) return false;
            if (!SetJumpParameters()) return false;
            commands_in_list++;
            
            G0(x, y, 0, prm.jump_speed_x, prm.jump_speed_y, prm.jump_speed_z, true, true, false);

            Delay((int)Settings.General.DelayAfterJump);
            last_x = x; last_y = y;
            CheckToSplit(true); 
            return true;
        }

        public override bool Jump(double x, double y, double z)
        {
            if (!CanMove(true, true, true)) return false;
            if (!SetJumpParameters()) return false;
            commands_in_list++;
            
            G0(x, y, z, prm.jump_speed_x, prm.jump_speed_y, prm.jump_speed_z, true, true, true);

            Delay((int)Settings.General.DelayAfterJump);
            last_x = x; last_y = y; last_z = z;
            CheckToSplit(true); 
            return true;
        }

        public override bool Jump(double position, IAxisSettings axis_settings)
        {
            if (!(axis_settings.Axis is Axis))
            {
                SplitCode();
                if (!axis_settings.Axis.Move(position, true)) return false;
            }

            if (!SetJumpParameters()) return false;

            bool is_x = axis_settings == Settings.StageX;
            bool is_y = axis_settings == Settings.StageY;
            bool is_z = axis_settings == Settings.StageZ;
            commands_in_list++;

            // TODO: move one axis

            Delay((int)Settings.General.DelayAfterJump);
    
            if (is_x) last_x = position; 
            if (is_y) last_y = position; 
            if (is_z) last_z = position;

            CheckToSplit(true); 
            return true;
        }

        public override bool Home()
        {
            if (SystemMotion.device_stage != this) return true;
            if (!State.IsCancel && axisX != null) axisX.Home();
            if (!State.IsCancel && axisY != null) axisY.Home();
            if (!State.IsCancel && axisZ != null) axisZ.Home();
            return base.Home();
        }

        private void Delay(int delay_ms) {
            if (delay_ms > 0)
            {
                commands_in_list++;
                code.AppendLine("G04"); // TODO: 
            }
        }

        private void G1(double x, double y, double z, double speed, bool move_x, bool move_y, bool move_z)
        {
            code.AppendLine("G01"); // TODO: 
        }

        private void G0(double x, double y, double z, double speed_x, double speed_y, double speed_z, bool move_x, bool move_y, bool move_z)
        {
            code.AppendLine("G00"); // TODO: 
        }

        public override bool CanMoveXY()
        {
            return CanMove(true, true, false);
        }

        public override bool CanMoveXYZ()
        {
            return CanMove(true, true, true);
        }

        public override bool Delay(double time_in_s)
        {
            Delay((int)(time_in_s * 1000)); return true;
        }

        public override bool FireContinuous(bool on, uint frequency_divider = 1)
        {
            // TODO: turn laser signal on/off
            if (is_list_started)
            {
                // generate laser on/off code
            }
            else 
            {
                // turn on/off laser now
            }
            return true;
        }

        public override bool FirePulse(ulong pulse_count)
        {
            // TODO: fire 'pulse_count' laser pulses
            if (is_list_started)
            {
                // generate laser pulses code
            }
            else
            {
                // turn on/off laser now
            }
            return true;
        }

        private bool CanMove(bool move_x, bool move_y, bool move_z)
        {
            if (move_x)
            {
                if (axisX == null) return Functions.Error("Axis X is not available!");
                if (!axisX.Enabled) return Functions.Error("Axis X is not enabled!");
            }
            if (move_y)
            {
                if (axisY == null) return Functions.Error("Axis Y is not available!");
                if (!axisY.Enabled) return Functions.Error("Axis Y is not enabled!");
            }
            if (move_z)
            {
                if (axisZ == null) return Functions.Error("Axis Z is not available!");
                if (!axisZ.Enabled) return Functions.Error("Axis Z is not enabled!");
            }
            return true;
        }
    }
}
