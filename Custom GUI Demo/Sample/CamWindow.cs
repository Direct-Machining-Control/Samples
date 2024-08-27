using DMC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Sample
{
    public partial class CamWindow : UserControl
    {
        public CamWindow()
        {
            InitializeComponent();
        }

        object locker = new object();
        Bitmap image = null;

        private void timer1_Tick(object sender, EventArgs e)
        {
            try {
                if (Base.Devices.Camera.camera_devices == null) return;
                var active_camera = Base.Devices.Camera.camera_devices.Where(cam => cam.IsEnabled()).FirstOrDefault(); // select first enabled camera
                if (active_camera == null) return;

                lock (locker)
                {
                    Bitmap new_image = active_camera.GetCameraImage();
                    if (new_image != null)
                    {
                        if (image != null) image.Dispose();
                        image = new_image;
                        pictureBox1.Refresh();
                    }
                }
            }
            catch(Exception ex) { }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                lock (locker)
                {
                    if (image == null)
                    {
                        return;
                    }
                    float scaleX = ((float)this.Width) / (float)image.Width;
                    float scaleY = ((float)this.Height) / (float)image.Height;
                    float zoom = scaleY;

                    Graphics g = e.Graphics;
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.ScaleTransform(zoom, zoom);
                    e.Graphics.DrawImage(image, 0, 0, image.Width, image.Height);
                }
            }
            catch (Exception) { GC.Collect(); }
        }
    }
}
