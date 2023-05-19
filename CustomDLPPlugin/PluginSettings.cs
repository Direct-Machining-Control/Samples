using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomDLPPlugin
{
    class CustomDLPPluginSettings : IDeviceSettings
    {
        #region STATIC FIELDS
        static readonly string _UniqueName_ = "CustomDLPPluginSettings";
        static readonly string _FriendlyName_ = "CustomDLPPlugin";
        static readonly string _Description_ = "CustomDLPPlugin Settings Description";
        #endregion

        #region VARIABLES AND PARAMETERS
        BoolParameter mode = new BoolParameter("mode", "Mode", "Unique mode option", true, "Unique Process", "Add Border");
        #endregion

        public CustomDLPPluginSettings() : base(_UniqueName_, _FriendlyName_, _Description_)
        {
            //Add(<variable_name);
            Add(mode);
        }

        public bool ModeUniqueView { get { return mode.value; } }
        public bool ModeAddBorder { get { return !mode.value; } }

        public override System.Windows.Forms.UserControl GetGUI()
        {
            CustomDLPPluginSettingsGUI gui = new CustomDLPPluginSettingsGUI();
            gui.Set(this);
            return gui;
        }
    }
}
