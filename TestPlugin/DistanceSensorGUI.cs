using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Base;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TestPlugin
{
    public partial class DistanceSensorGUI : UserControl
    {
        public DistanceSensorGUI()
        {
            InitializeComponent();
            AxisList = new List<IAxisSettings>() { Settings.StageX, Settings.StageY, Settings.StageZ };
        }

        List<IAxisSettings> AxisList = new List<IAxisSettings>();

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (DistanceSensorPlugin.plugin != null && DistanceSensorPlugin.plugin.device != null)
                {
                    DistanceSensor sensor = DistanceSensorPlugin.plugin.device;
                    sensor.ReadPosition(Base.Settings.StageZ.Axis == null ? 0 : Base.Settings.StageZ.Axis.GetPosition());
                    label1.Text = string.Format("{0}: {1:0.###} mm", sensor.GetName(), sensor.GetPosition());
                }
            }
            catch (Exception) { }
        }

        async Task<RichStatus> DoMotion()
        {
            List<Task<RichStatus>> TasksToWait = new List<Task<RichStatus>>();
            TasksToWait.Add(MovingAxisAsync(Axis.X, 0, 2));
            TasksToWait.Add(MovingAxisAsync(Axis.Y, 10, 2));
            await Task.WhenAll(TasksToWait);
            foreach (Task<RichStatus> Tsk in TasksToWait)
            {
                if (!Tsk.Result.Success)
                {
                    return Tsk.Result;
                }
            }

            if (Settings.StageX.Axis.GetPosition() > Settings.Epsilon)
            {
                Functions.Action(this, "Wrong X position");
                Base.StatusBar.Set("Wrong X position", true, Base.StatusBar.MessageType.Error);
                return new RichStatus(false);
            }
            Task<RichStatus> s2 = MovingAxisAsync(Axis.Z, -19, 2);

            TasksToWait.Clear();
            TasksToWait.Add(MovingAxisAsync(Axis.X, 10, 20));
            TasksToWait.Add(MovingAxisAsync(Axis.Y, 0, 20));
            TasksToWait.Add(MovingAxisAsync(Axis.Z, 0, 20));
            await Task.WhenAll(TasksToWait);

            return new RichStatus(true);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                Task<RichStatus> s = DoMotion();
                //await s;// Task.WhenAll(TasksToWait);
            }
            
        }

        enum Axis
        {
            X = 0,
            Y = 1,
            Z = 2
        }

        int nn = 0;

        private async Task<RichStatus> MovingAxisAsync(Axis Ax, double Position, double Speed)
        {
            RichStatus Result = new RichStatus(true);

            if (!Base.State.is_connected_to_hardware)
            {
                Result = new RichStatus(false, 0, "not connected to hardware");
            }
            if ((AxisList[(int)Ax].Axis.GetAxisState() & Base.AxisState.Error) > 0)
            {
                Result = new RichStatus(false, 0, "axis in error");
            }

            if ((AxisList[(int)Ax].Axis.GetAxisState() & Base.AxisState.Enabled) == 0)
            {
                Result = new RichStatus(false, 0, "axis not enabled");
            }

            
            if ((AxisList[(int)Ax].Axis.GetAxisState() & Base.AxisState.Homed) == 0)
            {
                Result = new RichStatus(false, 0, "axis in warning (not homed)");
            }

            Functions.Action(this, "Move axis. ");
            using (LockDMC.Lock())
            {

                Functions.Action(this, "Try throw ex. ");
                if (nn++ > 100) throw new Exception("Test exc");
                if (!AxisList[(int)Ax].Axis.Move(Position, Speed, false))
                {
                    Result = new RichStatus(false, 0, "Axis move command failed");
                }
            }

            System.Diagnostics.Stopwatch TimerStartMove = new System.Diagnostics.Stopwatch();
            TimerStartMove.Restart();

            Functions.Action(this, string.Format("Wait for axis '{0}' motion started. ", Ax));
            while (((AxisList[(int)Ax].Axis.GetAxisState() & Base.AxisState.Moving) == 0) &&
                    (TimerStartMove.Elapsed.TotalMilliseconds < 5000))
            {
                await Task.Delay(50);
            }

            Functions.Action(this, string.Format("Axis '{0}' is moving. Wait for axis motion end. ", Ax));
            while ((AxisList[(int)Ax].Axis.GetAxisState() & Base.AxisState.Moving) > 0)
            {
                await Task.Delay(50);
            }


            Functions.Action(this, string.Format("Axis '{0}' motion ended. ", Ax));
            if (((AxisList[(int)Ax].Axis.GetAxisState() & Base.AxisState.Error) > 0))
            {
                Result = new RichStatus(false, 0, "Axis move failed");
            }

            return Result;
        }



        class LockDMC : IDisposable
        {
            public static LockDMC Lock() { return new LockDMC(); }
            public void Dispose()
            {
                // The implementation of this method not described here.
                // ... For now, just report the call.
                Console.WriteLine(0);
            }
        }

        private void AsynProc22()
        {
            Thread.Sleep(500);
            Functions.Action(this, "New test StageZ to 10");
            
            var a = Base.Settings.StageZ.Axis;
            a.Move(10, 1, false);

            Stopwatch WaitMoveStarted = new Stopwatch();
            WaitMoveStarted.Restart();

            while (WaitMoveStarted.ElapsedMilliseconds < 1000)
            {
                bool is_moving = (a.GetAxisState() & Base.AxisState.Moving) > 0;
                Functions.Action(this, is_moving ? "Axis Z is moving" : "Axis Z is not moving");
                if (is_moving) break;
                Thread.Sleep(100);
            }

            while ((a.GetAxisState() & Base.AxisState.Moving) > 0)
            {
                Functions.Action(this, "Axis Z is moving");
                Thread.Sleep(100);
            }

            if (((a.GetAxisState() & Base.AxisState.Error) > 0))
            {
                Functions.Action(this, "Axis Z move failed");
            }
            Functions.Action(this, "New test end");
        }




        private void AsynProc2()
        {
            Thread.Sleep(500);
            Functions.Action(this, "A5 StageZ to 10");
            Base.Settings.StageZ.Axis.Move(10, 5, false);

        }

        private void AsynProcA1()
        {
            Hardware.Actions.JumpTo(Hardware.PositioningDevice.Stage1, new Vector3(10, 10, 10), true);
            Functions.Action(this, "StageXYZ move done");
        }

        private void AsynProcB1()
        {
            Functions.Action(this, "StageX2 to -10 at 2mm/s");
            Base.Settings.StageX2.Axis.Move(-10, 2, false);
            Functions.Action(this, "Wait for X2 motion done");
            Base.Settings.StageX2.Axis.WaitForMoveDone();
            Functions.Action(this, "X2 motion done");

            return;

            Functions.Action(this, "StageX2 to 10 at 2mm/s");
            Base.Settings.StageX2.Axis.Move(10, 2, false);

            Functions.Action(this, "Wait for X2 motion done");
            Base.Settings.StageX2.Axis.WaitForMoveDone();
            Functions.Action(this, "X2 motion done");
        }

        object obj = new object();
        static bool waiting = false;

        private void Sync()
        {
            if (waiting) { waiting = false; return; }
            else {
                waiting = true;
                while (waiting && !State.is_exit) Thread.Sleep(1);
            }
        }


        private void AsynProcA()
        {
            Functions.Action(this, "A1 Asyn Move XA to -76");
            Base.Settings.StageX.Axis.Move(-76, 20, false);
            Base.Settings.StageX.Axis.WaitForMoveDone();

            Functions.Action(this, "A2 Asyn Move XA to 76");
            Base.Settings.StageX.Axis.Move(76, 20, false);
            Base.Settings.StageX.Axis.WaitForMoveDone();

            Functions.Action(this, "A3 Asyn Move XA to 0");
            Base.Settings.StageX.Axis.Move(0, 20, false);
            Base.Settings.StageX.Axis.WaitForMoveDone();

            Functions.Action(this, "A4 SyncA");
            Sync();

            Functions.Action(this, "A5 StageZ to 10");
            Base.Settings.StageZ.Axis.Move(10, 5, true);

            Functions.Action(this, "A6 StageXYZ to 20;20;20");
            Hardware.Actions.JumpTo(Hardware.PositioningDevice.Stage1, new Vector3(20, 20, 20), true);
        }

        private void AsynProcB()
        {
            Functions.Action(this, "B1 Sync Move Z to 10");
            Base.Settings.StageZ.Axis.Move(10, 5, true);

            Functions.Action(this, "B2 StageX2YZ to 20;20;20");
            Hardware.Actions.JumpTo(Hardware.PositioningDevice.Stage2, new Vector3(20, 20, 20), true);


            Functions.Action(this, "B3 SyncB");
            Sync();
            Functions.Action(this, "B4 Async Move XB to -76");
            Base.Settings.StageX2.Axis.Move(-76, 20, false);
            Base.Settings.StageX2.Axis.WaitForMoveDone();

            Functions.Action(this, "B5 Async Move XB to 76");
            Base.Settings.StageX2.Axis.Move(76, 20, false);
            Base.Settings.StageX2.Axis.WaitForMoveDone();

            Functions.Action(this, "B6 Async Move XB to 0");
            Base.Settings.StageX2.Axis.Move(0, 10, false);
            Base.Settings.StageX2.Axis.WaitForMoveDone();


        }
    }

    internal class RichStatus
    {
        public bool Success;
        private int v2;
        private string v3;

        public RichStatus(bool Success)
        {
            this.Success = Success;
        }

        public RichStatus(bool Success, int v2, string v3)
        {
            this.Success = Success;
            this.v2 = v2;
            this.v3 = v3;
        }
    }
}
