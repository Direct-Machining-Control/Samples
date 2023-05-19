namespace PythonCommandPlugin
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
            this.executable_path = new Base.StringParameterField();
            this.buttonDetect = new Base.NoSelectButton();
            this.SuspendLayout();
            // 
            // enabled
            // 
            this.enabled._title = "enabled";
            this.enabled.AutoSize = true;
            this.enabled.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.enabled.Location = new System.Drawing.Point(0, 0);
            this.enabled.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.enabled.Name = "enabled";
            this.enabled.Size = new System.Drawing.Size(306, 23);
            this.enabled.TabIndex = 75;
            this.enabled.Value = false;
            this.enabled.ParameterFieldValueChanged += new Base.GUI.ParameterFieldValueChangedDelegate(this.enabled_ParameterFieldValueChanged);
            // 
            // executable_path
            // 
            this.executable_path._title = "executable_path";
            this.executable_path.AutoSize = true;
            this.executable_path.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.executable_path.CanBeEmpty = false;
            this.executable_path.Location = new System.Drawing.Point(0, 22);
            this.executable_path.Margin = new System.Windows.Forms.Padding(0);
            this.executable_path.Name = "executable_path";
            this.executable_path.Size = new System.Drawing.Size(303, 24);
            this.executable_path.TabIndex = 76;
            this.executable_path.ValueOnlyFromList = false;
            // 
            // buttonDetect
            // 
            this.buttonDetect.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonDetect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.buttonDetect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.buttonDetect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDetect.Location = new System.Drawing.Point(306, 22);
            this.buttonDetect.Name = "buttonDetect";
            this.buttonDetect.Size = new System.Drawing.Size(75, 23);
            this.buttonDetect.TabIndex = 77;
            this.buttonDetect.Text = "Detect";
            this.buttonDetect.UseVisualStyleBackColor = true;
            this.buttonDetect.Click += new System.EventHandler(this.buttonDetect_Click);
            // 
            // SettingsGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.executable_path);
            this.Controls.Add(this.buttonDetect);
            this.Controls.Add(this.enabled);
            this.Name = "SettingsGUI";
            this.Size = new System.Drawing.Size(399, 306);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Base.CheckBoolParameterField enabled;
        private Base.StringParameterField executable_path;
        private Base.NoSelectButton buttonDetect;
    }
}
