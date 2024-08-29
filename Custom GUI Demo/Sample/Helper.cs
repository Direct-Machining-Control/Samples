using Base;
using Core.Commands;
using Core;
using Core.Tools;
using DMC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.Tools._3D;
using System.Threading;

namespace Sample
{
    public class Helper
    {
        public static Base.IView view;
        static System.Windows.Forms.Form main_form = null;
        static System.Windows.Forms.Panel view_panel = null;

        // Initialize DMC (load plugins, settings)
        public static void InitDMC(System.Windows.Forms.Form main_form, System.Windows.Forms.Panel view_panel)
        {
            Helper.main_form = main_form;
            Helper.view_panel = view_panel;

            Base.Functions.SetThreadCulture();
            Base.Settings.main_window = main_form;
            if (!DMC.Helpers.CheckLicense()) Base.Functions.ShowLastError();

            Core.Actions.Init();

            view = DMC.Actions.GetDisplay(view_panel); // assign view to view_panel panel
            view.SetBackgroundColor(new Base.ColorF(0.9f, 0.9f, 0.9f));


            bool settings_loaded = Base.Settings.LoadSettings();
            Base.SystemDevices.ApplySettings();

            
            Base.View.Update();
        }

        public static void CloseDMC()
        {
            Base.Settings.main_window = null;
            try
            {
                Base.SystemDevices.Disconnect(); // Disconnect from hardware
            }
            catch (Exception ex) { Base.Functions.Error("Main form closing error.", ex); }
            Base.State.is_exit = true;
        }


        public static void ShowDMC()
        {
            if (main_form == null) return;
            try
            {
                //this.jobRecipe = Core.Recipes.ActiveRecipe;
                DMC.Form1 f = DMC.Form1.GetMainForm();
                f.HideOnClose = true;
                Base.Settings.main_window = f;
                f.FormClosing -= DMCFormClosing;
                f.FormClosing += DMCFormClosing;
                Base.Functions.FixForm(f);
                f.Show();
                main_form.Hide();
                DMC.Actions.SetDisplayPanel(null, null);
                //MainForm.form.UnsubscribeActions();
            }
            catch (Exception ex)
            {
                Base.Functions.ErrorF("Unable to show form for admin. ", ex);
                Base.Functions.ShowLastError();
            }
        }


        private static void DMCFormClosing(object sender, FormClosingEventArgs e)
        {
            if (main_form == null) return;

            try
            {
                main_form.Show();
                //MainForm.form.ResubscribeActions();
                //GUI.PreviewGUI.Active.ReasignView();
                view = DMC.Actions.GetDisplay(view_panel); // assign view to view_panel panel
                Base.Settings.main_window = main_form;

            }
            catch (Exception ex) { Base.Functions.ErrorF("Unable to close admin form. ", ex); Base.Functions.ShowLastError(); }
        }

        public enum GeometryTool
        {
            Arc,
            Line,
            Circle,
            Rectangle,
            Polyline,
            Ellipse,
            Spiral,
            Spline,
            Barcode,
            Text
        }

        public static void CreateGeometry(GeometryTool tool_name)
        {
            IViewTool tool = null;
            switch (tool_name.ToString())
            {
                case "Arc": tool = new ToolArcSER(); break;
                case "Line": tool = new ToolLineSE(); break;
                case "Circle": tool = new ToolCircleCRSE(false); break;
                case "Rectangle": tool = new ToolRectangleSE(); break;
                case "Polyline": tool = new ToolPolyline(); break;
                case "Text": tool = new ToolText(); break;
                case "Spiral": tool = new ToolCircleCRSE(true); break;
                case "Ellipse": tool = new ToolEllipse(); break;
                case "Barcode": tool = new ToolBarcode(); break;
                case "Spline": tool = new ToolSpline(); break;
                default: tool = null; break;
            }

            if (tool == null) return;
            Base.View.CurrentView.SetActiveTool(tool);
            if (tool != null) tool.Start();
        }

        /// <summary>
        /// Example ho to set global variable
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        public static bool AddGlobalVariable(Variable variable)
        {
            //variable = new Variable("data", 0);
            if (variable == null) return Functions.ErrorS(typeof(Helpers), "Variable is not assigned.");
            return Base.Variables.global_variables.Add(variable);
        }

        /// <summary>
        /// Example how to get global variable
        /// </summary>
        /// <param name="variableName"></param>
        /// <returns></returns>
        public static Variable GetGlobalVariable(string  variableName)
        {
            if (string.IsNullOrWhiteSpace(variableName))
                return null;

            return Base.Variables.global_variables.Get(variableName);
        }

        /// <summary>
        /// Sets marking parameters preset name
        /// </summary>
        /// <param name="presetName">Name of the marking parameters preset.</param>
        /// <param name="commandCustomTitle">Custom Title of the command we want to apply marking parameters to. If not set - first MarkingParametersCommand will be used.</param>
        /// <returns>True on success</returns>
        public static bool SetMarkingParametersPreset(string presetName, string commandCustomTitle = null)
        {
            if (string.IsNullOrWhiteSpace(presetName))
                return Functions.ErrorS(typeof(Helpers), "Marking Preset name is not set.");

            #region Search for preset in library
            string _presetName = presetName.ToLower();
            presetName = string.Empty;

            string[] presetNames = Core.Commands.MarkingParamsLibrary.GetList();
            for (int i = 0; i < presetNames.Length; i++)
            {
                if (presetNames[i].ToLower() != _presetName)
                    continue;
                presetName = presetNames[i];
                break;
            }

            if (string.IsNullOrWhiteSpace(presetName))
                return Functions.ErrorS(typeof(Helpers), $"Preset with name '{_presetName}' is not found.");
            #endregion

            MarkingParams marking = null;

            #region Search for command and get MarkingParams parameter
            if (!string.IsNullOrWhiteSpace(commandCustomTitle))
            {
                // Search for command with custom name
                ICommand command = Core.Recipes.ActiveRecipe.GetCommandByName(commandCustomTitle);
                if (command == null)
                    return Functions.ErrorS(typeof(Helpers), $"Command with name '{commandCustomTitle}' was not found in the active recipe.");

                marking = command.Parameters.Find(p => p is MarkingParams) as MarkingParams;

                if (marking == null)
                    return Functions.ErrorS(typeof(Helpers), $"Command with name '{commandCustomTitle}' does not have marking parameters.");
            }
            else
            {
                // Get first MarkingParametersCommand
                MarkingParametersCommand command = Core.Recipes.ActiveRecipe.GetAllCommandsRecursivelyByType<MarkingParametersCommand>().FirstOrDefault();
                if (command == null)
                    return Functions.ErrorS(typeof(Helpers), $"Marking Parameters command was not found in the active recipe.");

                marking = command.marking;
            }
            #endregion

            marking.preset_name.Set(presetName);
            marking.ResetParameters();

            // Just an example how to change one of the parameters:
            // marking.mark_speed.SetValue(500);

            return true;
        }
        public static void SetAlignment(string commandName)
        {
            if (string.IsNullOrWhiteSpace(commandName))
                return;

            //VisionPlugin.SimpleAlignment cmd2 = new VisionPlugin.SimpleAlignment();
            //cmd2.pattern1.LoadPreset("abc");

            VisionPlugin.AlignCommand cmd = new VisionPlugin.AlignCommand();
            cmd.CustomTitle = commandName;
            cmd.alignment_mode.Set(3); //Sets to automatic
            cmd.pattern1.x.SetValue(25);
            cmd.pattern1.y.SetValue(50);
            cmd.pattern1.SetupFindCircle(120, 320);
            //cmd.pattern1.CreatePatternFromCamera(new Vector3(0, 0, 0), 10, 10);
            //Console.WriteLine($"Min rad: {cmd.pattern1.GetParameterValue("arc_min_radius")}");
            Recipes.ActiveRecipe.AddCommand(cmd);
            //Recipes.ActiveRecipe.AddCommand(cmd2);
        }
        public static bool RunAlignmentPattern(string commandName)
        {
            ICommand cmd = Recipes.ActiveRecipe.GetCommandByName(commandName);
            if (cmd != null)
            {
                VisionPlugin.AlignCommand align_cmd = cmd as VisionPlugin.AlignCommand;
                if (align_cmd != null)
                {
                    return align_cmd.pattern1.Run(true, false);
                }
            }
            return false;
        }
        public static bool AlignmentLoadPreset(string commandName, string presetName)
        {
            ICommand cmd = Recipes.ActiveRecipe.GetCommandByName(commandName);
            if (cmd != null)
            {
                VisionPlugin.AlignCommand align_cmd = cmd as VisionPlugin.AlignCommand;
                if (align_cmd != null)
                {
                    return align_cmd.pattern1.LoadPreset(presetName);
                }
            }
            return false;
        }
        public static bool AlignmentSavePreset(string commandName, string presetFilePath)
        {
            ICommand cmd = Recipes.ActiveRecipe.GetCommandByName(commandName);
            if (cmd != null)
            {
                VisionPlugin.AlignCommand align_cmd = cmd as VisionPlugin.AlignCommand;
                if (align_cmd != null)
                {
                    return align_cmd.pattern1.SavePreset(presetFilePath);
                }
            }
            return false;
        }
        public static void EditAlignmentPattern(string commandName)
        {
            ICommand cmd = Recipes.ActiveRecipe.GetCommandByName(commandName);
            if (cmd != null)
            {
                VisionPlugin.AlignCommand align_cmd = cmd as VisionPlugin.AlignCommand;
                if (align_cmd != null)
                {
                    align_cmd.pattern1.EditAdvancedParameters();
                }
            }
        }
        public static string GetAlignmentResult(string commandName)
        {
            string result = "";
            ICommand cmd = Recipes.ActiveRecipe.GetCommandByName(commandName);
            if(cmd != null)
            {
                VisionPlugin.AlignCommand align_cmd = cmd as VisionPlugin.AlignCommand;
                if(align_cmd != null)
                {
                    result = align_cmd.pattern1.result.GetResultString();
                }
            }
            return result;
        }
        public static string GetParameterValue(string commandName, string paramName)
        {
            ICommand cmd = Recipes.ActiveRecipe.GetCommandByName(commandName);
            if (cmd != null)
            {
                VisionPlugin.AlignCommand align_cmd = cmd as VisionPlugin.AlignCommand;
                if (align_cmd != null)
                {
                    //VisionLT.IParameter[] list = VisionLT.Action.GetParameters(align_cmd.pattern1.ProgramHandle); //All available parameters
                    return align_cmd.pattern1.GetParameterValue(paramName);
                }
            }
            return null;
        }
    }
    

}
