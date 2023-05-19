namespace LaserPlugin
{
    partial class CommandGUI
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.set_ld = new Base.CheckBoolParameterField();
            this.ld_on = new Base.GUI.BoolParameterField();
            this.set_shutter = new Base.CheckBoolParameterField();
            this.shutter_open = new Base.GUI.BoolParameterField();
            this.set_pp_frequency = new Base.CheckBoolParameterField();
            this.pp_frequency = new Base.StringParameterField();
            this.set_mod_frequency = new Base.CheckBoolParameterField();
            this.mod_frequency = new Base.StringParameterField();
            this.set_mod_efficiency = new Base.CheckBoolParameterField();
            this.mod_efficiency = new Base.StringParameterField();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.set_ld);
            this.flowLayoutPanel1.Controls.Add(this.ld_on);
            this.flowLayoutPanel1.Controls.Add(this.set_shutter);
            this.flowLayoutPanel1.Controls.Add(this.shutter_open);
            this.flowLayoutPanel1.Controls.Add(this.set_pp_frequency);
            this.flowLayoutPanel1.Controls.Add(this.pp_frequency);
            this.flowLayoutPanel1.Controls.Add(this.set_mod_frequency);
            this.flowLayoutPanel1.Controls.Add(this.mod_frequency);
            this.flowLayoutPanel1.Controls.Add(this.set_mod_efficiency);
            this.flowLayoutPanel1.Controls.Add(this.mod_efficiency);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(-5, -2);
            this.flowLayoutPanel1.MaximumSize = new System.Drawing.Size(310, 0);
            this.flowLayoutPanel1.MinimumSize = new System.Drawing.Size(0, 200);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(310, 261);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // set_ld
            // 
            this.set_ld._title = "set_ld";
            this.set_ld.AutoSize = true;
            this.set_ld.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.set_ld.Location = new System.Drawing.Point(3, 3);
            this.set_ld.Name = "set_ld";
            this.set_ld.Size = new System.Drawing.Size(306, 23);
            this.set_ld.TabIndex = 110;
            this.set_ld.Value = false;
            this.set_ld.ParameterFieldValueChanged += new Base.GUI.ParameterFieldValueChangedDelegate(this.set_ld_ParameterFieldValueChanged);
            // 
            // ld_on
            // 
            this.ld_on._title = "ld_on";
            this.ld_on._value_title1 = "ON";
            this.ld_on._value_title2 = "OFF";
            this.ld_on.AutoSize = true;
            this.ld_on.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ld_on.IsCheckedFirst = false;
            this.ld_on.IsCheckedSecond = false;
            this.ld_on.Location = new System.Drawing.Point(0, 29);
            this.ld_on.Margin = new System.Windows.Forms.Padding(0);
            this.ld_on.Name = "ld_on";
            this.ld_on.Shift = 0;
            this.ld_on.Shift2 = 0;
            this.ld_on.Size = new System.Drawing.Size(284, 22);
            this.ld_on.TabIndex = 111;
            // 
            // set_shutter
            // 
            this.set_shutter._title = "set_shutter";
            this.set_shutter.AutoSize = true;
            this.set_shutter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.set_shutter.Location = new System.Drawing.Point(3, 54);
            this.set_shutter.Name = "set_shutter";
            this.set_shutter.Size = new System.Drawing.Size(306, 23);
            this.set_shutter.TabIndex = 108;
            this.set_shutter.Value = false;
            this.set_shutter.ParameterFieldValueChanged += new Base.GUI.ParameterFieldValueChangedDelegate(this.set_shutter_ParameterFieldValueChanged);
            // 
            // shutter_open
            // 
            this.shutter_open._title = "shutter_open";
            this.shutter_open._value_title1 = "Open";
            this.shutter_open._value_title2 = "Close";
            this.shutter_open.AutoSize = true;
            this.shutter_open.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.shutter_open.IsCheckedFirst = false;
            this.shutter_open.IsCheckedSecond = false;
            this.shutter_open.Location = new System.Drawing.Point(0, 80);
            this.shutter_open.Margin = new System.Windows.Forms.Padding(0);
            this.shutter_open.Name = "shutter_open";
            this.shutter_open.Shift = 0;
            this.shutter_open.Shift2 = 0;
            this.shutter_open.Size = new System.Drawing.Size(290, 22);
            this.shutter_open.TabIndex = 109;
            // 
            // set_pp_frequency
            // 
            this.set_pp_frequency._title = "set_pp_frequency";
            this.set_pp_frequency.AutoSize = true;
            this.set_pp_frequency.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.set_pp_frequency.Location = new System.Drawing.Point(3, 105);
            this.set_pp_frequency.Name = "set_pp_frequency";
            this.set_pp_frequency.Size = new System.Drawing.Size(306, 23);
            this.set_pp_frequency.TabIndex = 103;
            this.set_pp_frequency.Value = false;
            this.set_pp_frequency.ParameterFieldValueChanged += new Base.GUI.ParameterFieldValueChangedDelegate(this.set_pp_frequency_ParameterFieldValueChanged);
            // 
            // pp_frequency
            // 
            this.pp_frequency._title = "pp_frequency";
            this.pp_frequency.AutoSize = true;
            this.pp_frequency.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pp_frequency.CanBeEmpty = false;
            this.pp_frequency.Enabled = false;
            this.pp_frequency.Location = new System.Drawing.Point(0, 131);
            this.pp_frequency.Margin = new System.Windows.Forms.Padding(0);
            this.pp_frequency.Name = "pp_frequency";
            this.pp_frequency.Size = new System.Drawing.Size(303, 24);
            this.pp_frequency.TabIndex = 98;
            this.pp_frequency.ValueOnlyFromList = false;
            // 
            // set_mod_frequency
            // 
            this.set_mod_frequency._title = "set_mod_frequency";
            this.set_mod_frequency.AutoSize = true;
            this.set_mod_frequency.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.set_mod_frequency.Location = new System.Drawing.Point(3, 158);
            this.set_mod_frequency.Name = "set_mod_frequency";
            this.set_mod_frequency.Size = new System.Drawing.Size(306, 23);
            this.set_mod_frequency.TabIndex = 104;
            this.set_mod_frequency.Value = false;
            this.set_mod_frequency.ParameterFieldValueChanged += new Base.GUI.ParameterFieldValueChangedDelegate(this.set_mod_frequency_ParameterFieldValueChanged);
            // 
            // mod_frequency
            // 
            this.mod_frequency._title = "mod_frequency";
            this.mod_frequency.AutoSize = true;
            this.mod_frequency.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mod_frequency.CanBeEmpty = false;
            this.mod_frequency.Enabled = false;
            this.mod_frequency.Location = new System.Drawing.Point(0, 184);
            this.mod_frequency.Margin = new System.Windows.Forms.Padding(0);
            this.mod_frequency.Name = "mod_frequency";
            this.mod_frequency.Size = new System.Drawing.Size(303, 24);
            this.mod_frequency.TabIndex = 106;
            this.mod_frequency.ValueOnlyFromList = false;
            // 
            // set_mod_efficiency
            // 
            this.set_mod_efficiency._title = "set_mod_efficiency";
            this.set_mod_efficiency.AutoSize = true;
            this.set_mod_efficiency.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.set_mod_efficiency.Location = new System.Drawing.Point(3, 211);
            this.set_mod_efficiency.Name = "set_mod_efficiency";
            this.set_mod_efficiency.Size = new System.Drawing.Size(306, 23);
            this.set_mod_efficiency.TabIndex = 105;
            this.set_mod_efficiency.Value = false;
            this.set_mod_efficiency.ParameterFieldValueChanged += new Base.GUI.ParameterFieldValueChangedDelegate(this.set_mod_efficiency_ParameterFieldValueChanged);
            // 
            // mod_efficiency
            // 
            this.mod_efficiency._title = "mod_efficiency";
            this.mod_efficiency.AutoSize = true;
            this.mod_efficiency.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mod_efficiency.CanBeEmpty = false;
            this.mod_efficiency.Enabled = false;
            this.mod_efficiency.Location = new System.Drawing.Point(0, 237);
            this.mod_efficiency.Margin = new System.Windows.Forms.Padding(0);
            this.mod_efficiency.Name = "mod_efficiency";
            this.mod_efficiency.Size = new System.Drawing.Size(303, 24);
            this.mod_efficiency.TabIndex = 107;
            this.mod_efficiency.ValueOnlyFromList = false;
            // 
            // CommandGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "CommandGUI";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Base.CheckBoolParameterField set_ld;
        private Base.GUI.BoolParameterField ld_on;
        private Base.CheckBoolParameterField set_shutter;
        private Base.GUI.BoolParameterField shutter_open;
        private Base.CheckBoolParameterField set_pp_frequency;
        private Base.StringParameterField pp_frequency;
        private Base.CheckBoolParameterField set_mod_frequency;
        private Base.StringParameterField mod_frequency;
        private Base.CheckBoolParameterField set_mod_efficiency;
        private Base.StringParameterField mod_efficiency;
    }
}
