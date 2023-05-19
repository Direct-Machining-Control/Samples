using Base;
using DMC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PythonCommandPlugin
{
    public partial class SettingsGUI : IDeviceSettingsGUI
    {
        PluginSettings settings;

        public SettingsGUI()
        {
            InitializeComponent();
        }

        internal void Set(PluginSettings settings)
        {
            this.settings = settings;

            Base.IDeviceSettingsGUI.Set(settings.Parameters, Controls.GetEnumerator(), toolTip1);
            
            SettingsState();
        }

        private void SettingsState()
        {
            executable_path.Enabled = enabled.Value;
        }

        private void enabled_ParameterFieldValueChanged(object sender, Base.IParameter param)
        {
            SettingsState();
        }

        private bool IsPythonInstalled()
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = "cmd.exe";
                start.Arguments = "/C python --version";
                start.CreateNoWindow = true;
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string output = reader.ReadLine();
                        if (output.Length < 20) // example: "Python 3.10.8"
                            return true;
                        else // example: "Python was not found; run without....."
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Functions.Log("Failed to detect python. " + ex.Message);
                return false;
            }
        }

        private void DetectPythonPath()
        {
            try
            {
                string path;
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = "cmd.exe";
                start.Arguments = "/C where python";
                start.CreateNoWindow = true;
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        path = reader.ReadLine();
                    }
                }
                if (File.Exists(path))
                {
                    settings.executable_path.Value = path;
                    executable_path.SetValue(path);
                }
                else
                {
                    Base.Functions.ShowMessage(string.Format("Detection failed. Invalid path: \"{0}\"", path));
                }
            }
            catch (Exception ex)
            {
                Functions.Log("Failed to detect python. " + ex.Message);
                Functions.ShowMessage("Failed to detect python executable path.");
            }
        }

        private void buttonDetect_Click(object sender, EventArgs e)
        {
            if (IsPythonInstalled())
                DetectPythonPath();
            else
                Functions.ShowMessage("Python not found. Make sure it is installed.");
        }
    }
}
