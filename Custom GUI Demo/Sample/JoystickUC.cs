using Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample
{
    public partial class JoystickUC : UserControl
    {
        bool isStepMode = false; // motion mode

        public JoystickUC()
        {
            InitializeComponent();


            buttonMode.Text = isStepMode ? "MODE STEP" : "MODE FREEMOVE";
            numStep.Enabled = isStepMode;

            Base.State.StateChangedEvent += State_StateChangedEvent;
            List<Button> buttons = new List<Button>() { buttonXnegative, buttonYnegative, buttonXpositive, buttonYpositive, buttonZnegative, buttonZpositive };

            foreach (Button button in buttons)
            {
                button.MouseDown += Button_MouseDown;
                button.MouseUp += Button_MouseUp;
                button.MouseLeave += Button_MouseLeave;
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            Jog(sender as Button, false);
        }

        private void Button_MouseUp(object sender, MouseEventArgs e)
        {
            Jog(sender as Button, false);
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            Jog(sender as Button, true);
        }

        /// <summary>
        /// Jog
        /// </summary>
        void Jog(Button button, bool start) {
            if (button == null || button.Tag == null) return;
            try {
                bool positive = button.Name.Contains("positive");
                IAxisSettings axisSettings = button.Tag as IAxisSettings;
                if (axisSettings == null || axisSettings.Axis == null) return;
                IAxis axis = axisSettings.Axis;
                IAxisFreemove axisFreemove = axisSettings.Axis as IAxisFreemove;

                if (axisFreemove != null && !isStepMode)
                {
                    // axis support freemove
                    if (start)
                    {
                        double dir = positive ? 1 : -1;
                        axisFreemove.StartFreemove(dir*Math.Min(axis.Settings.MaxSpeed, (double)numSpeed.Value));
                    }
                    else
                        axisFreemove.StopFreemove();
                }
                else {
                    // axis doesn't support freemove
                    if (start)
                    {
                        double position = axis.GetPosition();
                        if (positive) position = Math.Min(axisSettings.MaxPosition, position + (double)numStep.Value);
                        else position = Math.Max(axisSettings.MinPosition, position - (double)numStep.Value);

                        if (!axis.GetAxisState().HasFlag(AxisState.Moving))
                            axis.Move(position, (double)numSpeed.Value, false);
                    }
                    else
                    {

                    }
                }

            }catch (Exception ) { }
        }

        private void State_StateChangedEvent(Base.StateType new_state)
        {
            if (new_state == Base.StateType.ConnectedToHardware)
            {
                InitializeControls();
            }
        }

        public void InitializeControls()
        {
            buttonXpositive.Tag = Settings.StageX;
            buttonXnegative.Tag = Settings.StageX;

            buttonYpositive.Tag = Settings.StageY;
            buttonYnegative.Tag = Settings.StageY;

            buttonZpositive.Tag = Settings.StageZ;
            buttonZnegative.Tag = Settings.StageZ;

            flowAxes.Controls.Clear();

            foreach (var axis in Base.Settings.Axes)
            {
                if (!axis.Enabled) continue;

                AxisUC axisUC = new AxisUC() { AxisSettings = axis, Width = 500, Height = 30, Tag = axis, Margin = new Padding(3) };
                flowAxes.Controls.Add(axisUC);
            }
        }


        private void buttonMode_Click(object sender, EventArgs e)
        {
            isStepMode = !isStepMode;
            buttonMode.Text = isStepMode ? "MODE STEP" : "MODE FREEMOVE";
            numStep.Enabled = isStepMode;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try { } catch (Exception) { }
        }
    }
}
