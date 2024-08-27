using Base;
using DMC;
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
    public partial class InputOutputUC : UserControl
    {
        public InputOutputUC()
        {
            InitializeComponent();
            Base.SystemDevices.SettingsApplied += SystemDevices_SettingsApplied;
        }

        private void SystemDevices_SettingsApplied()
        {
            UpdateIOList();
        }

        private void UpdateIOList()
        {
            flowOutputs.Controls.Clear();
            flowInputs.Controls.Clear();

            foreach (var io in Base.Settings.IOTools.list)
            {
                if (!io.DisplayInJoystick && !io.DisplayInRibbon) continue;
                if (io.IsInput)
                {
                    flowInputs.Controls.Add(CreateInput(io));
                }
                else
                {
                    flowOutputs.Controls.Add(CreateOutput(io));
                }
            }
        }

        private Control CreateInput(IOTool io)
        {
            Button button = new Button() { Enabled = false };
            button.Tag = io;
            button.Width = 200;
            button.Text = io.text_on_high.Value;
            button.FlatStyle = FlatStyle.Flat;
            return button;
        }

        private Control CreateOutput(IOTool io)
        {
            Button button = new Button() { Enabled = true };
            button.Click += Button_Click;
            button.Tag = io;
            button.Width = 200;
            button.Text = io.text_on_high.Value;
            return button;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                if (btn == null) return;
                if (!Base.State.is_connected_to_hardware) return;
                if (btn.Tag == null) return;
                IOTool tool = btn.Tag as IOTool;
                if (tool == null) return;

                if (!tool.Run()) ShowError();

            }
            catch (Exception) { }
        }

        private void ShowError()
        {
            var msg = Functions.GetLastErrorMessage();
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateInputState();
            UpdateOutputState();
        }

        private void UpdateInputState()
        {
            try
            {
                foreach (var input in flowInputs.Controls)
                {
                    Button btn = input as Button;
                    if (btn == null) continue;
                    if (btn.Tag == null) continue;
                    IOTool tool = btn.Tag as IOTool;
                    if (tool == null) continue;

                    bool is_high = State.is_connected_to_hardware ? tool.IsDigitalInputHigh() : false;
                    btn.Text = is_high ? tool.text_on_high.Value : tool.text_on_low.Value;
                    btn.BackColor = Color.FromArgb(is_high ? tool.color_on_high.value : tool.color_on_low.value);
                }
            }
            catch (Exception) { }
        }

        private void UpdateOutputState()
        {
            try
            {
                foreach (var input in flowOutputs.Controls)
                {
                    Button btn = input as Button;
                    if (btn == null) continue;
                    btn.Enabled = Base.State.is_connected_to_hardware;

                    if (btn.Tag == null) continue;
                    IOTool tool = btn.Tag as IOTool;
                    if (tool == null) continue;

                    btn.Text = (Base.State.is_connected_to_hardware && tool.IsDigitalOutputHigh()) ? tool.text_on_high.Value : tool.text_on_low.Value;

                }
            }
            catch (Exception) { }
        }
    }
}
