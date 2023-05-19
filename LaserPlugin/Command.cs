using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaserPlugin
{

    public class Command : Core.ICommand
    {
        // command parameters 
        public Base.BoolParameter set_shutter = new Base.BoolParameter("set_shutter", "Set Shutter", false);
        public Base.BoolParameter set_ld = new Base.BoolParameter("set_ld", "Set Laser On/Off", false);
        public Base.BoolParameter set_mod_frequency = new Base.BoolParameter("set_mod_frequency", "Set Modulator Frequency", false);
        public Base.BoolParameter set_mod_width = new Base.BoolParameter("set_mod_width", "Set Modulator Width", false);
        public Base.BoolParameter set_mod_delay = new Base.BoolParameter("set_mod_delay", "Set Modulator Delay", false);
        public Base.BoolParameter set_mod_efficiency = new Base.BoolParameter("set_mod_efficiency", "Set Modulator Efficiency", false);
        public Base.BoolParameter set_pp_frequency = new Base.BoolParameter("set_pp_frequency", "Set PP Frequency", false);

        public Base.BoolParameter ld_on = new Base.BoolParameter("ld_on", "Laser", false);
        public Base.BoolParameter shutter_open = new Base.BoolParameter("shutter_open", "Shutter", false);
        public Base.ParamSD mod_frequency = new Base.ParamSD("mod_frequency", "Frequency (kHz)", (Base.Settings.LaserControl.CurrentLaserFrequency / 1000).ToString());
        public Base.ParamSD mod_width = new Base.ParamSD("mod_width", "Width (ns)", "0");
        public Base.ParamSD mod_delay = new Base.ParamSD("mod_delay", "Delay (ns)", "0");
        public Base.ParamSD mod_efficiency = new Base.ParamSD("mod_efficiency", "Efficiency (%)", "0");
        public Base.ParamSD pp_frequency = new Base.ParamSD("pp_frequency", "PP Frequency (kHz)", (Base.Settings.LaserControl.CurrentLaserFrequency / 1000).ToString());

        // methods
        public static Core.ICommand Create() { return new Command(); } // used by Code.dll to create this command
        public override System.Drawing.Bitmap GetIcon() { return Properties.Resources.laser16; }

        public Command()
            : base("my_laser", "My Laser", "My laser control")
        {

            Add(set_shutter); Add(set_ld); Add(set_mod_frequency); Add(set_mod_width); Add(set_mod_delay); Add(set_mod_efficiency); Add(set_pp_frequency);
            Add(ld_on); Add(shutter_open); Add(mod_frequency); Add(mod_width); Add(mod_delay); Add(mod_efficiency); Add(pp_frequency);
        }

        // when user clicks on this command in recipe command list, this method is called
        public override Core.ICommandGUI GetGUI()
        {
            CommandGUI gui = new CommandGUI();
            gui.SetGUI(this);
            return gui;
        }

        // User clicked Compile, so check parameters
        public override bool Compile()
        {
            if (!ParseAll()) return false;

            if (set_pp_frequency.value)
            {
                if (pp_frequency.number < 200 || pp_frequency.number > 10000) return Base.Functions.Error("'" + pp_frequency.title + "' must be in [200kHz..10000kHz] range!");
            }

            if (set_mod_frequency.value)
            {
                if (mod_frequency.number < 200 || mod_frequency.number > 10000) return Base.Functions.Error("'" + mod_frequency.title + "' must be in [200kHz..10000kHz] range!");
            }

            if (set_mod_efficiency.value)
            {
                if (mod_efficiency.number < 0 || mod_efficiency.number > 100) return Base.Functions.Error("'" + mod_efficiency.title + "' must be in [0%..100%] range!");
            }
            return true;
        }

        // just show message at status bar
        void SetState(string state)
        {
            Base.StatusBar.Set(state);
        }

        // run this command
        public override bool Run()
        {
            try
            {
                // lets compile again
                if (!Compile()) return false;

                // check if we need to change LD state
                if (set_ld.value)
                {
                    SetState(ld_on.value ? "Laser ON" : "Laser OFF");

                    // set laser on or off
                    //if (!laser.SetLD(ld_on.value)) return false;

                    if (ld_on.value)
                    {
                        SetState("Waiting for laser ready ...");
                        while (!Base.State.is_cancel && !Base.State.is_exit)
                        {
                            //if (laser.status.LaserStatus)
                            if (true)
                            {
                                SetState("Laser is ready");
                                break;
                            }
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                    Base.StatusBar.Set(ld_on.value ? "Laser is ON" : "Laser is OFF", true);
                }

                // check if we need to change shutter state
                if (set_shutter.value)
                {
                    //if (!laser.Shutter(shutter_open.value)) return false;
                }

                if (set_pp_frequency.value)
                {
                    //if (!laser.SetPPFrequency(pp_frequency.number * 1000)) return false;
                }
                if (Base.State.IsCancel) return true;

                if (set_mod_frequency.value)
                {
                    //if (!laser.SetMOD2Frequency(mod_frequency.number * 1000)) return false;
                }
                if (Base.State.IsCancel) return true;

                if (set_mod_efficiency.value)
                {
                    //if (!laser.SetMOD2Efficiency(mod_efficiency.number)) return false;
                }

                if (Base.State.IsCancel) return true;

                return true;
            }
            catch (Exception ex)
            {
                return Base.Functions.Error("Unable to run My Laser command. ", ex);
            }
        }
    }
}
