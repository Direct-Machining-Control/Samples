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
    public partial class CommandGUI : Core.ICommandGUI
    {
        public CommandGUI()
        {
            InitializeComponent();
        }

        internal void SetGUI(Command command)
        {
            Set(this.Controls.GetEnumerator(), command.Parameters, toolTip);
            ld_on.Enabled = set_ld.Value;
            shutter_open.Enabled = set_shutter.Value;

            pp_frequency.Enabled = set_pp_frequency.Value;
            mod_frequency.Enabled = set_mod_frequency.Value;
            mod_efficiency.Enabled = set_mod_efficiency.Value;
        }

        private void set_ld_ParameterFieldValueChanged(object sender, Base.IParameter param)
        {
            ld_on.Enabled = set_ld.Value;
        }

        private void set_shutter_ParameterFieldValueChanged(object sender, Base.IParameter param)
        {
            shutter_open.Enabled = set_shutter.Value;
        }

        private void set_pp_frequency_ParameterFieldValueChanged(object sender, Base.IParameter param)
        {
            pp_frequency.Enabled = set_pp_frequency.Value;
        }

        private void set_mod_frequency_ParameterFieldValueChanged(object sender, Base.IParameter param)
        {
            mod_frequency.Enabled = set_mod_frequency.Value;
        }

        private void set_mod_efficiency_ParameterFieldValueChanged(object sender, Base.IParameter param)
        {
            mod_efficiency.Enabled = set_mod_efficiency.Value;
        }
    }
}
