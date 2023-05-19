using Base;
using Base.Shapes;
using Core;
using Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlugin
{
    public class MyPoly : ICommand
    {
        public static void AddCommandToCommandList()
        {
            // add command to Geometry->More list
            DMC.Helpers.AddTool(DMC.ToolLocation.HomeTab, Core.ICommand.AddCreator(typeof(MyPoly), UN, "Geometry"), FN);
        }

        const string UN = "MyPoly";
        const string FN = "MyPoly";
        const string DESC = "MyPoly";

        public MyPoly()
            : base(UN, FN, DESC)
        {
        }

        public static ICommand Create() { return new MyPoly(); }
        public override bool IsControlCommand { get { return false; } }

        public override bool CanContainChilds() { return true; }


        /// <summary>
        /// Create geometry from child nodes
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
            catch (Exception ex) { Functions.ErrorF("Unable to prepare 2D layout for wrapping/projection. ", ex); }
            Recipe.processor.RemoveFromView();

            Recipe.processor = default_processor;
            if (!ok) { collector.Reset(); return false; }
            res = (ActionCommandList)collector.commands.Clone();
            if (res.Count > 0 && !(res[0] is MarkingParameters))
                res.Insert(default_processor.last_marking_parameters);

            collector.Reset();
            return true;
        }

        public override bool Compile()
        {
            if (!ParseAll()) return false;

            // evaluate formula/variable and save value to variable
            double radius = 0;
            if (!Evaluation.Parse("radius", ref radius)) return false;

            // generate polygon
            List<Vector3> points = new List<Vector3>();
            int corners = 5;
            double angle_increment = 360 / corners;

            for (int i = 0; i < corners; i++)
            {
                points.Add(new Vector3(radius * Math.Cos(i * angle_increment * Geometry.deg2rad), radius * Math.Sin(i * angle_increment * Geometry.deg2rad), 0));
            }

            PolylineCommand p = new PolylineCommand(points, true);

            var list = new ActionCommandList(); // create command list

            if (!Make2DCommands(ref list)) return false; // get geometry from child nodes

            var cube_of_childs = list.GetCube();
            var cube_of_outline = p.GetCube();

            list.Move(cube_of_outline.Center - cube_of_childs.Center); // move to the center of outline

            list.Add(p); // add polygon to the list

            var hatch = new ActionCommandList(); // create command list where hatching lines will go
            Hatching.HatchLines(list, 0, list.GetCube(), 0.1, 45, 0, 0, ref hatch, Cube.None); // line hatch
            hatch.MirrorX(hatch.GetCube().CenterX); // mirror hatching lines 
            hatch.Move(new Vector3(10, 10, 0)); // move hatch

            // add polygon to recipe
            Recipe.processor.Add(hatch);


            return true;
        }
        
        public override bool Run()
        {
            var cad = Recipe.processor; // take cad processor (buffer with ActionCommands)

            bool split_code = true;

            // do marking 6 times
            for (int i = 0; i < 6; i++)
            {
                if (!Compile()) return false;
                if (split_code)
                {
                    bool ok = cad.Run(); // force to generate and run motion code
                    if (!ok) {
                        Base.StatusBar.Set("Unable to run code. " + Base.Functions.GetLastErrorMessage(), true, StatusBar.MessageType.Error);
                        i--;
                    }
                }
            }

            if (!split_code) return cad.Run(); // force to generate and run motion code
            return true;
        }
    }
}
