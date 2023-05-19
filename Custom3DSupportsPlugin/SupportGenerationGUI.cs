using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Custom3DSupportsPlugin
{
    public partial class SupportGenerationGUI : UserControl
    {
        public SupportGenerationGUI()
        {
            InitializeComponent();
        }



        internal void Set(CustomSupportGeneration supportGeneration)
        {
            Base.IDeviceSettingsGUI.Set(supportGeneration.parameters, this.Controls.GetEnumerator(), null);
        }
    }
}
