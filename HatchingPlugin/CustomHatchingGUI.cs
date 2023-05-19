using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HatchingPlugin
{
    public partial class CustomHatchingGUI : UserControl
    {
        public CustomHatchingGUI()
        {
            InitializeComponent();
        }

        public void Set(CustomHatching customHatching)
        {
            Base.IDeviceSettingsGUI.Set(customHatching.parameters, this.Controls.GetEnumerator(), null);
        }
    }
}
