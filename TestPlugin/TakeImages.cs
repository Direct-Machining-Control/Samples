using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace TestPlugin
{
    public partial class TakeImages : Form
    {
        public TakeImages()
        {
            InitializeComponent();
            Core.Recipe.CommandSelectedEvent += Recipe_CommandSelectedEvent;
        }

        internal static void ShowForm()
        {
            new Thread(new ThreadStart(Start)).Start();
        }

        internal static void Start()
        {
            Thread.Sleep(5000);
            var form = new TakeImages();
            form.TopMost = true;
            form.ShowDialog();
        }



        public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
            int nWidth, int nHeight, IntPtr hObjectSource,
            int nXSrc, int nYSrc, int dwRop);

        [DllImport("gdi32.dll")]
        public static extern bool StretchBlt(
              IntPtr hdcDest,      // handle to destination DC
              int nXOriginDest, // x-coord of destination upper-left corner
              int nYOriginDest, // y-coord of destination upper-left corner
              int nWidthDest,   // width of destination rectangle
              int nHeightDest,  // height of destination rectangle
              IntPtr hdcSrc,       // handle to source DC
              int nXOriginSrc,  // x-coord of source upper-left corner
              int nYOriginSrc,  // y-coord of source upper-left corner
              int nWidthSrc,    // width of source rectangle
              int nHeightSrc,   // height of source rectangle
              int dwRop       // raster operation code
            );


        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
            int nHeight);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);



        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
        /*
        private void run_test()
        {

            Rectangle rc = new Rectangle();
            Image img = ScreenToImage(ref rc, false);
            img.Save(@"C:\Users\ssaamm\Desktop\Capt44ure.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
            img.Dispose();

        }*/


        private Image ScreenToImage(ref Rectangle rcDest, bool IsPapaer, Control ccc)
        {
            IntPtr handle = ccc.Handle;

            //this.Handle
            // get te hDC of the target window
            IntPtr hdcSrc = GetWindowDC(handle);
            // get the size
            RECT windowRect = new RECT();
            GetWindowRect(handle, ref windowRect);
            int nWidth = windowRect.right - windowRect.left;
            int nHeight = windowRect.bottom - windowRect.top;

            if (IsPapaer)
            {
                float fRate = (float)rcDest.Width / nWidth;
                //float fHeight = nHeight * fRate;
                //rcDest.Height = (int)(nHeight * fRate);
                //rcDest.Width = (int)(rcDest.Width);// * fRate);
                rcDest.X = 0;
                rcDest.Y = 0;
                rcDest.Height = (int)(nHeight * fRate);
                //rcDest.Width = (int)(nWidth * fRate);
            }
            else
            {
                rcDest.X = 0;
                rcDest.Y = 0;
                rcDest.Height = nHeight;
                rcDest.Width = nWidth;
            }

            // create a device context we can copy to
            IntPtr hdcDest = CreateCompatibleDC(hdcSrc);
            // create a bitmap we can copy it to,
            // using GetDeviceCaps to get the width/height
            IntPtr hBitmap = CreateCompatibleBitmap(hdcSrc, rcDest.Width, rcDest.Height);
            // select the bitmap object
            IntPtr hOld = SelectObject(hdcDest, hBitmap);
            // bitblt over
            StretchBlt(hdcDest, rcDest.X, rcDest.Y, rcDest.Width, rcDest.Height, hdcSrc, 0, 0, nWidth, nHeight, SRCCOPY);
            // restore selection
            SelectObject(hdcDest, hOld);
            // clean up 
            DeleteDC(hdcDest);
            ReleaseDC(handle, hdcSrc);

            // get a .NET image object for it
            Image img = Image.FromHbitmap(hBitmap);
            // free up the Bitmap object
            DeleteObject(hBitmap);
            return img;
        }



        private void Recipe_CommandSelectedEvent(Core.ICommand command, object sender)
        {
            UpdateTree(command.GetGUI());
        }

        List<Tuple<Control, Color>> color_to_replace = new List<Tuple<Control, Color>>();

        private void tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;
            Control c = (Control)e.Node.Tag;
            for (int i = 0; i < color_to_replace.Count; i++)
                if (color_to_replace[i].Item1 == c) return;

            color_to_replace.Add(new Tuple<Control, Color>(c, c.BackColor));
            c.BackColor = Color.Black;
        }

        void UpdateTree(Control uc)
        {
            tree.Nodes.Clear();

            if (uc == null) return;
            TreeNode treeNode = new TreeNode("Command: "+uc.Name);
            treeNode.Tag = uc;
            
            Fill(uc.Controls.GetEnumerator(), treeNode, 0);
            if (tree.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate ()
                {
                    tree.Nodes.Add(treeNode);
                });
            }
            else
                tree.Nodes.Add(treeNode);

            //tree.ExpandAll();
        }

        public void Fill(IEnumerator en, TreeNode node, int depth)
        {
            if (depth > 5) return;
            Object ob = null;
            en.Reset();
            while (en.MoveNext())
            {
                Control tempCtrl = (Control)(en.Current);
                TreeNode treeNode = new TreeNode(tempCtrl.GetType() + ": " + tempCtrl.Name);
                treeNode.Tag = tempCtrl;
                node.Nodes.Add(treeNode);
                if (tempCtrl.Controls.Count > 0)
                {
                    Fill(tempCtrl.Controls.GetEnumerator(), treeNode, depth+1);
                }
                if (depth < 3) node.Expand();
            }
        }


        private void treeView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) TakeImage();
        }

        void TakeImage()
        {
            TreeNode node = tree.SelectedNode;
            if (node == null) return;
            string title = "";// ((Control)node.Tag).Text;
            TreeNode n = node;
            string pn = null;

            do
            {
                Control cc = (Control)n.Tag;
                if (pn == null && !cc.ProductName.StartsWith("Microsoft")) 
                    pn = cc.ProductName;

                string name = ((Control)n.Tag).Name;
                if (name.Length > 0)
                {
                    if (title.Length > 0) title = ((Control)n.Tag).Name + "-" + title;
                    else title = ((Control)n.Tag).Name;
                }
                n = n.Parent;
            } while (n != null);

            if (pn != null) title = pn + "-" + title;

            Rectangle r = new Rectangle();
            
            Image bmp = ScreenToImage(ref r, false, (Control)node.Tag); // GetControlImage((Control)node.Tag);
            Base.Functions.SaveImage(bmp, Base.Settings.PathTEMP + title + ".png");
        }

        private Bitmap GetControlImage(Control ctl)
        {
            Bitmap bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            // Create a graphics object from the bitmap
            Graphics gfxScreenshot = Graphics.FromImage(bmpScreenshot);
            // Take the screenshot from the upper left corner to the right bottom corner
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            // Save the screenshot to the specified path that the user has chosen

            /*
            Bitmap bm = new Bitmap(ctl.Width, ctl.Height);
            ctl.DrawToBitmap(bm,
                new Rectangle(0, 0, ctl.Width, ctl.Height));*/
            return bmpScreenshot;
        }

        
        private void timer1_Tick(object sender, EventArgs e)
        {
            RefreshList(false);
            if (color_to_replace.Count < 1) return;
            {
                int n = color_to_replace.Count;
                for (int i=0; i<color_to_replace.Count; i++)
                {
                    color_to_replace[i].Item1.BackColor = color_to_replace[i].Item2;
                }
                color_to_replace.RemoveRange(0, n);
            }
        }

        Form last_form = null;
        string last_form_name = "";

        void RefreshList(bool force)
        {
            Form f = Form.ActiveForm;
            if (force) f = last_form;
            if (f != null && f.Name != "TakeImages" && (force || last_form_name != f.Name))
            {
                last_form_name = f.Name;
                last_form = f;
                BringToFront();
                UpdateTree(last_form);
            }
            if (f != null && f.Name != "TakeImages" && WindowState == FormWindowState.Normal)
            {
                //Visible = true;
                //BringToFront();
                //TopLevel = true;

                Activate();
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshList(true);
        }
    }
}
