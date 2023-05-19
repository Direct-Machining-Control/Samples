using Base;
using DMC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ViewWindowPlugin
{
    public partial class ViewWindow : Form
    {
        public ViewWindow()
        {
            InitializeComponent();
            panel.Dock = DockStyle.Fill;
            btnExpand.Image = Properties.Resources.arrowhead_down_32;
        }

        static int w = 600; // expanded window width
        static int h = 500; // expanded window height

        bool is_expanded = false;
        FormWindowState LastWindowState = FormWindowState.Normal;
        static ViewWindow form = null;
        StateMonitoring state_monitoring = new StateMonitoring();

        public static void ShowForm()
        {
            if (!Plugin.plugin.settings.ShowPreviewWindow) return;

            if (Settings.main_window.InvokeRequired)
            {
                Settings.main_window.BeginInvoke((MethodInvoker)delegate ()
                {
                    if (form == null)
                    {
                        form = new ViewWindow();
                        form.FormClosing += Form_FormClosing;
                    }
                    form.Show();
                });
            }
            else
            {
                if (form == null)
                {
                    form = new ViewWindow();
                    form.FormClosing += Form_FormClosing;
                }
                form.Show();
            }
        }

        

        private static void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Base.Settings.InBackgroundMode) RecipeListUC.CreateViewPanel();
            Functions.Action("ViewWindow", "FormClosing");
            form = null;
        }

        public static void HideForm()
        {
            if (form != null)
                form.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!Visible) return;

                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch(); sw.Start();
                if (timer1.Interval != Base.View.ViewRefreshInterval)
                    timer1.Interval = Base.View.ViewRefreshInterval;

                timer1.Stop();

                if (Base.View.CurrentView != null && (DateTime.Now.Subtract(Base.View.CurrentView.LastFrame()).TotalMilliseconds > timer1.Interval))
                {
                    Base.View.CurrentView.Refresh();
                    long ms = sw.ElapsedMilliseconds;

                    if (ms < 40) Base.View.ViewRefreshInterval = 50;
                    else Base.View.ViewRefreshInterval = 100;
                }

                if (WindowState == FormWindowState.Maximized)
                {
                    WindowState = FormWindowState.Normal;

                    if (Base.Settings.main_window != null)
                    {
                        Base.Settings.main_window.WindowState = FormWindowState.Maximized;
                        Base.Settings.main_window.BringToFront();
                    }
                }
                state_monitoring.UpdateState(this);

                timer1.Start();
            }
            catch (Exception ex)
            {
                Functions.Error("Main timmer error. ", ex);
            }
        }

        private void ViewWindow_Load(object sender, EventArgs e)
        {
            try
            {
                Location = Plugin.plugin.settings.GetWindowPosition();
                Base.Settings.UpdateViewInBackgroundMode = true;
                Width = btnExpand.Width+2;
                Height = btnExpand.Height+2;
                BackColor = Plugin.color_main;
                GUIColors.SetColors(this.Controls, true);
            }
            catch (Exception ex)
            {
                Functions.Error("Unable to get display. ", ex);
            }
        }

        private void btnExpand_Click(object sender, EventArgs e)
        {
            Expand(!is_expanded);
        }

        public void Expand(bool expand)
        {
            TopMost = true;
            btnExpand.Image = GUIColors.Change(!is_expanded ? Properties.Resources.arrowhead_up_32 : Properties.Resources.arrowhead_down_32, true);
            Width = !expand ? btnExpand.Width + 4 : w;
            Height = !expand ? btnExpand.Height + 4 : h;
            is_expanded = expand;
            Base.Settings.UpdateViewInBackgroundMode = expand;

            if (!expand)
            {
                // move view to DMC
                if (!Base.Settings.InBackgroundMode) RecipeListUC.CreateViewPanel();
            }
            else
            {
                Actions.GetDisplay(panel);

                CADImport._3D.STLActionCommand.global_slice_view = true;
                CADImport._3D.STLActionCommand.global_show_model = false;

                this.WindowState = FormWindowState.Normal;
                if (Base.Devices.Camera.Control != null) // camera might not exist
                    Base.Devices.Camera.Control.TrackView(btnFitCamera.IsChecked());

            }
        }

        private void ViewWindow_Paint(object sender, PaintEventArgs e)
        {
            Padding = new Padding(1);
            e.Graphics.DrawRectangle(Pens.Gray, 0, 0, Width-1, Height-1);
        }
        
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void btnExpand_MouseDown_1(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void ViewWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void btnExpand_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
            if (!Point.Subtract(dragFormPoint, new Size(Location)).IsEmpty)
            {
                Plugin.plugin.settings.SaveWindowPosition(Location);
                return;
            }

            btnExpand_Click(sender, null);
        }

        private void btnExpand_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void btnFitCamera_Click(object sender, EventArgs e)
        {
            btnFitCamera.SetState(!btnFitCamera.IsChecked());
            if (Base.Devices.Camera.Control != null) // camera might not exist
                Base.Devices.Camera.Control.TrackView(btnFitCamera.IsChecked());
        }

        private void btnZoomFit_Click(object sender, EventArgs e)
        {
            DMC.Actions.ViewFitScreen();
        }

        private void btnZoomReset_Click(object sender, EventArgs e)
        {
            DMC.Actions.ViewReset();
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            DMC.Actions.GetDisplay(null).ZoomIn();
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            DMC.Actions.GetDisplay(null).ZoomOut();
        }
    }

    class StateMonitoring
    {
        Core.ICommand last_command;
        bool was_expanded = false;
        public StateMonitoring()
        {
        }

        public void UpdateState(ViewWindow window)
        {
            var command = GetRunningCommand();
            if (last_command == command) return;
            last_command = command;
            if (last_command != null && last_command.friendly_name == "Alignment")
            {
                window.Expand(true);
                was_expanded = true;
            }
            else
            {
                if (was_expanded) window.Expand(false);
            }
        }
        Core.ICommand GetRunningCommand()
        {
            var recipe = Core.Recipes.ActiveRecipe;
            if (!recipe.IsRunning) return null;
            return recipe.current_running_command;
        }
    }
}
