using System;
using System.Collections.Generic;
using System.Text;

// Allows access to data types within the VectorNav .NET Library.
using VectorNav.Sensor;
using VectorNav.Math;
using VectorNav.Protocol.Uart;

namespace FinisGUI
{
    public class IMU
    {

        VnSensor vs;
        string sensorPort;
        uint sensorBaudrate;

        public IMU(string sensorPort, uint sensorBaudrate)
        {
            vs = new VnSensor();
            this.sensorPort = sensorPort;
            this.sensorBaudrate = sensorBaudrate;
        }

        ~IMU()
        {
            if (vs.IsConnected)
            {
                vs.Disconnect();
            }
            
        } 
        

        
        //const string sensorPort = "COM12";
        //const UInt32 sensorBaudrate = 115200;
        public void ConnectSensor()
        {
            if (!vs.IsConnected)
            {
                vs.Connect(sensorPort, sensorBaudrate);
            }
        }

        public bool isConnected()
        {
            if (vs.IsConnected)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool GetTime()
        {
            //vs.ReadGpsSolutionLla;

            return true;
        }

        public string GetYPR()
        {
            if (vs.IsConnected)
            {
                var ymaa = vs.ReadYawPitchRollMagneticAccelerationAndAngularRates();
                return ymaa.YawPitchRoll.ToString();
            }
            return ("IMU NOT CONNECTED");
        }
        


        
        
   
    }
}
