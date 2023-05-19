using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SendEmailPlugin
{
    public partial class SendEmaiGUI : Core.ICommandGUI
    {
        public SendEmaiGUI()
        {
            InitializeComponent();
        }

        bool isSetting = false;

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (isSetting) return;
            cmd.Password = maskedTextBox1.Text;
        }

        SendEmailCommand cmd;

        internal static Core.ICommandGUI Get(SendEmailCommand cmd)
        {
            SendEmaiGUI gui = new SendEmaiGUI();
            gui.cmd = cmd;
            gui.SetGUI();
            return gui;
        }

        private void SetGUI()
        {
            if (cmd == null) return;
            isSetting = true;
            flowLogin.Visible = cmd.use_login.value;
            maskedTextBox1.Text = cmd.Password;
            isSetting = false;
        }

        private void use_login_ParameterFieldValueChanged(object sender, Base.IParameter param)
        {
            flowLogin.Visible = cmd.use_login.value;
        }
    }
}
