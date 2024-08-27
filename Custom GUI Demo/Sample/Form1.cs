using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core;
using Core.Commands;


namespace Sample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Helper.InitDMC(this, recipe.panel_view);
            recipe.Init();
            timer1.Start();
            Base.StatusBar.StatusBarMessageReceived += StatusBar_StatusBarMessageReceived;

            Core.Tools.ToolPositioning.CanMoveObject = false; // Disables move tool, user unable to move objects using mouse.
        }

        private void StatusBar_StatusBarMessageReceived(object sender, Base.StatusBar.StatusBarMessageEventArgs e)
        {
            // status bar event
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Helper.CloseDMC();
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            DMC.Actions.ShowSettings();
        }

        bool check_for_error = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            buttonRun.Enabled = !(Base.State.is_running) && Base.State.is_connected_to_hardware;
            buttonCancel.Enabled = Base.State.is_connected_to_hardware;

            if (check_for_error && Base.State.is_error)
            {
                check_for_error = false;
                Base.Functions.ShowLastError();
            }
        }
        
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            DMC.Actions.ConnectDisconnect();
            buttonConnect.Text = Base.State.is_connected_to_hardware ? "Disconnect" : "Connect";
        }
        
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DMC.Actions.RecipeCancel();
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            DMC.Actions.RecipeRun();
            check_for_error = true;
        }
        
        private void zoomInButton_Click(object sender, EventArgs e)
        {
            Base.View.CurrentView.ZoomIn();
        }

        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            Base.View.CurrentView.ZoomOut();
        }

        private void zoomFitButton_Click(object sender, EventArgs e)
        {
            Base.View.CurrentView.ZoomFitObject();
        }

        private void zoomResetButton_Click(object sender, EventArgs e)
        {
            Base.View.CurrentView.ZoomReset();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowHideViewControl(tabControl1.SelectedIndex == 0);
        }

        void ShowHideViewControl(bool show)
        {
            zoomInButton.Visible = show;
            zoomOutButton.Visible = show;
            zoomFitButton.Visible = show;
            zoomResetButton.Visible = show;
        }

        private void showDMC_Click(object sender, EventArgs e)
        {
            Helper.ShowDMC();
        }
    }
}
