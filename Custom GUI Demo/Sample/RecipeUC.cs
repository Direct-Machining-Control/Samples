using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core;
using Core.Commands;
using Core.Tools;
using DMC;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;


namespace Sample
{
    public partial class RecipeUC : UserControl
    {
        public RecipeUC()
        {
            InitializeComponent();
        }

        UserControl command_gui = null; // selected command gui

        // Update command tree
        void UpdateCommandList()
        {
            bool show_only_CAD_commands = false;
            treeView1.Nodes.Clear();
            List<Core.ICommand> commands = Core.Recipes.ActiveRecipe.Childs; // get all commands form active recipe
            for (int i = 0; i < commands.Count; i++)
            {
                if (show_only_CAD_commands)
                {
                    if (!(commands[i] is Core.Commands.Cad)) continue;
                }
                string title = commands[i].friendly_name + ": " + commands[i].GetInfo();

                TreeNode node = new TreeNode(title);
                node.Checked = !commands[i].IsSkipped;
                node.Tag = commands[i];
                treeView1.Nodes.Add(node);
            }
            treeView1.ExpandAll();
        }

        internal void Init()
        {
            // receive recipe changed event 
            Core.Recipe.RecipeChanged += Recipe_RecipeChanged;
            Core.Recipes.RecipeLoaded += Recipes_RecipeLoaded;
            Base.State.StateChangedEvent += State_StateChangedEvent;
        }

        private void State_StateChangedEvent(Base.StateType new_state)
        {
            switch(new_state)
            {
                case Base.StateType.CompilingStopped:
                case Base.StateType.CompilingStoppedWithError:
                    DMC.Actions.ViewFitScreen();
                    break;

            }
        }

        private void Recipes_RecipeLoaded(Recipe recipe)
        {
            DMC.Actions.RecipeCompile();
        }

        private void Recipe_RecipeChanged(Core.ICommand command)
        {
            UpdateCommandList();
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null || e.Node.Tag == null) return;
            if (e.Node.Tag is Core.ICommand)
            {
                // Show/hide CAD file
                ICommand cmd = (ICommand)e.Node.Tag;
                cmd.IsSkipped = !e.Node.Checked;
            }
            else if (e.Node.Tag is CAD_Layers.CADLayerEx)
            {
                // Show/hide CAD layer
                CAD_Layers.CADLayerEx layer = (CAD_Layers.CADLayerEx)e.Node.Tag;
                layer.enabled.Set(e.Node.Checked);
            }
            DMC.Actions.RecipeCompile();
        }


        private void treeView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Delete(treeView1.SelectedNode);
            }
        }

        // Delete selected CAD object
        void Delete(TreeNode node)
        {
            if (node == null || node.Tag == null || !(node.Tag is Core.ICommand)) return;
            Core.ICommand command = (Core.ICommand)node.Tag;
            string title = command.friendly_name + ": " + command.GetInfo();

            if (MessageBox.Show("Are you sure you want to delete '" + title + "'?", "Delete", MessageBoxButtons.YesNoCancel) != DialogResult.Yes) return;
            Core.Recipes.ActiveRecipe.UnselectAllCommands();
            Core.Recipes.ActiveRecipe.SelectCommand(command);
            Core.Recipes.ActiveRecipe.OperationForSelectedCommands(Core.RecipeCommandOperation.Delete);

        }


        // Get CAD command GUI
        void Select(TreeNode node)
        {
            if (command_gui != null) { panel_command.Controls.Remove(command_gui); command_gui.Hide(); command_gui = null; }
            if (node == null || node.Tag == null || !(node.Tag is Core.ICommand)) return;
            Core.ICommand command = (Core.ICommand)node.Tag;
            command_gui = command.GetGUI();
            if (command_gui != null)
            {
                panel_command.Controls.Add(command_gui);
                command_gui.Show();
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Select(treeView1.SelectedNode);
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            DMC.Actions.ImportCAD(Core.Commands.FileImportType.CAD);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Base.View.CurrentView.Refresh(); // refresh preview window
            buttonCompile.Enabled = !(Base.State.is_running);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void buttonCompile_Click(object sender, EventArgs e)
        {
            DMC.Actions.RecipeCompile();
        }

        private void createPolyline_Click(object sender, EventArgs e)
        {
            Helper.CreateGeometry(Helper.GeometryTool.Polyline);
        }

        private void openRecipe_Click(object sender, EventArgs e)
        {
            contextMenuStripRecipes.Items.Clear();

            // Option to load recipe with OpenRecipe dialog
            contextMenuStripRecipes.Items.Add(new ToolStripMenuItem("Browse...", null, recipeMenuItemClick) { Margin = new Padding(10) });

            // Show all recipes in Recipes folder
            string recipe_folder = Base.Settings.Path + "Recipes";
            if (System.IO.Directory.Exists(recipe_folder))
            {
                foreach (var recipe_name in System.IO.Directory.GetFiles(recipe_folder, "*.rcp"))
                {
                    contextMenuStripRecipes.Items.Add(new ToolStripMenuItem(Path.GetFileNameWithoutExtension(recipe_name), null, recipeMenuItemClick) { Tag = recipe_name, Margin = new Padding(10) }); 
                }
            }
            contextMenuStripRecipes.Show(openRecipe, new Point(0, openRecipe.Height));
        }

        private void recipeMenuItemClick(object sender, EventArgs e)
        {
            // Unload all recipes
            Recipes.RemoveAll();

            ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
            if (toolStripMenuItem != null)
            {
                string recipe_name = toolStripMenuItem.Tag as string;
                if (recipe_name != null)
                {
                    // open selected recipe from recipe folder
                    Recipe recipe = Recipes.Load(recipe_name, true, true, false);
                    if (recipe == null || recipe.HasUnrecognizedCommands()) Base.Functions.ShowLastError();
                }
                else
                    DMC.Actions.OpenRecipe(); // show open recipe dialog
            }
        }
    }
}
