using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestPlugin
{
    public partial class DistanceSensorCommandGUI : Core.ICommandGUI
    {
        public DistanceSensorCommandGUI()
        {
            InitializeComponent();
        }

        private static DistanceSensorCommandGUI gui = null;
        private DistanceSensorCommand command = null;
        public static Core.ICommandGUI Get(DistanceSensorCommand cmd) { 
            if (gui == null) gui = new DistanceSensorCommandGUI(); 
            gui.command = cmd; 
            gui.SetGUI(); 
            return gui; 
        }

        private void SetGUI()
        {
            position.Set(command.position);
            // Other fields are automatically assigned by (Name) which needs to be the same as IParameter.unique_name
        }
    }
}
