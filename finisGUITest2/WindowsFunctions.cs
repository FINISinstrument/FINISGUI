using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace FinisGUI
{
    public class WindowsFunctions
    {
        [DllImport("gdi32.dll")]
        public static extern int SetStretchBltMode(IntPtr hDC, int mode);
        public const int STRETCH_DELETESCANS = 3; // Constant used in calling SetStretchBltMode
    }
}
