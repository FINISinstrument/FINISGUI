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
        //const string sensorPort = "COM12";
        //const UInt32 sensorBaudrate = 115200;
        public bool ConnectSensor(string sensorPort, UInt32 sensorBaudrate = 115200)
        {
            var vs = new VnSensor();
            vs.Connect(sensorPort, sensorBaudrate);
            if (vs.IsConnected)
            {
                return true;
            }
            return false;
        }
        


        
        
   
    }
}
