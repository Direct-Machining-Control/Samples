using DLPPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CustomDLPPlugin
{
    /// <summary>
    /// Unique image display
    /// </summary>
    class UniqueProcess : IDLPViewManager
    {
        public string GetName()
        {
            return "Unique Process";
        }

        public bool Show(IDLPView view, Bitmap image, ulong frame)
        {
            // Draw border around image 
            AddBorder.DrawBorder(image);


            // Display for 0.1 second
            view.Show(image, 0.1);
            return true;
        }
    }


    /// <summary>
    /// Option to add border around image and use standard display procedure
    /// </summary>
    class AddBorder : IDLPViewProcessor
    {
        public string GetName()
        {
            return "Add Border";
        }

        /// <summary>
        /// Draw white border around image 
        /// </summary>
        /// <param name="image"></param>
        internal static void DrawBorder(Bitmap image)
        {
            using (var gr = System.Drawing.Graphics.FromImage(image))
            {
                gr.DrawRectangle(new Pen(Brushes.White, 1), 0, 0, image.Width - 1, image.Height - 2);
            }
        }

        public Bitmap Process(Bitmap image, ulong frame)
        {
            // Draw border around image 
            DrawBorder(image);
            return image;
        }
    }
}
