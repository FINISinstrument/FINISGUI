using System;
using System.Collections.Generic;
using System.Text;

// Allows access to data types within the VectorNav .NET Library.
using VectorNav.Sensor;
using VectorNav.Math;
using VectorNav.Protocol.Uart;
using System.Threading;

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



        public string GetYPR()
        {
            if (vs.IsConnected)
            {
                var ymaa = vs.ReadYawPitchRollMagneticAccelerationAndAngularRates();
                return ymaa.YawPitchRoll.ToString();
            }
            return ("IMU NOT CONNECTED");
        }

        public string GetUpdatedYPR()
        {
            vs.WriteAsyncDataOutputType(AsciiAsync.VNGPS);
            var asyncType = vs.ReadAsyncDataOutputType();
            Console.WriteLine("ASCII Async Type: {0}", asyncType);
            //Form1.promptBox.Text = "hello";
            vs.AsyncPacketReceived += AsyncPacketReceived;
            Thread.Sleep(5000);
		    vs.AsyncPacketReceived -= AsyncPacketReceived;
            return "return";
        }


        /// <summary>
        /// This is our basic method for handling new asynchronous data packets
        /// received events. When this method is called by VnSensor, the packet has
        /// already been verified as valid and determined to be an asynchronous
        /// data packet. Howerver, some processing is required on the user side to
        /// make sure it is the expected type of asynchronous message so that it
        /// can be parsed correctly.
        /// </summary>
        private static void AsyncPacketReceived(object sender, PacketFoundEventArgs packetFoundEventArgs)
        {
            var packet = packetFoundEventArgs.FoundPacket;

            // Make sure we have an ASCII packet and not a binary packet.
            if (packet.Type != PacketType.Ascii)
            {
                Console.WriteLine ("Error - PacketType is not ASCII");
            }


            // Make sure we have a VNYPR data packet.
            if (packet.AsciiAsyncType != AsciiAsync.VNYPR)
            {
                Console.WriteLine ("Error- PacketAscii type is not VNYPR");
                
            }


            // We now need to parse out the yaw, pitch, roll data.
            vec3f ypr;
            packet.ParseVNYPR(out ypr);

            // Now print out the yaw, pitch, roll measurements.
            Console.WriteLine ("ASCII Async YPR: {0}" , ypr);
        }





    }


}
