using Base;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LaserPlugin
{

    public class Command : Core.ICommand
    {
        Laser laser;
        public CommandParameters parameters;
        // methods
        public static Core.ICommand Create() { return new Command(); } // used by Code.dll to create this command
        public override System.Drawing.Bitmap GetIcon() { return Properties.Resources.laser16; }

        public Command()
            : base("my_laser", "My Laser", "My laser control")
        {
            foreach (var device in Base.SystemDevices.devices)
                if (device is Plugin)
                {
                    Plugin cp = device as Plugin;
                    laser = cp.Laser;
                    //settings = (Settings)cp.GetSettings(); Settings if needed
                }

            parameters = new CommandParameters();
            Add(parameters);
        }

        // when user clicks on this command in recipe command list, this method is called
        public override Core.ICommandGUI GetGUI()
        {
            //GUI can be created manually
            //CommandGUI gui = new CommandGUI();
            //gui.SetGUI(this);
            //return gui;

            //GUI can be created automatically from parameters
            HeaderGroupBox header = new HeaderGroupBox();
            header.Text = $"{CommandParameters.FN}";
            return ICommandGUI.GetGUIForm(parameters.parameters, new List<Control> { header }, parameters);
        }

        // User clicked Compile, so check parameters
        public override bool Compile()
        {
            if (!ParseAll()) return false;

            if (parameters.set_pp_frequency.value)
            {
                if (parameters.pp_frequency.number < 200 || parameters.pp_frequency.number > 10000) return Base.Functions.Error(this, "'" + parameters.pp_frequency.title + "' must be in [200kHz..10000kHz] range!");
            }

            if (parameters.set_mod_frequency.value)
            {
                if (parameters.mod_frequency.number < 200 || parameters.mod_frequency.number > 10000) return Base.Functions.Error(this, "'" + parameters.mod_frequency.title + "' must be in [200kHz..10000kHz] range!");
            }

            if (parameters.set_mod_efficiency.value)
            {
                if (parameters.mod_efficiency.number < 0 || parameters.mod_efficiency.number > 100) return Base.Functions.Error(this, "'" + parameters.mod_efficiency.title + "' must be in [0%..100%] range!");
            }
            return true;
        }

        

        // run this command
        public override bool Run()
        {
            if (laser == null) return Base.Functions.Error(this, $"Unable to run {Settings.deviceName} command. Laser is null. ");

            try
            {
                // lets compile again
                if (!Compile()) return false;

                // check if we need to change LD state
                if (parameters.set_ld.value)
                {
                    laser.SetState(parameters.ld_on.value ? "Laser ON" : "Laser OFF");

                    // set laser on or off
                    //if (!laser.SetLD(ld_on.value)) return false;

                    if (parameters.ld_on.value)
                    {
                        laser.SetState("Waiting for laser ready ...");
                        while (!Base.State.is_cancel && !Base.State.is_exit)
                        {
                            //if (laser.status.LaserStatus)
                            if (true)
                            {
                                laser.SetState("Laser is ready");
                                break;
                            }
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                    Base.StatusBar.Set(parameters.ld_on.value ? "Laser is ON" : "Laser is OFF", true);
                }

                // check if we need to change shutter state
                if (parameters.set_shutter.value)
                {
                    //if (!laser.Shutter(shutter_open.value)) return Base.Functions.Error(this, "Unable to set shutter. ");
                }

                if (parameters.set_pp_frequency.value)
                {
                    //if (!laser.SetPPFrequency(pp_frequency.number * 1000)) return Base.Functions.Error(this, "Unable to set PP frequency. ");
                }
                if (Base.State.IsCancel) return true;

                if (parameters.set_mod_frequency.value)
                {
                    //if (!laser.SetMOD2Frequency(mod_frequency.number * 1000)) return Base.Functions.Error(this, "Unable to set MOD frequency. ");
                }
                if (Base.State.IsCancel) return true;

                if (parameters.set_mod_width.value)
                {
                    //if (!laser.SetMOD2Width(mod_width.number)) return Base.Functions.Error(this, "Unable to set MOD width. ");
                }
                if (Base.State.IsCancel) return true;

                if (parameters.set_mod_delay.value)
                {
                    //if (!laser.SetMOD2Delay(mod_delay.number)) return Base.Functions.Error(this, "Unable to set MOD delay. ");
                }
                if (Base.State.IsCancel) return true;

                if (parameters.set_mod_efficiency.value)
                {
                    //if (!laser.SetMOD2Efficiency(mod_efficiency.number)) return Base.Functions.Error(this, "Unable to set MOD efficiency. ");
                }

                if (Base.State.IsCancel) return true;

                return true;
            }
            catch (Exception ex)
            {
                return Base.Functions.Error(this, $"Unable to run {Settings.deviceName} command. ", ex);
            }
        }
    }
}
