using Base;
using DMC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPlugin
{
    public class LaserToolClick : ITool
    {
        string name;
        public LaserToolClick(string name)
        {
            this.name = name;
        }
        public void Run()
        {
            MessageBox.Show(name);
        }
    }

    class PopupTools
    {
        static List<IFormTool> tool_list = new List<IFormTool>();

        private static void Add(IFormTool tool)
        {
            if (tool == null) return;
            tool_list.Add(tool);
        }

        private static void RemoveTools()
        {
            foreach (var it in tool_list)
                DMC.Helpers.RemoveTool(it.GetKey());
            tool_list.Clear();
        }

        private static void AddTools()
        {
            foreach (var it in Settings.LaserControls)
                Add(DMC.Helpers.AddToPopup(tool.GetKey(), it.Name, new LaserToolClick(it.Name), it.Name, false, false, null));
        }

        public static void FillDropDown(object sender, EventArgs args)
        {
            RemoveTools();
            AddTools();
        }

        static IFormTool tool;
        public static void Create()
        {
            tool = DMC.Helpers.AddPopupMenuTool(ToolLocation.HomeTab.ToString(), "Test Tools", "Laser Tools", "Laser Tools", "Show my tools", true, null, FillDropDown);
        }
    }
}
