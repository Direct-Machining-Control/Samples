using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Example
{
    /// <summary>
    /// Example power meter device settings implementation.
    /// </summary>
    public class DeviceSettings : Base.IPowerMeterSettings
    {
        Device power_meter = null;

        DoubleExtParameter max_power_to_show = new DoubleExtParameter(nameof(max_power_to_show), "Max Power to Show (W)", "What max power should be shown", 1, 0.01, 1000, 0.01);

        /// <summary>
        /// Maximum power to show for device (W)
        /// </summary>
        public double MaxPowerToShow { get { return max_power_to_show.value; } }

        public DeviceSettings() : base("pm_example", "Power Meter Example", "Simulates power")
        {
            Parameters.Clear();
            Add(max_power_to_show);
        }

        public override bool CanPerformAutomaticDetection()
        {
            return false;
        }

        public override IPowerMeter GetPowerMeter()
        {
            if (power_meter != null) return power_meter;
            return power_meter = new Device(this);
        }
    }
}
