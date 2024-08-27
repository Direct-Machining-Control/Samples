namespace Sample
{
    partial class JoystickUC
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
            this.buttonYpositive = new System.Windows.Forms.Button();
            this.buttonYnegative = new System.Windows.Forms.Button();
            this.buttonXnegative = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonXpositive = new System.Windows.Forms.Button();
            this.buttonZpositive = new System.Windows.Forms.Button();
            this.buttonZnegative = new System.Windows.Forms.Button();
            this.numSpeed = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numStep = new System.Windows.Forms.NumericUpDown();
            this.buttonMode = new System.Windows.Forms.Button();
            this.flowAxes = new System.Windows.Forms.FlowLayoutPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStep)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonYpositive
            // 
            this.buttonYpositive.Location = new System.Drawing.Point(87, 3);
            this.buttonYpositive.Name = "buttonYpositive";
            this.buttonYpositive.Size = new System.Drawing.Size(50, 44);
            this.buttonYpositive.TabIndex = 0;
            this.buttonYpositive.Text = "Y+";
            this.buttonYpositive.UseVisualStyleBackColor = true;
            // 
            // buttonYnegative
            // 
            this.buttonYnegative.Location = new System.Drawing.Point(87, 103);
            this.buttonYnegative.Name = "buttonYnegative";
            this.buttonYnegative.Size = new System.Drawing.Size(50, 39);
            this.buttonYnegative.TabIndex = 1;
            this.buttonYnegative.Text = "Y-";
            this.buttonYnegative.UseVisualStyleBackColor = true;
            // 
            // buttonXnegative
            // 
            this.buttonXnegative.Location = new System.Drawing.Point(3, 53);
            this.buttonXnegative.Name = "buttonXnegative";
            this.buttonXnegative.Size = new System.Drawing.Size(50, 44);
            this.buttonXnegative.TabIndex = 2;
            this.buttonXnegative.Text = "X-";
            this.buttonXnegative.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.tableLayoutPanel1.Controls.Add(this.buttonXnegative, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonYnegative, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonYpositive, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonXpositive, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonZpositive, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonZnegative, 4, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(409, 145);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // buttonXpositive
            // 
            this.buttonXpositive.Location = new System.Drawing.Point(171, 53);
            this.buttonXpositive.Name = "buttonXpositive";
            this.buttonXpositive.Size = new System.Drawing.Size(50, 39);
            this.buttonXpositive.TabIndex = 3;
            this.buttonXpositive.Text = "X+";
            this.buttonXpositive.UseVisualStyleBackColor = true;
            // 
            // buttonZpositive
            // 
            this.buttonZpositive.Location = new System.Drawing.Point(336, 3);
            this.buttonZpositive.Name = "buttonZpositive";
            this.buttonZpositive.Size = new System.Drawing.Size(50, 44);
            this.buttonZpositive.TabIndex = 4;
            this.buttonZpositive.Text = "Z+";
            this.buttonZpositive.UseVisualStyleBackColor = true;
            // 
            // buttonZnegative
            // 
            this.buttonZnegative.Location = new System.Drawing.Point(336, 103);
            this.buttonZnegative.Name = "buttonZnegative";
            this.buttonZnegative.Size = new System.Drawing.Size(50, 39);
            this.buttonZnegative.TabIndex = 5;
            this.buttonZnegative.Text = "Z-";
            this.buttonZnegative.UseVisualStyleBackColor = true;
            // 
            // numSpeed
            // 
            this.numSpeed.DecimalPlaces = 2;
            this.numSpeed.Location = new System.Drawing.Point(6, 240);
            this.numSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numSpeed.Name = "numSpeed";
            this.numSpeed.Size = new System.Drawing.Size(120, 20);
            this.numSpeed.TabIndex = 4;
            this.numSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 224);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Jog speed (mm/s)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(171, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Jog step (mm)";
            // 
            // numStep
            // 
            this.numStep.DecimalPlaces = 2;
            this.numStep.Location = new System.Drawing.Point(158, 240);
            this.numStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numStep.Name = "numStep";
            this.numStep.Size = new System.Drawing.Size(120, 20);
            this.numStep.TabIndex = 6;
            this.numStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // buttonMode
            // 
            this.buttonMode.Location = new System.Drawing.Point(107, 167);
            this.buttonMode.Name = "buttonMode";
            this.buttonMode.Size = new System.Drawing.Size(75, 54);
            this.buttonMode.TabIndex = 8;
            this.buttonMode.Text = "MODE FREEMOVE";
            this.buttonMode.UseVisualStyleBackColor = true;
            this.buttonMode.Click += new System.EventHandler(this.buttonMode_Click);
            // 
            // flowAxes
            // 
            this.flowAxes.AutoSize = true;
            this.flowAxes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowAxes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowAxes.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowAxes.Location = new System.Drawing.Point(6, 266);
            this.flowAxes.MinimumSize = new System.Drawing.Size(500, 100);
            this.flowAxes.Name = "flowAxes";
            this.flowAxes.Size = new System.Drawing.Size(500, 100);
            this.flowAxes.TabIndex = 9;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // JoystickUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowAxes);
            this.Controls.Add(this.buttonMode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numStep);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numSpeed);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "JoystickUC";
            this.Size = new System.Drawing.Size(621, 395);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonYpositive;
        private System.Windows.Forms.Button buttonYnegative;
        private System.Windows.Forms.Button buttonXnegative;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonXpositive;
        private System.Windows.Forms.Button buttonZpositive;
        private System.Windows.Forms.Button buttonZnegative;
        private System.Windows.Forms.NumericUpDown numSpeed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numStep;
        private System.Windows.Forms.Button buttonMode;
        private System.Windows.Forms.FlowLayoutPanel flowAxes;
        private System.Windows.Forms.Timer timer1;
    }
}
