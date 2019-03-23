using System;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;

namespace FinisGUI
{
    ///<summary>
    ///Class designed to control the Numato Lab 2 Channel USB Powered Relay.
    ///This controls a solenoid attached to a shutter pane in the FINIS instrument.
    ///</summary>
    public class ShutterControl
    {
        public bool ShutterOpen { get; set; }
        public bool ShutterLocked { get; set; }

        ///<summary>
        ///Runs a precompiled C++ program to open the Shutter when it is unlocked.
        ///Returns the state of the shutter as a string, "Open" or "Closed"
        ///</summary>
        public string Open()
        {
            try
            {
                if (!ShutterOpen && !ShutterLocked)
                {
                    ShutterOpen = true;
                    var process = Process.Start(Constants.projectPath + "Resources/OpenShutter.exe");
                    process.WaitForExit();
                    Thread.Sleep(100);
                    return "Open";
                }
                else
                {
                    if (ShutterOpen)
                    {
                        return "Open";
                    }
                    else
                    {
                        return "Closed";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\n");
                return "Err";
            }
        }

        ///<summary>
        ///Runs a precompiled C++ program to close the Shutter when it is unlocked.
        ///Returns the state of the shutter as a string, "Open" or "Closed"
        ///</summary>
        public string Close()
        {
            try
            {
                if (ShutterOpen && !ShutterLocked)
                {
                    ShutterOpen = false;
                    var process = Process.Start(Constants.projectPath + "Resources/CloseShutter.exe");
                    process.WaitForExit();
                    Thread.Sleep(100);
                    return "Closed";
                }
                else
                {
                    if (ShutterOpen)
                    {
                        return "Open";
                    }
                    else
                    {
                        return "Closed";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\n");
                return "Err";
            }
        }

        ///<summary>
        ///Runs a precompiled C++ program to close the Shutter whether or not it's locked.
        ///</summary>
        public void ForceClose()
        {
            try
            {
                Process.Start(Constants.projectPath + "Resources/CloseShutter.exe");
            }
            catch
            {
                // ...
            }
        }
    }
}
