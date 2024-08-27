namespace Sample
{
    partial class InputOutputUC
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
            this.flowOutputs = new System.Windows.Forms.FlowLayoutPanel();
            this.flowInputs = new System.Windows.Forms.FlowLayoutPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // flowOutputs
            // 
            this.flowOutputs.Location = new System.Drawing.Point(259, 3);
            this.flowOutputs.Name = "flowOutputs";
            this.flowOutputs.Size = new System.Drawing.Size(250, 379);
            this.flowOutputs.TabIndex = 0;
            // 
            // flowInputs
            // 
            this.flowInputs.Location = new System.Drawing.Point(3, 3);
            this.flowInputs.Name = "flowInputs";
            this.flowInputs.Size = new System.Drawing.Size(250, 379);
            this.flowInputs.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // InputOutputUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowInputs);
            this.Controls.Add(this.flowOutputs);
            this.Name = "InputOutputUC";
            this.Size = new System.Drawing.Size(569, 396);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowOutputs;
        private System.Windows.Forms.FlowLayoutPanel flowInputs;
        private System.Windows.Forms.Timer timer1;
    }
}
