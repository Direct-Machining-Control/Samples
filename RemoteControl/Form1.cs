using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace RemoteControl
{
    public partial class Form1 : Form
    {
        RCMClient client = new RCMClient();
        bool autorun = false;
        private StreamWriter log_stream;
        bool close = false;

        public Form1()
        {
            InitializeComponent();
            LoadRecipeList();
            label1.Text = "Not Connected";
            this.FormClosing += Form1_FormClosing;
            client.RCMEvent += Client_RCMEvent;
            //log_stream = new System.IO.StreamWriter(Application.StartupPath + "\\log.txt", false);
        }

        private void Client_RCMEvent(string message, bool send)
        {
            Log((send ? ">" : "<") + message, true, false);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (log_stream != null)
            {
                log_stream.Close();
                log_stream.Dispose();
            }
            close = true;

        }

        void Connect()
        {
            if (client.Connect(IPAddress.Loopback))
            {
                comboBoxRecipe.Text = client.GetActiveRecipe();
                buttonRun.Text = "Run";
                System.Threading.Tasks.Task.Factory.StartNew(() => { StatusMon(); });

                ReadGalvoSettings();

                //client.SetWindow(RCMClient.WindowType.PREVIEW, panel_preview);
            }
        }

        void StatusMon()
        {
            while (!close)
            {
                double position = 0;
                string error_message = string.Empty;
                if (client.GetPosition(axis1_letter.Text, out position, ref error_message))
                    actual_axis1_position.Invoke(new Action(() => { actual_axis1_position.Text = position.ToString(); }));

                if (client.GetPosition(axis2_letter.Text, out position, ref error_message))
                    actual_axis2_position.Invoke(new Action(() => { actual_axis2_position.Text = position.ToString(); }));

                this.Invoke(new Action(() => { labelSConnected.Text = "Connected: " + (client.IsConnectedToHardware ? "Yes" : "No"); }));
                this.Invoke(new Action(() => { labelSFinished.Text = "Recipe Idle: " + (client.IsRecipeIdle? "Yes" : "No"); }));
                this.Invoke(new Action(() => { labelSInProgress.Text = "In Progress: " + (client.IsInProgress ? "Yes" : "No"); }));
                this.Invoke(new Action(() => { labelSError.Text = "Error State: " + (client.IsInErrorState ? "Yes" : "No"); }));
                this.Invoke(new Action(() => { labelSRecipeRunning.Text = "Recipe Running: " + (client.IsRecipeRunning ? "Yes" : "No"); }));



                System.Threading.Thread.Sleep(100);
            }
        }

        void LoadRecipeList()
        {
            string path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Recipes");
            if (!Directory.Exists(path)) return;
            foreach (var fn in Directory.EnumerateFiles(path, "*.rcp", SearchOption.TopDirectoryOnly))
                comboBoxRecipe.Items.Add(fn);
        }

        public static void Sample1()
        {
            RCMClient client = new RCMClient();
            if (!client.Connect(IPAddress.Loopback))
                return;

            string error_message = string.Empty;
            if (!client.ConnectToHardware(ref error_message))
            {
                System.Windows.Forms.MessageBox.Show("Unable to connect to hardware. " + error_message);
                return;
            }

            if (!client.RecipeLoad(@"C:\Recipes\Recipe1.rcp", ref error_message))
            {
                System.Windows.Forms.MessageBox.Show("Unable to load recipe. " + error_message);
                return;
            }

            string error_msg = "";
            if (!client.RecipeRunSync(ref error_msg))
            {
                MessageBox.Show(error_msg);
                return;
            }
            System.Windows.Forms.MessageBox.Show("Recipe completed. ");
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (client.IsConnected) {
                if (!client.IsConnectedToHardware)
                {
                    string error_message = string.Empty;
                    if (!client.ConnectToHardware(ref error_message))
                    {
                        System.Windows.Forms.MessageBox.Show("Unable to connect to hardware. " + error_message);
                        return;
                    }
                }
                //client.RecipeRun();

                string msg = "";
                if (!client.RecipeRunSync(ref msg))
                    MessageBox.Show(msg);

                autorun = checkBoxAutorun.Checked;
            }
            else Connect();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (client.IsConnected)
            {
                string status = client.GetStatus();
                Log("Status: " + status);

                label1.Text = status;
                if (client.IsRecipeIdle && autorun)
                {
                    string error_message = string.Empty;
                    client.RecipeRun(ref error_message);

                    System.Threading.Thread.Sleep(100);
                    //client.Send("VIEW FITSCREEN");
                }
                else if (status.Contains("CANCELED"))
                    autorun = false;

                label2.Text = "Run Times: " + client.run_count.ToString();
            }
        }



        void ReadGalvoSettings()
        {
            double value = 0;
            string error_message = string.Empty;
            if (client.GetGalvo(1, RCMClient.GalvoSettings.offset_x, out value, ref error_message)) Log("Galvo offset X: " + value.ToString("0.###"));
            else Log("Galvo offset X read error");

            if (client.GetGalvo(1, RCMClient.GalvoSettings.offset_y, out value, ref error_message)) Log("Galvo offset Y: " + value.ToString("0.###"));
            else Log("Galvo offset Y read error");
        }

        void Log(string message, bool add_time = true, bool show_in_GUI = true)
        {
            if (message == null) return;
            message = (add_time ? DateTime.Now.ToString("H.mm.ss:FFF") + "\t" : "") + message + "\r\n";
            if (log_stream != null) log_stream.Write(message);

            if (show_in_GUI)
            {
                log.Invoke(new Action(() =>
                {
                    if (log.Text.Length > 1000) log.Text = log.Text.Substring(100, log.Text.Length - 100);
                    log.AppendText(message);
                }));
            }
        }


        private void buttonStop_Click(object sender, EventArgs e)
        {
            string error_message = string.Empty;
            if (client.IsConnected) { client.RecipeCancel(ref error_message); autorun = false; }
        }

        private void checkBoxAutorun_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxAutorun.Checked) autorun = false;
        }
        
        private void comboBoxRecipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (client.IsConnected && comboBoxRecipe.Text.Length > 0)
            {
                string error_message = string.Empty;
                client.RecipeUnloadAll(ref error_message);
                if (!client.RecipeLoad(comboBoxRecipe.Text, ref error_message))
                    System.Windows.Forms.MessageBox.Show("Unable to select recipe. " + error_message);
            }
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            string error_message = null;
            if (!client.Move(axis1_letter.Text, (double)move1_to.Value, ref error_message))
                System.Windows.Forms.MessageBox.Show("Unable to move axis. " + error_message);

        }

        private void buttonMove2_Click(object sender, EventArgs e)
        {
            string error_message = null;
            if (!client.Move(axis2_letter.Text, (double)move2_to.Value, ref error_message))
                System.Windows.Forms.MessageBox.Show("Unable to move axis. " + error_message);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Connect();
        }

        private void use_log_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (use_log.Checked)
                {
                    log_stream = new System.IO.StreamWriter(Application.StartupPath + "\\log.txt", false);
                }
                else
                {
                    var stream = log_stream;
                    log_stream = null;
                    if (stream != null) stream.Close();
                }
            }
            catch (Exception) { }
        }

        private void checksum_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if(client != null)
                client.UseChecksum = client.UseMessageID = checksum_checkbox.Checked;
        }
    }
}
