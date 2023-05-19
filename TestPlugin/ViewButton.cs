using Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlugin
{
    class ViewButton : Base.Shapes.ActionCommand
    {
        byte[] data;
        int w;
        int h;
        public ViewButton(Bitmap image)
        {
            data = CADImport.ImageReader.GetArrayARGB(image);
            w = image.Width;
            h = image.Height;
        }

        public override void AddToView(IView view)
        {
            view.Add(this);
        }
        public override void Draw(IView view)
        {
            view.DrawImage(data, w, h, 1, 1, 1, 1, 1, 4, false, 1);
            base.Draw(view);
        }
    }
}
