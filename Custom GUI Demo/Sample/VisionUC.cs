using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisionPlugin;

namespace Sample
{
    public partial class VisionUC : UserControl
    {
        public VisionUC()
        {
            InitializeComponent();
        }
        string command_name = "test";
        private void result_button_Click(object sender, EventArgs e)
        {
            logBox.AppendText(Helper.GetAlignmentResult($"{command_name}") + Environment.NewLine);
        }

        private void test_button(object sender, EventArgs e)
        {
            bool ok = Helper.RunAlignmentPattern($"{command_name}");
            logBox.AppendText($"Pattern started = {ok}" + Environment.NewLine);
        }

        private void edit_button_Click(object sender, EventArgs e)
        {
            Helper.EditAlignmentPattern($"{command_name}");
        }

        private void create_pattern_Click(object sender, EventArgs e)
        {
            Helper.SetAlignment("test");
            logBox.AppendText($"Alignment command added ({command_name})" + Environment.NewLine);
        }
        private void load_button_Click(object sender, EventArgs e)
        {
            bool ok = Helper.AlignmentLoadPreset("test", Base.Settings.PathParent + "Process\\MV\\" + "abc.mv");
            logBox.AppendText($"Preset loaded = {ok}" + Environment.NewLine);
        }
        private void save_button_Click(object sender, EventArgs e)
        {
            bool ok = Helper.AlignmentSavePreset("test", Base.Settings.PathParent + "Process\\MV\\" + "save_test.mv");
            logBox.AppendText($"Preset saved = {ok}" + Environment.NewLine);
        }

        private void get_value_Click(object sender, EventArgs e)
        {
            string parameter_name = "arc_min_radius";
            logBox.AppendText($"Parameter({parameter_name})={Helper.GetParameterValue(command_name, parameter_name)}" + Environment.NewLine);
        }
    }
}
