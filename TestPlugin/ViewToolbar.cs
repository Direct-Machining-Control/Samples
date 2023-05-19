using Base;
using DMC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlugin
{
    public class ViewToolbar
    {
        public static Core.Tools.ViewToolbar toolbar;
        public static void AddViewTools()
        {
            toolbar = new Core.Tools.ViewToolbar();
            Base.View.CurrentView.AddViewToolbar(toolbar);

            Core.Tools.ViewButton view_fit = new Core.Tools.ViewButton();
            view_fit.SetTitle("Fit View");
            //view_fit.SetImage(Properties.Resources.zoom_fit);
            view_fit.MouseClick += ClickViewFit;
            toolbar.Add(view_fit);

            Core.Tools.ViewButton view_reset = new Core.Tools.ViewButton();
            //view_reset.State.title = "Reset View";
            //view_reset.State.SetImage(Properties.Resources.zoom_reset);
            view_reset.MouseClick += ClickViewReset;
            toolbar.Add(view_reset);

            Core.Tools.ViewButton view_measure = new Core.Tools.ViewButton();
            view_measure.State.title = "Measure";
            view_measure.Size = new Vector2f(44, 44);
            //view_measure.State.SetImage(Properties.Resources.wizard_24);
            toolbar.Add(view_measure);
        }

        static void ClickViewFit(object sender, EventArgs args)
        {
            Actions.ViewFitScreen();
        }
        static void ClickViewReset(object sender, EventArgs args)
        {
            Actions.ViewReset();
        }
    }
}
