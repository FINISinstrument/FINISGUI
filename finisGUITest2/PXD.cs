using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

namespace FinisGUI
{
    ///<summary>
    ///Class designed to consolidate/simplify PIXCI variables and commands.
    ///PIXCI is the software intended to control EPIX frame grabbers.
    ///In FINIS, we use the EPIX EB1mini frame grabber.
    ///</summary>
    public class PXD
    {
        #region Variables
        public bool IsStreaming { get; set; }
        public bool IsSixteenBit { get; set; }
        public bool IsOpen { get; set; }
        public string imageName { get; set; }
        public string liveName { get; set; }
        public string dateTime { get; set; }
        public int frameCount { get; set; }
        public int frameCountRemainder { get; set; }
        public int imagesCaptured { get; set; }
        public int loopCount;
        public int videoIndex; // Keep track of what major number to append to a video image
        #endregion

        public string ToggleBits(bool IsThirtyFPS)
        {
            string bootFile = null;
            try
            {
                if (IsStreaming)
                {
                    pxd_goUnLive(1);
                }
                if (IsSixteenBit)
                {
                    if (!IsThirtyFPS)
                    {
                        pxd_PIXCIclose();
                        Thread.Sleep(100);
                        pxd_PIXCIopen("", "", Constants.projectPath+"Resources/XCAPVideoSetup14Bit15Hz.fmt");
                        Thread.Sleep(100);
                    }
                    else
                    {
                        pxd_PIXCIclose();
                        Thread.Sleep(100);
                        pxd_PIXCIopen("", "", Constants.projectPath+"Resources/XCAPVideoSetup14Bit30Hz.fmt");
                        Thread.Sleep(100);
                    }
                    IsSixteenBit = false;
                    bootFile = "14-bit";
                }
                else
                {
                    if (!IsThirtyFPS)
                    {
                        pxd_PIXCIclose();
                        Thread.Sleep(100);
                        pxd_PIXCIopen("", "", Constants.projectPath+"Resources/XCAPVideoSetup16Bit15Hz.fmt");
                        Thread.Sleep(100);
                    }
                    else
                    {
                        pxd_PIXCIclose();
                        Thread.Sleep(100);
                        pxd_PIXCIopen("", "", Constants.projectPath+"Resources/XCAPVideoSetup16Bit30Hz.fmt");
                        Thread.Sleep(100);
                    }
                    IsSixteenBit = true;
                    bootFile = "16-bit";
                }
                if (IsStreaming)
                {
                    pxd_goLive(1, 1);
                }
                Thread.Sleep(100);
                return bootFile;
            }
            catch
            {
                return bootFile;
            }
        }

        public void Initialize()
        {
            try
            {
                // Determine indices for writing images
                String videoBase = Constants.videoPath + dateTime + "/" + liveName;
                videoIndex = 1;

                // Video number
                while (File.Exists(videoBase + videoIndex + "-1.tif"))
                {
                    videoIndex++;
                }


                pxd_PIXCIopen("", "", Constants.projectPath+"Resources/XCAPVideoSetup16Bit30Hz.fmt");
                IsOpen = true;
            }
            catch
            {
                IsOpen = false;
                pxd_PIXCIclose();
            }
            Thread.Sleep(100);
        }

        public void Close()
        {
            pxd_PIXCIclose();
            Thread.Sleep(100);
        }

        public void Restart(bool IsThirtyFPS)
        {
            pxd_PIXCIclose();
            if (IsThirtyFPS)
            {
                if (IsSixteenBit)
                {
                    pxd_PIXCIopen("", "", Constants.projectPath+"Resources/XCAPVideoSetup16Bit30Hz.fmt");
                }
                else
                {
                    pxd_PIXCIopen("", "", Constants.projectPath + "Resources/XCAPVideoSetup14Bit30Hz.fmt");
                }
            }
            else
            {
                if (IsSixteenBit)
                {
                    pxd_PIXCIopen("", "", Constants.projectPath + "Resources/XCAPVideoSetup16Bit15Hz.fmt");
                }
                else
                {
                    pxd_PIXCIopen("", "", Constants.projectPath + "Resources/XCAPVideoSetup14Bit15Hz.fmt");
                }
            }
            Thread.Sleep(1000);
        }

        public void StartStreaming()
        {
            pxd_goLive(1, 1);
            IsStreaming = true;
            Thread.Sleep(200);
        }

        public void StopStreaming()
        {
            pxd_goUnLive(1);
            IsStreaming = false;
            Thread.Sleep(200);
        }

        public void Snap()
        {
            pxd_goSnap(1, 1);
        }

        public void SaveStill(string stillName)
        {
            int i = 1;
            while (true)
            {
                if (File.Exists($"{Constants.stillPath}{stillName}{i}.tif"))
                {
                    i++;
                }
                else
                {
                    pxd_saveTiff(1, $"{Constants.stillPath}{stillName}{i}.tif", 1, 0, 0, -1, -1, 0, 0);
                    break;
                }
            }
        }

        public void Record(int startBuf, int videoPeriod)
        {
            // Call Record, but define frames based on member variable
            Record(startBuf, videoPeriod, frameCount);
        }

        public void Record(int startBuf, int videoPeriod, int frameCountAlt)
        {
            try
            {
                pxd_goLiveSeq(1, startBuf, startBuf + frameCountAlt, 1, frameCountAlt, videoPeriod);
                while (pxd_goneLive(1, 0)) ;
            }
            catch (Exception ex)
            {
                // ... promptBox.Text += $"Could not record images to memory:\n{ex.Message}\n";
            }
        }

        public void SaveSet()
        {
            int i = 1;

            while (true)
            {
                if (File.Exists($"{Constants.videoPath}{dateTime}/{liveName}{i}-1.tif"))
                {
                    i++;
                }
                else
                {
                    for (int j = 1; j <= frameCount; j++)
                    {
                        pxd_saveTiff(1, $"{Constants.videoPath}{dateTime}/{liveName}{i}-{j}.tif", j, 0, 0, -1, -1, 0, 0);
                    }
                    break;
                }
            }
        }

        public void SaveSet(int countOffset)
        {
            int i = 1;

            while (true)
            {
                if (File.Exists($"{Constants.videoPath}{dateTime}/{liveName}{i}-1.tif"))
                {
                    i++;
                }
                else
                {
                    i--;
                    for (int j = 1; j <= frameCount; j++)
                    {
                        pxd_saveTiff(1, $"{Constants.videoPath}{dateTime}/{liveName}{i}-{j + countOffset}.tif", j, 0, 0, -1, -1, 0, 0);
                    }
                    break;
                }
            }
        }

        // Function for threaded saving, where the start and end indices are specified
        public void ThreadedSaveSetRange(int start, int end)
        {
            try
            {
                // Create base filename that will be saved to
                String baseFilename = Constants.videoPath + dateTime + "/" + liveName + videoIndex;

                // Write all images in buffer to file
                for (int j = start; j <= end; j++)
                {
                    pxd_saveTiff(1, baseFilename + "-" + ( j + (loopCount * 400)) + ".tif", j, 0, 0, -1, -1, 0, 0);
                }

                // Increment loopCount for frame index
                loopCount++;
            }
            catch (Exception ex)
            {
                // ...
            }
        }

        #region Function Imports
        [DllImport("C:/FINIS/XCLIB/lib/xclybwnt.dll")]
        public static extern int pxd_PIXCIopen(string c_driverparms, string c_formatname, string c_formatfile);

        [DllImport("C:/FINIS/XCLIB/lib/xclybwnt.dll")]
        public static extern int pxd_PIXCIclose();

        [DllImport("C:/FINIS/XCLIB/lib/xclybwnt.dll")]
        public static extern int pxd_goLiveSeq(int c_unitmap, int c_startbuf, int c_endbuf, int c_incbuf, int c_numbuf, int c_videoperiod);

        [DllImport("C:/FINIS/XCLIB/lib/xclybwnt.dll")]
        public static extern bool pxd_goneLive(int c_unitmap, int c_timeout);

        [DllImport("C:/FINIS/XCLIB/lib/xclybwnt.dll")]
        public static extern int pxd_mesgFault(int c_unitmap);

        [DllImport("C:/FINIS/XCLIB/lib/xclybwnt.dll")]
        public static extern int pxd_doSnap(int c_unitmap, int c_buffer, int c_timeout);

        [DllImport("C:/FINIS/XCLIB/lib/xclybwnt.dll")]
        public static extern int pxd_goSnap(int c_unitmap, int c_buffer);

        [DllImport("C:/FINIS/XCLIB/lib/xclybwnt.dll")]
        public static extern int pxd_goLive(int c_unitmap, int c_buffer);

        [DllImport("C:/FINIS/XCLIB/lib/xclybwnt.dll")]
        public static extern int pxd_goUnLive(int c_unitmap);

        [DllImport("C:/FINIS/XCLIB/lib/xclybwnt.dll")]
        public static extern int pxd_imageXdim();

        [DllImport("C:/FINIS/XCLIB/lib/xclybwnt.dll")]
        public static extern int pxd_imageYdim();

        [DllImport("C:/FINIS/XCLIB/lib/xclybwnt.dll")]
        public static extern double pxd_imageAspectRatio();

        [DllImport("C:/FINIS/XCLIB/lib/xclybwnt.dll")]
        public static extern int pxd_capturedFieldCount(int c_unitmap);

        [DllImport("C:/FINIS/XCLIB/lib/xclybwnt.dll")]
        public static extern int pxd_renderStretchDIBits(int c_unitmap, int c_buf,
            int c_ulx, int c_uly, int c_lrx, int c_lry, int c_options, IntPtr c_hDC,
            int c_nX, int c_nY, int c_nWidth, int c_nHeight, int c_winoptions);

        [DllImport("C:/FINIS/XCLIB/lib/xclybwnt.dll")]
        public static extern int pxd_saveTiff(int c_unitmap, string c_name, int c_buf,
            int c_ulx, int c_uly, int c_lrx, int c_lry, int c_savemode, int c_options);

        [DllImport("C:/FINIS/XCLIB/lib/xclybwnt.dll")]
        public static extern int pxd_readuchar(int c_unitmap, int c_framebuf,
            int c_ulx, int c_uly, int c_lrx, int c_lry, byte[] c_membuf, int c_cnt, string c_colorspace);

        [DllImport("C:/FINIS/XCLIB/lib/xclybwnt.dll")]
        public static extern int pxd_writeuchar(int c_unitmap, int c_framebuf,
            int c_ulx, int c_uly, int c_lrx, int c_lry, byte[] c_membuf, int c_cnt, string c_colorspace);

        [DllImport("C:/FINIS/XCLIB/lib/xclybwnt.dll")]
        public static extern int pxd_defineImage(int c_unitmap, int c_framebuf,
            int c_ulx, int c_uly, int c_lrx, int c_lry, string c_colorspace);
        #endregion
    }
}
