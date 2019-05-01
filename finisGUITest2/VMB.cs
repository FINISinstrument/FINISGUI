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
        //Feature feature { get; set; }               // Used only in secondary syntax shown in the Initialize() function
        public Camera camera { get; set; }
        public int exposureTime { get; set; }
        public bool cameraOpen { get; set; }
        public bool IsAcquiring { get; set; }
        public bool highGain { get; set; }
        public double temperature { get; set; }
        public bool IsOpen { get; set; }

        /// <summary>
        /// Starts the Vimba API, opening a camera object by reference to the ID of a particular CL-008 camera, and setting the exposure, frame rate, gain, and temperature setpoint of the camera.
        /// </summary>
        /// <returns>
        /// 0 if successful, -1 if exception is thrown
        /// </returns>
        public int Initialize()
        {
            try
            {
                sys.Shutdown();
                sys.Startup();
                Thread.Sleep(500);
                camera = sys.OpenCameraByID("DEV_64AA2C448F1F2349", VmbAccessModeType.VmbAccessModeFull);   // OpenCameraByID creates a Camera instance associated with a camera with a unique ID
                camera.Features["ExposureTime"].FloatValue = 33000; // Sets the float associated with the ExposureTime of the camera (in microseconds). Each feature can be changed using the different member variables
                // The previous line of code is equivalent to the following:
                /*
                feature = camera.Features["ExposureTime"];
                feature.FloatValue = 33000;
                */
                camera.Features["AcquisitionFrameRate"].FloatValue = 30.0f;         // Defaults to 30 frames per second
                camera.Features["SensorGain"].EnumValue = "Gain1";                  // Defaults to high gain (brighter image)
                camera.Features["SensorTemperatureSetpointValue"].IntValue = 20;    // Sets the TEC setpoint value in Degrees Celcius
                camera.Features["SensorTemperatureSetpointSelector"].IntValue = 1;  // Activates the TEC (0 is off)
                IsOpen = true;
                return 0;
            }
            catch
            {
                sys.Shutdown();
                IsOpen = false;
                return -1;
            }
        }

        /// <summary>
        /// The camera and vimba should be closed before power is removed from the camera or windows is shut down.
        /// </summary>
        public void Close()
        {
            camera.Close();
            sys.Shutdown();
        }

        /// <summary>
        /// Instructs the camera to begin acquiring and sending images to the frame grabber.
        /// </summary>
        public void StartAcquisition()
        {
            if (!IsAcquiring)
            {
                camera.Features["AcquisitionStart"].RunCommand();   //Some features, such as AcquisitionStart and AcquisitionStop are commands without any member variables associated with them
                IsAcquiring = true;
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Updates the exposure time of the camera within certain limits determined by whether the camera is running at 30 fps (IsThirtyFPS) or 15 fps (!IsThirtyFPS) 
        /// </summary>
        /// <param name="IsThirtyFPS"></param>
        /// <param name="exposureTime"></param>
        /// <returns></returns>
        public int UpdateExposureTime(bool IsThirtyFPS, float exposureTime)
        {
            if (IsThirtyFPS)        // Checks for current frame rate to determine limits of exposure time
            {
                if (exposureTime > 33000)   // 30fps corresponds to 33000us of exposure
                {
                    exposureTime = 33000;
                }
                else if (exposureTime <= 0) // disallow negative exposures
                {
                    exposureTime = 33000;
                }
            }
            else
            {
                if (exposureTime > 66000)   // 15fps corresponds to 66000us of exposure
                {
                    exposureTime = 66000;
                }
                else if (exposureTime <= 0)
                {
                    exposureTime = 66000;
                }
            }

            camera.Features["ExposureTime"].FloatValue = exposureTime;  // Exposure time is a floating point value. For Vimba in C#, a double will not work, must be cast to float.
            return (int)exposureTime;
        }

        /// <summary>
        /// Updates the exposure time of the camera within certain limits determined by whether the camera is running at 30 fps (IsThirtyFPS) or 15 fps (!IsThirtyFPS)
        /// </summary>
        /// <param name="IsThirtyFPS"></param>
        /// <param name="exposureTime"></param>
        /// <returns></returns>
        public int UpdateExposureTime(bool IsThirtyFPS, int exposureTime)
        {
            float exposure = exposureTime;
            exposureTime = UpdateExposureTime(IsThirtyFPS, exposure);
            return exposureTime;
        }

        /// <summary>
        /// Toggles frame rate of the camera AND resets exposure time to the corresponding max
        /// </summary>
        /// <param name="IsThirtyFPS"></param>
        /// <returns></returns>
        public bool ToggleFramesPerSecond(bool IsThirtyFPS)
        {
            if (IsThirtyFPS)
            {
                IsThirtyFPS = false;
                UpdateExposureTime(IsThirtyFPS, 66000.0f);
                camera.Features["AcquisitionFrameRate"].FloatValue = 15.0f; // Frame rate is a floating point value. For Vimba in C#, a double must be cast to a float.
            }
            else
            {
                IsThirtyFPS = true;
                UpdateExposureTime(IsThirtyFPS, 33000.0f);
                camera.Features["AcquisitionFrameRate"].FloatValue = 30.0f;
            }

            return IsThirtyFPS;
        }

        /// <summary>
        /// Toggles the gain of the camera. High gain produces a brighter image.
        /// </summary>
        public void ToggleGain()
        {
            if (highGain)
            {
                camera.Features["SensorGain"].EnumValue = "Gain0";  // Gain is an enumeration with only two options: "Gain0" and "Gain1". "Gain0" is low gain, and "Gain1" is high gain
                highGain = false;                                   // Low gain corresponds to a higher number of electrons per pixel count in the ADC, producing a dimmer image.
            }
            else
            {
                camera.Features["SensorGain"].EnumValue = "Gain1";  // High gain corresponds to a lower number of electrons per pixel count in the ADC.
                highGain = true;
            }
        }

        /// <summary>
        /// Sets the VMB object's temperature variable to the current temperature of the imaging plane of the camera.
        /// </summary>
        public void UpdateTemperature()
        {
            temperature = camera.Features["DeviceTemperature"].FloatValue;  // DeviceTemperature is a non-modifiable (changed only by the camera itself) float value.
        }

    }
}
