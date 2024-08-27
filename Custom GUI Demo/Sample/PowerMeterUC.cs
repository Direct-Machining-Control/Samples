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
    public partial class PowerMeterUC : UserControl
    {
        public PowerMeterUC()
        {
            InitializeComponent();
            Base.SystemDevices.SettingsApplied += SystemDevices_SettingsApplied;
        }

        private void SystemDevices_SettingsApplied()
        {
            UpdatePowerMeterList();
        }

        private void UpdatePowerMeterList()
        {
            flowList.Controls.Clear();

            foreach (var device_settings in Base.Settings.PowerMeters)
            {
                if (!device_settings.Enabled) continue;
                flowList.Controls.Add(Create(device_settings));
            }
        }

        private Control Create(Base.IPowerMeterSettings device_settings)
        {
            Button button = new Button() { Enabled = false };
            button.Tag = device_settings;
            button.Width = 200;
            button.Text = device_settings.FriendlyName;
            button.FlatStyle = FlatStyle.Flat;
            return button;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateState();
        }

        private void UpdateState()
        {
            try
            {
                foreach (var input in flowList.Controls)
                {
                    Button btn = input as Button;
                    if (btn == null) continue;
                    if (btn.Tag == null) continue;
                    Base.IPowerMeterSettings pm_settings = btn.Tag as Base.IPowerMeterSettings;
                    if (pm_settings == null) continue;

                    double power = 0;
                    var device = pm_settings.GetPowerMeter();
                    if (device != null)
                    {
                        power = device.LastMeasured;
                    }

                    btn.Text = string.Format("{0}: {1:0.00}W", pm_settings.FriendlyName, power);
                }
            }
            catch (Exception) { }
        }
    }
}
