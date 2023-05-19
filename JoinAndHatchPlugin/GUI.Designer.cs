namespace JoinAndHatchPlugin
{
    partial class GUI
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
            this.hatching = new Core.Commands.HatchingGUI();
            this.SuspendLayout();
            // 
            // hatching
            // 
            this.hatching.AutoSize = true;
            this.hatching.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.hatching.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.hatching.Location = new System.Drawing.Point(0, 0);
            this.hatching.Margin = new System.Windows.Forms.Padding(0);
            this.hatching.MaximumSize = new System.Drawing.Size(300, 0);
            this.hatching.MinimumSize = new System.Drawing.Size(300, 0);
            this.hatching.Name = "hatching";
            this.hatching.Size = new System.Drawing.Size(300, 565);
            this.hatching.TabIndex = 0;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.hatching);
            this.Name = "GUI";
            this.Size = new System.Drawing.Size(300, 565);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Core.Commands.HatchingGUI hatching;
    }
}
