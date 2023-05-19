using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using Base;
using Base.Shapes;
using Core;
using Core.Commands;


namespace JoinAndHatchPlugin
{
    public partial class GUI : ICommandGUI
    {
        public GUI()
        {
            InitializeComponent();
        }

        private static GUI gui = null;
        private static JoinAndHatch command = null;

        public override void HideGUI() { command = null; hatching.Set(null); }
        public static ICommandGUI Get(JoinAndHatch cmd)
        {
            if (gui == null) gui = new GUI();
            command = cmd;

            gui.SetGUI();

            return gui;
        }

        public void SetGUI()
        {
            hatching.Set(command.hatching);
            Set(command);
        }
    }





    public class JoinAndHatch : ICommand
    {
        public static void AddCommandToCommandList()
        {
            DMC.Helpers.AddTool(DMC.ToolLocation.HomeTab, Core.ICommand.AddCreator(typeof(JoinAndHatch), UN, "Recipe Flow"), FN);//.SetImage(Properties.Resources.distance_sensor, false);
        }

        const string UN = "JoinAndHatch";
        const string FN = "Join And Hatch";
        const string DESC = "Join And Hatch";

        public Hatching hatching = new Hatching();

        public JoinAndHatch()
            : base(UN, FN, DESC)
        {
            Add(hatching);
        }

        public static ICommand Create() { return new JoinAndHatch(); }


        /// <summary>
        /// Create 2D geometry for wrapping
        /// </summary>
        private bool Make2DCommands(ref ActionCommandList res)
        {
            CADProcessor collector = new CADProcessor();
            collector.CollectGeometryOnly = true;

            CADProcessor default_processor = Recipe.processor;
            if (default_processor.last_marking_parameters != null)
                collector.last_marking_parameters = default_processor.last_marking_parameters;
            Recipe.SetNewProcessor(collector, false);

            bool ok = false;
            try
            {
                ok = CompileRunChilds(false) && collector.PostCompileRun();
            }
            catch (Exception ex) { Functions.ErrorF("Unable to prepare 2D layout. ", ex); }
            Recipe.processor.RemoveFromView();

            Recipe.processor = default_processor;
            if (!ok) { collector.Reset(); return false; }
            res = (ActionCommandList)collector.commands.Clone();
            if (res.Count > 0 && !(res[0] is MarkingParameters))
                res.Insert(default_processor.last_marking_parameters);

            collector.Reset();
            return true;
        }

        public override bool CanContainChilds()
        {
            return true;
        }

        public override ICommandGUI GetGUI()
        {
            return GUI.Get(this);
        }

        public override bool Compile()
        {
            if (!ParseAll()) return false;

            ActionCommandList shape_2d = null;
            if (!Make2DCommands(ref shape_2d)) return false;
            if (shape_2d == null || shape_2d.Count < 1) return true;
            //hatching.Enabled = true;
            if (hatching.Enabled)
            {
                if (!hatching.Compile()) return false;

                ActionCommandList hatch = new ActionCommandList();
                hatching.Hatch(shape_2d, shape_2d.GetCube(), ref hatch, Recipe.processor);
                hatching.Combine(hatch, shape_2d, Recipe.processor);

                hatch.SetICommand(this);
                Recipe.processor.Add(hatch);
            }
            else
            {
                Recipe.processor.Add(shape_2d);
            }

            return true;
        }

        public override bool Run()
        {
            if (!Compile()) return false;
            return true;
        }
    }
    

}
