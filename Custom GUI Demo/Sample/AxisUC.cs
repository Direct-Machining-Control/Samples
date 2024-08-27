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
    public partial class AxisUC : UserControl
    {
        public AxisUC()
        {
            InitializeComponent();
        }

        public IAxisSettings AxisSettings { get; set; } = null;
        IAxis Axis { get { return AxisSettings == null ? null : AxisSettings.Axis; } }

        public void UpdateStatus()
        {
            var axisSettings = AxisSettings;

            if (axisSettings == null) return;
            IAxis axis = axisSettings.Axis;
            if (axis == null) return;
            AxisState axis_state = axis.GetAxisState();

            // set position
            labelPosition.Text = String.Format("{0}       {1:0.###}{2}", axisSettings.GetName(), axis.GetPosition(), axisSettings.IsRotary ? "deg" : "mm");

            // set enabled/disabled
            buttonEnable.Text = axis.Enabled ? "Disable" : "Enable";

            // Set status text based on axis status
            bool is_axis_error = false;
            if (axis_state.HasFlag(AxisState.Error))
            {
                IAxisError error_info = axis as IAxisError;
                string error_text = "";
                if (error_info != null)
                {
                    is_axis_error = error_info.IsError();
                    var info = error_info.GetInfoList(); // information about errors and warnings
                    if (info != null) error_text = string.Join("\t", info);
                }
                labelStatus.Text = "Error: "+ error_text;
            }
            else if (axis_state.HasFlag(AxisState.Moving))
                labelStatus.Text = "Axis Moving";
            else if (!axis_state.HasFlag(AxisState.Enabled))
                labelStatus.Text = "Axis Disabled";
            else
                labelStatus.Text = "Axis OK";


            buttonHomeReset.Text = is_axis_error ? "Clear" : "Home";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void buttonEnable_Click(object sender, EventArgs e)
        {
            IAxis axis = Axis;
            if (axis == null) return;

            if (axis == null) return;
            if (!axis.CanDisableEnable || !State.is_connected_to_hardware) return;

            bool disable = axis.Enabled;
            Functions.TopMostForm(false);
            string message = disable ?
                MultiLang.Format("Axis '{0}' will be disabled. ", axis.GetName()) :
                MultiLang.Format("Axis '{0}' will be enabled. ", axis.GetName());
            Base.StatusBar.ResetWarningError();
            Base.StatusBar.Set("", false);

            if (MessageBox.Show(message, MultiLang.Translate("Message"), MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    bool ok = true;
                    if (disable) ok = axis.Disable();
                    else ok = axis.Enable();
                    if (!ok) Base.Functions.ShowLastError();
                }
                catch (Exception ex)
                {
                    Err.Axis(axis, disable ? Err.AxisError.Disable : Err.AxisError.Enable, ex); Functions.ShowLastError();
                }
            }
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            try
            {
                IAxis axis = Axis;
                if (axis == null) return;

                IAxisError error_info = axis as IAxisError;
                if (error_info != null && error_info.IsError())
                {
                    // Clear error
                    if (!error_info.ClearError())
                        Functions.ShowLastError();
                }
                else
                {
                    // Home axis
                    if (!Hardware.Actions.HomeAxis(true, axis))
                        Base.Functions.ShowLastError();
                }
            }catch (Exception) { }
        }
    }
}
