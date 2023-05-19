using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LaserPlugin
{
    public partial class SettingsGUI : UserControl
    {
        Settings settings;

        public SettingsGUI()
        {
            InitializeComponent();
        }

        internal void Set(Settings settings)
        {
            this.settings = settings;
            settings.port.list = System.IO.Ports.SerialPort.GetPortNames();
            Base.IDeviceSettingsGUI.Set(settings.Parameters, Controls.GetEnumerator(), null);
            flow.Enabled = enabled.Value;
        }

        private void enabled_ParameterFieldValueChanged(object sender, Base.IParameter param)
        {
            flow.Enabled = settings.Enabled;
        }
    }
}
