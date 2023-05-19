using Base;
using Base.Shapes;
using Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HatchingPlugin
{
    /// <summary>
    /// HatchingPreProcessing allows to remove small contours before hatching
    /// </summary>
    class HatchingPreProcessing : IHatchProcessor
    {
        ParamSD min_size = new ParamSD("min_size", "Min Size (mm²)", 0);

        public HatchingPreProcessing() : base("hp_remove_small_contours", "Exclude Small Contours", "Option to remove small contours before hatching")
        {
            Add(min_size);
        }

        public override bool IsPreProcessor => true; // Pre processor - before hatching

        public override System.Windows.Forms.UserControl GetGUI()
        {
            return Core.ICommandGUI.GetGUIForm(parameters);
        }

        public override string GetData()
        {
            return string.Format(" < {0}mm²", min_size.number);
        }

        public override bool Compile(Hatching hatching)
        {
            if (min_size.number < 0) return Functions.ErrorF("'Min Size' needs to be positive. ");
            return true;
        }

        protected override MultiParameter CloneObj()
        {
            HatchingPreProcessing p = new HatchingPreProcessing();
            p.AssignValuesFrom(parameters);
            return p;
        }

        public override bool Run(Hatching hatch, HatchProcessorPrms prms)
        {
            RemoveSmallContours(prms.Commands);
            return true;
        }

        void RemoveSmallContours(ActionCommandList list)
        {
            if (list == null) return;
            for (int i=0; i<list.Count; i++)
            {
                var cmd = list[i];
                if (cmd is ActionCommandList && !(cmd is JoinedCommandList))
                    RemoveSmallContours((ActionCommandList)cmd); // command is command list
                else
                    if (!cmd.HasLocation || !cmd.IsClosed || cmd is IFakeMotion) continue; // command might be not geometry or not closed or IFakeMotion (jump, position, ...)
                else
                {
                    // calculate area
                    var points = cmd.GetPolyline(Settings.Epsilon);
                    var area = (points != null && points.Count > 2) ? Geometry.PolygonArea(points) : 0;
                    area = Math.Abs(area); // might be negative (CW/CCW)
                    if (area >= min_size.number) continue; // contour is larger

                    // remove this contour
                    list.Remove(cmd);
                    i--;
                }
            }
        }
    }



    /// <summary>
    /// HatchingPreProcessing allows to remove short hatching lines/polylines
    /// </summary>
    class HatchingPostProcessing : IHatchProcessor
    {
        ParamSD min_length = new ParamSD("min_length", "Min Length (mm)", 0);

        public HatchingPostProcessing() : base("hp_remove_short", "Exclude Short Polylines", "Option to remove short hatching lines/polylines")
        {
            Add(min_length);
        }

        public override bool IsPreProcessor => false; // Post processor - after hatching

        public override System.Windows.Forms.UserControl GetGUI()
        {
            return Core.ICommandGUI.GetGUIForm(parameters);
        }

        public override string GetData()
        {
            return string.Format(" < {0}mm", min_length.number);
        }

        public override bool Compile(Hatching hatching)
        {
            if (min_length.number < 0) return Functions.ErrorF("'Min Length' needs to be positive. ");
            return true;
        }

        protected override MultiParameter CloneObj()
        {
            HatchingPostProcessing p = new HatchingPostProcessing();
            p.AssignValuesFrom(parameters);
            return p;
        }

        public override bool Run(Hatching hatch, HatchProcessorPrms prms)
        {
            RemoveShortTrajectories(prms.Result);
            return true;
        }

        void RemoveShortTrajectories(ActionCommandList list)
        {
            if (list == null) return;
            for (int i = 0; i < list.Count; i++)
            {
                var cmd = list[i];
                if (cmd is ActionCommandList && !(cmd is JoinedCommandList))
                    RemoveShortTrajectories((ActionCommandList)cmd); // command is command list
                else
                    if (!cmd.HasLocation || cmd is IFakeMotion) continue; // command might be not geometry or IFakeMotion (jump, position, ...)
                else
                {
                    // check if trajectory is long enough
                    var length = cmd.GetLength();
                    if (length >= min_length.number) continue;

                    // remove this trajectory
                    list.Remove(cmd);
                    i--;
                }
            }
        }
    }
}
