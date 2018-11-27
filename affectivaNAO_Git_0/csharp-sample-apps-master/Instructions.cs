﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace csharp_sample_app
{
    public class Instructions
    {
        static byte magicN = 0x33; //from 33 to 34 and it works

        #region Connection
        public static byte[] connect0 = new byte[]
        {
            0x42 ,0xde ,
            0xad ,0x42 ,0x02 ,0x00 ,0x00 ,0x00 ,0x4f ,0x00 ,
            0x00 ,0x00 ,0x00 ,0x00 ,0x06 ,0x00 ,0x00 ,0x00 ,
            0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,
            0x00 ,0x00 ,0x03 ,0x00 ,0x00 ,0x00 ,0x12 ,0x00 ,
            0x00 ,0x00 ,0x43 ,0x6c ,0x69 ,0x65 ,0x6e ,0x74 ,
            0x53 ,0x65 ,0x72 ,0x76 ,0x65 ,0x72 ,0x53 ,0x6f ,
            0x63 ,0x6b ,0x65 ,0x74 ,0x01 ,0x00 ,0x00 ,0x00 ,
            0x62 ,0x01 ,0x0c ,0x00 ,0x00 ,0x00 ,0x4d ,0x65 ,
            0x73 ,0x73 ,0x61 ,0x67 ,0x65 ,0x46 ,0x6c ,0x61 ,
            0x67 ,0x73 ,0x01 ,0x00 ,0x00 ,0x00 ,0x62 ,0x01 ,
            0x0f ,0x00 ,0x00 ,0x00 ,0x4d ,0x65 ,0x74 ,0x61 ,
            0x4f ,0x62 ,0x6a ,0x65 ,0x63 ,0x74 ,0x43 ,0x61 ,
            0x63 ,0x68 ,0x65 ,0x01 ,0x00 ,0x00 ,0x00 ,0x62 ,
            0x01
        };
        public static byte[] connect1 = new byte[]
        {
            0x42 ,0xde ,
            0xad ,0x42 ,0x04 ,0x00 ,0x00 ,0x00 ,0x04 ,0x00 ,
            0x00 ,0x00 ,0x00 ,0x00 ,0x01 ,0x00 ,0x01 ,0x00 ,
            0x00 ,0x00 ,0x01 ,0x00 ,0x00 ,0x00 ,0x02 ,0x00 ,
            0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00
        };
        public static byte[] connect2 = new byte[]
        {
            0x42 ,0xde ,
            0xad ,0x42 ,0x08 ,0x00 ,0x00 ,0x00 ,0x10 ,0x00 ,
            0x00 ,0x00 ,0x00 ,0x00 ,0x01 ,0x00 ,0x01 ,0x00 ,
            0x00 ,0x00 ,0x01 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,
            0x00 ,0x00 ,0x01 ,0x00 ,0x00 ,0x00 ,0x6a ,0x00 ,
            0x00 ,0x00 ,0x0d ,0x00 ,0x00 ,0x00 ,0x6a ,0x00 ,
            0x00 ,0x00
        };
        public static byte[] connect3 = new byte[]
        {
            0x42 ,0xde ,
            0xad ,0x42 ,0x0a ,0x00 ,0x00 ,0x00 ,0x10 ,0x00 ,
            0x00 ,0x00 ,0x00 ,0x00 ,0x01 ,0x00 ,0x01 ,0x00 ,
            0x00 ,0x00 ,0x01 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,
            0x00 ,0x00 ,0x01 ,0x00 ,0x00 ,0x00 ,0x6b ,0x00 ,
            0x00 ,0x00 ,0x0e ,0x00 ,0x00 ,0x00 ,0x6b ,0x00 ,
            0x00 ,0x00
        };

        public static byte[] connect4 = new byte[]
        {
            0x42 ,0xde ,
            0xad ,0x42 ,0x0e ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,
            0x00 ,0x00 ,0x00 ,0x00 ,0x01 ,0x00 ,0x01 ,0x00 ,
            0x00 ,0x00 ,0x01 ,0x00 ,0x00 ,0x00 ,0x6c ,0x00 ,
            0x00 ,0x00
        };

        #endregion

        #region tts
        public static byte[] tts0 = new byte[]
        {
            0x42, 0xde,
            0xad, 0x42, 0x11, 0x00, 0x00, 0x00, 0x12, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x01, 0x00,
            0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x64, 0x00,
            0x00, 0x00, 0x0e, 0x00, 0x00, 0x00, 0x41, 0x4c,
            0x54, 0x65, 0x78, 0x74, 0x54, 0x6f, 0x53, 0x70,
            0x65, 0x65, 0x63, 0x68
        };

        public static byte[] tts1 = new byte[]
        {
            0x42 ,0xde ,
            0xad ,0x42 ,0x14 ,0x00 ,0x00 ,0x00 ,0x04 ,0x00 ,
            0x00 ,0x00 ,0x00 ,0x00 ,0x01 ,0x00 ,magicN ,0x00 , //this 33?
            0x00 ,0x00 ,0x01 ,0x00 ,0x00 ,0x00 ,0x02 ,0x00 ,
            0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00


        };

        private static byte[] template = new byte[]
            {

                0x42 ,0xde ,0xad ,0x42 ,0x17 ,0x00 ,0x00 ,0x00 ,
                0x18 ,//24
                0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x01 ,0x00 ,
                magicN ,//this number what the hell?
                0x00 ,
                0x00 ,0x00 ,0x01 ,0x00 ,0x00 ,0x00 ,0x71 ,0x00 ,0x00 ,
                0x00 ,
                0x14 ,//20
                0x00 ,0x00 ,0x00 ,
                0x63 ,0x61 ,0x6e ,0x20 ,0x49 ,0x20 ,0x68 ,0x61 ,0x76 ,
                0x65 ,0x20 ,0x61 ,0x20 ,0x62 ,0x61 ,0x6e ,0x61 ,0x6e ,
                0x61 ,0x3f

            };
        #endregion

        #region motion Head
        public static byte[] motion0 = new byte[]
            {
                0x42 ,0xde ,
                0xad ,0x42 ,0x11 ,0x00 ,0x00 ,0x00 ,0x0c ,0x00 ,
                0x00 ,0x00 ,0x00 ,0x00 ,0x01 ,0x00 ,0x01 ,0x00 ,
                0x00 ,0x00 ,0x01 ,0x00 ,0x00 ,0x00 ,0x64 ,0x00 ,
                0x00 ,0x00 ,0x08 ,0x00 ,0x00 ,0x00 ,0x41 ,0x4c ,
                0x4d ,0x6f ,0x74 ,0x69 ,0x6f ,0x6e
            };

        public static byte[] motion1 = new byte[]
            {
                0x42 ,0xde ,
                0xad ,0x42 ,0x14 ,0x00 ,0x00 ,0x00 ,0x04 ,0x00 ,
                0x00 ,0x00 ,0x00 ,0x00 ,0x01 ,0x00 ,0x15 ,0x00 , //this 15?
                0x00 ,0x00 ,0x01 ,0x00 ,0x00 ,0x00 ,0x02 ,0x00 ,
                0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00
            };

        private static byte[] templatStiffnessHeadYaw = new byte[]
            {
                0x42 ,0xde ,
                0xad ,0x42 ,0x17 ,0x00 ,0x00 ,0x00 ,0x22 ,0x00 ,
                0x00 ,0x00 ,0x00 ,0x00 ,0x01 ,0x00 ,0x15 ,0x00 , //15?
                0x00 ,0x00 ,0x01 ,0x00 ,0x00 ,0x00 ,0x7b ,0x00 ,
                0x00 ,0x00 ,0x01 ,0x00 ,0x00 ,0x00 ,0x73 ,0x07 ,
                0x00 ,0x00 ,0x00 ,

                0x48 ,0x65 ,0x61 ,0x64 ,0x59 ,0x61 ,0x77 , //HeadYaw
                //48 65 61 64 59 61 77
                0x01 ,0x00 ,0x00 ,0x00 ,
                0x66 ,
                0xcd ,0xcc ,0x4c ,0x3f ,    //stiffness 
                0x01 ,0x00 ,0x00 ,0x00 ,
                0x66 ,
                0x00 ,0x00 ,0x80 ,0x3f      //time
            };
        private static byte[] templateMoveHeadYaw = new byte[]
       {
            0x42 ,0xde ,
            0xad ,0x42 ,0x1a ,0x00 ,0x00 ,0x00 ,0x39 ,0x00 ,
            0x00 ,0x00 ,0x00 ,0x00 ,0x01 ,0x00 ,0x15 ,0x00 ,
            0x00 ,0x00 ,0x01 ,0x00 ,0x00 ,0x00 ,0x7f ,0x00 ,
            0x00 ,0x00 ,0x01 ,0x00 ,0x00 ,0x00 ,0x73 ,0x07 ,
            0x00 ,0x00 ,0x00 ,
            0x48 ,0x65 ,0x61 ,0x64 ,0x59 ,0x61 ,0x77 , //HeadYaw
            0x03 ,0x00 ,0x00 ,0x00 ,
            0x5b ,0x6d ,0x5d ,       //[m]
            0x01 ,0x00 ,0x00 ,0x00 ,
            0x01 ,0x00 ,0x00 ,0x00 ,0x66 ,
            0x9a ,0x99 ,0x99 ,0x3f , //angle
            0x03 ,0x00 ,0x00 ,0x00 ,
            0x5b ,0x6d ,0x5d ,       //[m]
            0x01 ,0x00 ,0x00 ,0x00 ,
            0x01 ,0x00 ,0x00 ,0x00 ,0x66 ,
            0x00 ,0x00 ,0x40 ,0x40 , //time
            0x00 // absolute true

       };
        private static byte[] templatStiffnessHeadPitch = new byte[]
            {
                0x42 ,0xde ,
                0xad ,0x42 ,0x17 ,0x00 ,0x00 ,0x00 ,0x24 ,0x00 , //22 el numero de bytes
                0x00 ,0x00 ,0x00 ,0x00 ,0x01 ,0x00 ,0x15 ,0x00 , //15?
                0x00 ,0x00 ,0x01 ,0x00 ,0x00 ,0x00 ,0x7b ,0x00 ,
                0x00 ,0x00 ,0x01 ,0x00 ,0x00 ,0x00 ,0x73 ,0x09 , // el tamanio del label
                0x00 ,0x00 ,0x00 ,
                //37
                0x48 ,0x65 ,0x61 ,0x64 ,0x50 ,0x69 ,0x74 ,0x63 ,0x68 , //HeadPitch
                //48 65 61 64 50 69 74 63 68
                0x01 ,0x00 ,0x00 ,0x00 ,
                0x66 ,
                0xcd ,0xcc ,0x4c ,0x3f ,    //stiffness 
                0x01 ,0x00 ,0x00 ,0x00 ,
                0x66 ,
                0x00 ,0x00 ,0x80 ,0x3f      //time
            };

        private static byte[] templateMoveHeadPitch = new byte[]
        {
            0x42 ,0xde ,
            0xad ,0x42 ,0x1a ,0x00 ,0x00 ,0x00 ,0x3b ,0x00 ,
            0x00 ,0x00 ,0x00 ,0x00 ,0x01 ,0x00 ,0x15 ,0x00 ,
            0x00 ,0x00 ,0x01 ,0x00 ,0x00 ,0x00 ,0x7f ,0x00 ,
            0x00 ,0x00 ,0x01 ,0x00 ,0x00 ,0x00 ,0x73 ,0x09 ,// el 9 es el tamanio de la palabra 
            0x00 ,0x00 ,0x00 ,
            0x48 ,0x65 ,0x61 ,0x64 ,0x50 ,0x69 ,0x74 ,0x63 ,0x68 , //HeadPitch
            0x03 ,0x00 ,0x00 ,0x00 ,
            0x5b ,0x6d ,0x5d ,       //[m]
            0x01 ,0x00 ,0x00 ,0x00 ,
            0x01 ,0x00 ,0x00 ,0x00 ,0x66 ,
            0x9a ,0x99 ,0x99 ,0x3f , //angle
            0x03 ,0x00 ,0x00 ,0x00 ,
            0x5b ,0x6d ,0x5d ,       //[m]
            0x01 ,0x00 ,0x00 ,0x00 ,
            0x01 ,0x00 ,0x00 ,0x00 ,0x66 ,
            0x00 ,0x00 ,0x40 ,0x40 , //time
            0x00 // absolute true

        };

        #endregion

        public static void Read(NetworkStream stream)
        {
            if (stream.CanRead)
            {
                byte[] myReadBuffer = new byte[1024];
                StringBuilder myCompleteMessage = new StringBuilder();
                int numberOfBytesRead = 0;

                // Incoming message may be larger than the buffer size. 
                do
                {
                    numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);

                    myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));

                }
                while (stream.DataAvailable);

                // Print out the received message to the console.
                Console.WriteLine("You received the following message : " +
                                             myCompleteMessage.Length);
            }
            else
            {
                Console.WriteLine("Sorry.  You cannot read from this NetworkStream.");
            }
        }

        public static byte[] askVideo = new byte[]
        {
            0x42 ,0xde ,
            0xad ,0x42 ,0xa6 ,0x00 ,0x00 ,0x00 ,0x12 ,0x00 ,
            0x00 ,0x00 ,0x00 ,0x00 ,0x02 , //this number one I do not have an idea, before was 2
            0x00 ,0x1d ,0x00 ,0x00 ,0x00 ,0x01 ,0x00 ,0x00 ,0x00 ,0xa2 ,0x00 , //method = 31.1.162 
            0x00 ,0x00 ,0x0e ,0x00 ,0x00 ,0x00 ,0x43 ,0x61 ,0x6d ,0x65 ,0x72 ,0x61 ,0x56 ,0x69 ,0x65 ,0x77 ,
            0x65 ,0x72 ,0x5f ,0x30
        };

        public static byte[] askVideo2 = new byte[]
       {
            0x42 ,0xde ,
            0xad ,0x42 ,0xa6 ,0x00 ,0x00 ,0x00 ,0x12 ,0x00 ,
            0x00 ,0x00 ,0x00 ,0x00 ,0x01 , //this number one I do not have an idea, before was 2
            0x00 ,0x1d ,0x00 ,0x00 ,0x00 ,0x01 ,0x00 ,0x00 ,0x00 ,0xa2 ,0x00 , //method = 31.1.162 
            0x00 ,0x00 ,0x0e ,0x00 ,0x00 ,0x00 ,0x43 ,0x61 ,0x6d ,0x65 ,0x72 ,0x61 ,0x56 ,0x69 ,0x65 ,0x77 ,
            0x65 ,0x72 ,0x5f ,0x32
       };

        public static byte[] askVideo2C = new byte[]
       {
            //0x42 ,0xde ,
            //0xad ,0x42 ,0xa6 ,0x01 ,0x00 ,0x00 ,0x12 ,0x00 , //a6 before 9d magic number
            //     //0xad ,0x42 ,0xa6 ,0x00 ,0x00 ,0x00 ,0x12 ,0x00 , //a6 before 9d magic number or 0x60
            //0x00 ,0x00 ,0x00 ,0x00 ,0x01 , //this number one I do not have an idea, before was 2
            //0x00 ,0x1d ,0x00 ,0x00 ,0x00 ,0x01 ,0x00 ,0x00 ,0x00 ,0xa2 ,0x00 , //method = 31.1.162 
            //0x00 ,0x00 ,0x0e ,0x00 ,0x00 ,0x00 ,0x43 ,0x61 ,0x6d ,0x65 ,0x72 ,0x61 ,0x56 ,0x69 ,0x65 ,0x77 ,
            //0x65 ,0x72 ,0x5f ,0x36 //what? 30 it should be 36 (Camera_viewer6)

            0x42 ,0xde ,
            0xad ,0x42 ,0xa6 ,0x30 ,0x00 ,0x00 ,0x12 ,0x00 , //a6 before 9d magic number
            0x00 ,0x00 ,0x00 ,0x00 ,0x01 , //this number one I do not have an idea, before was 2
            0x00 ,0x1d ,0x00 ,0x00 ,0x00 ,0x01 ,0x00 ,0x00 ,0x00 ,0xa2 ,0x00 , //method = 31.1.162 
            0x00 ,0x00 ,0x0e ,0x00 ,0x00 ,0x00 ,0x43 ,0x61 ,0x6d ,0x65 ,0x72 ,0x61 ,0x56 ,0x69 ,0x65 ,0x77 ,
            0x65 ,0x72 ,0x5f ,0x30 //what? 30 it should be 36 (Camera_viewer6)
       };

        public static byte[] askVideoMod(byte value, byte value2)
        {
            byte[] instruction = new byte[] {
            0x42 ,0xde ,
            0xad ,0x42 ,value ,value2 ,0x00 ,0x00 ,0x12 ,0x00 , //a6 before 9d magic number
            0x00 ,0x00 ,0x00 ,0x00 ,0x01 , //this number one I do not have an idea, before was 2
            0x00 ,0x1d ,0x00 ,0x00 ,0x00 ,0x01 ,0x00 ,0x00 ,0x00 ,0xa2 ,0x00 , //method = 31.1.162 
            0x00 ,0x00 ,0x0e ,0x00 ,0x00 ,0x00 ,0x43 ,0x61 ,0x6d ,0x65 ,0x72 ,0x61 ,0x56 ,0x69 ,0x65 ,0x77 ,
            0x65 ,0x72 ,0x5f ,0x31 };//what? 30 it should be 36 (Camera_viewer6)

            return instruction;
        }

       

        public static byte[] moveToTemplate = new byte[]//40
            {
                0x42 ,0xde ,
                0xad ,0x42 ,0x1a ,0x00 ,0x00 ,0x00 ,0x0c ,0x00 ,
                0x00 ,0x00 ,0x00 ,0x00 ,0x01 ,0x00 ,0x15 ,0x00 ,
                0x00 ,0x00 ,0x01 ,0x00 ,0x00 ,0x00 ,0x8f ,0x00 ,
                0x00 ,0x00 ,
                0xcd ,0xcc ,0x4c ,0xbe , //x
                0x00 ,0x00 ,0x00 ,0x00 , //y
                0x00 ,0x00 ,0x00 ,0x00 //theta
            };

        public static byte[] Message(string text)
        {
            byte[] textBytes = Encoding.ASCII.GetBytes(text);



            byte[] message = new byte[29 + textBytes.Length + 3];
            for (int i = 0; i < 29; i++)
            {
                message[i] = template[i];
            }
            message[8] = (byte)(textBytes.Length + 4);
            message[28] = (byte)(textBytes.Length);
            for (int i = 0; i < textBytes.Length; i++)
            {
                message[i + 32] = textBytes[i];
            }

            return message;
        }

        public static byte[] StiffnessHeadYaw(float stiffness, float time)
        {
            byte[] message = (byte[])templatStiffnessHeadYaw.Clone();

            //49
            byte[] byteArray = BitConverter.GetBytes(stiffness);
            message[49] = byteArray[0];
            message[50] = byteArray[1];
            message[51] = byteArray[2];
            message[52] = byteArray[3];
            //58
            byteArray = BitConverter.GetBytes(time);
            message[58] = byteArray[0];
            message[59] = byteArray[1];
            message[60] = byteArray[2];
            message[61] = byteArray[3];
            return message;
        }

        public static byte[] MotionHeadYaw(float angle, float time, bool absolute)
        {
            byte[] message = (byte[])templateMoveHeadYaw.Clone();

            //60
            byte[] byteArray = BitConverter.GetBytes(angle);
            message[60] = byteArray[0];
            message[61] = byteArray[1];
            message[62] = byteArray[2];
            message[63] = byteArray[3];
            //80
            byteArray = BitConverter.GetBytes(time);
            message[80] = byteArray[0];
            message[81] = byteArray[1];
            message[82] = byteArray[2];
            message[83] = byteArray[3];

            if (absolute)
                message[84] = 0x01;

            return message;
        }

        public static byte[] StiffnessHeadPitch(float stiffness, float time)
        {
            byte[] message = (byte[])templatStiffnessHeadPitch.Clone();

            //51
            byte[] byteArray = BitConverter.GetBytes(stiffness);
            message[51] = byteArray[0];
            message[52] = byteArray[1];
            message[53] = byteArray[2];
            message[54] = byteArray[3];
            //60
            byteArray = BitConverter.GetBytes(time);
            message[60] = byteArray[0];
            message[61] = byteArray[1];
            message[62] = byteArray[2];
            message[63] = byteArray[3];
            return message;
        }

        public static byte[] MotionHeadPitch(float angle, float time, bool absolute)
        {
            byte[] message = (byte[])templateMoveHeadPitch.Clone();

            //62
            byte[] byteArray = BitConverter.GetBytes(angle);
            message[62] = byteArray[0];
            message[63] = byteArray[1];
            message[64] = byteArray[2];
            message[65] = byteArray[3];
            //82
            byteArray = BitConverter.GetBytes(time);
            message[82] = byteArray[0];
            message[83] = byteArray[1];
            message[84] = byteArray[2];
            message[85] = byteArray[3];

            if (absolute)
                message[86] = 0x01;

            return message;
        }

        

        public static byte[] MoveTo(float x, float y, float theta)
        {
            byte[] message = (byte[])moveToTemplate.Clone();


            byte[] byteArray = BitConverter.GetBytes(x);
            message[28] = byteArray[0];
            message[29] = byteArray[1];
            message[30] = byteArray[2];
            message[31] = byteArray[3];
            byteArray = BitConverter.GetBytes(y);
            message[32] = byteArray[0];
            message[33] = byteArray[1];
            message[34] = byteArray[2];
            message[35] = byteArray[3];
            byteArray = BitConverter.GetBytes(theta);
            message[36] = byteArray[0];
            message[37] = byteArray[1];
            message[38] = byteArray[2];
            message[39] = byteArray[3];

            return message;
        }


        public static byte[] video0 = new byte[] {
             0x42,0xde,0xad,0x42,0x02,0x00,0x00,0x00,0x4f,0x00
        ,0x00,0x00,0x00,0x00,0x06,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
        ,0x00,0x00,0x03,0x00,0x00,0x00,0x12,0x00,0x00,0x00,0x43,0x6c,0x69,0x65,0x6e,0x74
        ,0x53,0x65,0x72,0x76,0x65,0x72,0x53,0x6f,0x63,0x6b,0x65,0x74,0x01,0x00,0x00,0x00
        ,0x62,0x01,0x0c,0x00,0x00,0x00,0x4d,0x65,0x73,0x73,0x61,0x67,0x65,0x46,0x6c,0x61
        ,0x67,0x73,0x01,0x00,0x00,0x00,0x62,0x01,0x0f,0x00,0x00,0x00,0x4d,0x65,0x74,0x61
        ,0x4f,0x62,0x6a,0x65,0x63,0x74,0x43,0x61,0x63,0x68,0x65,0x01,0x00,0x00,0x00,0x62
        ,0x01
        };

        public static byte[] video1 = new byte[]
        {
            0x42,0xde,0xad,0x42,0x03,0x00,0x00,0x00,0x04,0x00
        ,0x00,0x00,0x00,0x00,0x01,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00
        ,0x00,0x00,0x00,0x00,0x00,0x00
        };

        public static byte[] video2 = new byte[]
            {
                0x42,0xde,0xad,0x42,0x08,0x00,0x00,0x00,0x10,0x00
                ,0x00,0x00,0x00,0x00,0x01,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x00,0x00
                ,0x00,0x00,0x01,0x00,0x00,0x00,0x6a,0x00,0x00,0x00,0x0d,0x00,0x00,0x00,0x6a,0x00
                ,0x00,0x00
            };

        public static byte[] video3 = new byte[]
            {
                0x42,0xde,0xad,0x42,0x09,0x00,0x00,0x00,0x10,0x00
                ,0x00,0x00,0x00,0x00,0x01,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x00,0x00
                ,0x00,0x00,0x01,0x00,0x00,0x00,0x6b,0x00,0x00,0x00,0x0e,0x00,0x00,0x00,0x6b,0x00
                ,0x00,0x00
            };

        public static byte[] video4 = new byte[]
            {
             0x42,0xde,0xad,0x42,0x0e,0x00,0x00,0x00,0x00,0x00
            ,0x00,0x00,0x00,0x00,0x01,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x6c,0x00
            ,0x00,0x00
            };

        public static byte[] video5 = new byte[]
            {
                0x42,0xde,0xad,0x42,0x11,0x00,0x00,0x00,0x11,0x00
            ,0x00,0x00,0x00,0x00,0x01,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x64,0x00
            ,0x00,0x00,0x0d,0x00,0x00,0x00,0x41,0x4c,0x56,0x69,0x64,0x65,0x6f,0x44,0x65,0x76
            ,0x69,0x63,0x65
            };

        public static byte[] video6 = new byte[]
            {
                0x42,0xde,0xad,0x42,0x14,0x00,0x00,0x00,0x04,0x00
            ,0x00,0x00,0x00,0x00,0x01,0x00,0x1d,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00
            ,0x00,0x00,0x00,0x00,0x00,0x00
            };

        public static byte[] video7 = new byte[]
            {
               0x42,0xde,0xad,0x42,0x17,0x00,0x00,0x00,0xc0,0x00
,0x00,0x00,0x00,0x00,0x01,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x66,0x00
,0x00,0x00,0x0c,0x00,0x00,0x00,0x43,0x61,0x6d,0x65,0x72,0x61,0x56,0x69,0x65,0x77
,0x65,0x72,0x00,0x00,0x00,0x00,0x24,0x00,0x00,0x00,0x38,0x39,0x37,0x62,0x32,0x61
,0x62,0x31,0x2d,0x38,0x30,0x35,0x39,0x2d,0x34,0x66,0x39,0x61,0x2d,0x62,0x36,0x34
,0x30,0x2d,0x36,0x30,0x36,0x34,0x34,0x36,0x30,0x36,0x32,0x33,0x62,0x66,0xa0,0x2b
,0x00,0x00,0x03,0x00,0x00,0x00,0x18,0x00,0x00,0x00,0x74,0x63,0x70,0x3a,0x2f,0x2f
,0x31,0x30,0x30,0x2e,0x37,0x30,0x2e,0x31,0x33,0x2e,0x31,0x30,0x3a,0x35,0x30,0x32
,0x37,0x35,0x1b,0x00,0x00,0x00,0x74,0x63,0x70,0x3a,0x2f,0x2f,0x31,0x36,0x39,0x2e
,0x32,0x35,0x34,0x2e,0x31,0x32,0x33,0x2e,0x31,0x34,0x31,0x3a,0x35,0x30,0x32,0x37
,0x35,0x15,0x00,0x00,0x00,0x74,0x63,0x70,0x3a,0x2f,0x2f,0x31,0x32,0x37,0x2e,0x30
,0x2e,0x30,0x2e,0x31,0x3a,0x35,0x30,0x32,0x37,0x35,0x24,0x00,0x00,0x00,0x34,0x32
,0x65,0x31,0x66,0x33,0x30,0x31,0x2d,0x37,0x61,0x33,0x65,0x2d,0x34,0x66,0x34,0x33
,0x2d,0x61,0x64,0x62,0x33,0x2d,0x61,0x65,0x62,0x38,0x61,0x39,0x37,0x65,0x37,0x34
,0x34,0x37
            };

        public static byte[] video8 = new byte[]
            {
                0x42,0xde,0xad,0x42,0x1a,0x00,0x00,0x00,0x04,0x00
,0x00,0x00,0x00,0x00,0x01,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x68,0x00
,0x00,0x00,0xc7,0x06,0x00,0x00
            };

        public static byte[] video9 = new byte[]
            {
                0x42,0xde,0xad,0x42,0x1e,0x00,0x00,0x00,0x1c,0x00
,0x00,0x00,0x00,0x00,0x01,0x00,0x1d,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0xbf,0x00
,0x00,0x00,0x0c,0x00,0x00,0x00,0x43,0x61,0x6d,0x65,0x72,0x61,0x56,0x69,0x65,0x77
,0x65,0x72,0x00,0x00,0x00,0x00,0x09,0x00,0x00,0x00,0x05,0x00,0x00,0x00
            };

        public static byte[] video10 = new byte[]
            {
                0x42,0xde,0xad,0x42,0x7e,0x00,0x00,0x00,0x16,0x00
,0x00,0x00,0x00,0x00,0x01,0x00,0x1d,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x95,0x00
,0x00,0x00,0x0e,0x00,0x00,0x00,0x43,0x61,0x6d,0x65,0x72,0x61,0x56,0x69,0x65,0x77
,0x65,0x72,0x5f,0x31,0x01,0x00,0x00,0x00
            };
    }
}
