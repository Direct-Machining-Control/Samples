using Base;
using System.Collections.Generic;

namespace LaserPlugin
{
    public class CommandParameters : MultiParameter, IGUIManagerParameter
    {
        public const string UN = "my_laser_command_parameters";
        public const string FN = "My Laser Parameters";
        // command parameters 
        public Base.BoolParameter set_shutter = new Base.BoolParameter("set_shutter", "Set Shutter", false);
        public Base.BoolParameter set_ld = new Base.BoolParameter("set_ld", "Set Laser On/Off", false);
        public Base.BoolParameter set_mod_frequency = new Base.BoolParameter("set_mod_frequency", "Set Modulator Frequency", false);
        public Base.BoolParameter set_mod_width = new Base.BoolParameter("set_mod_width", "Set Modulator Width", false);
        public Base.BoolParameter set_mod_delay = new Base.BoolParameter("set_mod_delay", "Set Modulator Delay", false);
        public Base.BoolParameter set_mod_efficiency = new Base.BoolParameter("set_mod_efficiency", "Set Modulator Efficiency", false);
        public Base.BoolParameter set_pp_frequency = new Base.BoolParameter("set_pp_frequency", "Set PP Frequency", false);

        public Base.BoolParameter ld_on = new Base.BoolParameter("ld_on", "Laser", "Laser On/Off", false, "On", "Off");
        public Base.BoolParameter shutter_open = new Base.BoolParameter("shutter_open", "Shutter", "Shutter Control", false, "Open", "Close");
        public Base.ParamSD mod_frequency = new Base.ParamSD("mod_frequency", "Frequency (kHz)", (Base.Settings.LaserControl.LaserFrequencyActual / 1000).ToString());
        public Base.ParamSD mod_width = new Base.ParamSD("mod_width", "Width (ns)", "0");
        public Base.ParamSD mod_delay = new Base.ParamSD("mod_delay", "Delay (ns)", "0");
        public Base.ParamSD mod_efficiency = new Base.ParamSD("mod_efficiency", "Efficiency (%)", "0");
        public Base.ParamSD pp_frequency = new Base.ParamSD("pp_frequency", "PP Frequency (kHz)", (Base.Settings.LaserControl.LaserFrequencyActual / 1000).ToString());

        public CommandParameters() : base(UN, FN, FN)
        {
            Add(set_ld); Add(ld_on);
            Add(set_shutter); Add(shutter_open);
            Add(set_mod_frequency); Add(mod_frequency);
            Add(set_mod_width); Add(mod_width);
            Add(set_mod_delay); Add(mod_delay);
            Add(set_mod_efficiency); Add(mod_efficiency);
            Add(set_pp_frequency); Add(pp_frequency);
        }

        public List<IParameter> GetDependencies(IParameter prm)
        {
            switch (prm)
            {
                case BoolParameter _ when prm == set_shutter:
                    return new List<IParameter> { shutter_open };

                case BoolParameter _ when prm == set_ld:
                    return new List<IParameter> { ld_on };

                case BoolParameter _ when prm == set_mod_frequency:
                    return new List<IParameter> { mod_frequency };

                case BoolParameter _ when prm == set_mod_width:
                    return new List<IParameter> { mod_width };

                case BoolParameter _ when prm == set_mod_delay:
                    return new List<IParameter> { mod_delay };

                case BoolParameter _ when prm == set_mod_efficiency:
                    return new List<IParameter> { mod_efficiency };

                case BoolParameter _ when prm == set_pp_frequency:
                    return new List<IParameter> { pp_frequency };

                default:
                    return null;
            }
        }


        public bool IsIParameterEnabled(IParameter prm)
        {
            return true;
        }

        public bool IsIParameterVisible(IParameter prm)
        {
            switch (prm)
            {
                case BoolParameter _ when prm == shutter_open:
                    return set_shutter.value;

                case BoolParameter _ when prm == ld_on:
                    return set_ld.value;

                case ParamSD _ when prm == mod_frequency:
                    return set_mod_frequency.value;

                case ParamSD _ when prm == mod_width:
                    return set_mod_width.value;

                case ParamSD _ when prm == mod_delay:
                    return set_mod_delay.value;

                case ParamSD _ when prm == mod_efficiency:
                    return set_mod_efficiency.value;

                case ParamSD _ when prm == pp_frequency:
                    return set_pp_frequency.value;

                default:
                    return true; // Default to visible
            }
        }
    }
}
