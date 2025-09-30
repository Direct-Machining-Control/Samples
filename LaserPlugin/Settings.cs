using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaserPlugin
{
    public class Settings : Base.IDeviceSettings, ILaserSettings //The plugin settings are displayed at the bottom of Laser Control tab when a Laser device is selected.
    {
        public Base.StringListParameter port = new Base.StringListParameter("port", "COM Port", "", null, true);
        static string[] baud_rates = { "600", "1200", "2400", "4800", "9600", "14400", "19200", "28800", "38400", "56000", "57600", "115200", "128000", "256000" };
        public Base.StringListParameter baud_rate = new Base.StringListParameter("baud_rate", "Baud Rate", baud_rates[11], baud_rates, true);
        public Base.IntParameter timeout_ms = new Base.IntParameter("timeout_ms", "Timeout (ms)", 200);

        public int BaudRate
        {
            get { return Convert.ToInt32(baud_rate.Value); }
        }

        public const string deviceName = "My Laser";
        public Settings()
            : base("my_laser", "My Laser", "My Laser")
        {
            Add(port); Add(baud_rate); Add(timeout_ms); 
        }

        public override System.Windows.Forms.UserControl GetGUI()
        {
            SettingsGUI gui = new SettingsGUI();
            string[] li = System.IO.Ports.SerialPort.GetPortNames();
            port.list = li;
            gui.Set(this); 
            return gui;
        }

        public override bool IsValid()
        {
            if (!Enabled) return true;
            return true;
        }

        internal int GetPort()
        {
            string s = port.Value;
            if (s == null) return 1;
            s = s.ToLower();
            if (s.StartsWith("com")) s = s.Substring(3);
            if (s != null && s.Length > 0 && s.StartsWith(" ")) s = s.Substring(1);
            int p = 1;
            if (s != null && s.Length > 0) int.TryParse(s, out p);
            return p;
        }

    }
}
