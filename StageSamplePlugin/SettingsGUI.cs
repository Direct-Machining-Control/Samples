using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Base;

namespace StageSamplePlugin
{
    public partial class SettingsGUI : IDeviceSettingsGUI
    {
        public SettingsGUI()
        {
            InitializeComponent();
        }

        internal void Set(ControllerSettings settings)
        {
            Set(settings.Parameters, this.Controls.GetEnumerator(), toolTip1);
            flow.Enabled = enabled.Value;
        }

        private void enabled_ParameterFieldValueChanged(object sender, IParameter param)
        {
            flow.Enabled = enabled.Value;
        }


    }
}
