using Base;
using Base.Shapes;
using Core;
using Core.Commands;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LaserPlugin
{
    public class Laser_MarkingParameters : Core.Commands.MarkingParamsEx
    {
        public CommandParameters param;
        Laser laser;
        Settings settings;
        bool use_params = false;
        public Laser_MarkingParameters(Laser laser, Settings settings) : base("my_laser_mp_ext")
        {
            this.laser = laser;
            this.settings = settings;
            param = new CommandParameters();
            Add(param);
        }

        protected override MultiParameter CloneObj()
        {
            Laser_MarkingParameters p = new Laser_MarkingParameters(laser, settings);
            p.AssignValuesFrom(parameters);
            return p;
        }
        bool CanUse(MarkingParams prm)
        {
            if (!settings.Enabled) return false;
            if (prm.LaserDevice.laser.Value != Settings.deviceName) return false;
            return true;
        }
        public override UserControl GetGUI(MarkingParams prm)
        {
            if (!CanUse(prm)) return null;

            //GUI can be created manually
            //CommandGUI gui = new CommandGUI();
            //gui.SetGUI(this);
            //return gui;

            //GUI can be created automatically from parameters
            HeaderGroupBox header = new HeaderGroupBox();
            header.Text = $"{CommandParameters.FN}";
            return ICommandGUI.GetGUIForm(param.parameters, new List<Control> { header }, param);
        }

        public override bool Compile(MarkingParams prm, MarkingParameters action)
        {
            if (settings == null)
                return Functions.Error(this, $"Failed to get {Settings.deviceName} plugin settings. ");

            use_params = CanUse(prm);
            if (!use_params) return true;


            if (param.set_pp_frequency.value)
            {
                if (param.pp_frequency.number < 200 || param.pp_frequency.number > 10000) return Base.Functions.Error(this, "'" + param.pp_frequency.title + "' must be in [200kHz..10000kHz] range!");
            }

            if (param.set_mod_frequency.value)
            {
                if (param.mod_frequency.number < 200 || param.mod_frequency.number > 10000) return Base.Functions.Error(this, "'" + param.mod_frequency.title + "' must be in [200kHz..10000kHz] range!");
            }

            if (param.set_mod_efficiency.value)
            {
                if (param.mod_efficiency.number < 0 || param.mod_efficiency.number > 100) return Base.Functions.Error(this, "'" + param.mod_efficiency.title + "' must be in [0%..100%] range!");
            }

            return true;
        }

        public bool NeedToSetup =>
        param.set_shutter.value ||
        param.set_ld.value ||
        param.set_mod_frequency.value ||
        param.set_mod_width.value ||
        param.set_mod_delay.value ||
        param.set_mod_efficiency.value ||
        param.set_pp_frequency.value;

        public override ActionCommand GetActionCommand()
        {
            if (!NeedToSetup || !use_params) return null;

            Laser_MPExtState state = new Laser_MPExtState(laser);

            if (param.set_shutter.value)
                state.ShutterOpen = param.shutter_open.value;

            if (param.set_ld.value)
                state.LdOn = param.ld_on.value;

            if (param.set_mod_frequency.value)
                state.ModFrequency = param.mod_frequency.number;

            if (param.set_mod_width.value)
                state.ModWidth = param.mod_width.number;

            if (param.set_mod_delay.value)
                state.ModDelay = param.mod_delay.number;

            if (param.set_mod_efficiency.value)
                state.ModEfficiency = param.mod_efficiency.number;

            if (param.set_pp_frequency.value)
                state.PpFrequency = param.pp_frequency.number;

            state.SetShutter = param.set_shutter.value;
            state.SetLd = param.set_ld.value;
            state.SetModFrequency = param.set_mod_frequency.value;
            state.SetModWidth = param.set_mod_width.value;
            state.SetModDelay = param.set_mod_delay.value;
            state.SetModEfficiency = param.set_mod_efficiency.value;
            state.SetPpFrequency = param.set_pp_frequency.value;

            return state;
        }

    }
    public class Laser_MPExtState : ActionCommand
    {
        public bool SetShutter { get; set; }
        public bool ShutterOpen { get; set; }

        public bool SetLd { get; set; }
        public bool LdOn { get; set; }

        public bool SetModFrequency { get; set; }
        public double ModFrequency { get; set; }

        public bool SetModWidth { get; set; }
        public double ModWidth { get; set; }

        public bool SetModDelay { get; set; }
        public double ModDelay { get; set; }

        public bool SetModEfficiency { get; set; }
        public double ModEfficiency { get; set; }

        public bool SetPpFrequency { get; set; }
        public double PpFrequency { get; set; }


        private readonly Laser laser;

        public Laser_MPExtState(Laser laser)
        {
            this.laser = laser;
        }

        public bool NeedToSet =>
        SetShutter ||
        SetLd ||
        SetModFrequency ||
        SetModWidth ||
        SetModDelay ||
        SetModEfficiency ||
        SetPpFrequency;

        public override object Clone()
        {
            var clone = new Laser_MPExtState(laser)
            {
                SetShutter = SetShutter,
                ShutterOpen = ShutterOpen,

                SetLd = SetLd,
                LdOn = LdOn,

                SetModFrequency = SetModFrequency,
                ModFrequency = ModFrequency,

                SetModWidth = SetModWidth,
                ModWidth = ModWidth,

                SetModDelay = SetModDelay,
                ModDelay = ModDelay,

                SetModEfficiency = SetModEfficiency,
                ModEfficiency = ModEfficiency,

                SetPpFrequency = SetPpFrequency,
                PpFrequency = PpFrequency
            };

            return CloneBase(clone);
        }

        public bool IsSame(Laser_MPExtState prm)
        {
            if (prm == null) return false;

            bool isSame = true;

            isSame &= prm.SetShutter == SetShutter;
            isSame &= prm.ShutterOpen == ShutterOpen;

            isSame &= prm.SetLd == SetLd;
            isSame &= prm.LdOn == LdOn;

            isSame &= prm.SetModFrequency == SetModFrequency;
            isSame &= Math.Abs(prm.ModFrequency - ModFrequency) < Base.Settings.Epsilon;

            isSame &= prm.SetModWidth == SetModWidth;
            isSame &= Math.Abs(prm.ModWidth - ModWidth) < Base.Settings.Epsilon;

            isSame &= prm.SetModDelay == SetModDelay;
            isSame &= Math.Abs(prm.ModDelay - ModDelay) < Base.Settings.Epsilon;

            isSame &= prm.SetModEfficiency == SetModEfficiency;
            isSame &= Math.Abs(prm.ModEfficiency - ModEfficiency) < Base.Settings.Epsilon;

            isSame &= prm.SetPpFrequency == SetPpFrequency;
            isSame &= Math.Abs(prm.PpFrequency - PpFrequency) < Base.Settings.Epsilon;

            return isSame;
        }


        public override bool Run(SystemMotion motion)
        {
            if (laser == null)
                return Base.Functions.Error(this, $"Unable to run {Settings.deviceName} command. Laser is null. ");

            try
            {
                // check if we need to change LD state
                if (SetLd)
                {
                    laser.SetState(LdOn ? "Laser ON" : "Laser OFF");

                    // set laser on or off
                    //if (!laser.SetLD(ld_on.value)) return false;

                    if (LdOn)
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
                    Base.StatusBar.Set(LdOn ? "Laser is ON" : "Laser is OFF", true);
                }

                // check if we need to change shutter state
                if (SetShutter)
                {
                    //if (!laser.Shutter(shutter_open.value)) return Base.Functions.Error(this, "Unable to set shutter. ");
                }

                if (SetPpFrequency)
                {
                    //if (!laser.SetPPFrequency(PpFrequency * 1000)) return Base.Functions.Error(this, "Unable to set PP frequency. ");
                }
                if (Base.State.IsCancel) return true;

                if (SetModFrequency)
                {
                    //if (!laser.SetMOD2Frequency(ModFrequency * 1000)) return Base.Functions.Error(this, "Unable to set MOD frequency. ");
                }
                if (Base.State.IsCancel) return true;

                if (SetModWidth)
                {
                    //if (!laser.SetMOD2Width(ModWidth)) return Base.Functions.Error(this, "Unable to set MOD width. ");
                }
                if (Base.State.IsCancel) return true;

                if (SetModDelay)
                {
                    //if (!laser.SetMOD2Delay(ModDelay)) return Base.Functions.Error(this, "Unable to set MOD delay. ");
                }
                if (Base.State.IsCancel) return true;

                if (SetModEfficiency)
                {
                    //if (!laser.SetMOD2Efficiency(ModEfficiency)) return Base.Functions.Error(this, "Unable to set MOD efficiency. ");
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
