using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinAndHatchPlugin
{
    class JoinAndHatchPluginSettings : IDeviceSettings
    {
        #region STATIC FIELDS
        static readonly string _UniqueName_ = "JoinAndHatchPluginSettings";
        static readonly string _FriendlyName_ = "JoinAndHatchPlugin Settings Friendly Name";
        static readonly string _Description_ = "JoinAndHatchPlugin Settings Description";
        #endregion

        #region VARIABLES AND PARAMETERS
        #endregion

        public JoinAndHatchPluginSettings() : base(_UniqueName_, _FriendlyName_, _Description_)
        {
            //Add(<variable_name);
        }

        public override System.Windows.Forms.UserControl GetGUI()
        {
            JoinAndHatchPluginSettingsGUI gui = new JoinAndHatchPluginSettingsGUI();
            gui.Set(this);
            return gui;
        }
    }
}
