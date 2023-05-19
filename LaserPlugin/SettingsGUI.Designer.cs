namespace LaserPlugin
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
            this.enabled = new Base.CheckBoolParameterField();
            this.flow = new System.Windows.Forms.FlowLayoutPanel();
            this.headerGroupBox1 = new Base.HeaderGroupBox();
            this.flowSerial = new System.Windows.Forms.FlowLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.port = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.baud_rate = new Base.ComboBoxWithBorder();
            this.flow.SuspendLayout();
            this.flowSerial.SuspendLayout();
            this.SuspendLayout();
            // 
            // enabled
            // 
            this.enabled._title = "Enabled";
            this.enabled.AutoSize = true;
            this.enabled.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.enabled.Location = new System.Drawing.Point(3, 3);
            this.enabled.Name = "enabled";
            this.enabled.Size = new System.Drawing.Size(306, 23);
            this.enabled.TabIndex = 0;
            this.enabled.Value = false;
            this.enabled.ParameterFieldValueChanged += new Base.GUI.ParameterFieldValueChangedDelegate(this.enabled_ParameterFieldValueChanged);
            // 
            // flow
            // 
            this.flow.AutoSize = true;
            this.flow.Controls.Add(this.headerGroupBox1);
            this.flow.Controls.Add(this.flowSerial);
            this.flow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flow.Location = new System.Drawing.Point(3, 32);
            this.flow.MaximumSize = new System.Drawing.Size(350, 0);
            this.flow.Name = "flow";
            this.flow.Size = new System.Drawing.Size(350, 202);
            this.flow.TabIndex = 10;
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
            this.headerGroupBox1.Text = "Connection";
            // 
            // flowSerial
            // 
            this.flowSerial.Controls.Add(this.label5);
            this.flowSerial.Controls.Add(this.port);
            this.flowSerial.Controls.Add(this.label3);
            this.flowSerial.Controls.Add(this.baud_rate);
            this.flowSerial.Location = new System.Drawing.Point(0, 32);
            this.flowSerial.Margin = new System.Windows.Forms.Padding(0);
            this.flowSerial.Name = "flowSerial";
            this.flowSerial.Size = new System.Drawing.Size(344, 85);
            this.flowSerial.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 23);
            this.label5.TabIndex = 18;
            this.label5.Text = "COM Port";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // port
            // 
            this.port.FormattingEnabled = true;
            this.port.Items.AddRange(new object[] {
            "Serial",
            "Network",
            "PCI Bus",
            "Simulator"});
            this.port.Location = new System.Drawing.Point(150, 3);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(150, 21);
            this.port.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 23);
            this.label3.TabIndex = 20;
            this.label3.Text = "Baud Rate (bps)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // baud_rate
            // 
            this.baud_rate.BorderColor = System.Drawing.Color.Gainsboro;
            this.baud_rate.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.baud_rate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.baud_rate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.baud_rate.FormattingEnabled = true;
            this.baud_rate.Items.AddRange(new object[] {
            "Auto",
            "115200",
            "57600",
            "19200",
            "9600",
            "4800",
            "1200",
            "300"});
            this.baud_rate.Location = new System.Drawing.Point(150, 30);
            this.baud_rate.Name = "baud_rate";
            this.baud_rate.Size = new System.Drawing.Size(150, 21);
            this.baud_rate.TabIndex = 19;
            // 
            // SettingsGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flow);
            this.Controls.Add(this.enabled);
            this.Name = "SettingsGUI";
            this.Size = new System.Drawing.Size(446, 273);
            this.flow.ResumeLayout(false);
            this.flowSerial.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Base.CheckBoolParameterField enabled;
        private System.Windows.Forms.FlowLayoutPanel flow;
        private Base.HeaderGroupBox headerGroupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowSerial;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox port;
        private System.Windows.Forms.Label label3;
        private Base.ComboBoxWithBorder baud_rate;
    }
}
