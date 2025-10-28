using Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Example
{
    /// <summary>
    /// Example power meter device implementation.
    /// </summary>
    public class Device : Base.IPowerMeter
    {
        #region Local variables
        private bool isConnected = false;
        private double lastMeasured = 0;
        private double averagingTime = 0;
        private bool isMeasurementActive = false;
        private ulong measurementID = 0;
        private Task measurementTask = null;
        private DeviceSettings deviceSettings;
        #endregion

        public Device(DeviceSettings deviceSettings)
        {
            this.deviceSettings = deviceSettings;
        }

        #region Base.IPowerMeter implementation
        public bool IsConnected => isConnected;

        public double LastMeasured => lastMeasured;

        /// <summary>
        /// Get new power measurement.
        /// </summary>
        public double GetPower 
        { 
            get 
            {
                // wait for new measurement
                Stopwatch sw = new Stopwatch(); sw.Start();
                var currentID = measurementID;
                while (true)
                {
                    if (!isMeasurementActive || !isConnected) return -1;
                    if (measurementID > currentID) break;
                    System.Threading.Thread.Sleep(10);
                    if (sw.Elapsed.TotalSeconds > 5) return -1; // timeout
                }

                return lastMeasured; 
            }
        }

        public double AveragingTime { get => averagingTime; set => averagingTime = value; }

        public bool Connect()
        {
            Disconnect();
            isConnected = true;
            measurementID = 0;
            return isConnected;
        }

        public void Disconnect()
        {
            Stop();
            isConnected = false;
        }

        public string GetErrorMessage()
        {
            return Base.Functions.GetLastErrorMessage();
        }

        public string GetName()
        {
            return "Power Meter Example";
        }

        public bool SetWavelength(double wavelength)
        {
            return true;
        }


        /// <summary>
        /// Start continuous power measurement.
        /// </summary>
        /// <returns>FALSE if error</returns>
        public bool Start()
        {
            Stop();
            measurementTask = Task.Run(() =>
            {
                // simulate power measurement
                Stopwatch sw = new Stopwatch(); sw.Start();
                while (isConnected && isMeasurementActive)
                {
                    lastMeasured = Base.Geometry.Mod(sw.Elapsed.TotalSeconds, deviceSettings.MaxPowerToShow); // Simulate power measurement between 10 and 100
                    measurementID++;
                    System.Threading.Thread.Sleep(100); // Simulate a delay for measurement
                }
            });
            isMeasurementActive = true;

            return true;
        }

        /// <summary>
        /// Stop continuous power measurement.
        /// </summary>
        public void Stop()
        {
            isMeasurementActive = false;
            measurementTask?.Wait();
        }


        #endregion
    }
}
