/*
 *	xclibex7.txt	External	19-Jun-2014
 *
 *	Copyright (C) 2006-2014  EPIX, Inc. All rights reserved.
 *
 *	Example program for XCLIB DLL, for use with Visual C# .NET
 *
 */

/*
 *
 * INSTRUCTIONS FOR INSTALLATION/USAGE:
 *
 * 1. Start a new Visual C# .NET project in Visual Studio .NET 2005.
 *
 * 2. Right-click on Form1 and select View Code.
 *
 * 3. Replace the default code by copying and pasting the entire text from this file over it.
 *
 * 4. This code assumes XCLIB is installed to c:\xclib.
 *    Modify the path in the function declarations if needed.
 *
 * 5. If not using PXIPL, comment out the PXIPL sections of code.
 *
 * 7. Change the FORMAT or FORMATFILE string in the Form1_Load subroutine
 *    for a video configuration suitable for the PIXCI(R) board being used.
 *
 * 6. Press F5 to Run sample code.
 *
 */

using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsApplication2
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Save_Button;
        private System.Windows.Forms.Button PXIPL_Button;
        private System.Timers.Timer timer1;
        private System.Windows.Forms.PictureBox ImagePictureBox;
        private System.Windows.Forms.PictureBox PixelPlotPictureBox;
        private System.Windows.Forms.Button Snap_Button;
        private System.Windows.Forms.Button Live_Button;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ImagePictureBox = new System.Windows.Forms.PictureBox();
            this.PixelPlotPictureBox = new System.Windows.Forms.PictureBox();
            this.Snap_Button = new System.Windows.Forms.Button();
            this.Live_Button = new System.Windows.Forms.Button();
            this.Save_Button = new System.Windows.Forms.Button();
            this.PXIPL_Button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
            this.SuspendLayout();
            //
            // ImagePictureBox
            //
            this.ImagePictureBox.Location = new System.Drawing.Point(0, 0);
            this.ImagePictureBox.Name = "ImagePictureBox";
            this.ImagePictureBox.Size = new System.Drawing.Size(528, 352);
            this.ImagePictureBox.TabIndex = 0;
            this.ImagePictureBox.TabStop = false;
            this.ImagePictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.ImagePictureBox_Paint);
            //
            // PixelPlotPictureBox
            //
            this.PixelPlotPictureBox.Location = new System.Drawing.Point(528, 56);
            this.PixelPlotPictureBox.Name = "PixelPlotPictureBox";
            this.PixelPlotPictureBox.Size = new System.Drawing.Size(200, 200);
            this.PixelPlotPictureBox.TabIndex = 1;
            this.PixelPlotPictureBox.TabStop = false;
            this.PixelPlotPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PixelPlotPictureBox_Paint);
            //
            // Snap_Button
            //
            this.Snap_Button.Location = new System.Drawing.Point(528, 280);
            this.Snap_Button.Name = "Snap_Button";
            this.Snap_Button.Size = new System.Drawing.Size(64, 32);
            this.Snap_Button.TabIndex = 2;
            this.Snap_Button.Text = "Snap";
            this.Snap_Button.Click += new System.EventHandler(this.Snap_Button_Click);
            //
            // Live_Button
            //
            this.Live_Button.Location = new System.Drawing.Point(592, 280);
            this.Live_Button.Name = "Live_Button";
            this.Live_Button.Size = new System.Drawing.Size(72, 32);
            this.Live_Button.TabIndex = 3;
            this.Live_Button.Text = "Live";
            this.Live_Button.Click += new System.EventHandler(this.Live_Button_Click);
            //
            // Save_Button
            //
            this.Save_Button.Location = new System.Drawing.Point(664, 280);
            this.Save_Button.Name = "Save_Button";
            this.Save_Button.Size = new System.Drawing.Size(64, 32);
            this.Save_Button.TabIndex = 4;
            this.Save_Button.Text = "Save";
            this.Save_Button.Click += new System.EventHandler(this.Save_Button_Click);
            //
            // label1
            //
            this.label1.Location = new System.Drawing.Point(528, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "Live Pixel Plot";
            //
            // timer1
            //
            this.timer1.Enabled = true;
            this.timer1.SynchronizingObject = this;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            //
            // Form1
            //
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(744, 357);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PXIPL_Button);
            this.Controls.Add(this.Save_Button);
            this.Controls.Add(this.Live_Button);
            this.Controls.Add(this.Snap_Button);
            this.Controls.Add(this.PixelPlotPictureBox);
            this.Controls.Add(this.ImagePictureBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Closed += new System.EventHandler(this.Form1_Closed);
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        #region XCLIB + PXIPL DLL Function Declarations

        ///
        /// DLL Imports for functions used.
        ///
        /// [DllImport("c:\xclib\xclybwnt.dll", EntryPoint="PXD_PIXCIOPEN")]
        ///
        /// Additional imports for other XCLIB functions can be found
        /// in XCLYBWNT_VBNET.TXT
        ///
        [DllImport("c:\\xclib2\\lib\\xclybwnt.dll")]
        private static extern int pxd_PIXCIopen(string c_driverparms, string c_formatname, string c_formatfile);

        [DllImport("c:\\xclib2\\lib\\xclybwnt.dll")]
        private static extern int pxd_PIXCIclose();

        [DllImport("c:\\xclib2\\lib\\xclybwnt.dll")]
        private static extern int pxd_mesgFault(int c_unitmap);

        [DllImport("c:\\xclib2\\lib\\xclybwnt.dll")]
        private static extern int pxd_doSnap(int c_unitmap, int c_buffer, int c_timeout);

        [DllImport("c:\\xclib2\\lib\\xclybwnt.dll")]
        private static extern int pxd_goSnap(int c_unitmap, int c_buffer);

        [DllImport("c:\\xclib2\\lib\\xclybwnt.dll")]
        private static extern int pxd_goLive(int c_unitmap, int c_buffer);

        [DllImport("c:\\xclib2\\lib\\xclybwnt.dll")]
        private static extern int pxd_goUnLive(int c_unitmap);

        [DllImport("c:\\xclib2\\lib\\xclybwnt.dll")]
        private static extern int pxd_imageXdim();

        [DllImport("c:\\xclib2\\lib\\xclybwnt.dll")]
        private static extern int pxd_imageYdim();

        [DllImport("c:\\xclib2\\lib\\xclybwnt.dll")]
        private static extern double pxd_imageAspectRatio();

        [DllImport("c:\\xclib2\\lib\\xclybwnt.dll")]
        private static extern int pxd_capturedFieldCount(int c_unitmap);

        [DllImport("c:\\xclib2\\lib\\xclybwnt.dll")]
        private static extern int pxd_renderStretchDIBits(int c_unitmap, int c_buf,
            int c_ulx, int c_uly, int c_lrx, int c_lry, int c_options, IntPtr c_hDC,
            int c_nX, int c_nY, int c_nWidth, int c_nHeight, int c_winoptions);

        [DllImport("c:\\xclib2\\lib\\xclybwnt.dll")]
        private static extern int pxd_saveBmp(int c_unitmap, string c_name, int c_buf,
            int c_ulx, int c_uly, int c_lrx, int c_lry, int c_savemode, int c_options);

        [DllImport("c:\\xclib2\\lib\\xclybwnt.dll")]
        private static extern int pxd_readuchar(int c_unitmap, int c_framebuf,
            int c_ulx, int c_uly, int c_lrx, int c_lry, byte[] c_membuf, int c_cnt, string c_colorspace);

        [DllImport("c:\\xclib2\\lib\\xclybwnt.dll")]
        private static extern int pxd_writeuchar(int c_unitmap, int c_framebuf,
            int c_ulx, int c_uly, int c_lrx, int c_lry, byte[] c_membuf, int c_cnt, string c_colorspace);

        [DllImport("c:\\xclib2\\lib\\xclybwnt.dll")]
        private static extern int pxd_defineImage(int c_unitmap, int c_framebuf,
            int c_ulx, int c_uly, int c_lrx, int c_lry, string c_colorspace);

        #endregion

        // Windows declarations that must be included in the declarations section.
        // SetStretchBltmode is a Windows function, for which C# .NET
        // doesn't have a direct equivalent.
        //
        [DllImport("gdi32.dll")]
        private static extern int SetStretchBltMode(IntPtr hDC, int mode);
        const int STRETCH_DELETESCANS = 3; // Constant used in calling SetStretchBltMode

        bool PIXCI_LIVE = false;
        int LastCapturedField = 0;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new Form1());
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            string FORMAT = "";
            string FORMATFILE = "";
            //
            // Open the XCLIB for use.
            //
            // First set options below according to the intended camera
            // and video format. Either FORMAT or FORMATFILE should be
            // set, not both.
            //
            // For PIXCI(R) SV2, SV3, SV4, SV5, SV5A, SV5B, and SV5L frame grabbers
            // common choices are RS-170, NSTC, NTSC/YC, CCIR, PAL, or PAL/YC.
            // (The SV5A and SV5B do not support NTSC/YC or PAL/YC).
            // For PIXCI(R) SV7 frame grabbers
            // common choices are RS-170, NSTC, CCIR, or PAL.
            // For PIXCI(R) SV8 frame grabbers
            // common choices are RS-170, NSTC, NTSC/YC, CCIR, PAL, PAL/YC.
            //
            // For PIXCI(R) A, CL1, CL2, CL3SD, D, D24, D32, D2X, D3X, D3XE, E1, E1DB, E4, E4DB, E4G2, E8, E8CAM, E8DB, e104x4,
            // EB1, EB1-POCL, EB1mini, EC1, ECB1, ECB1-34, ECB2, EL1, EL1DB, ELS2, SI, SI1, SI2, and SI4
            // frame grabbers, use "default" to select the default format for
            // the camera for which the PIXCI(R) frame grabber is intended.
            // For non default formats, use XCAP to save the video setup to
            // a file, and set FORMATFILE to the saved file's path name.
            // For camera's with RS-232 control, note that the saved
            // video setup only resets the PIXCI(R) frame grabber's
            // settings, but XCLIB does not reset the camera's settings.
            // For selected Camera Link cameras w. serial controls,
            // the video setup file may include serial commands which are
            // automatically sent by XCLIB to the camera.
            //
            //
            // For PIXCI(R) SV2, SV3, SV4, SV5, SV5A, SV5B, SV5L
            // FORMAT = "RS-170";	// RS-170 on input 2
            // FORMAT = "NTSC"; 	// NTSC on input 2
            // FORMAT = "NTSC/YC";	// NSTC S-Video on input 1		(N/A on SV5A,SV5B)
            // FORMAT = "CCIR"; 	// CCIR on input 2
            // FORMAT = "PAL";		// PAL (B,D,G,H,I) on input 2
            // FORMAT = "PAL/YC";	// PAL (B,D,G,H,I) S-Video on input 1	(N/A on SV5A,SV5B)
            FORMAT = "default";	// NSTC S-Video on input 1
            //
            //
            // For PIXCI(R) SV7
            // FORMAT = "RS-170";	// RS-170
            // FORMAT = "NTSC"; 	// NTSC
            // FORMAT = "CCIR"; 	// CCIR
            // FORMAT = "PAL";		// PAL
            // FORMAT = "default";	// NSTC
            //
            //
            // For PIXCI(R) SV8
            // FORMAT = "RS-170";	 // RS-170 on BNC 0
            // FORMAT = "NTSC"; 	 // NTSC on BNC 0
            // FORMAT = "NTSC/YC";	 // NSTC S-Video
            // FORMAT = "CCIR"; 	 // CCIR on BNC 0
            // FORMAT = "PAL";		 // PAL on BNC 0
            // FORMAT = "PAL/YC";	 // PAL (B,D,G,H,I) S-Video
            // FORMAT = "default";	 // NSTC on BNC 0
            //
            //
            // For PIXCI(R) A310
            // FORMAT = "default";	// as preset in board's eeprom
            // FORMAT = "RS-170";	// RS-170
            // FORMAT = "CCIR"; 	// CCIR
            // FORMAT = "Video 720x480i 60Hz";	     // RS-170
            // FORMAT = "Video 720x480i 60Hz RGB";
            // FORMAT = "Video 720x576i 50Hz";	     // CCIR
            // FORMAT = "Video 720x576i 50Hz RGB";
            // FORMAT = "Video 640x480i 60Hz";	     // RS-170
            // FORMAT = "Video 640x480i 60Hz RGB";
            // FORMAT = "Video 768x576i 50Hz";	     // CCIR
            // FORMAT = "Video 768x576i 50Hz RGB";
            // FORMAT = "Video 1920x1080i 60Hz";
            // FORMAT = "Video 1920x1080i 60Hz RGB";
            // FORMAT = "Video 1920x1080i 50Hz";
            // FORMAT = "Video 1920x1080i 50Hz RGB";
            // FORMAT = "Video 1920x1080p 60Hz";
            // FORMAT = "Video 1920x1080p 60Hz RGB";
            // FORMAT = "Video 1920x1080p 50Hz";
            // FORMAT = "Video 1920x1080p 50Hz RGB";
            // FORMAT = "Video 1280x720p 50Hz";
            // FORMAT = "Video 1280x720p 50Hz RGB";
            // FORMAT = "Video 1280x720p 60Hz";
            // FORMAT = "Video 1280x720p 60Hz RGB";
            // FORMAT = "SVGA 800x600 60Hz RGB";
            // FORMAT = "SXGA 1280x1024 60Hz RGB";
            // FORMAT = "VGA 640x480 60Hz RGB";
            // FORMAT = "XGA 1024x768 60Hz RGB";
            // FORMAT = "RS343 875i 60Hz";
            // FORMAT = "RS343 875i 60Hz RGB";
            //
            //
            // For PIXCI(R) A110
            // FORMAT = "default";	// as preset in board's eeprom
            // FORMAT = "RS-170";	// RS-170
            // FORMAT = "CCIR"; 	// CCIR
            // FORMAT = "Video 720x480i 60Hz";	     // RS-170
            // FORMAT = "Video 720x576i 50Hz";	     // CCIR
            // FORMAT = "Video 640x480i 60Hz";	     // RS-170
            // FORMAT = "Video 768x576i 50Hz";	     // CCIR
            // FORMAT = "Video 1920x1080i 60Hz";
            // FORMAT = "Video 1920x1080i 50Hz";
            // FORMAT = "Video 1920x1080p 60Hz";
            // FORMAT = "Video 1920x1080p 50Hz";
            // FORMAT = "Video 1280x720p 50Hz";
            // FORMAT = "Video 1280x720p 60Hz";
            // FORMAT = "RS343 875i 60Hz";
            //
            //
            // For PIXCI(R) A, CL1, CL2, CL3SD, D, D24, D32, D2X, D3X, D3XE, E1, E1DB, E4, E4DB, E4G2, E8, E8CAM, E8DB, e104x4,
            // EB1, EB1-POCL, EB1mini, EC1, ECB1, ECB1-34, ECB2, EL1, EL1DB, ELS2, SI, SI1, SI2, SI4
            // FORMAT = "default";    // as per board's intended camera
            //
            //
            // For any PIXCI(R) frame grabber
            // using a format file saved by XCAP which
            // can be loaded from file during execution
            // This may be a file name in the default directory,
            // or a full pathname.
            //
            // FORMATFILE = "xcvidset.fmt";
            //


            // MessageBox.Show("Opening PIXCI(R) Imaging  Board");

            pxd_PIXCIclose(); // In case this example was run before and aborted
            // before completion, the XCLIB may not have been
            // closed, and the open would fail.

            int i = pxd_PIXCIopen("", FORMAT, FORMATFILE);
            if (i < 0)
            {
                MessageBox.Show("Open Failed");
                pxd_mesgFault(1);
                Application.Exit();
            }
        }

        private void Snap_Button_Click(object sender, System.EventArgs e)
        {
            if (PIXCI_LIVE)
            {
                pxd_goUnLive(1);
                PIXCI_LIVE = false;
            }
            pxd_goSnap(1, 1);
            ImagePictureBox.Invalidate();
        }

        private void Live_Button_Click(object sender, System.EventArgs e)
        {
            PIXCI_LIVE = !PIXCI_LIVE; // Toggle Live
            if (PIXCI_LIVE)
            {
                pxd_goLive(1, 1);
            }
            else
            {
                pxd_goUnLive(1);
            }
        }

        private void Save_Button_Click(object sender, System.EventArgs e)
        {
            if (PIXCI_LIVE)
            {
                pxd_goUnLive(1);
                PIXCI_LIVE = false;
            }

            // Save the image in bitmap format, but do not overwrite an existing file.
            if (File.Exists("example.bmp"))
            {
                MessageBox.Show("File example.bmp already exists and will not be overwritten.");
            }
            else
            {
                pxd_saveBmp(1, "example.bmp", 1, 0, 0, -1, -1, 0, 0);
            }

        }

        
        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // If Live and captured image has changed then update the window
            if (PIXCI_LIVE && LastCapturedField != pxd_capturedFieldCount(1))
            {
                LastCapturedField = pxd_capturedFieldCount(1);
                ImagePictureBox.Invalidate();
            }
        }

        private void ImagePictureBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Create a local version of the graphics object for the PictureBox.
            Graphics Draw = e.Graphics;
            IntPtr hDC = Draw.GetHdc(); // Get a handle to ImagePictureBox.

            SetStretchBltMode(hDC, STRETCH_DELETESCANS);

            pxd_renderStretchDIBits(1, 1, 0, 0, -1, -1, 0, hDC, 0, 0, ImagePictureBox.Width, ImagePictureBox.Height, 0);

            Draw.ReleaseHdc(hDC); // Release ImagePictureBox handle.

            PixelPlotPictureBox.Invalidate(); // Redraw PixelPlotPictureBox.
        }

        private void PixelPlotPictureBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Create a local copy of the graphics object for the PictureBox.
            Graphics Plot = e.Graphics;
            Pen PlotPen = new Pen(Color.Red);
            int Xdim = pxd_imageXdim();
            int Ydim = pxd_imageYdim();
            float Xscale = (float)PixelPlotPictureBox.Width / Xdim;
            float Yscale = (float)PixelPlotPictureBox.Height / Ydim;

            byte[] buffer = new byte[Xdim * 3];

            int LineY = Ydim / 4; // Specify line at one quarter of image height to plot.

            // Read line into buffer
            int ReadUReturnCode = pxd_readuchar(1, 1, 0, LineY, Xdim, LineY + 1, buffer, buffer.Length, "RGB");

            if (ReadUReturnCode <= 0) // Check for errors
            {
                Font drawFont = new Font("Arial", 10);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                Plot.DrawString("ReadUchar Error:" + ReadUReturnCode.ToString(), drawFont, drawBrush, new PointF(15.0F, 60.0F));
            }
            else  // Draw Plot
            {
                for (int PlotColor = 0; PlotColor <= 2; PlotColor++)
                {
                    if (PlotColor.Equals(0)) PlotPen.Color = Color.Red;
                    else if (PlotColor.Equals(1)) PlotPen.Color = Color.Green;
                    else PlotPen.Color = Color.Blue;

                    int Element = PlotColor;
                    for (int x = 0; x <= Xdim - 3; x++)
                    {
                        Plot.DrawLine(PlotPen, x * Xscale, PixelPlotPictureBox.Height - (buffer[Element] * Yscale) - PlotColor,
                            (x + 3) * Xscale, PixelPlotPictureBox.Height - (buffer[Element + 3] * Yscale) - PlotColor);

                        Element = Element + 3;
                    }
                }
            }
        }

        private void Form1_Closed(object sender, System.EventArgs e)
        {
            // MessageBox.Show("Closing PIXCI(R) Board");
            pxd_PIXCIclose();
        }
    }
}
