namespace StageSamplePlugin
{
    partial class SettingsGUI
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
            this.flow = new System.Windows.Forms.FlowLayoutPanel();
            this.headerGroupBox1 = new Base.HeaderGroupBox();
            this.enabled = new Base.CheckBoolParameterField();
            this.port = new Base.StringParameterField();
            this.flow.SuspendLayout();
            this.SuspendLayout();
            // 
            // flow
            // 
            this.flow.Controls.Add(this.headerGroupBox1);
            this.flow.Controls.Add(this.port);
            this.flow.Location = new System.Drawing.Point(0, 29);
            this.flow.Name = "flow";
            this.flow.Size = new System.Drawing.Size(364, 296);
            this.flow.TabIndex = 9;
            // 
            // headerGroupBox1
            // 
            this.headerGroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.headerGroupBox1.Location = new System.Drawing.Point(3, 10);
            this.headerGroupBox1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.headerGroupBox1.Name = "headerGroupBox1";
            this.headerGroupBox1.Size = new System.Drawing.Size(300, 19);
            this.headerGroupBox1.TabIndex = 0;
            this.headerGroupBox1.TabStop = false;
            this.headerGroupBox1.Text = "General";
            // 
            // enabled
            // 
            this.enabled._title = "enabled";
            this.enabled.AutoSize = true;
            this.enabled.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.enabled.Location = new System.Drawing.Point(3, 3);
            this.enabled.Name = "enabled";
            this.enabled.Size = new System.Drawing.Size(306, 23);
            this.enabled.TabIndex = 8;
            this.enabled.Value = false;
            this.enabled.ParameterFieldValueChanged += new Base.GUI.ParameterFieldValueChangedDelegate(this.enabled_ParameterFieldValueChanged);
            // 
            // port
            // 
            this.port._title = "port";
            this.port.AutoSize = true;
            this.port.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.port.CanBeEmpty = false;
            this.port.Location = new System.Drawing.Point(0, 32);
            this.port.Margin = new System.Windows.Forms.Padding(0);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(303, 24);
            this.port.TabIndex = 80;
            this.port.ValueOnlyFromList = false;
            // 
            // SettingsGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flow);
            this.Controls.Add(this.enabled);
            this.Name = "SettingsGUI";
            this.flow.ResumeLayout(false);
            this.flow.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flow;
        private Base.HeaderGroupBox headerGroupBox1;
        private Base.CheckBoolParameterField enabled;
        private Base.StringParameterField port;
    }
}
