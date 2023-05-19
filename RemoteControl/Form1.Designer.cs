namespace RemoteControl
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonRun = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxRecipe = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonStop = new System.Windows.Forms.Button();
            this.checkBoxAutorun = new System.Windows.Forms.CheckBox();
            this.log = new System.Windows.Forms.TextBox();
            this.buttonMove1 = new System.Windows.Forms.Button();
            this.actual_axis1_position = new System.Windows.Forms.TextBox();
            this.axis1_letter = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.move1_to = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.axis2_letter = new System.Windows.Forms.TextBox();
            this.move2_to = new System.Windows.Forms.NumericUpDown();
            this.actual_axis2_position = new System.Windows.Forms.TextBox();
            this.buttonMove2 = new System.Windows.Forms.Button();
            this.labelSConnected = new System.Windows.Forms.Label();
            this.labelSInProgress = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.labelSFinished = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelSError = new System.Windows.Forms.Label();
            this.labelSRecipeRunning = new System.Windows.Forms.Label();
            this.use_log = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.move1_to)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.move2_to)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(9, 42);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(84, 57);
            this.buttonRun.TabIndex = 0;
            this.buttonRun.Text = "Connect";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(225, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(225, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 31);
            this.label2.TabIndex = 2;
            this.label2.Text = "...";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxRecipe
            // 
            this.comboBoxRecipe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRecipe.FormattingEnabled = true;
            this.comboBoxRecipe.Location = new System.Drawing.Point(12, 164);
            this.comboBoxRecipe.Name = "comboBoxRecipe";
            this.comboBoxRecipe.Size = new System.Drawing.Size(416, 21);
            this.comboBoxRecipe.TabIndex = 3;
            this.comboBoxRecipe.SelectedIndexChanged += new System.EventHandler(this.comboBoxRecipe_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 31);
            this.label3.TabIndex = 4;
            this.label3.Text = "Recipe:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(99, 42);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(84, 57);
            this.buttonStop.TabIndex = 5;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // checkBoxAutorun
            // 
            this.checkBoxAutorun.AutoSize = true;
            this.checkBoxAutorun.Checked = true;
            this.checkBoxAutorun.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutorun.Location = new System.Drawing.Point(19, 191);
            this.checkBoxAutorun.Name = "checkBoxAutorun";
            this.checkBoxAutorun.Size = new System.Drawing.Size(63, 17);
            this.checkBoxAutorun.TabIndex = 6;
            this.checkBoxAutorun.Text = "Autorun";
            this.checkBoxAutorun.UseVisualStyleBackColor = true;
            this.checkBoxAutorun.CheckedChanged += new System.EventHandler(this.checkBoxAutorun_CheckedChanged);
            // 
            // log
            // 
            this.log.Location = new System.Drawing.Point(439, 12);
            this.log.Multiline = true;
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.log.Size = new System.Drawing.Size(443, 254);
            this.log.TabIndex = 7;
            // 
            // buttonMove1
            // 
            this.buttonMove1.Location = new System.Drawing.Point(289, 35);
            this.buttonMove1.Name = "buttonMove1";
            this.buttonMove1.Size = new System.Drawing.Size(53, 23);
            this.buttonMove1.TabIndex = 8;
            this.buttonMove1.Text = "Move";
            this.buttonMove1.UseVisualStyleBackColor = true;
            this.buttonMove1.Click += new System.EventHandler(this.buttonMove_Click);
            // 
            // actual_axis1_position
            // 
            this.actual_axis1_position.Location = new System.Drawing.Point(91, 35);
            this.actual_axis1_position.Name = "actual_axis1_position";
            this.actual_axis1_position.ReadOnly = true;
            this.actual_axis1_position.Size = new System.Drawing.Size(54, 20);
            this.actual_axis1_position.TabIndex = 9;
            // 
            // axis1_letter
            // 
            this.axis1_letter.Location = new System.Drawing.Point(3, 35);
            this.axis1_letter.Name = "axis1_letter";
            this.axis1_letter.ReadOnly = true;
            this.axis1_letter.Size = new System.Drawing.Size(54, 20);
            this.axis1_letter.TabIndex = 11;
            this.axis1_letter.Text = "X";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.tableLayoutPanel1.Controls.Add(this.move1_to, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonMove1, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.axis1_letter, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.actual_axis1_position, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.axis2_letter, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.move2_to, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.actual_axis2_position, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonMove2, 3, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 249);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(345, 93);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // move1_to
            // 
            this.move1_to.DecimalPlaces = 4;
            this.move1_to.Location = new System.Drawing.Point(190, 35);
            this.move1_to.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.move1_to.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.move1_to.Name = "move1_to";
            this.move1_to.Size = new System.Drawing.Size(93, 20);
            this.move1_to.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(190, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Move To";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Axis";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(91, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Position";
            // 
            // axis2_letter
            // 
            this.axis2_letter.Location = new System.Drawing.Point(3, 67);
            this.axis2_letter.Name = "axis2_letter";
            this.axis2_letter.ReadOnly = true;
            this.axis2_letter.Size = new System.Drawing.Size(54, 20);
            this.axis2_letter.TabIndex = 15;
            this.axis2_letter.Text = "Y";
            // 
            // move2_to
            // 
            this.move2_to.DecimalPlaces = 4;
            this.move2_to.Location = new System.Drawing.Point(190, 67);
            this.move2_to.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.move2_to.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.move2_to.Name = "move2_to";
            this.move2_to.Size = new System.Drawing.Size(93, 20);
            this.move2_to.TabIndex = 17;
            // 
            // actual_axis2_position
            // 
            this.actual_axis2_position.Location = new System.Drawing.Point(91, 67);
            this.actual_axis2_position.Name = "actual_axis2_position";
            this.actual_axis2_position.ReadOnly = true;
            this.actual_axis2_position.Size = new System.Drawing.Size(54, 20);
            this.actual_axis2_position.TabIndex = 16;
            // 
            // buttonMove2
            // 
            this.buttonMove2.Location = new System.Drawing.Point(289, 67);
            this.buttonMove2.Name = "buttonMove2";
            this.buttonMove2.Size = new System.Drawing.Size(53, 23);
            this.buttonMove2.TabIndex = 18;
            this.buttonMove2.Text = "Move";
            this.buttonMove2.UseVisualStyleBackColor = true;
            this.buttonMove2.Click += new System.EventHandler(this.buttonMove2_Click);
            // 
            // labelSConnected
            // 
            this.labelSConnected.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.labelSConnected, true);
            this.labelSConnected.Location = new System.Drawing.Point(3, 15);
            this.labelSConnected.Name = "labelSConnected";
            this.labelSConnected.Size = new System.Drawing.Size(10, 13);
            this.labelSConnected.TabIndex = 14;
            this.labelSConnected.Text = "-";
            // 
            // labelSInProgress
            // 
            this.labelSInProgress.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.labelSInProgress, true);
            this.labelSInProgress.Location = new System.Drawing.Point(3, 28);
            this.labelSInProgress.Name = "labelSInProgress";
            this.labelSInProgress.Size = new System.Drawing.Size(10, 13);
            this.labelSInProgress.TabIndex = 16;
            this.labelSInProgress.Text = "-";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.label12, true);
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 15);
            this.label12.TabIndex = 19;
            this.label12.Text = "Status:";
            // 
            // labelSFinished
            // 
            this.labelSFinished.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.labelSFinished, true);
            this.labelSFinished.Location = new System.Drawing.Point(3, 41);
            this.labelSFinished.Name = "labelSFinished";
            this.labelSFinished.Size = new System.Drawing.Size(10, 13);
            this.labelSFinished.TabIndex = 20;
            this.labelSFinished.Text = "-";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label12);
            this.flowLayoutPanel1.Controls.Add(this.labelSConnected);
            this.flowLayoutPanel1.Controls.Add(this.labelSInProgress);
            this.flowLayoutPanel1.Controls.Add(this.labelSFinished);
            this.flowLayoutPanel1.Controls.Add(this.labelSError);
            this.flowLayoutPanel1.Controls.Add(this.labelSRecipeRunning);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(439, 298);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            this.flowLayoutPanel1.TabIndex = 14;
            // 
            // labelSError
            // 
            this.labelSError.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.labelSError, true);
            this.labelSError.Location = new System.Drawing.Point(3, 54);
            this.labelSError.Name = "labelSError";
            this.labelSError.Size = new System.Drawing.Size(10, 13);
            this.labelSError.TabIndex = 21;
            this.labelSError.Text = "-";
            // 
            // labelSRecipeRunning
            // 
            this.labelSRecipeRunning.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.labelSRecipeRunning, true);
            this.labelSRecipeRunning.Location = new System.Drawing.Point(3, 67);
            this.labelSRecipeRunning.Name = "labelSRecipeRunning";
            this.labelSRecipeRunning.Size = new System.Drawing.Size(10, 13);
            this.labelSRecipeRunning.TabIndex = 22;
            this.labelSRecipeRunning.Text = "-";
            // 
            // use_log
            // 
            this.use_log.AutoSize = true;
            this.use_log.Location = new System.Drawing.Point(19, 12);
            this.use_log.Name = "use_log";
            this.use_log.Size = new System.Drawing.Size(75, 17);
            this.use_log.TabIndex = 15;
            this.use_log.Text = "Log to File";
            this.use_log.UseVisualStyleBackColor = true;
            this.use_log.CheckedChanged += new System.EventHandler(this.use_log_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 503);
            this.Controls.Add(this.use_log);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.log);
            this.Controls.Add(this.checkBoxAutorun);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxRecipe);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonRun);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Remote Control";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.move1_to)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.move2_to)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxRecipe;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.CheckBox checkBoxAutorun;
        private System.Windows.Forms.TextBox log;
        private System.Windows.Forms.Button buttonMove1;
        private System.Windows.Forms.TextBox actual_axis1_position;
        private System.Windows.Forms.TextBox axis1_letter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.NumericUpDown move1_to;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox axis2_letter;
        private System.Windows.Forms.NumericUpDown move2_to;
        private System.Windows.Forms.TextBox actual_axis2_position;
        private System.Windows.Forms.Button buttonMove2;
        private System.Windows.Forms.Label labelSConnected;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label labelSInProgress;
        private System.Windows.Forms.Label labelSFinished;
        private System.Windows.Forms.Label labelSError;
        private System.Windows.Forms.Label labelSRecipeRunning;
        private System.Windows.Forms.CheckBox use_log;
    }
}

