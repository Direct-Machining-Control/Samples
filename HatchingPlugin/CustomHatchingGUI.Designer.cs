namespace HatchingPlugin
{
    partial class CustomHatchingGUI
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
            this.offset_to_contour = new Base.StringParameterField();
            this.offset_to_hatch = new Base.StringParameterField();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.spacing);
            this.flowLayoutPanel1.Controls.Add(this.offset_to_contour);
            this.flowLayoutPanel1.Controls.Add(this.offset_to_hatch);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(311, 72);
            this.flowLayoutPanel1.TabIndex = 1;
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
            // offset_to_contour
            // 
            this.offset_to_contour._title = "offset_to_contour";
            this.offset_to_contour.AutoSize = true;
            this.offset_to_contour.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.offset_to_contour.CanBeEmpty = false;
            this.offset_to_contour.Location = new System.Drawing.Point(0, 24);
            this.offset_to_contour.Margin = new System.Windows.Forms.Padding(0);
            this.offset_to_contour.Name = "offset_to_contour";
            this.offset_to_contour.Size = new System.Drawing.Size(311, 24);
            this.offset_to_contour.TabIndex = 3;
            this.offset_to_contour.ValueOnlyFromList = false;
            // 
            // offset_to_hatch
            // 
            this.offset_to_hatch._title = "offset_to_hatch";
            this.offset_to_hatch.AutoSize = true;
            this.offset_to_hatch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.offset_to_hatch.CanBeEmpty = false;
            this.offset_to_hatch.Location = new System.Drawing.Point(0, 48);
            this.offset_to_hatch.Margin = new System.Windows.Forms.Padding(0);
            this.offset_to_hatch.Name = "offset_to_hatch";
            this.offset_to_hatch.Size = new System.Drawing.Size(311, 24);
            this.offset_to_hatch.TabIndex = 4;
            this.offset_to_hatch.ValueOnlyFromList = false;
            // 
            // CustomHatchingGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(300, 0);
            this.Name = "CustomHatchingGUI";
            this.Size = new System.Drawing.Size(300, 72);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Base.StringParameterField spacing;
        private Base.StringParameterField offset_to_contour;
        private Base.StringParameterField offset_to_hatch;
    }
}
