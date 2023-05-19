namespace Custom3DSupportsPlugin
{
    partial class SupportGenerationGUI
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
            this.spacing = new Base.StringParameterField();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.spacing);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.MaximumSize = new System.Drawing.Size(300, 0);
            this.flowLayoutPanel1.MinimumSize = new System.Drawing.Size(300, 20);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(300, 24);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // spacing
            // 
            this.spacing._title = "spacing";
            this.spacing.AutoSize = true;
            this.spacing.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.spacing.CanBeEmpty = false;
            this.spacing.Location = new System.Drawing.Point(0, 0);
            this.spacing.Margin = new System.Windows.Forms.Padding(0);
            this.spacing.Name = "spacing";
            this.spacing.Size = new System.Drawing.Size(311, 24);
            this.spacing.TabIndex = 0;
            this.spacing.ValueOnlyFromList = false;
            // 
            // SupportGenerationGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(300, 0);
            this.MinimumSize = new System.Drawing.Size(300, 20);
            this.Name = "SupportGenerationGUI";
            this.Size = new System.Drawing.Size(300, 27);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Base.StringParameterField spacing;
    }
}
