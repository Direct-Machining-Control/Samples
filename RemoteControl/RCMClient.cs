using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Globalization;

namespace RemoteControl
{
    public delegate void RCMEventDelegate(string message, bool send);

    class RCMClient
    {
        byte[] buffer = new byte[8192];
        TcpClient client;
        NetworkStream ns;
        public ulong run_count = 0;
        string last_response = null;
        static IFormatProvider format = null;
        object locker = new object();
        public event RCMEventDelegate RCMEvent;

        public string Response { get { return last_response; } }

        public static IFormatProvider Format
        {
            get
            {
                if (format == null)
                {
                    CultureInfo cn = new CultureInfo("en-US", false);
                    cn.NumberFormat.NumberDecimalSeparator = ".";
                    format = cn;
                }
                return format;
            }
        }

        internal string GetActiveRecipe()
        {
            return SendReceive(ns, "GETACTIVE");
        }

        public void Test()
        {
            TcpClient client = new TcpClient();
            client.Connect("localhost", 23);
            NetworkStream ns = client.GetStream();
            
            // Read RCM module header
            int bytes = ns.Read(buffer, 0, buffer.Length);
            string text = Encoding.ASCII.GetString(buffer);
            System.Diagnostics.Trace.WriteLine(text); // Polaris CAD/CAM 1.2.55 RCM

            SendReceive(ns, "STATUS");  // DISCONNECTED
            SendReceive(ns, "...");     // Error: Wrong command '...'
            SendReceive(ns, "MOVE X 10"); // Error: Not connected to hardware.
            SendReceive(ns, "CONNECT"); // OK CONNECT
            SendReceive(ns, "STATUS");  // CONNECTED
            SendReceive(ns, "MOVE X 10"); // OK MOVE X 10
            client.Close();
        }

        public bool Connect()
        {
            try
            {
                client = new TcpClient();
                client.Connect("localhost", 23);
                ns = client.GetStream();

                int bytes = ns.Read(buffer, 0, buffer.Length);
                string text = Encoding.ASCII.GetString(buffer);
                System.Diagnostics.Trace.WriteLine(text);
                return true;
            }
            catch (Exception ex) {
                Disconnect();
                System.Windows.Forms.MessageBox.Show("Unable to connect to DMC. " + ex.Message);
                return false;
            }
        }

        public void Disconnect()
        {
            try { if (ns != null) ns.Close(); }
            catch (Exception) { }

            try { if (client != null) client.Close(); }
            catch (Exception) { }

            ns = null; client = null;
        }

        /// <summary>
        /// Is connected to the RCM module
        /// </summary>
        public bool IsConnected { get { return ns != null; } }


        public bool ConnectToHardware()
        {
            return SendReceive(ns, "CONNECT").StartsWith("OK");
        }

        public bool IsConnectedToHardware
        {
            get
            {
                return SendReceive(ns, "IS_CONNECTED").StartsWith("IS_CONNECTED 1");
            }
        }

        public bool DisconnectFromHardware()
        {
            return SendReceive(ns, "DISCONNECT").StartsWith("OK");
        }

        public bool RecipeRun() 
        {
            return SendReceive(ns, "RUN").StartsWith("OK");
        }

        public bool RecipePause(bool wait_for_paused_state, long timeout_ms = 0)
        {
            if (!SendReceive(ns, "PAUSE").StartsWith("OK PAUSE")) return false;
            if (!wait_for_paused_state) return true; // pause signal is set, but might not be paused yet

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch(); sw.Start();
            while (timeout_ms < 1 || sw.ElapsedMilliseconds < timeout_ms)
            {
                if (!SendReceive(ns, "IS_PAUSED").StartsWith("IS_PAUSED 1")) return true;
                Thread.Sleep(1);
            }
            return false;
        }


        public bool RecipeContinue()
        {
            return SendReceive(ns, "CONTINUE").StartsWith("OK CONTINUE");
        }

        public bool RecipeCancel()
        {
            return SendReceive(ns, "CANCEL").StartsWith("OK CANCEL");
        }

        public bool RecipeUnloadAll()
        {
            return SendReceive(ns, "UNLOADALL").StartsWith("OK");
        }

        public bool RecipeLoad(string file_name)
        {
            run_count = 0;
            return SendReceive(ns, string.Format("LOAD \"{0}\"", file_name)).StartsWith("OK");
        }

        public bool IsInErrorState
        {
            get
            {
                return SendReceive(ns, "IS_IN_ERROR").StartsWith("IS_IN_ERROR 1");
            }
        }

        /// <summary>
        /// Returns true if state is running, compiling, waiting or paused
        /// </summary>
        public bool IsInProgress
        {
            get
            {
                return SendReceive(ns, "IS_IN_PROGRESS").StartsWith("IS_IN_PROGRESS 1");
            }
        }

        /// <summary>
        /// Returns true if recipe is not running, not compiling, not waiting, not paused
        /// </summary>
        public bool IsRecipeIdle
        {
            get
            {
                return SendReceive(ns, "IS_IN_PROGRESS").StartsWith("IS_IN_PROGRESS 0");
            }
        }

        /// <summary>
        /// Returns true if recipe is not running
        /// </summary>
        public bool IsRecipeStopped
        {
            get
            {
                return SendReceive(ns, "IS_RUNNING").StartsWith("IS_RUNNING 0");
            }
        }

        /// <summary>
        /// Returns true if recipe is running
        /// </summary>
        public bool IsRecipeRunning
        {
            get
            {
                return SendReceive(ns, "IS_RUNNING").StartsWith("IS_RUNNING 1");
            }
        }

        public bool CommandSetSkip(string command_name, bool skip)
        {
            string msg = string.Format("SET_SKIP \"{0}\" {1}", command_name, skip ? "1" : "0");
            return SendReceive(ns, msg).StartsWith("OK "+ msg);
        }

        public bool GetPosition(string axis, out double position)
        {
            position = 0;
            if (string.IsNullOrEmpty(axis)) return false;
            try
            {
                string status = SendReceive(ns, string.Format("GETPOS {0}", axis));
                var cols = status.Split(' ');
                if (!double.TryParse(cols[1], NumberStyles.Any, Format, out position)) return false;
                if (axis.ToLower() != cols[0].ToLower()) return false;
                return true;
            }
            catch (Exception) { return false; }
        }

        public string Send(string cmd)
        {
            return SendReceive(ns, cmd);
        }

        string last_status = null;

        /// <summary>
        /// Get status of application
        /// </summary>
        public string GetStatus()
        {
            string status = SendReceive(ns, "STATUS");
            if (status == null) return "";
            if (status != last_status) 
            {
                if (status == "RUNNING DONE") run_count++;
            }

            last_status = status;
            return status;
        }

        public enum GalvoSettings
        {
            offset_x,
            offset_y,
            scale_x,
            scale_y,
            angle
        }
        public bool GetGalvo(int index, GalvoSettings item, out double result)
        {
            result = 0;
            try
            {
                string status = SendReceive(ns, string.Format("GET_GALVO {0} {1}", index, item.ToString()));
                status = status.Split(' ')[1];
                return double.TryParse(status, NumberStyles.Any, Format, out result);
            }catch(Exception) { return false; }
        }

        public bool Move(string axis_letter, double position, ref string error_message)
        {
            string status = SendReceive(ns, string.Format("MOVE {0} {1}", axis_letter, position.ToString(Format)));
            if (!status.StartsWith("OK")) { error_message = status; return false; }
            return true;
        }

        /// <summary>
        /// Function is available only with TestPlugin loaded
        /// </summary>
        public bool PickAndPlace(double x0, double y0, double z0, double x1, double y1, double z1, ref string error_message)
        {
            string status = SendReceive(ns, string.Format("PICK_AND_PLACE {0} {1} {2} {3} {4} {5}", 
                x0.ToString(Format), y0.ToString(Format), z0.ToString(Format),
                x1.ToString(Format), y1.ToString(Format), z1.ToString(Format)));
            if (!status.StartsWith("OK")) { error_message = status; return false; }
            return true;
        }

        /// <summary>
        /// Get exported variables from active recipe
        /// </summary>
        /// <returns>Dictionary made of variable name(as key) and variable value(as value)</returns>
        public Dictionary<string, string> GetVariables()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string status = SendReceive(ns, "GETVARS");
            if (status == null || status.Length < 1) return dic;
            string[] list = status.Split('\n');
            if (list == null || list.Length < 1) return dic;
            foreach (var item in list)
            {
                if (item == null || item.Length < 1) continue;
                string[] cols = item.Split('\t');
                if (cols.Length < 2 || cols[0].Length < 1) continue;
                string key = cols[0];
                string value = cols[1];
                if (!dic.ContainsKey(key))
                    dic.Add(key, value);
            }
            return dic;
        }

        string SendReceive(NetworkStream ns, string cmd)
        {
            lock (locker)
            {
                try
                {
                    last_response = "";
                    if (cmd == null || cmd.Length < 1) return "";

                    try { if (RCMEvent != null) RCMEvent(cmd, true); } catch (Exception) { }
                    // make sure we have \n at the end
                    if (cmd[cmd.Length - 1] != '\n') cmd += '\n';


                    byte[] data = Encoding.ASCII.GetBytes(cmd);
                    ns.Write(data, 0, data.Length);
                    ns.Flush();

                    Array.Clear(buffer, 0, buffer.Length);

                    // read response
                    int bytes = ns.Read(buffer, 0, buffer.Length);
                    if (bytes > 2)
                    {
                        string text = Encoding.ASCII.GetString(buffer, 0, bytes - 2); // remove /r/n from end
                        System.Diagnostics.Trace.WriteLine(text);
                        try { if (RCMEvent != null) RCMEvent(text, false); } catch (Exception) { }

                        last_response = text;
                        return text;
                    }
                    return "";
                }
                catch (Exception ex) { return "Error: " + ex.Message; }
            }
        }
    }
}
