using Base;
using Base.UIBase.Classes;
using System;

namespace LaserPlugin
{
    public enum LaserState
    {
        Error = 0,
        Warmup = 1,
        Ready = 2,
        Emission = 3
    }
    public class LaserStatus : StatusParameters 
    {
        public const string UN = "edgewave_state";
        public const string FN = "EdgeWave Laser State";

        public IntParameter trigger_frequency = new IntParameter(nameof(trigger_frequency), "Trigger Frequency (kHz)", "Trigger Frequency", 0);
        public DoubleParameter laser_power = new DoubleParameter(nameof(laser_power), "Laser Power (W)", 0);
        public IntParameter error_code = new IntParameter(nameof(error_code), "Error Code", "Error Code", 0);
        public IntSParameter state = new IntSParameter(nameof(state), "Laser State", "Laser State", 0, Enum.GetNames(typeof(LaserState)));


        public LaserStatus() : base(UN, FN, FN, true)
        {
            //Add all variables created using AddStatus() method, doing so will create global variables.
            AddStatus(trigger_frequency);
            AddStatus(laser_power);
            AddStatus(error_code);
            AddStatus(state);
        }
        
        public int TriggerFrequency
        {
            get => trigger_frequency.value;
            set => trigger_frequency.Set(value);
        }

        public double LaserPower
        {
            get => laser_power.value;
            set => laser_power.Set(value);
        }

        public int ErrorCode
        {
            get => error_code.value;
            set => error_code.Set(value);
        }

        public LaserState State
        {
            get => (LaserState)state.value;
            set => state.Set((int)value);
        }
    }
}
