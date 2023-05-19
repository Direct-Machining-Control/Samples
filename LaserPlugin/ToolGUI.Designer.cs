namespace LaserPlugin
{
    partial class ToolGUI
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
            this.buttonLaserOn = new System.Windows.Forms.Button();
            this.buttonLaserOff = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonLaserOn
            // 
            this.buttonLaserOn.Location = new System.Drawing.Point(45, 3);
            this.buttonLaserOn.Name = "buttonLaserOn";
            this.buttonLaserOn.Size = new System.Drawing.Size(75, 23);
            this.buttonLaserOn.TabIndex = 0;
            this.buttonLaserOn.Text = "Laser ON";
            this.buttonLaserOn.UseVisualStyleBackColor = true;
            this.buttonLaserOn.Click += new System.EventHandler(this.buttonLaserOn_Click);
            // 
            // buttonLaserOff
            // 
            this.buttonLaserOff.Location = new System.Drawing.Point(126, 3);
            this.buttonLaserOff.Name = "buttonLaserOff";
            this.buttonLaserOff.Size = new System.Drawing.Size(75, 23);
            this.buttonLaserOff.TabIndex = 1;
            this.buttonLaserOff.Text = "Laser OFF";
            this.buttonLaserOff.UseVisualStyleBackColor = true;
            this.buttonLaserOff.Click += new System.EventHandler(this.buttonLaserOff_Click);
            // 
            // ToolGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonLaserOff);
            this.Controls.Add(this.buttonLaserOn);
            this.Name = "ToolGUI";
            this.Size = new System.Drawing.Size(244, 195);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonLaserOn;
        private System.Windows.Forms.Button buttonLaserOff;
    }
}
