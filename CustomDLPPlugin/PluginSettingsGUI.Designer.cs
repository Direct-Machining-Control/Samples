namespace CustomDLPPlugin
{
    partial class CustomDLPPluginSettingsGUI
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
            this.enabled = new Base.CheckBoolParameterField();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.mode = new Base.GUI.BoolParameterField();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.enabled);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(361, 287);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // enabled
            // 
            this.enabled._title = "enabled";
            this.enabled.AutoSize = true;
            this.enabled.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.enabled.Location = new System.Drawing.Point(3, 3);
            this.enabled.Name = "enabled";
            this.enabled.Size = new System.Drawing.Size(306, 23);
            this.enabled.TabIndex = 0;
            this.enabled.Value = false;
            this.enabled.ParameterFieldValueChanged += new Base.GUI.ParameterFieldValueChangedDelegate(this.enabled_ParameterFieldValueChanged);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.mode);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 29);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(325, 224);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // mode
            // 
            this.mode._title = "mode";
            this.mode._value_title1 = "radioButton1";
            this.mode._value_title2 = "radioButton2";
            this.mode.AutoSize = true;
            this.mode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mode.IsCheckedFirst = false;
            this.mode.IsCheckedSecond = false;
            this.mode.Location = new System.Drawing.Point(0, 0);
            this.mode.Margin = new System.Windows.Forms.Padding(0);
            this.mode.Name = "mode";
            this.mode.Shift = 50;
            this.mode.Shift2 = 0;
            this.mode.Size = new System.Drawing.Size(324, 22);
            this.mode.TabIndex = 0;
            // 
            // CustomDLPPluginSettingsGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "CustomDLPPluginSettingsGUI";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Base.CheckBoolParameterField enabled;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Base.GUI.BoolParameterField mode;
    }
}
