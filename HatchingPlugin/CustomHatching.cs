using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base;
using Core;
using Core.Commands;
using Base.Shapes;
using System.Threading.Tasks;

namespace HatchingPlugin
{
    public class CustomHatching : IHatcher
    {
        CustomHatchingGUI gui;

        // our parameters that can be modified by the user
        ParamSD spacing = new ParamSD("spacing", "Spacing (mm)", 0.1);
        ParamSD offset_to_contour = new ParamSD("offset_to_contour", "Offset to Contour (mm)", 0);
        ParamSD offset_to_hatch = new ParamSD("offset_to_hatch", "Offset to Hatch (mm)", 0);


        public CustomHatching()
        {
            // add paramters to parameter list (parameters will be automatically saved, loaded and displayed)
            Add(spacing); Add(offset_to_contour); Add(offset_to_hatch);
        }

        protected override MultiParameter CloneObj()
        {
            return CloneIHatcher();
        }

        public override IHatcher CloneIHatcher()
        {
            CustomHatching ch = new CustomHatching();
            ch.AssignValuesFrom(parameters);
            return ch;
        }

        public override string GetHatchingType()
        {
            return "My Hatching"; 
        }

        public override System.Drawing.Image GetHatchingTypeImage()
        {
            return Properties.Resources.Line_Hatching;
        }

        public override System.Windows.Forms.UserControl GetGUI()
        {
            if (gui == null) gui = new CustomHatchingGUI();
            gui.Set(this);
            return gui;
        }

        /// <summary>
        /// parse and check parameters
        /// </summary>
        public override bool Compile()
        {
            if (!ICommand.ParseAll(parameters)) return false;

            if (spacing.number < Settings.Epsilon) return Functions.Error("'" + spacing.title + "' needs to be larger than zero");
            return true;
        }

        public override void Hatch(ActionCommandList commands, Cube size, double hatching_angle_offset, double hatching_shift_in_x, double hatching_shift_in_y, ref ActionCommandList result, MarkingParameters contour_parameters, object caller = null)
        {
            // make contour offset
            if (offset_to_contour.number > Settings.Epsilon)
                Hatching.MakeOffset(commands, offset_to_contour.number);


            ActionCommandList commands_for_hatching = commands;
            if (offset_to_hatch.number > Settings.Epsilon)
            {
                // make hatching offset
                commands_for_hatching = (ActionCommandList)commands.Clone();
                Hatching.MakeOffset(commands_for_hatching, offset_to_hatch.number);
            }

            // do simple hatching
            ActionCommandList my_result = SimpleHatching.Hatch(commands_for_hatching, size, spacing.number);
            result = my_result; // assign result

            if (commands_for_hatching != commands) commands_for_hatching.Clear();
        }

    }
}
