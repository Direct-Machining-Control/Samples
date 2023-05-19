using Base.Shapes;
using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;

namespace TestPlugin
{

    /// <summary>
    /// Draw rectangle
    /// </summary>
    class MyArea : ActionCommand
    {
        public override void AddToView(IView view) { view.Add(this); }

        public override void Draw(IView view)
        {
            view.SetColor(new ColorF(1, 0, 0));
            Base.Vector3f min = new Vector3f(-10, -10, 0);
            Base.Vector3f max = new Vector3f(20, 20, 0);
            view.DrawRectangle(min, max, false);
            
        }

        static MyArea active = null;

        /// <summary>
        /// Activate drawing of rectangle
        /// </summary>
        public static void Activate()
        {
            if (active != null) return;
            active = new MyArea();
            Base.View.CurrentView.AddToBase(active, ObjectDrawingOrder.BEGIN);
        }

        /// <summary>
        /// Deactivate drawing of rectangle
        /// </summary>
        public static void Deactivate()
        {
            if (active != null) return;
            Base.View.CurrentView.RemoveFromBase(active, ObjectDrawingOrder.BEGIN);
            active = null;
        }
    }


    
}
