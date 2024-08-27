namespace Sample
{
    partial class RecipeUC
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
            this.panel_view = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel_command = new System.Windows.Forms.Panel();
            this.buttonImport = new Base.NoSelectButton();
            this.buttonCompile = new Base.NoSelectButton();
            this.flowButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.createPolyline = new Base.NoSelectButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.openRecipe = new Base.NoSelectButton();
            this.contextMenuStripRecipes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.browseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.item1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowButtons.SuspendLayout();
            this.contextMenuStripRecipes.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_view
            // 
            this.panel_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_view.Location = new System.Drawing.Point(578, 0);
            this.panel_view.Name = "panel_view";
            this.panel_view.Size = new System.Drawing.Size(36, 386);
            this.panel_view.TabIndex = 4;
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.Location = new System.Drawing.Point(86, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(182, 386);
            this.treeView1.TabIndex = 5;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.treeView1_KeyUp);
            // 
            // panel_command
            // 
            this.panel_command.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_command.Location = new System.Drawing.Point(268, 0);
            this.panel_command.Name = "panel_command";
            this.panel_command.Size = new System.Drawing.Size(310, 386);
            this.panel_command.TabIndex = 7;
            // 
            // buttonImport
            // 
            this.buttonImport.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonImport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.buttonImport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.buttonImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonImport.Location = new System.Drawing.Point(3, 3);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(80, 50);
            this.buttonImport.TabIndex = 8;
            this.buttonImport.Text = "Import CAD";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // buttonCompile
            // 
            this.buttonCompile.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonCompile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.buttonCompile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.buttonCompile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCompile.Location = new System.Drawing.Point(3, 59);
            this.buttonCompile.Name = "buttonCompile";
            this.buttonCompile.Size = new System.Drawing.Size(80, 50);
            this.buttonCompile.TabIndex = 9;
            this.buttonCompile.Text = "Compile";
            this.buttonCompile.UseVisualStyleBackColor = true;
            this.buttonCompile.Click += new System.EventHandler(this.buttonCompile_Click);
            // 
            // flowButtons
            // 
            this.flowButtons.AutoSize = true;
            this.flowButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowButtons.Controls.Add(this.buttonImport);
            this.flowButtons.Controls.Add(this.buttonCompile);
            this.flowButtons.Controls.Add(this.createPolyline);
            this.flowButtons.Controls.Add(this.openRecipe);
            this.flowButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowButtons.Location = new System.Drawing.Point(0, 0);
            this.flowButtons.Name = "flowButtons";
            this.flowButtons.Size = new System.Drawing.Size(86, 386);
            this.flowButtons.TabIndex = 10;
            // 
            // createPolyline
            // 
            this.createPolyline.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.createPolyline.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.createPolyline.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.createPolyline.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createPolyline.Location = new System.Drawing.Point(3, 115);
            this.createPolyline.Name = "createPolyline";
            this.createPolyline.Size = new System.Drawing.Size(80, 50);
            this.createPolyline.TabIndex = 10;
            this.createPolyline.Text = "Polyline";
            this.createPolyline.UseVisualStyleBackColor = true;
            this.createPolyline.Click += new System.EventHandler(this.createPolyline_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // openRecipe
            // 
            this.openRecipe.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.openRecipe.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.openRecipe.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.openRecipe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openRecipe.Location = new System.Drawing.Point(3, 171);
            this.openRecipe.Name = "openRecipe";
            this.openRecipe.Size = new System.Drawing.Size(80, 50);
            this.openRecipe.TabIndex = 11;
            this.openRecipe.Text = "Recipe ...";
            this.openRecipe.UseVisualStyleBackColor = true;
            this.openRecipe.Click += new System.EventHandler(this.openRecipe_Click);
            // 
            // contextMenuStripRecipes
            // 
            this.contextMenuStripRecipes.Font = new System.Drawing.Font("Calibri", 14F);
            this.contextMenuStripRecipes.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStripRecipes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.browseToolStripMenuItem,
            this.item1ToolStripMenuItem});
            this.contextMenuStripRecipes.Name = "contextMenuStripRecipes";
            this.contextMenuStripRecipes.ShowImageMargin = false;
            this.contextMenuStripRecipes.Size = new System.Drawing.Size(156, 82);
            // 
            // browseToolStripMenuItem
            // 
            this.browseToolStripMenuItem.Name = "browseToolStripMenuItem";
            this.browseToolStripMenuItem.Size = new System.Drawing.Size(155, 28);
            this.browseToolStripMenuItem.Text = "Browse...";
            // 
            // item1ToolStripMenuItem
            // 
            this.item1ToolStripMenuItem.Name = "item1ToolStripMenuItem";
            this.item1ToolStripMenuItem.Size = new System.Drawing.Size(155, 28);
            this.item1ToolStripMenuItem.Text = "Item1";
            // 
            // RecipeUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_view);
            this.Controls.Add(this.panel_command);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.flowButtons);
            this.Name = "RecipeUC";
            this.Size = new System.Drawing.Size(614, 386);
            this.flowButtons.ResumeLayout(false);
            this.contextMenuStripRecipes.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TreeView treeView1;
        public System.Windows.Forms.Panel panel_view;
        private System.Windows.Forms.Panel panel_command;
        private Base.NoSelectButton buttonImport;
        private Base.NoSelectButton buttonCompile;
        private System.Windows.Forms.FlowLayoutPanel flowButtons;
        private System.Windows.Forms.Timer timer1;
        private Base.NoSelectButton createPolyline;
        private Base.NoSelectButton openRecipe;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripRecipes;
        private System.Windows.Forms.ToolStripMenuItem browseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem item1ToolStripMenuItem;
    }
}
