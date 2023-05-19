namespace SendEmailPlugin
{
    partial class SendEmaiGUI
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
            this.from = new Base.StringParameterField();
            this.to = new Base.StringParameterField();
            this.host = new Base.StringParameterField();
            this.use_login = new Base.CheckBoolParameterField();
            this.flowLogin = new System.Windows.Forms.FlowLayoutPanel();
            this.user_name = new Base.StringParameterField();
            this.label2 = new System.Windows.Forms.Label();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.headerGroupBox1 = new Base.HeaderGroupBox();
            this.subject = new Base.StringParameterField();
            this.label1 = new System.Windows.Forms.Label();
            this.body = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.from);
            this.flowLayoutPanel1.Controls.Add(this.to);
            this.flowLayoutPanel1.Controls.Add(this.host);
            this.flowLayoutPanel1.Controls.Add(this.use_login);
            this.flowLayoutPanel1.Controls.Add(this.flowLogin);
            this.flowLayoutPanel1.Controls.Add(this.headerGroupBox1);
            this.flowLayoutPanel1.Controls.Add(this.subject);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.body);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(312, 313);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // from
            // 
            this.from._title = "from";
            this.from.AutoSize = true;
            this.from.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.from.CanBeEmpty = false;
            this.from.Location = new System.Drawing.Point(0, 0);
            this.from.Margin = new System.Windows.Forms.Padding(0);
            this.from.Name = "from";
            this.from.Size = new System.Drawing.Size(303, 24);
            this.from.TabIndex = 0;
            this.from.ValueOnlyFromList = false;
            // 
            // to
            // 
            this.to._title = "to";
            this.to.AutoSize = true;
            this.to.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.to.CanBeEmpty = false;
            this.to.Location = new System.Drawing.Point(0, 24);
            this.to.Margin = new System.Windows.Forms.Padding(0);
            this.to.Name = "to";
            this.to.Size = new System.Drawing.Size(303, 24);
            this.to.TabIndex = 1;
            this.to.ValueOnlyFromList = false;
            // 
            // host
            // 
            this.host._title = "host";
            this.host.AutoSize = true;
            this.host.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.host.CanBeEmpty = false;
            this.host.Location = new System.Drawing.Point(0, 48);
            this.host.Margin = new System.Windows.Forms.Padding(0);
            this.host.Name = "host";
            this.host.Size = new System.Drawing.Size(303, 24);
            this.host.TabIndex = 2;
            this.host.ValueOnlyFromList = false;
            // 
            // use_login
            // 
            this.use_login._title = "use_login";
            this.use_login.AutoSize = true;
            this.use_login.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.use_login.Location = new System.Drawing.Point(3, 75);
            this.use_login.Name = "use_login";
            this.use_login.Size = new System.Drawing.Size(306, 23);
            this.use_login.TabIndex = 8;
            this.use_login.Value = false;
            this.use_login.ParameterFieldValueChanged += new Base.GUI.ParameterFieldValueChangedDelegate(this.use_login_ParameterFieldValueChanged);
            // 
            // flowLogin
            // 
            this.flowLogin.Controls.Add(this.user_name);
            this.flowLogin.Controls.Add(this.label2);
            this.flowLogin.Controls.Add(this.maskedTextBox1);
            this.flowLogin.Location = new System.Drawing.Point(0, 101);
            this.flowLogin.Margin = new System.Windows.Forms.Padding(0);
            this.flowLogin.Name = "flowLogin";
            this.flowLogin.Size = new System.Drawing.Size(300, 53);
            this.flowLogin.TabIndex = 6;
            // 
            // user_name
            // 
            this.user_name._title = "user_name";
            this.user_name.AutoSize = true;
            this.user_name.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.user_name.CanBeEmpty = false;
            this.user_name.Location = new System.Drawing.Point(0, 0);
            this.user_name.Margin = new System.Windows.Forms.Padding(0);
            this.user_name.Name = "user_name";
            this.user_name.Size = new System.Drawing.Size(303, 24);
            this.user_name.TabIndex = 3;
            this.user_name.ValueOnlyFromList = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password";
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(150, 24);
            this.maskedTextBox1.Margin = new System.Windows.Forms.Padding(0);
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.PasswordChar = '*';
            this.maskedTextBox1.Size = new System.Drawing.Size(150, 20);
            this.maskedTextBox1.TabIndex = 4;
            this.maskedTextBox1.TextChanged += new System.EventHandler(this.maskedTextBox1_TextChanged);
            // 
            // headerGroupBox1
            // 
            this.headerGroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.headerGroupBox1.Location = new System.Drawing.Point(3, 157);
            this.headerGroupBox1.Name = "headerGroupBox1";
            this.headerGroupBox1.Size = new System.Drawing.Size(300, 19);
            this.headerGroupBox1.TabIndex = 7;
            this.headerGroupBox1.TabStop = false;
            this.headerGroupBox1.Text = "Message";
            // 
            // subject
            // 
            this.subject._title = "subject";
            this.subject.AutoSize = true;
            this.subject.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.subject.CanBeEmpty = false;
            this.subject.Location = new System.Drawing.Point(0, 179);
            this.subject.Margin = new System.Windows.Forms.Padding(0);
            this.subject.Name = "subject";
            this.subject.Size = new System.Drawing.Size(303, 24);
            this.subject.TabIndex = 3;
            this.subject.ValueOnlyFromList = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 203);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Body";
            // 
            // body
            // 
            this.body.Location = new System.Drawing.Point(3, 219);
            this.body.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.body.Multiline = true;
            this.body.Name = "body";
            this.body.Size = new System.Drawing.Size(297, 94);
            this.body.TabIndex = 5;
            // 
            // SendEmaiGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "SendEmaiGUI";
            this.Size = new System.Drawing.Size(312, 313);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLogin.ResumeLayout(false);
            this.flowLogin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Base.StringParameterField from;
        private Base.StringParameterField to;
        private Base.StringParameterField host;
        private Base.StringParameterField subject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox body;
        private Base.CheckBoolParameterField use_login;
        private System.Windows.Forms.FlowLayoutPanel flowLogin;
        private Base.StringParameterField user_name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private Base.HeaderGroupBox headerGroupBox1;
    }
}
