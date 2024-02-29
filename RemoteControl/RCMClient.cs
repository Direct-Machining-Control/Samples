using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace RemoteControl
{
    public delegate void RCMEventDelegate(string message, bool send);
    /// <summary>
    /// Checksum calculation class
    /// </summary>
    public static class CRC32
    {
        private static uint[] crc32Table;

        static CRC32()
        {
            InitializeCrc32Table();
        }
        /// <summary>
        /// Calculate input checksum
        /// </summary>
        /// <param name="input">Input data</param>
        /// <returns>calculated checksum</returns>
        public static uint ComputeChecksum(string input)
        {
            // Convert the input string to bytes
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            uint crc = 0xFFFFFFFF;

            foreach (byte b in inputBytes)
            {
                byte index = (byte)((crc ^ b) & 0xFF);
                crc = (crc >> 8) ^ crc32Table[index];
            }

            return crc ^ 0xFFFFFFFF;
        }

        private static void InitializeCrc32Table()
        {
            crc32Table = new uint[256];
            const uint polynomial = 0xEDB88320;

            for (uint i = 0; i < 256; i++)
            {
                uint crc = i;
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 1) == 1)
                    {
                        crc = (crc >> 1) ^ polynomial;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
                crc32Table[i] = crc;
            }
        }
    }
    /// <summary>
    /// Remote Control API class
    /// </summary>
    /// <example>
    /// API Use example in C#
    /// <code>
    ///class Program
    ///{
    ///    static void Main()
    ///    {
    ///        RCMClient client = new RCMClient();
    ///
    ///        if (client.Connect(IPAddress.Loopback, true, true))
    ///            Console.WriteLine("RCM Connected. ");
    ///        else
    ///        {
    ///            Console.WriteLine("Failed to connect. Press any key to close application. ");
    ///            Console.Read(); return;
    ///        }
    ///
    ///        Console.WriteLine(string.Format("DMC is connected to hardware = {0}", client.IsConnectedToHardware));
    ///
    ///        string error_message = "";
    ///        if (!client.ConnectToHardware(ref error_message))
    ///            Console.WriteLine("Unable to connect to hardware, " + error_message);
    ///        else
    ///            Console.WriteLine(string.Format("DMC is connected to hardware = {0}", client.IsConnectedToHardware));
    ///
    ///        client.Disconnect();
    ///
    ///        Console.WriteLine("Press any key to close application. ");
    ///        Console.Read();
    ///    }
    ///}
    /// </code>
    /// </example>
    public class RCMClient
    {
        byte[] buffer = new byte[8192];
        TcpClient client;
        NetworkStream ns;
        internal ulong run_count = 0;
        string last_response = null;
        static IFormatProvider format = null;
        object locker = new object();
        public event RCMEventDelegate RCMEvent;

        string Response { get { return last_response; } }

        static IFormatProvider Format
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

        #region Communication
        /// <summary>
        /// Connects Remote Control
        /// </summary>
        /// <param name="ip_adress">IP Adress</param>
        /// <param name="use_message_id">Use Message ID</param>
        /// <param name="use_checksum">Use Checksum</param>
        /// <returns>True if connected, otherwise false</returns>
        public bool Connect(IPAddress ip_adress, bool use_message_id = false, bool use_checksum = false)
        {
            try
            {
                this.use_checksum = use_checksum;
                this.use_msg_id = use_message_id;
                client = new TcpClient();
                client.Connect(ip_adress, 23);
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
        /// <summary>
        /// Disconnects Remote Control
        /// </summary>
        public void Disconnect()
        {
            try { if (ns != null) ns.Close(); }
            catch (Exception) { }

            try { if (client != null) client.Close(); }
            catch (Exception) { }

            ns = null; client = null;
        }
        int message_id = 0;
        bool use_checksum = false;
        bool use_msg_id = false;
        /// <summary>
        /// Use Checksum for communication
        /// </summary>
        public bool UseChecksum { get { return use_checksum; } set { use_checksum = value; } }
        /// <summary>
        /// Use Message IDs for communication
        /// </summary>
        public bool UseMessageID { get { return use_msg_id;} set { use_msg_id = value; } }
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

                    if (use_checksum)
                    {
                        string data_to_send = "";
                        if(use_msg_id)
                        {
                            if (message_id >= 999999) message_id = 0;
                            data_to_send += $"{message_id++}|";
                        }
                        data_to_send += $"{cmd}|";
                        data_to_send += CRC32.ComputeChecksum(data_to_send).ToString("X8");
                        if (data_to_send[data_to_send.Length - 1] != '\n') data_to_send += '\n';
                        byte[] data = Encoding.ASCII.GetBytes(data_to_send);
                        ns.Write(data, 0, data.Length);
                        ns.Flush();
                    }
                    else
                    {
                        if (cmd[cmd.Length - 1] != '\n') cmd += '\n';
                        byte[] data = Encoding.ASCII.GetBytes(cmd);
                        ns.Write(data, 0, data.Length);
                        ns.Flush();
                    }

                    Array.Clear(buffer, 0, buffer.Length);

                    // read response
                    int bytes = ns.Read(buffer, 0, buffer.Length);
                    if (bytes > 2)
                    {
                        string text = Encoding.ASCII.GetString(buffer, 0, bytes - 2); // remove /r/n from end
#if DEBUG
                        System.Diagnostics.Trace.WriteLine(text);
#endif

                        if (use_checksum)
                        {
                            string pattern = @"^(?:(?<MESSAGE_ID>\d*)\|)?(?<MESSAGE>.*?)\|(?<CHECKSUM>[A-F0-9]*)$";
                            Regex regex = new Regex(pattern);
                            Match match = regex.Match(text);
                            if (match.Success)
                            {
                                string messageId = match.Groups["MESSAGE_ID"].Value;
                                string message = match.Groups["MESSAGE"].Value;
                                string checksum = match.Groups["CHECKSUM"].Value;
                                uint calculated_checksum = CRC32.ComputeChecksum($"{messageId}|{message}|");
                                if (!uint.TryParse(checksum, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out uint received_checksum))
                                    return "Error: Could not parse checksum. ";
                                if (calculated_checksum != received_checksum)
                                    return "Error: Checksum error. ";
                                text = message;
                            }
                            else return "Error: Incorrect syntax. ";
                        }

                        try { if (RCMEvent != null) RCMEvent(text, false); } catch (Exception) { }

                        last_response = text;
                        return text;
                    }
                    return "";
                }
                catch (Exception ex) { return "Error: " + ex.Message; }
            }
        }
        /// <summary>
        /// Sends custom command
        /// </summary>
        /// <param name="cmd">Custom command</param>
        /// <returns>Returns response</returns>
        public string Send(string cmd)
        {
            return SendReceive(ns, cmd);
        }

        /// <summary>
        /// Is connected to the RCM module
        /// </summary>
        public bool IsConnected { get { return ns != null; } }
        #endregion

        #region API
        /// <summary>
        /// Connects to hardware
        /// </summary>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if connected to hardware, otherwise false</returns>
        public bool ConnectToHardware(ref string error_message)
        {
            string response = SendReceive(ns, "CONNECT");
            if (response.StartsWith("OK")) return true;
            else
            {
                error_message = response;
                return false;
            }
        }
        /// <summary>
        /// Returns true if is connected to hardware, otherwise false
        /// </summary>
        public bool IsConnectedToHardware
        {
            get
            {
                return SendReceive(ns, "IS_CONNECTED").StartsWith("IS_CONNECTED 1");
            }
        }
        /// <summary>
        /// Disconnects from hardware
        /// </summary>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if successfully disconnected from hardware, otherwise false</returns>
        public bool DisconnectFromHardware(ref string error_message)
        {
            string received = SendReceive(ns, "DISCONNECT");
            if (!received.StartsWith("OK")) { error_message = received; return false; }
            return true;
        }
        /// <summary>
        /// Runs recipe
        /// </summary>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if recipe has been started, otherwise false</returns>
        public bool RecipeRun(ref string error_message) 
        {
            string received = SendReceive(ns, "RUN");
            if (!received.StartsWith("OK")) { error_message = received; return false; }
            return true;
        }
        /// <summary>
        /// Runs active recipe synchronously
        /// </summary>
        /// <param name="error_message">Error message when recipe run stops with error</param>
        /// <returns>True if recipe run succeeded, False when run failed</returns>
        public bool RecipeRunSync(ref string error_message)
        {
            if (!SendReceive(ns, "RUN").StartsWith("OK"))
            {
                error_message = GetLastError();
                return false;
            }

            while (true)
            {
                if (IsRecipeIdle)
                {
                    if (IsInErrorState)
                    {
                        error_message = GetLastError();
                        return false;
                    }
                    return true;
                }
                System.Threading.Thread.Sleep(1);
            }
        }

        /// <summary>
        /// Compiles recipe synchronously
        /// </summary>
        /// <param name="error_message">Error message when recipe compilation fails</param>
        /// <returns>True if recipe compilation succeeded, False when compilation failed</returns>
        public bool RecipeCompileSync(ref string error_message)
        {
            if (!SendReceive(ns, "COMPILE").StartsWith("OK"))
            {
                error_message = GetLastError();
                return false;
            }

            while (true)
            {
                if (IsRecipeIdle)
                {
                    if (IsInErrorState)
                    {
                        error_message = GetLastError();
                        return false;
                    }
                    return true;
                }
                System.Threading.Thread.Sleep(1);
            }
        }

        internal enum WindowType
        {
            PREVIEW = 0
        }
        /// <summary>
        /// Gets window handle for embedding window into this application. This works ONLY if both applications are on same PC
        /// </summary>
        /// <param name="type">Type of DMC window to get</param>
        /// <param name="handle">Window handle</param>
        /// <returns>True if window is available</returns>
        internal bool GetWindow(WindowType type, ref IntPtr handle)
        {
            string result = SendReceive(ns, "GET_WINDOW "+ type.ToString());
            int h = 0;

            if (result != null && int.TryParse(result, out h) && h != 0)
            {
                handle = (IntPtr)h;
                return true;
            }
            return false;
        }

        internal bool SetWindow(WindowType type, System.Windows.Forms.Control window)
        {
            if (window == null) return false;
            IntPtr handle = IntPtr.Zero;
            if (GetWindow(RCMClient.WindowType.PREVIEW, ref handle))
                WinHelper.SetWindowHandle(handle, window);
            else
                return false;
            return true;
        }
        /// <summary>
        /// Pauses recipe
        /// </summary>
        /// <param name="wait_for_paused_state">Wait parameter</param>
        /// <param name="error_message">Error message</param>
        /// <param name="timeout_ms">Timeout</param>
        /// <returns>Returns true if recipe has been paused, otherwise false</returns>
        public bool RecipePause(bool wait_for_paused_state, ref string error_message, long timeout_ms = 0)
        {
            string received = SendReceive(ns, "PAUSE");
            if (!received.StartsWith("OK PAUSE")) { error_message = received; return false; }

            if (!wait_for_paused_state) return true; // pause signal is set, but might not be paused yet

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch(); sw.Start();
            while (timeout_ms < 1 || sw.ElapsedMilliseconds < timeout_ms)
            {
                if (!SendReceive(ns, "IS_PAUSED").StartsWith("IS_PAUSED 1")) return true;
                Thread.Sleep(1);
            }
            error_message = "Recipe pause timeout. ";
            return false;
        }
        /// <summary>
        /// Continues recipe
        /// </summary>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if recipe is resumed, otherwise false</returns>
        public bool RecipeContinue(ref string error_message)
        {
            string received = SendReceive(ns, "CONTINUE");
            if (!received.StartsWith("OK CONTINUE")) { error_message = received; return false; }
            return true;
        }
        /// <summary>
        /// Cancels recipe
        /// </summary>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if recipe has been canceled, otherwise false</returns>
        public bool RecipeCancel(ref string error_message)
        {
            string received = SendReceive(ns, "CANCEL");
            if (!received.StartsWith("OK CANCEL")) { error_message = received; return false; }
            return true;
        }
        /// <summary>
        /// Unloads recipe
        /// </summary>
        /// <param name="recipe_name">Recipe name</param>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if recipe unloaded, otherwise false</returns>
        public bool RecipeUnload(string recipe_name, ref string error_message)
        {
            string received = SendReceive(ns, $"UNLOAD {recipe_name}");
            if (!received.StartsWith("OK")) { error_message = received; return false; }
            return true;
        }
        /// <summary>
        /// Activates recipe
        /// </summary>
        /// <param name="recipe_name">Recipe name</param>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if the recipe has been activated</returns>
        public bool ActivateRecipe(string recipe_name, ref string error_message)
        {
            string received = SendReceive(ns, $"ACTIVATE {recipe_name}");
            if (!received.StartsWith("OK")) { error_message = received; return false; }
            return true;
        }
        /// <summary>
        /// Gets active recipe
        /// </summary>
        /// <returns>Returns full path of the activated recipe file</returns>
        public string GetActiveRecipe()
        {
            return SendReceive(ns, $"GETACTIVE");
        }
        /// <summary>
        /// Unloads all recipes
        /// </summary>
        /// <param name="error_message">Error message</param> 
        /// <returns>Returns true if all recipes have been unloaded</returns>
        public bool RecipeUnloadAll(ref string error_message)
        {
            string received = SendReceive(ns, "UNLOADALL");
            if (!received.StartsWith("OK")) { error_message = received; return false; }
            return true;
        }
        /// <summary>
        /// Loads recipe
        /// </summary>
        /// <param name="file_name">Full path to recipe file</param>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if recipe has been loaded, otherwise false</returns>
        public bool RecipeLoad(string file_name, ref string error_message)
        {
            run_count = 0;
            string received = SendReceive(ns, string.Format("LOAD \"{0}\"", file_name));
            if (received.StartsWith("OK")) return true;
            else
            {
                error_message = received;
                return false;
            }
        }
        /// <summary>
        /// Returns true if is in error state
        /// </summary>
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

        /// <summary>
        /// Returns true if recipe is paused
        /// </summary>
        public bool IsRecipePaused
        {
            get
            {
                return SendReceive(ns, "IS_PAUSED").StartsWith("IS_PAUSED 1");
            }
        }
        /// <summary>
        /// Gets value of variable command
        /// </summary>
        /// <param name="variable_command_name">Variable command name</param>
        /// <returns>Returns variable command value</returns>
        public string GetValue(string variable_command_name)
        {
            return SendReceive(ns, $"GETVALUE {variable_command_name}");
        }
        /// <summary>
        /// Sets skip for a command in recipe
        /// </summary>
        /// <param name="command_name">Recipe command name</param>
        /// <param name="skip">true=skip, false=unskip</param>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if skip property has been set</returns>
        public bool CommandSetSkip(string command_name, bool skip, ref string error_message)
        {
            string msg = string.Format("SET_SKIP \"{0}\" {1}", command_name, skip ? "1" : "0");
            string received = SendReceive(ns, msg);
            if (!received.StartsWith("OK"))
            {
                error_message = received;
                return false;
            }
            return true;
        }
        /// <summary>
        /// Gets position of axis
        /// </summary>
        /// <param name="axis">Axis name</param>
        /// <param name="position">Position result</param>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if managed to get position of axis, otherwise false</returns>
        public bool GetPosition(string axis, out double position, ref string error_message)
        {
            position = 0;
            if (string.IsNullOrEmpty(axis)) return false;
            try
            {
                string received = SendReceive(ns, string.Format("GETPOS {0}", axis));
                if (received.StartsWith("ERROR"))
                {
                    error_message = received;
                    return false;
                }
                var cols = received.Split(' ');
                if (!double.TryParse(cols[1], NumberStyles.Any, Format, out position)) return false;
                if (axis.ToLower() != cols[0].ToLower()) return false;
                return true;
            }
            catch (Exception ex) { error_message = ex.Message; return false; }
        }
        /// <summary>
        /// Sets variable value
        /// </summary>
        /// <param name="variable_name">Variable name</param>
        /// <param name="value">New value for variable</param>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if new value has been set, otherwise false</returns>
        public bool SetVarValue(string variable_name, string value, ref string error_message)
        {
            string received = SendReceive(ns, $"SETVARVALUE {variable_name} {value}");
            if (!received.StartsWith("OK"))
            {
                error_message = received;
                return false;
            }
            return true;
        }
        /// <summary>
        /// Estimates recipe run time
        /// </summary>
        /// <returns>Returns estimated recipe run time</returns>
        public string Estimate()
        {
            return SendReceive(ns, $"ESTIMATE");
        }
        /// <summary>
        /// Gets remaining recipe run time
        /// </summary>
        /// <returns>Returns remaining recipe run time</returns>
        public string Remaining()
        {
            return SendReceive(ns, $"REMAINING");
        }
        /// <summary>
        /// Resets status bar error
        /// </summary>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if status bar has been cleared</returns>
        public bool ClearError(ref string error_message)
        {
            string received = SendReceive(ns, "CLEAR_ERROR");
            if (!received.StartsWith("OK")) { error_message = received; return false; }
            return true;
        }
        /// <summary>
        /// Returns list of recipe name in specified path
        /// </summary>
        /// <param name="path">Search path</param>
        /// <returns>Returns list of recipes in specified path</returns>
        public string[] GetRecipes(string path)
        {
            string response = SendReceive(ns, $"GET_RECIPES {path}");
            return response.Split(',');
        }
        /// <summary>
        /// Gets command parameter value
        /// </summary>
        /// <param name="recipe_command_name">Recipe command name</param>
        /// <param name="param_name">Parameter to get value of</param>
        /// <returns>Returns parameter value if parameter exists, otherwise false</returns>
        public string GetPrm(string recipe_command_name, string param_name)
        {
            return SendReceive(ns, $"GETPRM {recipe_command_name} {param_name}");
        }
        /// <summary>
        /// Gets changeable parameters of a recipe command
        /// </summary>
        /// <param name="recipe_command_name">Recipe command name</param>
        /// <returns>Returns list of changeable parameters</returns>
        public string[] GetPrmList(string recipe_command_name)
        {
            string response = SendReceive(ns, $"GETPRMLIST {recipe_command_name}");
            return response.Split(',');
        }
        /// <summary>
        /// Sets new value for a command parameter
        /// </summary>
        /// <param name="recipe_command_name">Recipe command name</param>
        /// <param name="param_name">Parameter name</param>
        /// <param name="param_new_value">Value to set</param>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if new value for parameter has been set, otherwise false. </returns>
        public bool SetPrm(string recipe_command_name, string param_name, string param_new_value, ref string error_message)
        {
            string received = SendReceive(ns, $"SETPRM {recipe_command_name} {param_name} {param_new_value}");
            if (!received.StartsWith("OK")) { error_message = received; return false; }
            return true;
        }
        /// <summary>
        /// Sets working zone
        /// </summary>
        /// <param name="zone_id">Zone ID</param>
        /// <param name="pos_x">Position cordinate X</param>
        /// <param name="pos_y">Position cordinate Y</param>
        /// <param name="size_x">Zone width</param>
        /// <param name="size_y">Zone height</param>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if zone has been added, otherwise false</returns>
        public bool SetWorkingZone(int zone_id, double pos_x, double pos_y, double size_x, double size_y, ref string error_message)
        {
            string received = SendReceive(ns, $"SET_WORKING_ZONE {zone_id} {pos_x} {pos_y} {size_x} {size_y}");
            if (!received.StartsWith("OK")) { error_message = received; return false; }
            return true;
        }
        /// <summary>
        /// Removes working zone
        /// </summary>
        /// <param name="zone_id">Zone ID</param>
        /// <param name="error_message">Error message</param>
        /// <returns>Return true if zone with zone_id has been removed. </returns>
        public bool RemoveWorkingZone(int zone_id, ref string error_message)
        {
            string received = SendReceive(ns, $"REMOVE_WORKING_ZONE {zone_id}");
            if (!received.StartsWith("OK")) { error_message = received; return false; }
            return true;
        }
        /// <summary>
        /// Enables axis
        /// </summary>
        /// <param name="axis_name">Axis name</param>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if axis has been enabled, otherwise returns false. </returns>
        public bool Enable(string axis_name, ref string error_message)
        {
            string received = SendReceive(ns, $"ENABLE {axis_name}");
            if (!received.StartsWith("OK")) { error_message = received; return false; }
            return true;
        }
        /// <summary>
        /// Disables axis
        /// </summary>
        /// <param name="axis_name">Axis name</param>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if axis has been disabled, otherwise returns false. </returns>
        public bool Disable(string axis_name, ref string error_message)
        {
            string received = SendReceive(ns, $"DISABLE {axis_name}");
            if (!received.StartsWith("OK")) { error_message = received; return false; }
            return true;
        }
        /// <summary>
        /// Clears axis errors
        /// </summary>
        /// <param name="axis_name">Axis name</param>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if errors have been cleared, otherwise returns false. </returns>
        public bool ClearAxisError(string axis_name, ref string error_message)
        {
            string received = SendReceive(ns, $"CLEAR_AXIS_ERROR {axis_name}");
            if (!received.StartsWith("OK")) { error_message = received; return false; }
            return true;
        }
        /// <summary>
        /// Transforms all geometry in recipe
        /// </summary>
        /// <param name="angle_deg">Rotate</param>
        /// <param name="offset_x">Offset X</param>
        /// <param name="offset_y">Offset Y</param>
        /// <param name="error_message">Error Message</param>
        /// <param name="offset_z">Offet Z (default = 0)</param>
        /// <param name="rotation_point_x">Rotation point X position (default = 0)</param>
        /// <param name="rotation_point_y">Rotation point Y position (default = 0)</param>
        /// <returns>Returns true if successfully transformed all geometry, otherwise false</returns>
        public bool SetTransform(double angle_deg, double offset_x, double offset_y, ref string error_message, double offset_z = 0, double rotation_point_x = 0, double rotation_point_y = 0)
        {
            string received = SendReceive(ns, $"SET_TRANSFORM {angle_deg} {offset_x} {offset_y} {offset_z} {rotation_point_x} {rotation_point_y}");
            if (!received.StartsWith("OK")) { error_message = received; return false; }
            return true;
        }

        string last_status = null;
        /// <summary>
        /// Gets status of application
        /// </summary>
        /// <returns>Returns current status</returns>
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
        /// <summary>
        /// Gets last error message
        /// </summary>
        /// <returns>Returns last error message</returns>
        public string GetLastError()
        {
            return SendReceive(ns, "GET_ERROR");
        }
        /// <summary>
        /// Galvo settings
        /// </summary>
        public enum GalvoSettings
        {
            /// <summary>
            /// Offset X
            /// </summary>
            offset_x,
            /// <summary>
            /// Offset Y
            /// </summary>
            offset_y,
            /// <summary>
            /// Scale X
            /// </summary>
            scale_x,
            /// <summary>
            /// Scale Y
            /// </summary>
            scale_y,
            /// <summary>
            /// Angle
            /// </summary>
            angle
        }
        /// <summary>
        /// Gets galvo scanner setting value
        /// </summary>
        /// <param name="index">Galvo scanner index</param>
        /// <param name="item">Galvo scanner setting</param>
        /// <param name="result">Result</param>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if managed to get galvo setting value, otherwise false</returns>
        public bool GetGalvo(int index, GalvoSettings item, out double result, ref string error_message)
        {
            result = 0;
            try
            {
                string received = SendReceive(ns, string.Format("GET_GALVO {0} {1}", index, item.ToString()));
                if(!received.StartsWith("OK")) error_message = received;
                received = received.Split(' ')[1];
                return double.TryParse(received, NumberStyles.Any, Format, out result);
            }catch(Exception ex) { error_message = ex.Message; return false; }
        }
        /// <summary>
        /// Sets galvo scanner setting
        /// </summary>
        /// <param name="index">Galvo scanner index</param>
        /// <param name="item">Galvo scanner setting</param>
        /// <param name="new_value">Setting new value</param>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if setting has been changed, otherwise false</returns>
        public bool SetGalvo(int index, GalvoSettings item, double new_value, ref string error_message)
        {
            string received = SendReceive(ns, string.Format("SET_GALVO {0} {1} {2}", index, item.ToString(), new_value.ToString(Format)));
            if (!received.StartsWith("OK")) { error_message = received; return false; }
            return true;
        }
        /// <summary>
        /// Applies galvo scanner settings
        /// </summary>
        /// <param name="index">Galvo scanner index</param>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if settings have been applied, otherwise false</returns>
        public bool GalvoApplySettings(int index, ref string error_message)
        {
            string received = SendReceive(ns, string.Format("GALVO_APPLY_SETTINGS {0}", index));
            if (!received.StartsWith("OK")) { error_message = received; return false; }
            return true;
        }
        /// <summary>
        /// Moves axis
        /// </summary>
        /// <param name="axis_letter">Axis name</param>
        /// <param name="position">Position to move to</param>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if axis has been moved, otherwise false</returns>
        public bool Move(string axis_letter, double position, ref string error_message)
        {
            string received = SendReceive(ns, string.Format("MOVE {0} {1}", axis_letter, position.ToString(Format)));
            if (!received.StartsWith("OK")) { error_message = received; return false; }
            return true;
        }

        /// <summary>
        /// Function is available only with TestPlugin loaded
        /// </summary>
        internal bool PickAndPlace(double x0, double y0, double z0, double x1, double y1, double z1, ref string error_message)
        {
            string status = SendReceive(ns, string.Format("PICK_AND_PLACE {0} {1} {2} {3} {4} {5}", 
                x0.ToString(Format), y0.ToString(Format), z0.ToString(Format),
                x1.ToString(Format), y1.ToString(Format), z1.ToString(Format)));
            if (!status.StartsWith("OK")) { error_message = status; return false; }
            return true;
        }
        /// <summary>
        /// Gets exported variables from active recipe
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
        /// <summary>
        /// Home axis
        /// </summary>
        /// <param name="axis_name">Axis name</param>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if axis has been homed</returns>
        public bool HomeAxis(string axis_name, ref string error_message)
        {
            string received = SendReceive(ns, $"HOME {axis_name}");
            if (!received.StartsWith("OK")) { error_message = received; return false; }
            return true;
        }
        /// <summary>
        /// Closes DMC application
        /// </summary>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if DMC has been closed, otherwise false</returns>
        public bool CloseDMC(ref string error_message)
        {
            string received = SendReceive(ns, $"CLOSE");
            if (!received.StartsWith("OK"))
            {
                error_message = received;
                return false;
            }
            return true;
        }
        /// <summary>
        /// Continuous laser emission on
        /// </summary>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if turned on, otherwise false</returns>
        public bool LaserON(ref string error_message)
        {
            string received = SendReceive(ns, $"LASERON");
            if (!received.StartsWith("OK")) { error_message = received; return false; }
            return true;
        }
        /// <summary>
        /// Continuous laser emission off
        /// </summary>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if turned off, otherwise false</returns>
        public bool LaserOFF(ref string error_message)
        {
            string received = SendReceive(ns, $"LASEROFF");
            if (!received.StartsWith("OK")) { error_message = received; return false; }
            return true;
        }
        /// <summary>
        /// Changes view projection
        /// </summary>
        /// <param name="view_projection">View projection</param>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if view projection has been changed, otherwise false</returns>
        public bool View(string view_projection, ref string error_message)
        {
            string received = SendReceive(ns, $"VIEW {view_projection}");
            if (!received.StartsWith("OK"))
            {
                error_message = received;
                return false;
            }
            return true ;
        }
        /// <summary>
        /// Gets variable value
        /// </summary>
        /// <param name="variable_name">Variable name</param>
        /// <returns>Returns variable value</returns>
        public string GetVar(string variable_name)
        {
            return SendReceive(ns, $"GETVAR {variable_name}");
        }
        /// <summary>
        /// Sets variable new value
        /// </summary>
        /// <param name="variable_name">Variable name</param>
        /// <param name="variable_new_value">Variable new value</param>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if new value has been set, otherwise false</returns>
        public bool SetVar(string variable_name, string variable_new_value, ref string error_message)
        {
            string received = SendReceive(ns, $"SETVAR {variable_name} {variable_new_value}");
            if (!received.StartsWith("OK"))
            {
                error_message = received;
                return false;
            }
            return true;
        }
        /// <summary>
        /// Gets global variable value
        /// </summary>
        /// <param name="variable_name">Global variable name</param>
        /// <returns>Returns global variable value</returns>
        public string GetGVar(string variable_name)
        {
            return SendReceive(ns, $"GETGVAR {variable_name}");
        }
        /// <summary>
        /// Sets global variable new value
        /// </summary>
        /// <param name="variable_name">Global variable name</param>
        /// <param name="variable_new_value">Global variable new value</param>
        /// <param name="error_message">Error message</param>
        /// <returns>Returns true if new value has been set, otherwise false</returns>
        public bool SetGVar(string variable_name, string variable_new_value, ref string error_message)
        {
            string received = SendReceive(ns, $"SETGVAR {variable_name} {variable_new_value}");
            if (!received.StartsWith("OK"))
            {
                error_message = received;
                return false;
            }
            return true;
        }
        #endregion
    }






    static class WinHelper
    {
        //Sets a window to be a child window of another window
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        //Sets window attributes
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, winStyle dwNewLong);

        public static int GWL_STYLE = -16;

        public static void SetWindowHandle(IntPtr handle, Control window)
        {
			WinHelper.SetWindowLong(handle, WinHelper.GWL_STYLE, WinHelper.winStyle.WS_VISIBLE | WinHelper.winStyle.WS_CHILD);
			WinHelper.SetParent(handle, window.Handle);
			bool ret = WinHelper.MoveWindow(handle, 0, 0, window.Width, window.Height, true);
        }

        [Flags]
        public enum winStyle : int
        {
            WS_VISIBLE = 0x10000000,
            WS_CHILD = 0x40000000, //child window
            WS_BORDER = 0x00800000, //window with border
            WS_DLGFRAME = 0x00400000, //window with double border but no title
            WS_CAPTION = WS_BORDER | WS_DLGFRAME //window with a title bar
        }

    }
}
