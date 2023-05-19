using Base;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestPlugin
{

    /// <summary>
    /// Device that communicates with external device via Serial Port
    /// This device monitors digital input preset "DoorOpened" (needs to be configured in Settings->IOTools) state and sends state to serial port
    /// </summary>
    //public class SerialControlPlugin : IDevice // use this if plugin is needed
    public class SerialControlPlugin 
    {
        SerialPort serial = null;
        Thread thread;      // thread for monitoring IO state
        object serial_lock;
        IOTool io_door_state = null;
        bool is_connected = false;

        public SerialControlPlugin()
        {
        }

        // Action when user clicks Connect to hardware and IsEnabled is true
        public bool Connect()
        {
            if (!IsEnabled()) return true;
            try
            {
                var ports = SerialPort.GetPortNames();
                if (ports == null || ports.Length < 1) throw new Exception("No serial ports found. ");
                serial = new SerialPort(ports[0]);
                serial.Open();
                is_connected = true;
                serial_lock = new object();
                serial.WriteLine("CONNECT");

                io_door_state = Settings.IOTools.GetInput("DoorOpened");
                if (io_door_state != null)
                {
                    thread = new Thread(new ThreadStart(IOStateMonitorThread));
                    thread.Start();
                }
            }
            catch (Exception ex)
            {
                return Functions.ErrorF("Unable to connect to Serial Port controller. ", ex);
            }
            return true;
        }

        // monitor IOState every 1s and send serial command if state changes
        void IOStateMonitorThread()
        {
            try
            {
                bool last_state = io_door_state.IsDigitalInputHigh(); // read IO state
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch(); sw.Start();
                while (!State.is_exit && is_connected)
                {
                    Thread.Sleep(10);
                    if (sw.ElapsedMilliseconds < 1000) continue;
                    sw.Restart();

                    // read IO state
                    bool current_state = io_door_state.IsDigitalInputHigh();
                    if (current_state != last_state)
                    {
                        lock (serial_lock)
                        {
                            try
                            {
                                serial.WriteLine(current_state ? "DOOR OPENED" : "DOOR CLOSED");
                            }catch(Exception) { }
                        }
                    }
                    last_state = current_state;
                }
            }
            catch (Exception) { }
        }

        // Action when user clicks Disconnect from hardware
        public void Disconnect()
        {
            try
            {
                if (!IsEnabled()) return;
            
                if (serial == null) return;
                is_connected = false;

                lock (serial_lock)
                {
                    try { serial.WriteLine("DISCONNECT"); }
                    catch (Exception) { }
                }

                serial.Close();
                serial = null;
            }
            catch (Exception) { }
            finally
            {
                serial = null;
                try { if (thread != null) thread.Join(); } catch(Exception) { }
                thread = null;
            }
        }

        // Action when user clicks Stop button
        public void Stop()
        {
            try
            {
                if (serial == null) return;
                serial.WriteLine("STOP");
            }
            catch (Exception) { }
        }

        public string GetName() { return "Serial Port Controller"; }

        // Action when changes to settings are confirmed or loaded during DMC startup
        public bool ApplySettings() { return true; }

        // Needs to return if device is connected 
        public bool IsConnected() { return false; }

        // Is device is enabled 
        public bool IsEnabled() { return true; }

        // Called before starting recipe
        public bool OnRecipeStart() { return true; }

        // Called after recipe is finished or stopped
        public void OnRecipeFinish() { }

        // Get device settings
        public IDeviceSettings GetSettings() { return null; }

        // Get error message ( is called if Connect returns false )
        public string GetErrorMessage() { return Base.Functions.GetLastErrorMessage(); }
    }
}
