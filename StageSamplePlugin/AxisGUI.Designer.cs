namespace StageSamplePlugin
{
    partial class AxisGUI
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
            this.label1 = new System.Windows.Forms.Label();
            this.axis_index = new System.Windows.Forms.NumericUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.steps_per_mm = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.axis_index)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.steps_per_mm)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Axis Index";
            // 
            // axis_index
            // 
            this.axis_index.Location = new System.Drawing.Point(0, 25);
            this.axis_index.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.axis_index.Name = "axis_index";
            this.axis_index.Size = new System.Drawing.Size(90, 20);
            this.axis_index.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Steps per mm";
            // 
            // steps_per_mm
            // 
            this.steps_per_mm.Location = new System.Drawing.Point(0, 68);
            this.steps_per_mm.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.steps_per_mm.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.steps_per_mm.Name = "steps_per_mm";
            this.steps_per_mm.Size = new System.Drawing.Size(90, 20);
            this.steps_per_mm.TabIndex = 19;
            this.steps_per_mm.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // AxisGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.steps_per_mm);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.axis_index);
            this.Name = "AxisGUI";
            this.Size = new System.Drawing.Size(98, 93);
            ((System.ComponentModel.ISupportInitialize)(this.axis_index)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.steps_per_mm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown axis_index;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown steps_per_mm;
    }
}
