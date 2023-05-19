namespace TestPlugin
{
    partial class DistanceSensorCommandGUI
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
            this.position = new Core.Commands.PositionGUI();
            this.headerGroupBox1 = new Base.HeaderGroupBox();
            this.variable_name = new Base.StringParameterField();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.position);
            this.flowLayoutPanel1.Controls.Add(this.headerGroupBox1);
            this.flowLayoutPanel1.Controls.Add(this.variable_name);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.MaximumSize = new System.Drawing.Size(310, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(306, 166);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // position
            // 
            this.position.Location = new System.Drawing.Point(3, 3);
            this.position.Name = "position";
            this.position.ShowOriginal = false;
            this.position.ShowReference = false;
            this.position.Size = new System.Drawing.Size(300, 111);
            this.position.TabIndex = 100;
            this.position.ZRefEnabled = true;
            // 
            // headerGroupBox1
            // 
            this.headerGroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.headerGroupBox1.Location = new System.Drawing.Point(3, 120);
            this.headerGroupBox1.Name = "headerGroupBox1";
            this.headerGroupBox1.Size = new System.Drawing.Size(300, 19);
            this.headerGroupBox1.TabIndex = 101;
            this.headerGroupBox1.TabStop = false;
            this.headerGroupBox1.Text = "Other";
            // 
            // variable_name
            // 
            this.variable_name._title = "variable_name";
            this.variable_name.AutoSize = true;
            this.variable_name.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.variable_name.CanBeEmpty = false;
            this.variable_name.Location = new System.Drawing.Point(0, 142);
            this.variable_name.Margin = new System.Windows.Forms.Padding(0);
            this.variable_name.Name = "variable_name";
            this.variable_name.Size = new System.Drawing.Size(303, 24);
            this.variable_name.TabIndex = 98;
            this.variable_name.ValueOnlyFromList = false;
            // 
            // DistanceSensorCommandGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DistanceSensorCommandGUI";
            this.Size = new System.Drawing.Size(309, 169);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Core.Commands.PositionGUI position;
        private Base.HeaderGroupBox headerGroupBox1;
        private Base.StringParameterField variable_name;
    }
}
