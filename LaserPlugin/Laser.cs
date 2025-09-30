using Base;
using Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LaserPlugin
{
    public class Laser
    {
        LaserStatus _laserStatus = new LaserStatus();
        Settings settings;
        Task monitoringThread = null;
        CancellationTokenSource tokenSource;
        CancellationToken cancellationToken;
        public Laser(Settings settings)
        {
            this.settings = settings;
        }
        public LaserStatus LaserStatus => _laserStatus;

        public bool IsConnected => true; //Your implementation

        internal bool Connect()
        {
            if (!settings.Enabled)
            {
                if (IsConnected) Disconnect(); return true;
            }

            //Your connect to device implementation

            tokenSource = new CancellationTokenSource();
            cancellationToken = tokenSource.Token;

            monitoringThread = Task.Factory.StartNew(MonitoringThread, cancellationToken);

            return true;
        }

        private void MonitoringThread()
        {
            while (!cancellationToken.IsCancellationRequested && IsConnected && !Base.State.is_exit)
            {
                //Your monitoring implementation

                //_laserStatus.LaserPower = 1;
                //_laserStatus.State = LaserState.Warmup;


                //If laser frequency can be monitored, Base.Settings.LaserControls[i].LaserFrequencyActual needs to be updated here! Example below:
                //for (int i = 0; i < Base.Settings.LaserControls.Count; i++)
                //    if (Base.Settings.LaserControls[i].SelectedToolName == Settings.deviceName)
                //        if (Base.Settings.LaserControls[i].LaserFrequencyActual != LaserStatus.Frequency) //Actual frequency is in Hz
                //            Base.Settings.LaserControls[i].LaserFrequencyActual = LaserStatus.Frequency;
            }
        }

        internal void Disconnect()
        {
            //Your disconnect to device implementation

            if (monitoringThread != null && !monitoringThread.IsFaulted && !monitoringThread.IsCompleted && !monitoringThread.IsCanceled)
            {
                try
                {
                    tokenSource.Cancel();
                    monitoringThread.Wait();
                }
                catch { }
                finally
                {
                    tokenSource.Dispose();
                }
            }
        }



        // just show message at status bar
        public void SetState(string state)
        {
            Base.StatusBar.Set(state);
        }

        //public bool Shutter(bool value)
        //{
        //    //Your implementation
        //    return true;
        //}
        //public bool SetPPFrequency(double value)
        //{
        //    //Your implementation
        //    return true;
        //}
        //public bool SetMOD2Frequency(double value)
        //{
        //    //Your implementation
        //    return true;
        //}
        //public bool SetMOD2Efficiency(double value)
        //{
        //    //Your implementation
        //    return true;
        //}
        
    }
}
