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
    public partial class AxisGUI : UserControl
    {
        AxisSettings settings;

        public AxisGUI(AxisSettings settings)
        {
            InitializeComponent();
            this.settings = settings;
            IDeviceSettingsGUI.Set(settings.parameters, this.Controls.GetEnumerator(), null);
            toolTip1.SetToolTip(axis_index, "Axis ID in controller");
        }
    }
}
