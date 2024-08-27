namespace Sample
{
    partial class VisionUC
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
            this.create_pattern = new Base.NoSelectButton();
            this.result_button = new Base.NoSelectButton();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.noSelectButton3 = new Base.NoSelectButton();
            this.edit_button = new Base.NoSelectButton();
            this.load_button = new Base.NoSelectButton();
            this.save_button = new Base.NoSelectButton();
            this.get_value = new Base.NoSelectButton();
            this.SuspendLayout();
            // 
            // create_pattern
            // 
            this.create_pattern.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.create_pattern.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.create_pattern.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.create_pattern.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.create_pattern.Location = new System.Drawing.Point(3, 3);
            this.create_pattern.Name = "create_pattern";
            this.create_pattern.Size = new System.Drawing.Size(80, 50);
            this.create_pattern.TabIndex = 7;
            this.create_pattern.Text = "Create Pattern";
            this.create_pattern.UseVisualStyleBackColor = true;
            this.create_pattern.Click += new System.EventHandler(this.create_pattern_Click);
            // 
            // result_button
            // 
            this.result_button.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.result_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.result_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.result_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.result_button.Location = new System.Drawing.Point(175, 3);
            this.result_button.Name = "result_button";
            this.result_button.Size = new System.Drawing.Size(80, 50);
            this.result_button.TabIndex = 8;
            this.result_button.Text = "Pattern Result";
            this.result_button.UseVisualStyleBackColor = true;
            this.result_button.Click += new System.EventHandler(this.result_button_Click);
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(3, 59);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(621, 293);
            this.logBox.TabIndex = 9;
            this.logBox.Text = "";
            // 
            // noSelectButton3
            // 
            this.noSelectButton3.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.noSelectButton3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.noSelectButton3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.noSelectButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.noSelectButton3.Location = new System.Drawing.Point(89, 3);
            this.noSelectButton3.Name = "noSelectButton3";
            this.noSelectButton3.Size = new System.Drawing.Size(80, 50);
            this.noSelectButton3.TabIndex = 10;
            this.noSelectButton3.Text = "Test Pattern";
            this.noSelectButton3.UseVisualStyleBackColor = true;
            this.noSelectButton3.Click += new System.EventHandler(this.test_button);
            // 
            // edit_button
            // 
            this.edit_button.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.edit_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.edit_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.edit_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.edit_button.Location = new System.Drawing.Point(261, 3);
            this.edit_button.Name = "edit_button";
            this.edit_button.Size = new System.Drawing.Size(80, 50);
            this.edit_button.TabIndex = 11;
            this.edit_button.Text = "Pattern Edit";
            this.edit_button.UseVisualStyleBackColor = true;
            this.edit_button.Click += new System.EventHandler(this.edit_button_Click);
            // 
            // load_button
            // 
            this.load_button.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.load_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.load_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.load_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.load_button.Location = new System.Drawing.Point(347, 3);
            this.load_button.Name = "load_button";
            this.load_button.Size = new System.Drawing.Size(80, 50);
            this.load_button.TabIndex = 12;
            this.load_button.Text = "Load Preset";
            this.load_button.UseVisualStyleBackColor = true;
            this.load_button.Click += new System.EventHandler(this.load_button_Click);
            // 
            // save_button
            // 
            this.save_button.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.save_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.save_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.save_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.save_button.Location = new System.Drawing.Point(433, 3);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(80, 50);
            this.save_button.TabIndex = 13;
            this.save_button.Text = "Save Preset";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // get_value
            // 
            this.get_value.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.get_value.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(181)))));
            this.get_value.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.get_value.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.get_value.Location = new System.Drawing.Point(519, 3);
            this.get_value.Name = "get_value";
            this.get_value.Size = new System.Drawing.Size(80, 50);
            this.get_value.TabIndex = 14;
            this.get_value.Text = "Get Param Value";
            this.get_value.UseVisualStyleBackColor = true;
            this.get_value.Click += new System.EventHandler(this.get_value_Click);
            // 
            // VisionUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.get_value);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.load_button);
            this.Controls.Add(this.edit_button);
            this.Controls.Add(this.noSelectButton3);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.result_button);
            this.Controls.Add(this.create_pattern);
            this.Name = "VisionUC";
            this.Size = new System.Drawing.Size(627, 355);
            this.ResumeLayout(false);

        }

        #endregion

        private Base.NoSelectButton create_pattern;
        private Base.NoSelectButton result_button;
        private System.Windows.Forms.RichTextBox logBox;
        private Base.NoSelectButton noSelectButton3;
        private Base.NoSelectButton edit_button;
        private Base.NoSelectButton load_button;
        private Base.NoSelectButton save_button;
        private Base.NoSelectButton get_value;
    }
}
