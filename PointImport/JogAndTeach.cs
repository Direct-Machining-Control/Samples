using Base;
using Base.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PointImport
{
    class JogAndTeach : IViewTool, DMC.ITool
    {
        public bool isActive = false;
        ActionCommandList li = new ActionCommandList();
        bool isLine = true;
        int pointsSet = 0;
        LineCommand line = new LineCommand(Vector3.Zero, Vector3.Zero);
        ArcCommand arc = new ArcCommand(Vector3.Zero, 0, 0, 0, true);

        public override bool PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddPoint(Base.View.LaserFocus());
                return true;
            }
            else if (e.KeyCode == Keys.A)
                isLine = false;
            else if (e.KeyCode == Keys.L)
                isLine = true;
            else if (e.KeyCode == Keys.Shift)
                isLine = !isLine;

            return base.PreviewKeyDown(e);
        }

        void MonThread()
        {
            while (isActive && !State.is_exit)
            {
                ModifyLastPoint();
                Thread.Sleep(20);
            }
        }

        public override void JoystickPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            PreviewKeyDown(e);
        }

        void ModifyLastPoint()
        {
            Vector3 point = Base.View.LaserFocus();
            if (isLine)
            {
                if (pointsSet == 1) line.end_point = point;
            }
            else
            {
                if (pointsSet == 1)
                {
                    Vector3 v = point - line.start_point;
                    v = new Vector3(v.y, v.x, v.z);
                    v.SafeNormalize();
                    v *= Base.View.GetSnapRadius();
                    line.end_point = point;
                    if (Vector3.Distance(line.start_point, point) > Base.View.GetSnapRadius() / 10)
                        arc.SetFrom3Points(line.start_point, point, Vector3.MidPoint(line.start_point, point) + v);
                }
                else if (pointsSet == 2)
                {
                    if (Vector3.Distance(line.end_point, point) > Base.View.GetSnapRadius() / 10)
                        arc.SetFrom3Points(line.start_point, point, line.end_point);
                }
            }
        }

        void AddPoint(Vector3 point)
        {
            pointsSet++;
            if (isLine)
            {
                if (pointsSet == 1) line.start_point = line.end_point = point;
                else
                {
                    line.end_point = point;
                    li.Add(line); line = new LineCommand(point, point); pointsSet = 1;
                }
            }
            else
            {
                if (pointsSet == 1) line.start_point = line.end_point = point;
                else if (pointsSet == 2 && Vector3.DistanceXY(line.start_point, point) > Settings.max_deviation)
                {
                    line.end_point = point;
                    Base.StatusBar.Set(MultiLang.Format("Arc middle point ({0}) added", point.ToString()));
                }
                else if (pointsSet == 3 && Vector3.DistanceXY(line.end_point, point) > Settings.max_deviation)
                {
                    arc.SetFrom3Points(line.start_point, point, line.end_point);
                    if (arc.radius > Settings.Epsilon && arc.GetLength() > Settings.Epsilon)
                    {
                        li.Add(arc); arc = new ArcCommand(Vector3.Zero, 0, 0, 0, false); pointsSet = 1;
                        line.start_point = line.end_point = point;
                        Base.StatusBar.Set(MultiLang.Format("Arc end point ({0}) added", point.ToString()));
                    }
                    else pointsSet--;
                }
                else pointsSet--;
            }
            
        }

        public override void Draw(IView view)
        {
            int color = view.GetLastColor();
            view.SetColor(Base.View.color_active_item);
            li.Draw(view);


            if (isLine)
            {
                if (line.GetLength() > Settings.Epsilon) line.Draw(view);
            }
            else
            {
                if (arc.radius > Settings.Epsilon) 
                    arc.Draw(view);
            }
            if (line.GetLength() > Settings.Epsilon)
            {
                PointCommand p1 = new PointCommand(line.start_point);
                PointCommand p2 = new PointCommand(line.end_point);
                p1.Draw(view);
                p2.Draw(view);

                //view.DrawPoints(new List<Vector3>() { line.start_point, line.end_point });
            }

            view.SetColor(color);
        }

        public override void Start()
        {
            Base.StatusBar.Set("Jog and Teach");
            base.Start();
        }

        public override void Stop()
        {
            Base.StatusBar.Set("Jog and Teach stopped");
            base.Stop();
        }

        IViewTool default_view_tool = null;

        public void Run()
        {
            if (isActive)
            {
                if (default_view_tool != null)
                {
                    Base.View.CurrentView.SetDefaultTool(default_view_tool);
                    Base.View.CurrentView.SetActiveTool(null);
                }
                tool.SetTitle("Start");
                isActive = false;
                if (li.Count > 0)
                    MakeCommand();
                li.Clear();
            }
            else
            {
                Base.View.CurrentView.SetActiveTool(null);
                default_view_tool = Base.View.CurrentView.GetActiveTool();

                Base.View.CurrentView.SetDefaultTool(this);
                Base.View.CurrentView.SetActiveTool(this);
                li.Clear();
                line.start_point = line.end_point = Vector3.Zero;

                Start();
                pointsSet = 0;
                tool.SetTitle("Stop");
                isActive = true;
                (new Thread(new ThreadStart(MonThread))).Start();
                DMC.Actions.ShowJoystick();
            }
        }

        private void MakeCommand()
        {
            Core.Recipes.ActiveRecipe.AddCommand(Plugin.MakeCommand((ActionCommandList)li.Clone(), ""));
        }

        static IFormTool tool = null;
        public static void AddTool()
        {
            tool = DMC.Helpers.AddTool(DMC.ToolLocation.EditTab, "Jog And Teach", "Start", new JogAndTeach(), null, false, true, Properties.Resources.add_line);
        }
    }
}
