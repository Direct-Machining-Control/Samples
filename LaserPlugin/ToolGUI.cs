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
    public partial class ToolGUI : UserControl
    {
        public ToolGUI()
        {
            InitializeComponent();
        }

        private void buttonLaserOn_Click(object sender, EventArgs e)
        {
            //Plugin.plugin.dev.SetLD(true);
        }

        private void buttonLaserOff_Click(object sender, EventArgs e)
        {
            //Plugin.plugin.dev.SetLD(false);
        }
    }
}
