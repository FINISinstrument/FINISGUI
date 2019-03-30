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

        public int halfBufferSize; // Number of frames in half the buffer
        public int loopCount;
        public int folderIndex; // Keep track of what major number to append to a video image
        public string folderPath;
        #endregion

        /// <summary>
        /// Reboots the PIXCI software to change from 14-bit pixel output to 16-bit or vice versa from the frame grabber.
        /// </summary>
        /// <param name="IsThirtyFPS"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Initializes the PIXCI software with the default settings file.
        /// Initializes for 16-bit pixel output at 30Hz.
        /// </summary>
        public void Initialize()
        {
            try
            {
                // Number of frames stored in half the image buffer
                halfBufferSize = 200;


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

        /// <summary>
        /// Closes the PIXCI software.
        /// </summary>
        public void Close()
        {
            pxd_PIXCIclose();
            Thread.Sleep(100);
        }

        /// <summary>
        /// Reboots the PIXCI software with settings prior to reboot.
        /// </summary>
        /// <param name="IsThirtyFPS"></param>
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

        /// <summary>
        /// Begins a live feed from camera.
        /// </summary>
        public void StartStreaming()
        {
            pxd_goLive(1, 1);
            IsStreaming = true;
            Thread.Sleep(200);
        }

        /// <summary>
        /// Stops live feed from camera.
        /// </summary>
        public void StopStreaming()
        {
            pxd_goUnLive(1);
            IsStreaming = false;
            Thread.Sleep(200);
        }

        /// <summary>
        /// Captures a single image.
        /// </summary>
        public void Snap()
        {
            pxd_goSnap(1, 1);
        }

        /// <summary>
        /// Saves the image currently stored in buffer 1.
        /// </summary>
        /// <param name="name"></param>
        public void SaveStill(string name)
        {
            int i = 1;
            while (true)
            {
                if (File.Exists($"{Constants.stillPath}{name}{i}.tif"))
                {
                    i++;
                }
                else
                {
                    pxd_saveTiff(1, $"{Constants.stillPath}{name}{i}.tif", 1, 0, 0, -1, -1, 0, 0);
                    break;
                }
            }
        }
        
        /// <summary>
        /// Saves the image currently stored in the specified buffer
        /// </summary>
        /// <param name="name"></param>
        /// <param name="buffer"></param>
        public void SaveStill(string name, int buffer)
        {
            int i = 1;
            while (true)
            {
                if (File.Exists($"{Constants.stillPath}{name}{i}.tif"))
                {
                    i++;
                }
                else
                {
                    pxd_saveTiff(1, $"{Constants.stillPath}{name}{i}.tif", buffer, 0, 0, -1, -1, 0, 0);
                    break;
                }
            }
        }

        /// <summary>
        /// Records a number of frames specified beginning where specified.
        /// </summary>
        /// <param name="startBuf"></param>
        /// <param name="numFrames"></param>
        public void Record(int startBuf, int numFrames, int videoPeriod)
        {
            try
            {
                pxd_goLiveSeq(1, startBuf, startBuf + numFrames, 1, numFrames, videoPeriod);
                waitForLiveSequence();
            }
            catch (Exception ex)
            {
                // Do nothing
            }
        }

        /// <summary>
        /// Saves a set of images from the frame buffer.
        /// </summary>
        public void SaveSet()
        {
            for (int j = 1; j <= frameCount; j++)
            {
                pxd_saveTiff(1, string.Join("", new string[] { folderPath, liveName, "-", j.ToString(), ".tif" }), j, 0, 0, -1, -1, 0, 0);
            }
        }

        /// <summary>
        /// Saves a set of images from the frame buffer beginning with a countOffset difference in the name.
        /// </summary>
        /// <param name="countOffset"></param>
        public void SaveSet(int countOffset)
        {
            for (int j = 1; j <= frameCountRemainder; j++)
            {
                pxd_saveTiff(1, string.Join("", new string[] { folderPath, liveName, "-", (j + countOffset).ToString(), ".tif" }), j, 0, 0, -1, -1, 0, 0);
            }
        }

        /// <summary>
        /// Function for threaded saving with specified starting indes.
        /// startIndex corresponds to beginning frame buffer and how frames are saved.
        /// </summary>
        /// <param name="startIndex"></param>
        public void ThreadedSaveSetRange(int startIndex)
        {
            try
            {
                // Create base form of fileName that will be saved to
                string baseFileName = folderPath + liveName + "-";
                string fileName;
                int index;

                // Write 200 frames of buffer beginning at startIndex to file
                for (int j = 1; j <= halfBufferSize; j++)
                {
                    index = j + (loopCount * halfBufferSize);
                    fileName = string.Join("", new string[] { baseFileName, index.ToString(), ".tif" });
                    pxd_saveTiff(1, fileName, j, 0, 0, -1, -1, 0, 0);
                }

                // Increment loopCount for frame index
                loopCount++;
                imagesCaptured += halfBufferSize;
            }
            catch (Exception ex)
            {
                // ...
            }
        }

        public void waitForLiveSequence()
        {
            while (pxd_goneLive(1, 0)) ;
        }

        #region Function Imports
        //Imports of functions from the XCLIB library.

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
