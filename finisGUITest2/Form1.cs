/*
 * FINIS Project
 * Instrument Control Interface
 * 
 * Levi Norman - Research Assistant
 * Center for Space Engineering
 * Utah State University
 * 2018
 * 
 * Shutter Control
 * by Taylor Davison and Levi Norman
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using AVT.VmbAPINET;
using System.Text;

namespace FinisGUI
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public partial class Form1 : Form // Other part in Form1.Designer.cs
    {
        #region Variables
        // Forms Control
        Stopwatch Timer = new Stopwatch();  // Time video capture
        //float fps = 30.0f;
        float fpsActual = 0;
        string[] info = new string[42];
        int infoFiles = 0;
        bool IsThirtyFPS = true;
        bool restartLocked = true;
        //string imageName = "stillShot";   // Naming still shots
        //string liveName = "videoFrame";   // Naming video frames
        int LastCapturedField = 0;
        //int loopCount = 0;
        //int stillCount = 1;
        //int imagesCaptured = 0;
        //int frameCount = 40;
        //int frameCountRemainder = 0;
        //int fileBegin = 1;

        // XCLIB
        PXD pxd = new PXD();

        // VIMBA
        VMB vmb = new VMB();
        //Vimba sys = new Vimba();
        //CameraCollection cameras = null;
        //FeatureCollection features = null;
        //Feature feature = null;
        //Camera camera = null;
        //int exposureTime = 33000;
        //bool cameraOpen = false;
        //bool acquiring = false;
        //bool highGain = true;
        //double temperature = 0.0f;

        // Shutter
        ShutterControl shutter = new ShutterControl();
        #endregion

        #region Buttons
        private void Live_Button_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (!vmb.acquiring)
                {
                    vmb.StartAcquisition();
                    promptBox.Text += "Acquisition Running\n";
                }
                if (!pxd.IsStreaming)
                {
                    Shutter_Label.Text = shutter.Open();
                    Live_Button.BackColor = Color.Pink;
                    pxd.StartStreaming();
                }
                else
                {
                    Shutter_Label.Text = shutter.Close();
                    pxd.StopStreaming();
                    Live_Button.BackColor = Color.White;
                }
            }
            catch (VimbaException ve)
            {
                promptBox.Text += $"{ve.Message}\n";
            }
            catch (Exception ex)
            {
                promptBox.Text += $"{ex.Message}\n";
            }
        }   //

        private void Snap_Button_Click(object sender, System.EventArgs e)
        {
            try
            {
                vmb.StartAcquisition();

                if (pxd.IsStreaming)
                {
                    pxd.StopStreaming();
                }
                else
                {
                    shutter.Open();
                }

                pxd.Snap();
                ImagePictureBox.Invalidate();
                pxd.SaveStill(pxd.imageName);

                if (!pxd.IsStreaming)
                {
                    shutter.Close();
                }
                else
                {
                    pxd.StartStreaming();
                }
            }
            catch (VimbaException ve)
            {
                promptBox.Text += $"{ve.Message}\n";
            }
            catch (Exception ex)
            {
                promptBox.Text += $"{ex.Message}\n";
            }
        }

        private void Record_Button_Click(object sender, EventArgs e)
        {
            try
            {
                pxd.imagesCaptured = 0;
                pxd.loopCount = 0;
                if (pxd.IsStreaming)
                {
                    pxd.StopStreaming();
                }
                else if (!vmb.acquiring)
                {
                    vmb.StartAcquisition();
                }
                if (!pxd.IsSixteenBit)
                {
                    Bits_Label.Text = pxd.ToggleBits(IsThirtyFPS);
                }
                shutter.Open();

                pxd.dateTime = DateTime.Now.ToString("MM-dd-yyyy-HHmm");

                // Determine indices for writing images
                pxd.folderIndex = 1;
                pxd.folderPath = Constants.videoPath + pxd.dateTime + "-";

                // Video number
                while (File.Exists(string.Join("", new string[] { pxd.folderPath, pxd.folderIndex.ToString(), "/", pxd.liveName, "-1.tif" })))
                {
                    pxd.folderIndex++;
                }
                pxd.folderPath = pxd.folderPath + pxd.folderIndex + "/";
                Directory.CreateDirectory(pxd.folderPath);
                promptBox.Text += "Folder created at " + pxd.folderPath + "\n";
                pxd.frameCountRemainder = pxd.frameCount % 400;

                Timer.Reset();
                Timer.Start();
                for (int i = 0; i < pxd.frameCount / 400; i++)
                {
                    pxd.Record(1, pxd.halfBufferSize, 1);

                    Thread SAVE = new Thread(() => pxd.ThreadedSaveSetRange(1));
                    SAVE.Start();

                    pxd.Record(201, pxd.halfBufferSize, 1);
                    Thread SAVE2 = new Thread(() => pxd.ThreadedSaveSetRange(201));
                    SAVE2.Start();
                }
                //pxd.waitForLiveSequence();
                if (pxd.frameCountRemainder != 0)
                {
                    pxd.Record(1, pxd.frameCountRemainder, 1);
                    Timer.Stop();
                    pxd.SaveSet(pxd.frameCount - pxd.frameCountRemainder);
                    pxd.imagesCaptured += pxd.frameCountRemainder;
                }
                else Timer.Stop();
                shutter.Close();

                Thread.Sleep(2000);
                promptBox.Text += $"Capture Time: {(float)Timer.ElapsedMilliseconds / 1000}\n";
                promptBox.Text += $"Images Captured: {pxd.imagesCaptured}\n";
                fpsActual = ((pxd.imagesCaptured) / ((float)Timer.ElapsedMilliseconds / 1000));
                promptBox.Text += $"Actual Frame Rate: {fpsActual}\n";

                int j = 0;
                StreamWriter filesMissed = new StreamWriter(pxd.folderPath + "MissedFrames.txt", true);
                promptBox.Text += "Frames Missed: \"";
                filesMissed.WriteLine("Frames Missed:\n");
                for (int i = 1; i <= pxd.frameCount; i++)
                {
                    if (!File.Exists(pxd.folderPath + pxd.liveName + "-" + i.ToString() + ".tif"))
                    {
                        filesMissed.WriteLine($"{i}\n");
                        promptBox.Text += $"{i}, ";
                        j++;
                    }
                }
                promptBox.Text += $"\"\nTotal: {j}\n";
                filesMissed.WriteLine($"Total: {j}\n");
                filesMissed.Close();
            }
            catch (VimbaException ve)
            {
                promptBox.Text += $"{ve.Message}\n";
            }
            catch (Exception ex)
            {
                promptBox.Text += $"{ex.Message}\n";
            }
        }   // In Progress

        private void StillName_Button_Click(object sender, EventArgs e)
        {
            try
            {
                pxd.imageName = StillName_textBox.Text;
                StillName_Label.Text = pxd.imageName;
                StillName_textBox.Text = "";
            }
            catch
            {
                StillName_textBox.Text = "INVALID";
            }
        }

        private void RecordName_Button_Click(object sender, EventArgs e)
        {
            try
            {
                pxd.liveName = RecordName_textBox.Text;
                RecordName_Label.Text = pxd.liveName;
                RecordName_textBox.Text = "";
            }
            catch
            {
                RecordName_textBox.Text = "INVALID";
            }
        }

        private void Clear_Button_Click(object sender, EventArgs e)
        {
            promptBox.Text = "";
        }

        private void PicCount_Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (PicCount_TextBox.Text != "")
                {
                    int frameCountTemp = Convert.ToInt32(PicCount_TextBox.Text);

                    if (frameCountTemp > 100)
                    {
                        long driveSpace;
                        DriveInfo myDrive = new DriveInfo("C:\\");
                        driveSpace = (myDrive.AvailableFreeSpace / 1048576) - 500;
                        promptBox.Text += $"Available Disk Space: {driveSpace} Mb\n";
                        FileInfo file = new FileInfo("C:/FINIS/Images/Example.tif");
                        driveSpace = ((driveSpace * 1048576) / file.Length);
                        promptBox.Text += $"Max frames possible: {driveSpace}\n";

                        if (frameCountTemp > driveSpace)
                        {
                            promptBox.Text += $"Not enough disk space.\nPlease enter a number smaller than {driveSpace}.\n";
                        }
                        else
                        {
                            pxd.frameCount = frameCountTemp;
                            promptBox.Text += $"Frame Count set to {pxd.frameCount}\n";
                        }
                    }
                    else if (pxd.frameCount <= 0)
                    {
                        pxd.frameCount = 1;
                        promptBox.Text += "Frame Count must be larger than 0\n";
                    }
                    else
                    {
                        pxd.frameCount = frameCountTemp;
                    }
                    PicCount_Label.Text = $"{pxd.frameCount}";
                    PicCount_TextBox.Text = "";

                    if (pxd.frameCount < 400)
                    {
                        info = new string[pxd.frameCount + 2];
                    }
                    else if (pxd.frameCount % 400 == 0)
                    {
                        info = new string[pxd.frameCount / 400 + 2];
                    }
                    else // if (pxd.frameCount > 400 && pxd.frameCount % 400 != 0)
                    {
                        info = new string[pxd.frameCount / 400 + 3];
                    }
                    pxd.frameCountRemainder = pxd.frameCount % 400;

                    info[1] = "TempSetPoint\tCamTemp\tExposure\tGain\tFrameRate";
                }
            }
            catch (VimbaException ve)
            {
                promptBox.Text += $"{ve.Message}\n";
            }
            catch (Exception ex)
            {
                promptBox.Text += $"{ex.Message}\n";
            }
        }

        private void FPS_Button_Click(object sender, EventArgs e)
        {
            try
            {
                IsThirtyFPS = vmb.ToggleFramesPerSecond(IsThirtyFPS);
                if (IsThirtyFPS)
                {
                    exposureInfo_Label.Text = "Exposure (up to 66000μs):";
                    Exposure_Label.Text = "66000";
                    promptBox.Text += $"Exposure auto-reassigned: 66000μs";
                    FPS_Label.Text = "30";
                }
                else
                {
                    exposureInfo_Label.Text = "Exposure (up to 33000μs):";
                    Exposure_Label.Text = "33000";
                    promptBox.Text += $"Exposure auto-reassigned: 33000μs";
                    FPS_Label.Text = "15";
                }

                try
                {
                    pxd.Restart(IsThirtyFPS);

                    if (pxd.IsStreaming)
                    {
                        pxd.StartStreaming();
                    }
                }
                catch (Exception ex)
                {
                    pxd.Close();
                    promptBox.Text += "PIXCI failed during restart\n";
                    promptBox.Text += $"{ex.Message}\n";
                }
            }
            catch (VimbaException ve)
            {
                promptBox.Text += $"VIMBA: {ve.Message}\n";
            }
            catch (Exception ex)
            {
                promptBox.Text += $"{ex.Message}\n";
            }
        }   //

        private void Exposure_Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Exposure_TextBox.Text != "")
                {
                    if (vmb.exposureTime != Convert.ToInt32(Exposure_TextBox.Text))
                    {
                        Exposure_Label.Text = $"{vmb.UpdateExposureTime(IsThirtyFPS, Convert.ToInt32(Exposure_TextBox.Text))}";
                    }
                }
            }
            catch (VimbaException ve)
            {
                Exposure_TextBox.Text = "";
                promptBox.Text += $"{ve.Message}\n";
            }
            catch
            {
                Exposure_TextBox.Text = "";
                promptBox.Text += "Error\n";
            }
        }   //

        private void Bits_Button_Click(object sender, EventArgs e)
        {
            try
            {
                Bits_Label.Text = pxd.ToggleBits(IsThirtyFPS);
            }
            catch (Exception ex)
            {
                promptBox.Text += $"{ex.Message}\n";
            }
        }   //

        private void Gain_Button_Click(object sender, EventArgs e)
        {
            try
            {
                vmb.ToggleGain();
                if (vmb.highGain)
                {
                    Gain_Label.Text = "High";
                }
                else
                {
                    Gain_Label.Text = "Low";
                }
            }
            catch (VimbaException ve)
            {
                promptBox.Text += $"{ve.Message}\n";
            }
        }   //

        private void OpenShutter_Button_Click(object sender, EventArgs e)
        {
            shutter.Open();
            Shutter_Label.Text = "Open";
        }   //

        private void CloseShutter_Button_Click(object sender, EventArgs e)
        {
            shutter.Close();
            Shutter_Label.Text = "Closed";
        }   //

        private void ShutterLock_Button_Click(object sender, EventArgs e)
        {
            if (!shutter.ShutterLocked)
            {
                shutter.ShutterLocked = true;
                ShutterLock_Button.Text = "Unlock Shutter";
                Shutter_Label.BorderStyle = BorderStyle.FixedSingle;
                Shutter_Label.BackColor = Color.LightBlue;
                OpenShutter_Button.BackColor = Color.LightGray;
                OpenShutter_Button.ForeColor = Color.DimGray;
                CloseShutter_Button.BackColor = Color.LightGray;
                CloseShutter_Button.ForeColor = Color.DimGray;
            }
            else
            {
                shutter.ShutterLocked = false;
                ShutterLock_Button.Text = "Lock Shutter";
                Shutter_Label.BorderStyle = BorderStyle.None;
                Shutter_Label.BackColor = Color.LightCyan;
                OpenShutter_Button.BackColor = Color.White;
                OpenShutter_Button.ForeColor = Color.Black;
                CloseShutter_Button.BackColor = Color.White;
                CloseShutter_Button.ForeColor = Color.Black;
            }
        }   //

        private void EmptyVideoFolder_Button_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(Constants.videoPath);
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
            catch (Exception ex)
            {
                promptBox.Text += $"{ex.Message}\n Check Video Folder\n{Constants.videoPath}\n";
            }
        }

        private void EmptyStillFolder_Button_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(Constants.stillPath);
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
            catch (Exception ex)
            {
                promptBox.Text += $"{ex.Message}\n Check Still Image Folder\n{Constants.stillPath}\n";
            }
        }

        private void LockReinitialize_Button_Click(object sender, EventArgs e)
        {
            if (restartLocked)
            {
                restartLocked = false;
                ReinitializeSystem_Button.BackColor = Color.Pink;
                LockReinitialize_Button.Text = "Lock";
                LockReinitialize_Button.BackColor = Color.LightPink;
            }
            else
            {
                restartLocked = true;
                ReinitializeSystem_Button.BackColor = Color.DarkGray;
                LockReinitialize_Button.Text = "Locked";
                LockReinitialize_Button.BackColor = Color.White;
            }
        }

        private void ReinitializeSystem_Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (!restartLocked)
                {
                    if (shutter.ShutterOpen)
                    {
                        shutter.Close();
                        Shutter_Label.Text = "Closed";
                    }
                    if (vmb.acquiring)
                    {
                        vmb.StartAcquisition();
                    }
                    vmb.Close();
                    Thread.Sleep(100);

                    int i = vmb.Initialize();
                    if (i < 0)
                    {
                        MessageBox.Show("Camera Open Failed");
                        Application.Exit();
                    }

                    string FORMAT = "";
                    string FORMATFILE = "";
                    FORMATFILE = $"{Constants.projectPath}Resources/XCAPVideoSetup16Bit30Hz.fmt";
                    PXD.pxd_PIXCIclose();
                    Thread.Sleep(100);
                    i = PXD.pxd_PIXCIopen("", FORMAT, FORMATFILE);
                    Thread.Sleep(100);
                    if (i < 0)
                    {
                        MessageBox.Show("PXD Open Failed");
                        PXD.pxd_mesgFault(1);
                        Application.Exit();
                    }

                    promptBox.Text += "System Fully Booted\n";
                    restartLocked = true;
                    ReinitializeSystem_Button.BackColor = Color.DarkGray;
                    LockReinitialize_Button.Text = "Locked";
                    LockReinitialize_Button.BackColor = Color.White;
                }
            }
            catch (VimbaException ve)
            {
                promptBox.Text += $"{ve.Message}\n";
            }
            catch (Exception ex)
            {
                promptBox.Text += $"{ex.Message}\n";
            }
        }

        private void InfoFile_Button_Click(object sender, EventArgs e)
        {
            try
            {
                infoFiles = 0;
                while (File.Exists($"{Constants.infoPath}VideoInfo/info{infoFiles}.txt"))
                {
                    infoFiles++;
                }
                File.WriteAllLines($"{Constants.infoPath}VideoInfo/info{infoFiles}.txt", info);
                promptBox.Text += $"\"info{infoFiles}.txt\" created\n";
            }
            catch
            {
                promptBox.Text += "Could not create or overwrite file.\n";
            }
        }

        private void AppendImages_Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (pxd.frameCount <= 400)
                {
                    for (int i = 1; i <= pxd.frameCount; i++)
                    {
                        File.AppendAllText($"{Constants.videoPath}{pxd.liveName}{i}.tif", info[i + 1]);
                    }
                }
                else
                {
                    int j = 1;
                    for (int i = 0; (float)i < (pxd.frameCount / 400.0); i++)
                    {
                        if (i * 400 + 400 <= pxd.frameCount)
                        {
                            while (j <= 400)
                            {
                                File.AppendAllText($"{Constants.videoPath}{pxd.liveName}{i * 400 + j}.tif", info[i + 2]);
                                j++;
                            }
                            j = 1;

                        }
                        else
                        {
                            if (pxd.frameCountRemainder != 0)
                            {
                                for (int k = 1; k <= pxd.frameCountRemainder; k++)
                                {
                                    File.AppendAllText($"{Constants.videoPath}{pxd.liveName}{(i + 1) * 400 + k}.tif", info[i + 3]);
                                }
                            }
                            break;
                        }
                    }
                }
                promptBox.Text += "Success\n";
            }
            catch (Exception ex)
            {
                promptBox.Text += $"{ex.Message}\n";
            }
        }

        private void ContinuityCheck_Button_Click(object sender, EventArgs e)
        {
            int j = 0;
            promptBox.Text += "Files Missed: \"";
            for (int i = 1; i <= pxd.frameCount; i++)
            {
                if (!File.Exists($"{Constants.videoPath}03-30-2019-1031-1/videoFrame-{i}.tif"))
                {
                    promptBox.Text += $"{i}, ";
                    j++;
                }
            }
            promptBox.Text += $"\"\nFile missed count: {j}";

        }
        #endregion
        
        #region Other Functions
        /// <summary>
        /// This function executes as the program boots, or 'Loads'. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, System.EventArgs e)
        {
            try
            {
                shutter.Close();
                pxd.IsSixteenBit = true;
                vmb.exposureTime = 33000;
                vmb.highGain = true;

                pxd.imageName = "stillShot";
                pxd.liveName = "videoFrame";
                pxd.frameCount = 40;
                pxd.frameCountRemainder = 0;

                info[1] = "TempSetPoint\tCamTemp\tExposure\tGain\tFrameRate";
                while (File.Exists($"{Constants.infoPath}VideoInfo/info{infoFiles}.txt"))
                {
                    infoFiles++;
                }

                pxd.Close(); // In case the DLL was run before and aborted without closing
                pxd.Initialize();

                if (!pxd.IsOpen)
                {
                    Thread.Sleep(5000);
                    pxd.Initialize();
                }
                if (!pxd.IsOpen)
                {
                    MessageBox.Show("Open Failed, Possible Frame Grabber Malfunction\n" +
                        "If you get this message repeatedly, restart Windows");
                    PXD.pxd_mesgFault(1);
                    Application.Exit();
                }

                promptBox.Text += $"{vmb.Initialize()}";
                if (!vmb.IsOpen)
                {
                    Thread.Sleep(5000);
                    int i = vmb.Initialize();
                    if (i < 0)
                    {
                        MessageBox.Show("Camera Open Failed");
                        Application.Exit();
                    }
                }
                if (!vmb.IsOpen)
                {
                    promptBox.Text += "Could not boot Camera\n";
                    promptBox.Text += "Closing PIXCI\n";
                    PXD.pxd_PIXCIclose();
                }
                else
                {
                    promptBox.Text += "Camera booted and ready\n";
                }
            }
            catch (Exception ex)
            {
                promptBox.Text += $"{ex.Message}\n";
            }
        }   //

        private void Form1_Closed(object sender, System.EventArgs e)
        {
            try
            {
                pxd.Close();
                vmb.Close();
                if (shutter.ShutterOpen)
                {
                    shutter.ForceClose(); // Closes Shutter whether or not the shutter is locked in position
                }
            }
            catch
            {
                // ...
            }
        }   //

        private void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                // If Live and captured image has changed then update the window
                if (pxd.IsStreaming && LastCapturedField != PXD.pxd_capturedFieldCount(1))
                {
                    LastCapturedField = PXD.pxd_capturedFieldCount(1);
                    ImagePictureBox.Invalidate();
                }
            }
            catch
            {
                // ...
            }
        }

        private void ImagePictureBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Create a local version of the graphics object for the PictureBox.
            Graphics Draw = e.Graphics;
            IntPtr hDC = Draw.GetHdc(); // Get a handle to ImagePictureBox.

            WindowsFunctions.SetStretchBltMode(hDC, WindowsFunctions.STRETCH_DELETESCANS);

            PXD.pxd_renderStretchDIBits(1, 1, 0, 0, -1, -1, 0, hDC, 0, 0, ImagePictureBox.Width, ImagePictureBox.Height, 0);

            Draw.ReleaseHdc(hDC); // Release ImagePictureBox handle.
        }

        private void ThreadedSave()
        {
            try
            {
                for (int k = 1; k <= 200; k++)
                {
                    //PXD.pxd_saveTiff(1, $"c:/FINIS/Images/Video/{pxd.liveName}{fileBegin}-{k + (400 * pxd.loopCount)}.tif", k, 0, 0, -1, -1, 0, 0);
                }
            }
            catch (Exception ex)
            {
                promptBox.Text += $"Could not save images:\n{ex.Message}\n";
            }
        }

        private void ThreadedSave2()
        {
            try
            {
                for (int k = 201; k <= 400; k++)
                {
                    //PXD.pxd_saveTiff(1, $"c:/FINIS/Images/Video/{pxd.liveName}{fileBegin}-{k + (400 * pxd.loopCount)}.tif", k, 0, 0, -1, -1, 0, 0);
                }
                pxd.loopCount++;
            }
            catch (Exception ex)
            {
                promptBox.Text += $"Could not save images:\n{ex.Message}\n";
            }
        }

        /*private void StartAcquisition()
        {
            if (!acquiring)
            {
                try
                {
                    features = camera.Features;
                    feature = features["AcquisitionStart"];
                    feature.RunCommand();
                    promptBox.Text += "Acquisition Running\n";
                    acquiring = true;
                }
                catch (VimbaException ve)
                {
                    promptBox.Text += $"{ve.Message}";
                }
                catch (Exception ex)
                {
                    promptBox.Text += $"{ex.Message}\n";
                }
            }
            Thread.Sleep(100);
        }*/

        /*private void ToggleBits()
        {
            try
            {
                if (pxd.Live)
                {
                    PXD.pxd_goUnLive(1);
                }
                if (pxd.SixteenBit)
                {
                    Bits_Label.Text = "14-bit";
                    if (fps == 15.0f)
                    {
                        string FORMAT = "";
                        string FORMATFILE = "";
                        FORMATFILE = "c:/FINIS/FINISGUI/finisGUITest2/Resources/XCAPVideoSetup14Bit15Hz.fmt";
                        PXD.pxd_PIXCIclose();
                        Thread.Sleep(100);
                        PXD.pxd_PIXCIopen("", FORMAT, FORMATFILE);
                        Thread.Sleep(100);
                    }
                    else
                    {
                        string FORMAT = "";
                        string FORMATFILE = "";
                        FORMATFILE = "c:/FINIS/FINISGUI/finisGUITest2/Resources/XCAPVideoSetup14Bit30Hz.fmt";
                        PXD.pxd_PIXCIclose();
                        Thread.Sleep(100);
                        PXD.pxd_PIXCIopen("", FORMAT, FORMATFILE);
                        Thread.Sleep(100);
                    }
                }
                else
                {
                    Bits_Label.Text = "16-bit";
                    if (fps == 15.0f)
                    {
                        string FORMAT = "";
                        string FORMATFILE = "";
                        FORMATFILE = "c:/FINIS/FINISGUI/finisGUITest2/Resources/XCAPVideoSetup16Bit15Hz.fmt";
                        PXD.pxd_PIXCIclose();
                        Thread.Sleep(100);
                        PXD.pxd_PIXCIopen("", FORMAT, FORMATFILE);
                        Thread.Sleep(100);
                    }
                    else
                    {
                        string FORMAT = "";
                        string FORMATFILE = "";
                        FORMATFILE = "c:/FINIS/FINISGUI/finisGUITest2/Resources/XCAPVideoSetup16Bit30Hz.fmt";
                        PXD.pxd_PIXCIclose();
                        Thread.Sleep(100);
                        PXD.pxd_PIXCIopen("", FORMAT, FORMATFILE);
                        Thread.Sleep(100);
                    }
                }
                if (pxd.Live)
                {
                    PXD.pxd_goLive(1, 1);
                }
                Thread.Sleep(100);
            }
            catch
            {
                // ...
            }
        }*/

        private void PicCount_TextBox_TextChanged(object sender, EventArgs e)
        {
            AcceptButton = PicCount_Button;
        }

        private void Exposure_TextBox_TextChanged(object sender, EventArgs e)
        {
            AcceptButton = Exposure_Button;
        }

        private void RecordName_textBox_TextChanged(object sender, EventArgs e)
        {
            AcceptButton = RecordName_Button;
        }

        private void StillName_textBox_TextChanged(object sender, EventArgs e)
        {
            AcceptButton = StillName_Button;
        }
        #endregion

        private void Useless_Button_Click(object sender, EventArgs e)
        {
            //Initialize the sensor port
            string sensorPort = "COM12";
            uint sensorBaudrate = 115200;
            IMU imu = new IMU(sensorPort, sensorBaudrate);
            imu.ConnectSensor();
            if (imu.isConnected())
            {
                promptBox.Text += "IMU Sucessfully connected";
            }
            else
            {
                promptBox.Text += "Failed to connect IMU";
            }
            

            string data = imu.GetYPR();
            IMUData.Text = data;
            //StreamWriter imuData = new StreamWriter(Constants.infoPath + "MissedFrames.txt", true);
            //imuData.WriteLine("");
        }
    }
}
      