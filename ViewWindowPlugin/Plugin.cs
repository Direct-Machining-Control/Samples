using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base;
using Base.Shapes;
using System.IO;
using System.Globalization;
using Core;
using System.Drawing;

namespace ViewWindowPlugin
{
    public class Plugin : IDevice
    {
        public static Plugin plugin;
        bool isConnected = false;
        internal SettingsEMotion settings = new SettingsEMotion();
        public static Color color_main = Color.FromArgb(0, 109, 132);
        public static Color color_button = Color.FromArgb(165, 199, 0);

        public Plugin()
        {
            Base.View.color_on_hover = System.Drawing.Color.FromArgb(250, 240, 230);
            Base.View.color_on_down = System.Drawing.Color.FromArgb(255, 218, 185);
            Base.View.color_view_background = new ColorF(0.96f, 0.96f, 0.96f);
            
            plugin = this;
            
        }

        public string GetName() { return "EMotion Plugin"; }
        public IDeviceSettings GetSettings() { return this.settings; }
        public bool IsEnabled() { return true; }

        public bool Connect() {
            ViewWindow.ShowForm();
            return this.isConnected = true;
        }
        public void Disconnect() { this.isConnected = false; }
        public bool IsConnected() { return this.isConnected; }

        public bool ApplySettings()
        {
            return true;
        }

        private void PostCompile(Core.Recipe recipe)
        {
            ViewWindow.ShowForm();
            
        }

        public void Stop() { }
        public string GetErrorMessage() { return Functions.GetLastErrorMessage(); }

        public void OnRecipeFinish() { }
        public bool OnRecipeStart() { return true; }
    }

    public class SettingsEMotion : IDeviceSettings
    {
        IntParameter win_x = new IntParameter("win_x", "", 10);
        IntParameter win_y = new IntParameter("win_y", "", 10);
        BoolParameter show_external_view = new BoolParameter("show_external_view", "Show External Preview Window", true);

        internal Point GetWindowPosition() { return new Point(win_x.value, win_y.value); }

        internal void SaveWindowPosition(Point location)
        {
            win_x.value = location.X;
            win_y.value = location.Y;
            Base.Settings.SaveSettings();
        }
        
        public SettingsEMotion() : base("emotion", "EMotion", "EMotion Plugin Settings")
        {
            Add(win_x); Add(win_y); Add(show_external_view);
        }
        public bool ShowPreviewWindow {  get { return show_external_view.value; } }
        
        public override bool VisibleInSettings { get { return false; } }
        public override System.Windows.Forms.UserControl GetGUI()
        {
            return null;
        }
        
    }
    
}
