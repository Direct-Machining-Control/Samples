using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.IO;
using Base;
using Base.Shapes;
using Core;
using DMC;

namespace PointImport
{
    public class Plugin : IDevice
    {
        public Plugin()
        {
            Import3DFile.AddImporter();
            // create tool buttons
            //DMC.Helpers.AddTool(DMC.ToolLocation.HomeTab, "Geometry", "Import Points", new Tool(), null, false, false, PointImport.Properties.Resources.points);
            //DMC.Helpers.AddTool(DMC.ToolLocation.HomeTab, "Geometry", "Import Lines", new ToolImportHatch(), null, false, false, PointImport.Properties.Resources.points);
            //DMC.Helpers.AddTool(DMC.ToolLocation.HomeTab, "Geometry", "Add new shape", new Tool2(this), null, false, false, null);
            JogAndTeach.AddTool();

        }

        public bool Connect() { return true; }
        public void Stop() { }
        public string GetName() { return "points_form_step"; }
        public bool ApplySettings() { return true; }
        public void Disconnect() { }
        public bool IsConnected() { return false; }
        public bool IsEnabled() { return false; }
        public bool OnRecipeStart() { return true; }
        public void OnRecipeFinish() { }
        public IDeviceSettings GetSettings() { return null; }
        public string GetErrorMessage() { return Base.Functions.GetLastErrorMessage(); }

        // Create 3 shapes
        public void AddNewShape()
        {
            // Create new command
            Core.Commands.Cad cad = new Core.Commands.Cad();
            Core.CADProcessor proc = cad.GetCADProcessor();
            
            // create 3 shapes
            Base.Shapes.LineCommand shape1 = new Base.Shapes.LineCommand(new Vector3(-1, 0, 0), new Vector3(1, 0, 0));
            Base.Shapes.CircleCommand shape2 = new Base.Shapes.CircleCommand(Vector3.Zero, 1);

            List<Vector3> points = new List<Vector3>() { new Vector3(-1, 1, 0), new Vector3(0, 2, 0), new Vector3(1, 1, 0) };
            Base.Shapes.PolylineCommand shape3 = new Base.Shapes.PolylineCommand(points, true);

            // add shapes to command
            proc.commands.Add(shape1);
            proc.commands.Add(shape2);
            proc.commands.Add(shape3);
            proc.original_size = proc.commands.GetCube();
            cad.SetOriginalSize();

            // add command to recipe
            Core.Recipes.ActiveRecipe.AddCommand(cad);
        }

        // Make DMC command
        public static ICommand MakeCommand(ActionCommandList commands, string file_name)
        {
            Core.Commands.Cad command = new Core.Commands.Cad();
            command.proc.commands = commands;
            command.proc.original_size = commands.GetCube();

            command.hash.value = Core.Commands.FileImport.GetFileStamp(file_name);
            command.file_name.Value = file_name;
            command.reload_when_running.value = false;

            command.SetOriginalSize();
            command.Compile();

            return command;
        }


        public class ToolImportHatch : ITool
        {
            public ToolImportHatch() { }
            public void Run()
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Text files (*.txt)|*.txt";
                dialog.Title = "Select File";
                dialog.FileName = "";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ImportFile(dialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        Functions.Error("Unable to import file. ", ex);
                        Functions.ShowLastError();
                    }
                }
            }

            /// <summary>
            /// X1{TAB}Y1{TAB}X2{TAB}Y2{TAB}
            /// X1{TAB}Y1{TAB}Z1{TAB}X2{TAB}Y2{TAB}Z2
            /// </summary>
            private void ImportFile(string file_name)
            {
                TextReader tr = new StreamReader(file_name);
                ActionCommandList result = new ActionCommandList();

                string line = null;
                while ((line = tr.ReadLine()) != null)
                {
                    ProcessLine(line, result);
                }
                tr.Close();

                Recipes.ActiveRecipe.AddCommand(MakeCommand(result, file_name));
            }

            void ProcessLine(string line, ActionCommandList res)
            {
                var cols = line.Split('\t');
                if (cols.Length > 3)
                {
                    double x1 = 0, y1 = 0, x2 = 0, y2 = 0, z1 = 0, z2 = 0;
                    if (cols.Length > 5)
                    {
                        x1 = double.Parse(cols[0]);
                        y1 = double.Parse(cols[1]);
                        z1 = double.Parse(cols[2]);

                        x2 = double.Parse(cols[3]);
                        y2 = double.Parse(cols[4]);
                        z2 = double.Parse(cols[5]);
                    }
                    else
                    {
                        x1 = double.Parse(cols[0]);
                        y1 = double.Parse(cols[1]);
                        x2 = double.Parse(cols[2]);
                        y2 = double.Parse(cols[3]);
                    }


                    res.Add(new LineCommand(new Vector3(x1, y1, z1), new Vector3(x2,y2,z2)));
                }
            }
        }


    }

    // tool to create 3 shapes
    public class Tool2 : ITool
    {
        Plugin plugin;
        public Tool2(Plugin plugin) { this.plugin = plugin; }
        public void Run()
        {
            plugin.AddNewShape();
        }
    }


    // Tool to import points from stp file
    public class Tool : ITool
    {
        public Tool()
        {
        }
        public void Run()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Step files (*.stp)|*.stp";
            dialog.Title = "Select File";
            dialog.FileName = "";
            if (dialog.ShowDialog() == DialogResult.OK) {
                try
                {
                    ImportFile(dialog.FileName);
                }
                catch (Exception ex) {
                    Functions.Error("Unable to import file. ", ex);
                    Functions.ShowLastError();
                }
            }
        }

        void ProcessLine(string line, List<PointCommand> res)
        {
            if (line.Contains("CARTESIAN_POINT('',("))
            {
                string pattern = "CARTESIAN_POINT('',(";
                string pattern2 = "));";
                int index = line.IndexOf(pattern);

                line = line.Substring(index + pattern.Length);
                int index2 = line.IndexOf(pattern2);
                line = line.Substring(0, index2);


                string[] points = line.Split(',');
                if (points.Length != 3) return;
                double x = double.Parse(points[0]);
                double y = double.Parse(points[1]);
                double z = double.Parse(points[2]);
                res.Add(new PointCommand(new Vector3(x, y, z)));
            }
        }

        private void ImportFile(string file_name)
        {
            TextReader tr = new StreamReader(file_name);
            ActionCommandList res = new ActionCommandList();
            List<PointCommand> points = new List<PointCommand>();

            string line = null;// "#167587=CARTESIAN_POINT('',(4.85,4.78125,7.995));";
            while ((line = tr.ReadLine()) != null)
            {
                ProcessLine(line, points);
            }
            tr.Close();

            double max = -99999;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].location.z > max) max = points[i].location.z;
            }

            for (int i = 0; i < points.Count; i++)
            {
                if ((points[i].location.z + Settings.Epsilon) >= max)
                    res.Add(points[i]);
            }

            Recipes.ActiveRecipe.AddCommand(Plugin.MakeCommand(res, file_name));
        }

    }
}
