using System;
using System.Collections.Generic;
using System.Text;
using AVT.VmbAPINET;
using System.Threading;

namespace FinisGUI
{
    ///<summary>
    ///Class designed to consolidate VIMBA variables and commands.
    ///VIMBA is the software intended for use on Allied Vision Cameras.
    ///</summary>
    public class VMB
    {
        Vimba sys = new Vimba();
        FeatureCollection features { get; set; }
        Feature feature { get; set; }
        public Camera camera { get; set; }
        public int exposureTime { get; set; }
        public bool cameraOpen { get; set; }
        public bool acquiring { get; set; }
        public bool highGain { get; set; }
        public double temperature { get; set; }
        public bool IsOpen { get; set; }

        public string Initialize()
        {
            try
            {
                sys.Shutdown();
                sys.Startup();
                Thread.Sleep(500);
                camera = sys.OpenCameraByID("DEV_64AA2C448F1F2349", VmbAccessModeType.VmbAccessModeFull);
                camera.Features["ExposureTime"].FloatValue = 33000;
                camera.Features["AcquisitionFrameRate"].FloatValue = 30.0f;
                camera.Features["SensorGain"].EnumValue = "Gain1";
                camera.Features["SensorTemperatureSetpointValue"].IntValue = 20;
                camera.Features["SensorTemperatureSetpointSelector"].IntValue = 1;
                IsOpen = true;
                return "Open";
            }
            catch
            {
                sys.Shutdown();
                IsOpen = false;
                return "Closed";
            }
        }

        public void Close()
        {
            camera.Close();
            sys.Shutdown();
        }

        public void StartAcquisition()
        {
            if (!acquiring)
            {
                camera.Features["AcquisitionStart"].RunCommand();
                acquiring = true;
                Thread.Sleep(100);
            }
        }

        public int UpdateExposureTime(bool IsThirtyFPS, int exposureTime)
        {
            if (IsThirtyFPS)
            {
                if (exposureTime > 33000)
                {
                    exposureTime = 33000;
                }
                else if (exposureTime <= 0)
                {
                    exposureTime = 33000;
                }
            }
            else
            {
                if (exposureTime > 66000)
                {
                    exposureTime = 66000;
                }
                else if (exposureTime <= 0)
                {
                    exposureTime = 66000;
                }
            }

            camera.Features["ExposureTime"].FloatValue = (float)exposureTime;
            return exposureTime;
        }

        public bool ToggleFramesPerSecond(bool IsThirtyFPS)
        {
            if (IsThirtyFPS)
            {
                camera.Features["ExposureTime"].FloatValue = 66000.0f;
                IsThirtyFPS = false;
                camera.Features["AcquisitionFrameRate"].FloatValue = 15.0f;
            }
            else
            {
                camera.Features["ExposureTime"].FloatValue = 33000.0f;
                IsThirtyFPS = true;
                camera.Features["AcquisitionFrameRate"].FloatValue = 30.0f;
            }

            return IsThirtyFPS;
        }

        public void ToggleGain()
        {
            if (highGain)
            {
                camera.Features["SensorGain"].EnumValue = "Gain0";
                highGain = false;
            }
            else
            {
                camera.Features["SensorGain"].EnumValue = "Gain1";
                highGain = true;
            }
        }

        public void UpdateTemperature()
        {
            temperature = camera.Features["DeviceTemperature"].FloatValue;
        }

    }
}
