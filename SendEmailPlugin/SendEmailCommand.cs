using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using Core.Commands;
using Base;
using System.Net.Mail;
using System.Net;

namespace SendEmailPlugin
{
    public class SendEmailCommand : ICommand
    {
        public StringParameter from = new StringParameter("from", "From", "you@yourcompany.com");
        public StringParameter to = new StringParameter("to", "To", "user@hotmail.com");
        public StringParameter host = new StringParameter("host", "Host", "smtp.googlemail.com");

        public StringParameter subject = new StringParameter("subject", "Subject", "Enter subject here");
        public StringParameter body = new StringParameter("body", "Body", "Enter text here");

        public BoolParameter use_login = new BoolParameter("use_login", "Use Login", false);
        public StringParameter user_name = new StringParameter("user_name", "User Name", "");
        public StringParameter password = new StringParameter("password", "Password", "");

        internal string Password
        {
            get { return Functions.Decrypt(password.Value, "l2348^#^$3)(&^3"); }
            set { password.Value = Functions.Encrypt(value, "l2348^#^$3)(&^3"); }
        }
        
        public static ICommand Create() { return new SendEmailCommand(); }
        public override System.Drawing.Bitmap GetIcon() { return Properties.Resources.envelope_32; }

        public const string UN = "send_email";
        public const string FN = "Send Email";
        public const string DESC = "Send email command";


        public SendEmailCommand()
            : base(SendEmailCommand.UN, SendEmailCommand.FN, SendEmailCommand.DESC)
        {
            Add(from); Add(to);
            Add(subject); Add(body);
            Add(host); Add(use_login); Add(user_name); Add(password);
        }

        public override string GetInfo()
        {
            return subject.Value;
        }

        public override bool IsControlCommand { get { return true; } }

        public override bool Compile()
        {
            is_cancel = false;
            if (!ParseAll()) return false;

            return true;
        }

        public override ICommandGUI GetGUI()
        {
            return SendEmaiGUI.Get(this);
        }

        bool is_cancel = false;

        public override bool Run()
        {
            if (!Compile()) return false;
            try
            {
                string output = body.Value;
                TextCommand.ParseText(body.Value, ref output); // parse variables and custom math expressions

                MailMessage mail = new MailMessage(from.Value, to.Value, subject.Value, output);


                SmtpClient client = new SmtpClient();
                client.Host = host.Value;

                if (use_login.value)
                {
                    client.Port = 587;
                    client.UseDefaultCredentials = false;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(user_name.Value, Password);
                }
                else
                {
                    client.Credentials = CredentialCache.DefaultNetworkCredentials;
                }
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Functions.ErrorF("Unable to send email. ", ex);
                StatusBar.Set(Functions.GetLastErrorMessage(), true, true);
                Functions.ShowLastError();
            }

            return true;
        }


    }
}
