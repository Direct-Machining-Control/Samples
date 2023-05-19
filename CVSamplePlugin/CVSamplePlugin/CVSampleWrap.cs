using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CVSamplePlugin
{
    public static class CVSampleWrap
    {
        [DllImport("CVSampleDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int count_pixels(int width, int height, int bytes_per_pixel, byte[] image);

        [DllImport("CVSampleDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int find_point(int width, int height, int bytes_per_pixel, byte[] image, ref int pointX, ref int pointY);
    }
}
