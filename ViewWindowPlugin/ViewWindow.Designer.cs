namespace ViewWindowPlugin
{
    partial class ViewWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewWindow));
            this.panel = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnExpand = new Base.NoSelectButton();
            this.btnZoomFit = new Base.NoSelectButton();
            this.btnZoomReset = new Base.NoSelectButton();
            this.btnZoomIn = new Base.NoSelectButton();
            this.btnZoomOut = new Base.NoSelectButton();
            this.btnFitCamera = new Base.NoSelectButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Location = new System.Drawing.Point(94, 93);
            this.panel.Margin = new System.Windows.Forms.Padding(0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(72, 62);
            this.panel.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnExpand);
            this.flowLayoutPanel1.Controls.Add(this.btnZoomFit);
            this.flowLayoutPanel1.Controls.Add(this.btnZoomReset);
            this.flowLayoutPanel1.Controls.Add(this.btnZoomIn);
            this.flowLayoutPanel1.Controls.Add(this.btnZoomOut);
            this.flowLayoutPanel1.Controls.Add(this.btnFitCamera);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(300, 50);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // btnExpand
            // 
            this.btnExpand.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnExpand.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.btnExpand.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.btnExpand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExpand.Location = new System.Drawing.Point(1, 1);
            this.btnExpand.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnExpand.Name = "btnExpand";
            this.btnExpand.Size = new System.Drawing.Size(47, 47);
            this.btnExpand.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnExpand, "Expand/Collapse");
            this.btnExpand.UseVisualStyleBackColor = true;
            this.btnExpand.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnExpand_MouseDown_1);
            this.btnExpand.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnExpand_MouseMove);
            this.btnExpand.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnExpand_MouseUp);
            // 
            // btnZoomFit
            // 
            this.btnZoomFit.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnZoomFit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.btnZoomFit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.btnZoomFit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoomFit.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomFit.Image")));
            this.btnZoomFit.Location = new System.Drawing.Point(50, 1);
            this.btnZoomFit.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnZoomFit.Name = "btnZoomFit";
            this.btnZoomFit.Size = new System.Drawing.Size(47, 47);
            this.btnZoomFit.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnZoomFit, "Fit Object To Screen");
            this.btnZoomFit.UseVisualStyleBackColor = true;
            this.btnZoomFit.Click += new System.EventHandler(this.btnZoomFit_Click);
            // 
            // btnZoomReset
            // 
            this.btnZoomReset.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnZoomReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.btnZoomReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.btnZoomReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoomReset.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomReset.Image")));
            this.btnZoomReset.Location = new System.Drawing.Point(99, 1);
            this.btnZoomReset.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnZoomReset.Name = "btnZoomReset";
            this.btnZoomReset.Size = new System.Drawing.Size(47, 47);
            this.btnZoomReset.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnZoomReset, "Zoom Reset");
            this.btnZoomReset.UseVisualStyleBackColor = true;
            this.btnZoomReset.Click += new System.EventHandler(this.btnZoomReset_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnZoomIn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.btnZoomIn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.btnZoomIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomIn.Image")));
            this.btnZoomIn.Location = new System.Drawing.Point(148, 1);
            this.btnZoomIn.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(47, 47);
            this.btnZoomIn.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnZoomIn, "Zoom In");
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnZoomOut.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.btnZoomOut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.btnZoomOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomOut.Image")));
            this.btnZoomOut.Location = new System.Drawing.Point(197, 1);
            this.btnZoomOut.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(47, 47);
            this.btnZoomOut.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnZoomOut, "Zoom Out");
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btnFitCamera
            // 
            this.btnFitCamera.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnFitCamera.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.btnFitCamera.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.btnFitCamera.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFitCamera.Image = ((System.Drawing.Image)(resources.GetObject("btnFitCamera.Image")));
            this.btnFitCamera.Location = new System.Drawing.Point(246, 1);
            this.btnFitCamera.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnFitCamera.Name = "btnFitCamera";
            this.btnFitCamera.Size = new System.Drawing.Size(47, 47);
            this.btnFitCamera.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btnFitCamera, "Fit Camera");
            this.btnFitCamera.UseVisualStyleBackColor = true;
            this.btnFitCamera.Click += new System.EventHandler(this.btnFitCamera_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "aim_48.png");
            this.imageList1.Images.SetKeyName(1, "arrowhead_down_48.png");
            this.imageList1.Images.SetKeyName(2, "arrowhead_up_48.png");
            this.imageList1.Images.SetKeyName(3, "zoom_fit.png");
            this.imageList1.Images.SetKeyName(4, "zoom_in_48.png");
            this.imageList1.Images.SetKeyName(5, "zoom_out_48.png");
            this.imageList1.Images.SetKeyName(6, "zoom_reset1.png");
            // 
            // ViewWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimizeBox = false;
            this.Name = "ViewWindow";
            this.ShowIcon = false;
            this.Text = "View";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ViewWindow_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ViewWindow_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ViewWindow_MouseMove);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Timer timer1;
        private Base.NoSelectButton btnExpand;
        private Base.NoSelectButton btnZoomFit;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Base.NoSelectButton btnZoomReset;
        private Base.NoSelectButton btnZoomIn;
        private Base.NoSelectButton btnZoomOut;
        private Base.NoSelectButton btnFitCamera;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ImageList imageList1;
    }
}