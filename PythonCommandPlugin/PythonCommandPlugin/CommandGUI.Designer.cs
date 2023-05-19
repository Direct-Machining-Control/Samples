
namespace PythonCommandPlugin
{
    partial class CommandGUI
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
            this.headerScript = new Base.HeaderGroupBox();
            this.panelFile = new System.Windows.Forms.FlowLayoutPanel();
            this.label22 = new System.Windows.Forms.Label();
            this.script_file_name = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new Base.NoSelectButton();
            this.headerArgs = new Base.HeaderGroupBox();
            this.exportDataGUI1 = new Core.Commands.ExportDataGUI();
            this.headerResult = new Base.HeaderGroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.result_file_name = new System.Windows.Forms.TextBox();
            this.noSelectButton1 = new Base.NoSelectButton();
            this.wait_for_finish = new Base.CheckBoolParameterField();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelFile.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.headerScript);
            this.flowLayoutPanel1.Controls.Add(this.panelFile);
            this.flowLayoutPanel1.Controls.Add(this.wait_for_finish);
            this.flowLayoutPanel1.Controls.Add(this.headerArgs);
            this.flowLayoutPanel1.Controls.Add(this.exportDataGUI1);
            this.flowLayoutPanel1.Controls.Add(this.headerResult);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(312, 572);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // headerScript
            // 
            this.headerScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.headerScript.Location = new System.Drawing.Point(3, 3);
            this.headerScript.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.headerScript.Name = "headerScript";
            this.headerScript.Size = new System.Drawing.Size(300, 19);
            this.headerScript.TabIndex = 57;
            this.headerScript.TabStop = false;
            this.headerScript.Text = "Script File";
            // 
            // panelFile
            // 
            this.panelFile.Controls.Add(this.label22);
            this.panelFile.Controls.Add(this.script_file_name);
            this.panelFile.Controls.Add(this.buttonBrowse);
            this.panelFile.Location = new System.Drawing.Point(0, 22);
            this.panelFile.Margin = new System.Windows.Forms.Padding(0);
            this.panelFile.Name = "panelFile";
            this.panelFile.Size = new System.Drawing.Size(306, 27);
            this.panelFile.TabIndex = 1;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(3, 6);
            this.label22.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(23, 13);
            this.label22.TabIndex = 1;
            this.label22.Text = "File";
            // 
            // script_file_name
            // 
            this.script_file_name.Location = new System.Drawing.Point(32, 3);
            this.script_file_name.Name = "script_file_name";
            this.script_file_name.Size = new System.Drawing.Size(178, 20);
            this.script_file_name.TabIndex = 0;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.buttonBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.buttonBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBrowse.Location = new System.Drawing.Point(216, 1);
            this.buttonBrowse.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(80, 23);
            this.buttonBrowse.TabIndex = 2;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // headerArgs
            // 
            this.headerArgs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.headerArgs.Location = new System.Drawing.Point(3, 81);
            this.headerArgs.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.headerArgs.Name = "headerArgs";
            this.headerArgs.Size = new System.Drawing.Size(300, 19);
            this.headerArgs.TabIndex = 58;
            this.headerArgs.TabStop = false;
            this.headerArgs.Text = "Script Arguments";
            // 
            // exportDataGUI1
            // 
            this.exportDataGUI1.AutoSize = true;
            this.exportDataGUI1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.exportDataGUI1.Location = new System.Drawing.Point(0, 100);
            this.exportDataGUI1.Margin = new System.Windows.Forms.Padding(0);
            this.exportDataGUI1.Name = "exportDataGUI1";
            this.exportDataGUI1.Size = new System.Drawing.Size(300, 423);
            this.exportDataGUI1.TabIndex = 3;
            // 
            // headerResult
            // 
            this.headerResult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.headerResult.Location = new System.Drawing.Point(3, 526);
            this.headerResult.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.headerResult.Name = "headerResult";
            this.headerResult.Size = new System.Drawing.Size(300, 19);
            this.headerResult.TabIndex = 59;
            this.headerResult.TabStop = false;
            this.headerResult.Text = "Output Script Result To File";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label1);
            this.flowLayoutPanel2.Controls.Add(this.result_file_name);
            this.flowLayoutPanel2.Controls.Add(this.noSelectButton1);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 545);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(306, 27);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "File";
            // 
            // result_file_name
            // 
            this.result_file_name.Location = new System.Drawing.Point(32, 3);
            this.result_file_name.Name = "result_file_name";
            this.result_file_name.Size = new System.Drawing.Size(178, 20);
            this.result_file_name.TabIndex = 0;
            // 
            // noSelectButton1
            // 
            this.noSelectButton1.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.noSelectButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.noSelectButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.noSelectButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.noSelectButton1.Location = new System.Drawing.Point(216, 1);
            this.noSelectButton1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.noSelectButton1.Name = "noSelectButton1";
            this.noSelectButton1.Size = new System.Drawing.Size(80, 23);
            this.noSelectButton1.TabIndex = 2;
            this.noSelectButton1.Text = "Browse";
            this.noSelectButton1.UseVisualStyleBackColor = true;
            this.noSelectButton1.Click += new System.EventHandler(this.noSelectButton1_Click);
            // 
            // wait_for_finish
            // 
            this.wait_for_finish._title = "wait_for_finish";
            this.wait_for_finish.AutoSize = true;
            this.wait_for_finish.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.wait_for_finish.Location = new System.Drawing.Point(3, 52);
            this.wait_for_finish.Name = "wait_for_finish";
            this.wait_for_finish.Size = new System.Drawing.Size(306, 23);
            this.wait_for_finish.TabIndex = 0;
            this.wait_for_finish.Value = false;
            // 
            // CommandGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "CommandGUI";
            this.Size = new System.Drawing.Size(312, 572);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panelFile.ResumeLayout(false);
            this.panelFile.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel panelFile;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox script_file_name;
        private Base.NoSelectButton buttonBrowse;
        private Core.Commands.ExportDataGUI exportDataGUI1;
        private Base.HeaderGroupBox headerScript;
        private Base.HeaderGroupBox headerArgs;
        private Base.HeaderGroupBox headerResult;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox result_file_name;
        private Base.NoSelectButton noSelectButton1;
        private Base.CheckBoolParameterField wait_for_finish;
    }
}
