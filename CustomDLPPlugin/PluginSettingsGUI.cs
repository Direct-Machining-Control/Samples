using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Base;

namespace CustomDLPPlugin
{
    public partial class CustomDLPPluginSettingsGUI : IDeviceSettingsGUI
    {
        public CustomDLPPluginSettingsGUI()
        {
            InitializeComponent();
        }

        CustomDLPPluginSettings settings;
        internal void Set(CustomDLPPluginSettings settings)
        {
            this.settings = settings;
            Set(settings.Parameters, this.Controls.GetEnumerator(), toolTip1);
            flowLayoutPanel2.Enabled = settings.Enabled;

        }

        private void enabled_ParameterFieldValueChanged(object sender, IParameter param)
        {
            flowLayoutPanel2.Enabled = settings.Enabled;
        }
    }
}
