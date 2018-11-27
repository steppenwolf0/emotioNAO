using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

namespace CameraCapture
{
    public partial class naoControls : Form
    {
        float headPitch = 0;
        float maxHeadPitch = 0.25f;
        float minHeadPitch = -0.5f;

        float headYaw = 0;
        float maxheadYaw = 1.25f;
        float minheadYaw = -1.25f;


        public string ip = "169.254.175.171";

        int port = 9559;

        TcpClient client;

        NetworkStream stream;

        public naoControls()
        {
            InitializeComponent();
            client = new TcpClient(ip, port);
            stream = client.GetStream();
        }

       

       

        private void headUp_Click(object sender, EventArgs e)
        {
            upHead();
        }

        private void headDown_Click(object sender, EventArgs e)
        {
            downHead();
        }

        private void headReset_Click(object sender, EventArgs e)
        {
            centerHead();
        }

        private void headLeft_Click(object sender, EventArgs e)
        {
            leftHead();
        }

        private void headRight_Click(object sender, EventArgs e)
        {
            rightHead();
        }


        private void centerHead()
        {
            try
            {


                TcpClient client = new TcpClient(ip, 9559);

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(Instructions.connect0, 0, Instructions.connect0.Length);
                stream.Write(Instructions.connect1, 0, Instructions.connect1.Length);

                Instructions.Read(stream);
                Instructions.Read(stream);

                stream.Write(Instructions.connect2, 0, Instructions.connect2.Length);
                Instructions.Read(stream);
                stream.Write(Instructions.connect3, 0, Instructions.connect3.Length);
                Instructions.Read(stream);
                stream.Write(Instructions.connect4, 0, Instructions.connect4.Length);
                Instructions.Read(stream);

                stream.Write(Instructions.motion0, 0, Instructions.motion0.Length);
                stream.Write(Instructions.motion1, 0, Instructions.motion1.Length);

                byte[] stiffness = Instructions.StiffnessHeadPitch(0.5f, 1.0f);

                stream.Write(stiffness, 0, stiffness.Length);
                Instructions.Read(stream);
                byte[] movement = Instructions.MotionHeadPitch(0.0f, 1.0f, true);
                stream.Write(movement, 0, movement.Length);
                Instructions.Read(stream);


                headPitch = 0.0f;




                stiffness = Instructions.StiffnessHeadPitch(0.0f, 1.0f);

                stream.Write(stiffness, 0, stiffness.Length);
                Instructions.Read(stream);

                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException exc)
            {
                Console.WriteLine("ArgumentNullException: {0}", exc);
            }
            catch (SocketException exc)
            {
                Console.WriteLine("SocketException: {0}", exc);
            }

            try
            {


                TcpClient client = new TcpClient(ip, 9559);

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(Instructions.connect0, 0, Instructions.connect0.Length);
                stream.Write(Instructions.connect1, 0, Instructions.connect1.Length);

                Instructions.Read(stream);
                Instructions.Read(stream);

                stream.Write(Instructions.connect2, 0, Instructions.connect2.Length);
                Instructions.Read(stream);
                stream.Write(Instructions.connect3, 0, Instructions.connect3.Length);
                Instructions.Read(stream);
                stream.Write(Instructions.connect4, 0, Instructions.connect4.Length);
                Instructions.Read(stream);

                stream.Write(Instructions.motion0, 0, Instructions.motion0.Length);
                stream.Write(Instructions.motion1, 0, Instructions.motion1.Length);

                byte[] stiffness = Instructions.StiffnessHeadYaw(0.5f, 1.0f);

                stream.Write(stiffness, 0, stiffness.Length);
                Instructions.Read(stream);
                byte[] movement = Instructions.MotionHeadYaw(0.0f, 1.0f, true);
                headYaw = 0.0f;
                stream.Write(movement, 0, movement.Length);
                Instructions.Read(stream);
                stiffness = Instructions.StiffnessHeadYaw(0.0f, 1.0f);

                stream.Write(stiffness, 0, stiffness.Length);
                Instructions.Read(stream);

                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException exc)
            {
                Console.WriteLine("ArgumentNullException: {0}", exc);
            }
            catch (SocketException exc)
            {
                Console.WriteLine("SocketException: {0}", exc);
            }
        }

        private void leftHead()
        {
            try
            {


                TcpClient client = new TcpClient(ip, 9559);

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 

                stream.Write(Instructions.connect0, 0, Instructions.connect0.Length);

                stream.Write(Instructions.connect1, 0, Instructions.connect1.Length);

                Instructions.Read(stream);
                Instructions.Read(stream);

                stream.Write(Instructions.connect2, 0, Instructions.connect2.Length);
                Instructions.Read(stream);
                stream.Write(Instructions.connect3, 0, Instructions.connect3.Length);
                Instructions.Read(stream);
                stream.Write(Instructions.connect4, 0, Instructions.connect4.Length);
                Instructions.Read(stream);

                stream.Write(Instructions.motion0, 0, Instructions.motion0.Length);
                stream.Write(Instructions.motion1, 0, Instructions.motion1.Length);

                byte[] stiffness = Instructions.StiffnessHeadYaw(0.5f, 1.0f);

                stream.Write(stiffness, 0, stiffness.Length);
                Instructions.Read(stream);

                headYaw += 0.0625f;
                if (headYaw > maxheadYaw)
                    headYaw = maxheadYaw;

                byte[] movement = Instructions.MotionHeadYaw(headYaw, 1.0f, true);

                stream.Write(movement, 0, movement.Length);
                Instructions.Read(stream);
                stiffness = Instructions.StiffnessHeadYaw(0.0f, 1.0f);

                stream.Write(stiffness, 0, stiffness.Length);
                Instructions.Read(stream);

                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException exc)
            {
                Console.WriteLine("ArgumentNullException: {0}", exc);
            }
            catch (SocketException exc)
            {
                Console.WriteLine("SocketException: {0}", exc);
            }
        }

        private void rightHead()
        {
            try
            {


                TcpClient client = new TcpClient(ip, 9559);

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(Instructions.connect0, 0, Instructions.connect0.Length);
                stream.Write(Instructions.connect1, 0, Instructions.connect1.Length);

                Instructions.Read(stream);
                Instructions.Read(stream);

                stream.Write(Instructions.connect2, 0, Instructions.connect2.Length);
                Instructions.Read(stream);
                stream.Write(Instructions.connect3, 0, Instructions.connect3.Length);
                Instructions.Read(stream);
                stream.Write(Instructions.connect4, 0, Instructions.connect4.Length);
                Instructions.Read(stream);

                stream.Write(Instructions.motion0, 0, Instructions.motion0.Length);
                stream.Write(Instructions.motion1, 0, Instructions.motion1.Length);

                byte[] stiffness = Instructions.StiffnessHeadYaw(0.8f, 1.0f);

                stream.Write(stiffness, 0, stiffness.Length);
                Instructions.Read(stream);

                headYaw -= 0.0625f;
                if (headYaw < minheadYaw)
                    headYaw = minheadYaw;

                byte[] movement = Instructions.MotionHeadYaw(headYaw, 1.0f, true);

                stream.Write(movement, 0, movement.Length);
                Instructions.Read(stream);
                stiffness = Instructions.StiffnessHeadYaw(0.0f, 1.0f);

                stream.Write(stiffness, 0, stiffness.Length);
                Instructions.Read(stream);

                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException exc)
            {
                Console.WriteLine("ArgumentNullException: {0}", exc);
            }
            catch (SocketException exc)
            {
                Console.WriteLine("SocketException: {0}", exc);
            }
        }

        private void upHead()
        {
            try
            {


                TcpClient client = new TcpClient(ip, 9559);

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(Instructions.connect0, 0, Instructions.connect0.Length);
                stream.Write(Instructions.connect1, 0, Instructions.connect1.Length);

                Instructions.Read(stream);
                Instructions.Read(stream);

                stream.Write(Instructions.connect2, 0, Instructions.connect2.Length);
                Instructions.Read(stream);
                stream.Write(Instructions.connect3, 0, Instructions.connect3.Length);
                Instructions.Read(stream);
                stream.Write(Instructions.connect4, 0, Instructions.connect4.Length);
                Instructions.Read(stream);

                stream.Write(Instructions.motion0, 0, Instructions.motion0.Length);
                stream.Write(Instructions.motion1, 0, Instructions.motion1.Length);

                byte[] stiffness = Instructions.StiffnessHeadPitch(0.5f, 1.0f);

                stream.Write(stiffness, 0, stiffness.Length);
                Instructions.Read(stream);

                headPitch -= 0.0625f;
                if (headPitch < minHeadPitch)
                    headPitch = minHeadPitch;

                byte[] movement = Instructions.MotionHeadPitch(headPitch, 1.0f, true);

                stream.Write(movement, 0, movement.Length);
                Instructions.Read(stream);
                stiffness = Instructions.StiffnessHeadPitch(0.0f, 1.0f);

                stream.Write(stiffness, 0, stiffness.Length);
                Instructions.Read(stream);

                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException exc)
            {
                Console.WriteLine("ArgumentNullException: {0}", exc);
            }
            catch (SocketException exc)
            {
                Console.WriteLine("SocketException: {0}", exc);
            }
        }

        private void downHead()
        {
            try
            {


                TcpClient client = new TcpClient(ip, 9559);

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(Instructions.connect0, 0, Instructions.connect0.Length);
                stream.Write(Instructions.connect1, 0, Instructions.connect1.Length);

                Instructions.Read(stream);
                Instructions.Read(stream);

                stream.Write(Instructions.connect2, 0, Instructions.connect2.Length);
                Instructions.Read(stream);
                stream.Write(Instructions.connect3, 0, Instructions.connect3.Length);
                Instructions.Read(stream);
                stream.Write(Instructions.connect4, 0, Instructions.connect4.Length);
                Instructions.Read(stream);

                stream.Write(Instructions.motion0, 0, Instructions.motion0.Length);
                stream.Write(Instructions.motion1, 0, Instructions.motion1.Length);

                byte[] stiffness = Instructions.StiffnessHeadPitch(0.5f, 1.0f);

                stream.Write(stiffness, 0, stiffness.Length);
                Instructions.Read(stream);

                headPitch += 0.0625f;
                if (headPitch > maxHeadPitch)
                    headPitch = maxHeadPitch;

                byte[] movement = Instructions.MotionHeadPitch(headPitch, 1.0f, true);

                stream.Write(movement, 0, movement.Length);
                Instructions.Read(stream);
                stiffness = Instructions.StiffnessHeadPitch(0.0f, 1.0f);

                stream.Write(stiffness, 0, stiffness.Length);
                Instructions.Read(stream);

                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException exc)
            {
                Console.WriteLine("ArgumentNullException: {0}", exc);
            }
            catch (SocketException exc)
            {
                Console.WriteLine("SocketException: {0}", exc);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {


            float x = 0.5f;
            float y = 0.0f;
            float theta = 0.0f;

            movePosition(x, y, theta);
        }

        private void movePosition(float x, float y, float theta)
        {


            byte[] stiffnessEq1Mod = (byte[])stiffnessEq1.Clone();
            stiffnessEq1Mod[4] = getCount();
            stream.Write(stiffnessEq1Mod, 0, stiffnessEq1Mod.Length);

            readFixed(stream, 28);

            byte[] movementMod = movementBytes();
            movementMod[4] = getCount();
            stream.Write(movementMod, 0, movementMod.Length);

            readFixed(stream, 28);

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

            message[4] = getCount();
            stream.Write(message, 0, message.Length);

            readFixed(stream, 28);


        }

        byte[] stiffnessEq1 = new byte[] //276

                {
                    0x42,0xde,0xad,0x42,0x17,0x00,0x00,0x00,0xf8,0x00
    ,0x00,0x00,0x00,0x00,0x01,0x00,0x15,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x7c,0x00
    ,0x00,0x00,0x03,0x00,0x00,0x00,0x5b,0x6d,0x5d,0x08,0x00,0x00,0x00,0x01,0x00,0x00
    ,0x00,0x73,0x0e,0x00,0x00,0x00,0x4c,0x53,0x68,0x6f,0x75,0x6c,0x64,0x65,0x72,0x50
    ,0x69,0x74,0x63,0x68,0x01,0x00,0x00,0x00,0x73,0x0e,0x00,0x00,0x00,0x52,0x53,0x68
    ,0x6f,0x75,0x6c,0x64,0x65,0x72,0x50,0x69,0x74,0x63,0x68,0x01,0x00,0x00,0x00,0x73
    ,0x0d,0x00,0x00,0x00,0x4c,0x53,0x68,0x6f,0x75,0x6c,0x64,0x65,0x72,0x52,0x6f,0x6c
    ,0x6c,0x01,0x00,0x00,0x00,0x73,0x0d,0x00,0x00,0x00,0x52,0x53,0x68,0x6f,0x75,0x6c
    ,0x64,0x65,0x72,0x52,0x6f,0x6c,0x6c,0x01,0x00,0x00,0x00,0x73,0x09,0x00,0x00,0x00
    ,0x4c,0x45,0x6c,0x62,0x6f,0x77,0x59,0x61,0x77,0x01,0x00,0x00,0x00,0x73,0x09,0x00
    ,0x00,0x00,0x52,0x45,0x6c,0x62,0x6f,0x77,0x59,0x61,0x77,0x01,0x00,0x00,0x00,0x73
    ,0x05,0x00,0x00,0x00,0x4c,0x48,0x61,0x6e,0x64,0x01,0x00,0x00,0x00,0x73,0x05,0x00
    ,0x00,0x00,0x52,0x48,0x61,0x6e,0x64,0x03,0x00,0x00,0x00,
                    0x5b,0x6d,0x5d, //[m]
                    0x08,0x00,0x00,0x00,
                    0x01,0x00,0x00,0x00,0x66,
                    0x00,0x00,0x80,0x3f, //1
                    0x01,0x00,0x00,0x00,0x66,
                    0x00,0x00,0x80,0x3f, //1
                    0x01,0x00,0x00,0x00,0x66,
                    0x00,0x00,0x80,0x3f, //1
                    0x01,0x00,0x00,0x00,0x66,
                    0x00,0x00,0x80,0x3f, //1
                    0x01,0x00,0x00,0x00,0x66,
                    0x00,0x00,0x80,0x3f, //1
                    0x01,0x00,0x00,0x00,0x66,
                    0x00,0x00,0x80,0x3f, //1
                    0x01,0x00,0x00,0x00,0x66,
                    0x00,0x00,0x80,0x3f, //1
                    0x01,0x00,0x00,0x00,0x66,
                    0x00,0x00,0x80,0x3f //1
                };

        public static void readFixed(NetworkStream stream, int size)
        {
            int count = 0;

            do
            {
                if (stream.CanRead)
                {
                    byte[] myReadBuffer = new byte[1460];
                    StringBuilder myCompleteMessage = new StringBuilder();
                    int numberOfBytesRead = 0;

                    // Incoming message may be larger than the buffer size. 
                    do
                    {
                        numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);

                        myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));


                    }
                    while (stream.DataAvailable);
                    Console.WriteLine("You received the following message : " +
                                                 myCompleteMessage.Length);
                    count += myCompleteMessage.Length;
                }
                else
                {
                    Console.WriteLine("Sorry.  You cannot read from this NetworkStream.");
                }
            } while (count < size);

            Console.WriteLine(count);
        }

        byte[] moveToTemplate = new byte[]//40
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

        private byte[] movementBytes()
        {

            byte[] mov = (byte[])movement.Clone();

            byte[] value;
            value = BitConverter.GetBytes(LShoulderPitch);
            for (int i = 0; i < 4; i++)
                mov[209 + i] = value[i];
            value = BitConverter.GetBytes(RShoulderPitch);
            for (int i = 0; i < 4; i++)
                mov[218 + i] = value[i];
            value = BitConverter.GetBytes(LShoulderRoll);
            for (int i = 0; i < 4; i++)
                mov[227 + i] = value[i];
            value = BitConverter.GetBytes(RShoulderRoll);
            for (int i = 0; i < 4; i++)
                mov[236 + i] = value[i];


            value = BitConverter.GetBytes(LElbowYaw);
            for (int i = 0; i < 4; i++)
                mov[245 + i] = value[i];
            value = BitConverter.GetBytes(RElbowYaw);
            for (int i = 0; i < 4; i++)
                mov[254 + i] = value[i];
            value = BitConverter.GetBytes(LHand);
            for (int i = 0; i < 4; i++)
                mov[263 + i] = value[i];
            value = BitConverter.GetBytes(RHand);
            for (int i = 0; i < 4; i++)
                mov[272 + i] = value[i];

            value = BitConverter.GetBytes(speed);
            for (int i = 0; i < 4; i++)
                mov[276 + i] = value[i];
            return mov;

        }

        float LShoulderPitch = 1.6f;
        float RShoulderPitch = 1.6f;
        float LShoulderRoll = -0.3f;
        float RShoulderRoll = 0.3f;

        float LElbowYaw = -2.0f;
        float RElbowYaw = 2.0f;
        float LHand = 0.0f;
        float RHand = 0.0f;
        float speed = 0.1f;

        byte[] movement = new byte[] //280
            {
                0x42,0xde,0xad,0x42,0x1a,0x00,0x00,0x00,0xfc,0x00,
                0x00,0x00,0x00,0x00,0x01,0x00,0x15,0x00,0x00,0x00,
                0x01,0x00,0x00,0x00,0x80,0x00,0x00,0x00,0x03,0x00,
                0x00,0x00,0x5b,0x6d,0x5d,0x08,0x00,0x00,0x00,0x01,
                0x00,0x00,0x00,0x73,0x0e,0x00,0x00,0x00,0x4c,0x53,
                0x68,0x6f,0x75,0x6c,0x64,0x65,0x72,0x50,0x69,0x74,
                0x63,0x68,0x01,0x00,0x00,0x00,0x73,0x0e,0x00,0x00,
                0x00,0x52,0x53,0x68,0x6f,0x75,0x6c,0x64,0x65,0x72,
                0x50,0x69,0x74,0x63,0x68,0x01,0x00,0x00,0x00,0x73,
                0x0d,0x00,0x00,0x00,0x4c,0x53,0x68,0x6f,0x75,0x6c,
                0x64,0x65,0x72,0x52,0x6f,0x6c,0x6c,0x01,0x00,0x00,
                0x00,0x73,0x0d,0x00,0x00,0x00,0x52,0x53,0x68,0x6f,
                0x75,0x6c,0x64,0x65,0x72,0x52,0x6f,0x6c,0x6c,0x01,
                0x00,0x00,0x00,0x73,0x09,0x00,0x00,0x00,0x4c,0x45,
                0x6c,0x62,0x6f,0x77,0x59,0x61,0x77,0x01,0x00,0x00,
                0x00,0x73,0x09,0x00,0x00,0x00,0x52,0x45,0x6c,0x62,
                0x6f,0x77,0x59,0x61,0x77,0x01,0x00,0x00,0x00,0x73,
                0x05,0x00,0x00,0x00,0x4c,0x48,0x61,0x6e,0x64,0x01,
                0x00,0x00,0x00,0x73,0x05,0x00,0x00,0x00,0x52,0x48,
                0x61,0x6e,0x64,0x03,0x00,0x00,0x00,
                0x5b,0x6d,0x5d, //[m]
                //200
                0x08,0x00,0x00,0x00,
                0x01,0x00,0x00,0x00,0x66,
                0x00,0x00,0x80,0xbf, //-1 LShoulder Pitch 209-212
                0x01,0x00,0x00,0x00,0x66,
                0x00,0x00,0x80,0xbf, //-1 RShoulder Pitch 218-221
                0x01,0x00,0x00,0x00,0x66,
                0x66,0x66,0xa6,0x3f, //1.3 LShoulder Roll 227-230
                0x01,0x00,0x00,0x00,0x66,
                0x66,0x66,0xa6,0xbf, //-1.3 RShoulder Roll 236-239
                0x01,0x00,0x00,0x00,0x66,
                0x00,0x00,0x00,0x40, //2 LElbowYaw 245-248
                0x01,0x00,0x00,0x00,0x66,
                0x00,0x00,0x00,0xc0, //-2 RElbowYaw 254-257
                0x01,0x00,0x00,0x00,0x66,
                0x00,0x00,0x80,0x3f, //1 LHAND 263-266 
                0x01,0x00,0x00,0x00,0x66,
                0x00,0x00,0x80,0x3f, //1 RHAND 272-275
                0xcd,0xcc,0x4c,0x3e //0.2 276-279 
            };


        int count = Convert.ToInt16(0x14);

        private byte getCount()
        {
            count = count + 3;
            if (count > 255)
                count = 0;
            byte countTemp = Convert.ToByte(count);
            return countTemp;
        }
    }
}
