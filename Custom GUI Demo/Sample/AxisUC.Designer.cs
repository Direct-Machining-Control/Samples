namespace Sample
{
    partial class AxisUC
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
            this.components = new System.ComponentModel.Container();
            this.labelPosition = new System.Windows.Forms.Label();
            this.buttonEnable = new System.Windows.Forms.Button();
            this.buttonHomeReset = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Location = new System.Drawing.Point(3, 11);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(66, 13);
            this.labelPosition.TabIndex = 0;
            this.labelPosition.Text = "labelPosition";
            // 
            // buttonEnable
            // 
            this.buttonEnable.Location = new System.Drawing.Point(103, 6);
            this.buttonEnable.Name = "buttonEnable";
            this.buttonEnable.Size = new System.Drawing.Size(70, 23);
            this.buttonEnable.TabIndex = 1;
            this.buttonEnable.Text = "Enable";
            this.buttonEnable.UseVisualStyleBackColor = true;
            this.buttonEnable.Click += new System.EventHandler(this.buttonEnable_Click);
            // 
            // buttonHomeReset
            // 
            this.buttonHomeReset.Location = new System.Drawing.Point(185, 6);
            this.buttonHomeReset.Name = "buttonHomeReset";
            this.buttonHomeReset.Size = new System.Drawing.Size(70, 23);
            this.buttonHomeReset.TabIndex = 2;
            this.buttonHomeReset.Text = "Home";
            this.buttonHomeReset.UseVisualStyleBackColor = true;
            this.buttonHomeReset.Click += new System.EventHandler(this.buttonHome_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(282, 11);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(35, 13);
            this.labelStatus.TabIndex = 3;
            this.labelStatus.Text = "status";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // AxisUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonHomeReset);
            this.Controls.Add(this.buttonEnable);
            this.Controls.Add(this.labelPosition);
            this.Name = "AxisUC";
            this.Size = new System.Drawing.Size(390, 39);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.Button buttonEnable;
        private System.Windows.Forms.Button buttonHomeReset;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Timer timer1;
    }
}
