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

namespace JoinAndHatchPlugin
{
    public partial class JoinAndHatchPluginSettingsGUI : IDeviceSettingsGUI
    {
        public JoinAndHatchPluginSettingsGUI()
        {
            InitializeComponent();
        }

        internal void Set(JoinAndHatchPluginSettings settings)
        {
            Set(settings.Parameters, this.Controls.GetEnumerator(), toolTip1);
        }
    }
}
