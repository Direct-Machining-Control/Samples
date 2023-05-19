namespace LaserPlugin
{
    partial class StatusGUI
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
            this.label_status = new System.Windows.Forms.Label();
            this.label_shutter = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_ld = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_status
            // 
            this.label_status.ForeColor = System.Drawing.Color.DimGray;
            this.label_status.Location = new System.Drawing.Point(47, 47);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(42, 20);
            this.label_status.TabIndex = 16;
            this.label_status.Text = "...";
            this.label_status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_shutter
            // 
            this.label_shutter.ForeColor = System.Drawing.Color.DimGray;
            this.label_shutter.Location = new System.Drawing.Point(47, 25);
            this.label_shutter.Name = "label_shutter";
            this.label_shutter.Size = new System.Drawing.Size(42, 20);
            this.label_shutter.TabIndex = 14;
            this.label_shutter.Text = "...";
            this.label_shutter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(1, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Shutter:";
            // 
            // label_ld
            // 
            this.label_ld.ForeColor = System.Drawing.Color.DimGray;
            this.label_ld.Location = new System.Drawing.Point(47, 3);
            this.label_ld.Name = "label_ld";
            this.label_ld.Size = new System.Drawing.Size(42, 20);
            this.label_ld.TabIndex = 12;
            this.label_ld.Text = "...";
            this.label_ld.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(1, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Status:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(0, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "LD:";
            // 
            // StatusGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.label_shutter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_ld);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Name = "StatusGUI";
            this.Size = new System.Drawing.Size(94, 70);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.Label label_shutter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_ld;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
    }
}
