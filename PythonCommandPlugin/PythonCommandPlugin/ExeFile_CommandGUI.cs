using Base;
using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.Commands;

namespace PythonCommandPlugin
{
    public partial class ExeFile_CommandGUI :ICommandGUI
    {
        ExecutableFileCommand cmd;
        public ExeFile_CommandGUI()
        {
            InitializeComponent();
        }

        internal static ICommandGUI Get(ExecutableFileCommand cmd)
        {
            ExeFile_CommandGUI gui = new ExeFile_CommandGUI();
            gui.cmd = cmd;
            gui.SetGUI();
            gui.exportDataGUI1.SetGUI(cmd.exportDataCommand);
            return gui;
        }

        private void SetGUI()
        {
            if (cmd == null) return;
            exportDataGUI1.TempFileMode();
        }

        public string ExecutableFileName => executable_file_name.Text;
        public string ResultFileName => result_file_name.Text;

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                string currentFile = executable_file_name.Text;
                if (File.Exists(currentFile))
                    openFile.InitialDirectory = Path.GetDirectoryName(currentFile);
                else
                    openFile.InitialDirectory = Application.StartupPath;
                openFile.Filter = "exe files (*.exe)|*.exe|All files (*.*)|*.*";
                openFile.FileName = Path.GetFileName(currentFile);
                if (openFile.ShowDialog() == DialogResult.OK)
                    this.executable_file_name.Text = openFile.FileName;
            }
            catch (Exception ex) { Functions.ErrorF("Unable to import file. ", ex); Functions.ShowLastError(); }
        }

        private void noSelectButton1_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                string currentFile = result_file_name.Text;
                if (File.Exists(currentFile))
                    saveFile.InitialDirectory = Path.GetDirectoryName(currentFile);
                else
                    saveFile.InitialDirectory = Application.StartupPath;
                saveFile.Filter = "Text file |*.txt";
                saveFile.FileName = Path.GetFileName(currentFile);
                if (saveFile.ShowDialog() == DialogResult.OK)
                    this.result_file_name.Text = saveFile.FileName;
            }
            catch (Exception ex) { Functions.ErrorF("Unable to select file. ", ex); Functions.ShowLastError(); }
        }

        private void wait_for_finish_ParameterFieldValueChanged(object sender, IParameter param)
        {
            try
            {
                this.stop_on_error.Visible = cmd.wait_for_finish.value;
            }
            catch { }
        }
    }
    public class ExecutableFileCommand : ICommand
    {
        public static new string unique_name = "executable_file";
        public static new string friendly_name = "Executable File";
        public static new string description = "Executable File";

        readonly string tempFilePath = Settings.PathTEMP + "executable_arguments.tmp";

        public StringParameter executable_file_name = new StringParameter("executable_file_name", "Executable file name", "Executable file name.", "");
        public StringParameter arguments = new StringParameter("arguments", "Arguments", "Space-separated list of arguments to pass to the executable file.", "");
        public StringParameter result_file_name = new StringParameter("result_file_name", "Result File Name", "File for saving result from the executable file.", "");
        public BoolParameter wait_for_finish = new BoolParameter("wait_for_finish", "Wait For Executable To Finish", "Waits for the executable file process to quit before continuing executing the recipe.", true);
        public BoolParameter stop_on_error = new BoolParameter("stop_on_error", "Stop On Error", "Stops the recipe and returns exit code", true);


        public Core.Commands.ExportDataCommand exportDataCommand;

        ExeFile_CommandGUI gui;

        public ExecutableFileCommand() : base(unique_name, friendly_name, description)
        {
            exportDataCommand = new Core.Commands.ExportDataCommand();
            //exportDataCommand.end_of_line.Set(4);
            exportDataCommand.SetupWritter(tempFilePath);
            foreach (var prm in exportDataCommand.Parameters)
            {
                if (!Parameters.Select(p => p.unique_name).ToList().Contains(prm.unique_name)) Add(prm);
            }
            Add(executable_file_name); Add(arguments); Add(result_file_name); Add(wait_for_finish); Add(stop_on_error);
        }

        public static ICommand Create() { return new ExecutableFileCommand(); }

        public override System.Drawing.Bitmap GetIcon() { return Properties.Resources.computing_16; }

        public override bool IsControlCommand { get { return true; } }

        public override ICommandGUI GetGUI()
        {
            gui = (ExeFile_CommandGUI)ExeFile_CommandGUI.Get(this);
            return gui;
        }

        string executableFileName;
        string resultFileName;

        public override bool Compile()
        {
            if (!ParseAll()) return false;

            if (!TextCommand.ParseText(executable_file_name.Value, ref executableFileName, Recipe.variables)) return false;
            if (!File.Exists(executableFileName)) return Functions.Error("Script file not found. ");
            if (!TextCommand.ParseText(result_file_name.Value, ref resultFileName, Recipe.variables)) return false;

            exportDataCommand.Compile();
            exportDataCommand.SetupWritter(tempFilePath);

            return true;
        }

        public override bool Run()
        {
            if (!Compile()) return false;

            exportDataCommand.Run();
            if (!File.Exists(tempFilePath)) return Functions.Error("Failed to create arguments file. ");


            try
            {
                start = new ProcessStartInfo();
                start.FileName = executableFileName;
                if (File.Exists(tempFilePath))
                {
                    string[] lines = File.ReadAllLines(tempFilePath);
                    if(lines.Length >= 1)
                    {
                        //replacing tab sep with space for args // 3 quotes needed in order to pass argument with quotes to a process argument
                        lines[0] = lines[0].Replace("\t", " ");
                        lines[0] = lines[0].Replace("\"", "\"\"\"");
                        arguments.Set(lines[0]);
                    }
                    //System.Diagnostics.Trace.WriteLine(arguments.Value);
                }
                start.Arguments = string.Format("{0}", arguments.Value);
                //start.CreateNoWindow = true;
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;

                if (wait_for_finish.value)
                {
                    using (Process process = Process.Start(start))
                    {
                        using (StreamReader reader = process.StandardOutput)
                        {
                            WriteResult(reader.ReadToEnd());
                        }
                        if (stop_on_error.value)
                        {
                            if (process.ExitCode != 0)
                            {
                                //Base.Functions.Erro
                                return Functions.Error(this,"Executable file '{0}' failed to run. Exit code: {1}. ", Path.GetFileName(executable_file_name.Value), process.ExitCode);
                            }
                        }
                    }
                    
                }
                else
                {
                    Thread t = new Thread(new ThreadStart(StartProcess)); t.Start();
                }
                //RunExecutable(executable_file_name.Value, tempFilePath);
            }
            catch (Exception ex)
            {
                return Functions.Error(this,"Failed to run Executable File command. ", ex);
            }

            return true;
        }

        ProcessStartInfo start;

        private void StartProcess()
        {
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    WriteResult(reader.ReadToEnd());
                }
            }
        }

        private void WriteResult(string result)
        {
            if (resultFileName == "")
                return;

            using (StreamWriter writer = new StreamWriter(resultFileName))
            {
                writer.Write(result);
            }
        }
    }
}
