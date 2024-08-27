namespace Sample
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel_tools = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonConnect = new Base.NoSelectButton();
            this.buttonRun = new Base.NoSelectButton();
            this.buttonCancel = new Base.NoSelectButton();
            this.buttonSettings = new Base.NoSelectButton();
            this.showDMC = new Base.NoSelectButton();
            this.zoomInButton = new Base.NoSelectButton();
            this.zoomOutButton = new Base.NoSelectButton();
            this.zoomFitButton = new Base.NoSelectButton();
            this.zoomResetButton = new Base.NoSelectButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageEdit = new System.Windows.Forms.TabPage();
            this.recipe = new Sample.RecipeUC();
            this.tabPageControl = new System.Windows.Forms.TabPage();
            this.camWindow1 = new Sample.CamWindow();
            this.joystickUC1 = new Sample.JoystickUC();
            this.tabPageIO = new System.Windows.Forms.TabPage();
            this.inputOutputUC1 = new Sample.InputOutputUC();
            this.tabPowerMeter = new System.Windows.Forms.TabPage();
            this.powerMeterUC1 = new Sample.PowerMeterUC();
            this.visionTab = new System.Windows.Forms.TabPage();
            this.visionUC1 = new Sample.VisionUC();
            this.panel_tools.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageEdit.SuspendLayout();
            this.tabPageControl.SuspendLayout();
            this.tabPageIO.SuspendLayout();
            this.tabPowerMeter.SuspendLayout();
            this.visionTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel_tools
            // 
            this.panel_tools.Controls.Add(this.flowLayoutPanel1);
            this.panel_tools.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_tools.Location = new System.Drawing.Point(0, 0);
            this.panel_tools.Name = "panel_tools";
            this.panel_tools.Size = new System.Drawing.Size(1036, 70);
            this.panel_tools.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.buttonConnect);
            this.flowLayoutPanel1.Controls.Add(this.buttonRun);
            this.flowLayoutPanel1.Controls.Add(this.buttonCancel);
            this.flowLayoutPanel1.Controls.Add(this.buttonSettings);
            this.flowLayoutPanel1.Controls.Add(this.showDMC);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(932, 61);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // buttonConnect
            // 
            this.buttonConnect.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonConnect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.buttonConnect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.buttonConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonConnect.Location = new System.Drawing.Point(3, 3);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(80, 50);
            this.buttonConnect.TabIndex = 2;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonRun
            // 
            this.buttonRun.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonRun.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.buttonRun.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.buttonRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRun.Location = new System.Drawing.Point(89, 3);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(80, 50);
            this.buttonRun.TabIndex = 3;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.buttonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Location = new System.Drawing.Point(175, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(80, 50);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.buttonSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.buttonSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSettings.Location = new System.Drawing.Point(261, 3);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(80, 50);
            this.buttonSettings.TabIndex = 1;
            this.buttonSettings.Text = "Show Settings";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // showDMC
            // 
            this.showDMC.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.showDMC.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.showDMC.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.showDMC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showDMC.Location = new System.Drawing.Point(347, 3);
            this.showDMC.Name = "showDMC";
            this.showDMC.Size = new System.Drawing.Size(80, 50);
            this.showDMC.TabIndex = 5;
            this.showDMC.Text = "Show DMC";
            this.showDMC.UseVisualStyleBackColor = true;
            this.showDMC.Click += new System.EventHandler(this.showDMC_Click);
            // 
            // zoomInButton
            // 
            this.zoomInButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomInButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.zoomInButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.zoomInButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.zoomInButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zoomInButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomInButton.Image")));
            this.zoomInButton.Location = new System.Drawing.Point(973, 96);
            this.zoomInButton.Margin = new System.Windows.Forms.Padding(2);
            this.zoomInButton.Name = "zoomInButton";
            this.zoomInButton.Size = new System.Drawing.Size(54, 54);
            this.zoomInButton.TabIndex = 4;
            this.zoomInButton.UseVisualStyleBackColor = true;
            this.zoomInButton.Click += new System.EventHandler(this.zoomInButton_Click);
            // 
            // zoomOutButton
            // 
            this.zoomOutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomOutButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.zoomOutButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.zoomOutButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.zoomOutButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zoomOutButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomOutButton.Image")));
            this.zoomOutButton.Location = new System.Drawing.Point(973, 154);
            this.zoomOutButton.Margin = new System.Windows.Forms.Padding(2);
            this.zoomOutButton.Name = "zoomOutButton";
            this.zoomOutButton.Size = new System.Drawing.Size(54, 54);
            this.zoomOutButton.TabIndex = 5;
            this.zoomOutButton.UseVisualStyleBackColor = true;
            this.zoomOutButton.Click += new System.EventHandler(this.zoomOutButton_Click);
            // 
            // zoomFitButton
            // 
            this.zoomFitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomFitButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.zoomFitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.zoomFitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.zoomFitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zoomFitButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomFitButton.Image")));
            this.zoomFitButton.Location = new System.Drawing.Point(973, 212);
            this.zoomFitButton.Margin = new System.Windows.Forms.Padding(2);
            this.zoomFitButton.Name = "zoomFitButton";
            this.zoomFitButton.Size = new System.Drawing.Size(54, 54);
            this.zoomFitButton.TabIndex = 6;
            this.zoomFitButton.UseVisualStyleBackColor = true;
            this.zoomFitButton.Click += new System.EventHandler(this.zoomFitButton_Click);
            // 
            // zoomResetButton
            // 
            this.zoomResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomResetButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.zoomResetButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.zoomResetButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.zoomResetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zoomResetButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomResetButton.Image")));
            this.zoomResetButton.Location = new System.Drawing.Point(973, 270);
            this.zoomResetButton.Margin = new System.Windows.Forms.Padding(2);
            this.zoomResetButton.Name = "zoomResetButton";
            this.zoomResetButton.Size = new System.Drawing.Size(54, 54);
            this.zoomResetButton.TabIndex = 7;
            this.zoomResetButton.UseVisualStyleBackColor = true;
            this.zoomResetButton.Click += new System.EventHandler(this.zoomResetButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageEdit);
            this.tabControl1.Controls.Add(this.tabPageControl);
            this.tabControl1.Controls.Add(this.tabPageIO);
            this.tabControl1.Controls.Add(this.tabPowerMeter);
            this.tabControl1.Controls.Add(this.visionTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 70);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1036, 567);
            this.tabControl1.TabIndex = 8;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPageEdit
            // 
            this.tabPageEdit.Controls.Add(this.recipe);
            this.tabPageEdit.Location = new System.Drawing.Point(4, 22);
            this.tabPageEdit.Name = "tabPageEdit";
            this.tabPageEdit.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEdit.Size = new System.Drawing.Size(1028, 541);
            this.tabPageEdit.TabIndex = 0;
            this.tabPageEdit.Text = "EDIT";
            this.tabPageEdit.UseVisualStyleBackColor = true;
            // 
            // recipe
            // 
            this.recipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.recipe.Location = new System.Drawing.Point(3, 3);
            this.recipe.Name = "recipe";
            this.recipe.Size = new System.Drawing.Size(1022, 535);
            this.recipe.TabIndex = 2;
            // 
            // tabPageControl
            // 
            this.tabPageControl.Controls.Add(this.camWindow1);
            this.tabPageControl.Controls.Add(this.joystickUC1);
            this.tabPageControl.Location = new System.Drawing.Point(4, 22);
            this.tabPageControl.Name = "tabPageControl";
            this.tabPageControl.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageControl.Size = new System.Drawing.Size(1028, 541);
            this.tabPageControl.TabIndex = 1;
            this.tabPageControl.Text = "CONTROL";
            this.tabPageControl.UseVisualStyleBackColor = true;
            // 
            // camWindow1
            // 
            this.camWindow1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.camWindow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.camWindow1.Location = new System.Drawing.Point(426, 3);
            this.camWindow1.Name = "camWindow1";
            this.camWindow1.Size = new System.Drawing.Size(599, 535);
            this.camWindow1.TabIndex = 0;
            // 
            // joystickUC1
            // 
            this.joystickUC1.Dock = System.Windows.Forms.DockStyle.Left;
            this.joystickUC1.Location = new System.Drawing.Point(3, 3);
            this.joystickUC1.Name = "joystickUC1";
            this.joystickUC1.Size = new System.Drawing.Size(423, 535);
            this.joystickUC1.TabIndex = 1;
            // 
            // tabPageIO
            // 
            this.tabPageIO.Controls.Add(this.inputOutputUC1);
            this.tabPageIO.Location = new System.Drawing.Point(4, 22);
            this.tabPageIO.Name = "tabPageIO";
            this.tabPageIO.Size = new System.Drawing.Size(1028, 541);
            this.tabPageIO.TabIndex = 2;
            this.tabPageIO.Text = "IO";
            this.tabPageIO.UseVisualStyleBackColor = true;
            // 
            // inputOutputUC1
            // 
            this.inputOutputUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputOutputUC1.Location = new System.Drawing.Point(0, 0);
            this.inputOutputUC1.Name = "inputOutputUC1";
            this.inputOutputUC1.Size = new System.Drawing.Size(1028, 541);
            this.inputOutputUC1.TabIndex = 0;
            // 
            // tabPowerMeter
            // 
            this.tabPowerMeter.Controls.Add(this.powerMeterUC1);
            this.tabPowerMeter.Location = new System.Drawing.Point(4, 22);
            this.tabPowerMeter.Name = "tabPowerMeter";
            this.tabPowerMeter.Size = new System.Drawing.Size(1028, 541);
            this.tabPowerMeter.TabIndex = 3;
            this.tabPowerMeter.Text = "Power Meter";
            this.tabPowerMeter.UseVisualStyleBackColor = true;
            // 
            // powerMeterUC1
            // 
            this.powerMeterUC1.Location = new System.Drawing.Point(0, 0);
            this.powerMeterUC1.Name = "powerMeterUC1";
            this.powerMeterUC1.Size = new System.Drawing.Size(406, 303);
            this.powerMeterUC1.TabIndex = 0;
            // 
            // visionTab
            // 
            this.visionTab.Controls.Add(this.visionUC1);
            this.visionTab.Location = new System.Drawing.Point(4, 22);
            this.visionTab.Name = "visionTab";
            this.visionTab.Padding = new System.Windows.Forms.Padding(3);
            this.visionTab.Size = new System.Drawing.Size(1028, 541);
            this.visionTab.TabIndex = 4;
            this.visionTab.Text = "Vision";
            this.visionTab.UseVisualStyleBackColor = true;
            // 
            // visionUC1
            // 
            this.visionUC1.Location = new System.Drawing.Point(2, 0);
            this.visionUC1.Name = "visionUC1";
            this.visionUC1.Size = new System.Drawing.Size(723, 538);
            this.visionUC1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 637);
            this.Controls.Add(this.zoomResetButton);
            this.Controls.Add(this.zoomFitButton);
            this.Controls.Add(this.zoomOutButton);
            this.Controls.Add(this.zoomInButton);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel_tools);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Demo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel_tools.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageEdit.ResumeLayout(false);
            this.tabPageControl.ResumeLayout(false);
            this.tabPageIO.ResumeLayout(false);
            this.tabPowerMeter.ResumeLayout(false);
            this.visionTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel_tools;
        private Base.NoSelectButton buttonCancel;
        private Base.NoSelectButton buttonRun;
        private Base.NoSelectButton buttonConnect;
        private Base.NoSelectButton buttonSettings;
        private Base.NoSelectButton zoomInButton;
        private RecipeUC recipe;
        private Base.NoSelectButton zoomOutButton;
        private Base.NoSelectButton zoomFitButton;
        private Base.NoSelectButton zoomResetButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageEdit;
        private System.Windows.Forms.TabPage tabPageControl;
        private CamWindow camWindow1;
        private JoystickUC joystickUC1;
        private System.Windows.Forms.TabPage tabPageIO;
        private InputOutputUC inputOutputUC1;
        private Base.NoSelectButton showDMC;
        private System.Windows.Forms.TabPage tabPowerMeter;
        private PowerMeterUC powerMeterUC1;
        private System.Windows.Forms.TabPage visionTab;
        private VisionUC visionUC1;
    }
}

