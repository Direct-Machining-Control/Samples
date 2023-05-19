using Base;
using Core;
using Core.Commands;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PythonCommandPlugin
{
    public partial class CommandGUI : ICommandGUI
    {
        Command cmd;

        public CommandGUI()
        {
            InitializeComponent();
        }

        internal static ICommandGUI Get(Command cmd)
        {
            CommandGUI gui = new CommandGUI();
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

        public string ScriptFileName => script_file_name.Text;
        public string ResultFileName => result_file_name.Text;

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                string currentFile = script_file_name.Text;
                if (File.Exists(currentFile))
                    openFile.InitialDirectory = Path.GetDirectoryName(currentFile);
                else
                    openFile.InitialDirectory = Application.StartupPath;
                openFile.Filter = "Python script |*.py";
                openFile.FileName = Path.GetFileName(currentFile);
                if (openFile.ShowDialog() == DialogResult.OK)
                    this.script_file_name.Text = openFile.FileName;
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
    }

    public class Command : ICommand
    {
        public static new string unique_name = "python";
        public static new string friendly_name = "Python";
        public static new string description = "Python";

        readonly string tempFilePath = Settings.PathTEMP + "python_arguments.tmp";

        public StringParameter script_file_name = new StringParameter("script_file_name", "Script file name", "Python script file name.", "");
        public StringParameter arguments = new StringParameter("arguments", "Arguments", "Space-separated list of arguments to pass to the script.", "");
        public StringParameter result_file_name = new StringParameter("result_file_name", "Result File Name", "File for saving result from the script.", "");
        public BoolParameter wait_for_finish = new BoolParameter("wait_for_finish", "Wait For Script To Finish", "Waits for the Python process to quit before continuing executing the recipe.", true);

        public Core.Commands.ExportDataCommand exportDataCommand;

        CommandGUI gui;

        public Command() : base(unique_name, friendly_name, description)
        {
            exportDataCommand = new Core.Commands.ExportDataCommand();
            exportDataCommand.SetupWritter(tempFilePath);
            foreach ( var prm in exportDataCommand.Parameters)
            {
                if (!Parameters.Select(p => p.unique_name).ToList().Contains(prm.unique_name)) Add(prm);
            }
            Add(script_file_name); Add(arguments); Add(result_file_name); Add(wait_for_finish);
        }

        public static ICommand Create() { return new Command(); }

        public override System.Drawing.Bitmap GetIcon() { return Properties.Resources.computing_16; }

        public override bool IsControlCommand { get { return true; } }

        public override ICommandGUI GetGUI()
        {
            gui = (CommandGUI)CommandGUI.Get(this);
            return gui;
        }

        public override bool Compile()
        {
            if (!ParseAll()) return false;

            if (!Plugin.settings.enabled.value) return Functions.Error("Python is disabled. ");
            if (!File.Exists(Plugin.settings.executable_path.Value)) return Functions.Error("Python executable not found. ");
            if (gui != null) script_file_name.Value = gui.ScriptFileName;
            if (!File.Exists(script_file_name.Value)) return Functions.Error("Script file not found. ");
            if (gui != null) result_file_name.Value = gui.ResultFileName;

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
                RunPyhton(script_file_name.Value, tempFilePath);
            }
            catch (Exception ex)
            {
                return Functions.ErrorF("Failed to run Python command. ", ex);
            }

            return true;
        }

        ProcessStartInfo start;

        private bool RunPyhton(string fileName, string argsFileName)
        {
            string execPath = Plugin.settings.executable_path.Value;

            start = new ProcessStartInfo();
            start.FileName = execPath;
            start.Arguments = string.Format("\"{0}\" \"{1}\"", fileName, argsFileName);
            start.CreateNoWindow = true;
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
                }
            }
            else
            {
                Thread t = new Thread(new ThreadStart(StartProcess)); t.Start();
            }
            return true;
        }

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
            if (result_file_name.Value == "")
                return;

            using (StreamWriter writer = new StreamWriter(result_file_name.Value))
            {
                writer.Write(result);
            }
        }
    }
}
