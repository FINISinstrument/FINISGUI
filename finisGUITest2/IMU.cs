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
        

        
        //const string sensorPort = "COM12";
        //const UInt32 sensorBaudrate = 115200;
        public bool ConnectSensor()
        {
            //var vs = new VnSensor();
            vs.Connect(sensorPort, sensorBaudrate);
            if (vs.IsConnected)
            {
                return true;
            }
            return false;
        }

        public bool GetTime()
        {
            //vs.ReadGpsSolutionLla;

            return true;
        }
        


        
        
   
    }
}
