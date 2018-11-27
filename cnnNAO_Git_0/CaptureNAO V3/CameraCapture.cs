//----------------------------------------------------------------------------
//  Copyright (C) 2004-2018 by EMGU Corporation. All rights reserved.       
//----------------------------------------------------------------------------

using System;
using System.Drawing.Imaging;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.Util;
using Emgu.CV.UI;
using Emgu.CV.Cuda;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Linq;
namespace CameraCapture
{

   public partial class CameraCapture : Form
   {
        string ip = "169.254.175.171";
      private VideoCapture _capture = null;
      private bool _captureInProgress;
      private Mat _frame;
        bool video = false;
        Bitmap bitmap = new Bitmap(320, 240);
        private bool connected = false;
      //private Mat _grayFrame;
      //private Mat _smallGrayFrame;
      //private Mat _smoothedGrayFrame;
      private Mat _cannyFrame;
        DateTime date1;
        DateTime date2;

        public void MyDraw()
        {
            
            try
            {
                while (video)
                {
                    
                    Invoke(new Action(DrawItem));
                    //it was 40
                    Thread.Sleep(25);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

        }

        public CameraCapture()
      {
            
            InitializeComponent();
            
            //CvInvoke.UseOpenCL = false;
            //try
            //{
            //   _capture = new VideoCapture();
            //   _capture.ImageGrabbed += ProcessFrame;
            //}
            //catch (NullReferenceException excpt)
            //{
            //   MessageBox.Show(excpt.Message);
            //}
            //_frame = new Mat();

            //_cannyFrame = new Mat();


        }

        Image sendBitmap;
      private void ProcessFrame(object sender, EventArgs arg)
      {
         if (_capture != null && _capture.Ptr != IntPtr.Zero)
         {
            _capture.Retrieve(_frame, 0);

                _cannyFrame = _frame.Clone();

                long detectionTime;
                List<Rectangle> faces = new List<Rectangle>();
                List<Rectangle> eyes = new List<Rectangle>();

                DetectFace.Detect(
                  _frame,
                  "haarcascade_frontalface_default.xml",
                  "haarcascade_eye.xml",
                  faces, eyes,
                  out detectionTime);

                foreach (Rectangle face in faces)
                    CvInvoke.Rectangle(_frame, face, new Bgr(Color.Red).MCvScalar, 2);

                foreach (Rectangle eye in eyes)
                    CvInvoke.Rectangle(_frame, eye, new Bgr(Color.Blue).MCvScalar, 2);
                captureImageBox.Image = _frame.Bitmap;

               

                if (faces.Count == 1 )
                {
                    //System.Drawing.Imaging.PixelFormat format = image.Bitmap.PixelFormat;
                    Bitmap tempBitmapClone = _cannyFrame.Bitmap.Clone(faces[0], _frame.Bitmap.PixelFormat);
                    Bitmap tempBitmapResize = new Bitmap(tempBitmapClone, 100, 100);
                    Bitmap tempBitmap = MakeGrayscale3(tempBitmapResize);
                    tempBitmapClone.Dispose();
                    tempBitmapResize.Dispose();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    Image<Bgr, Byte> img = new Image<Bgr, Byte>(tempBitmap); //where bmp is a Bitmap
                    pictureBox2.Image = img.Bitmap;

                    if(connected)
                        try
                        {
                            sendBitmap = (Image)img.Bitmap.Clone();
                            sendImage();
                            
                        }
                        catch
                        {

                        }
                 

                }

                


            }
      }

        public Bitmap getFaceFromBitmap(Bitmap bmp)
        {
            //IImage image = new UMat(path, ImreadModes.Color); //UMat version
            Image<Bgr, Byte> image = new Image<Bgr, Byte>(bmp);
            long detectionTime;
            List<Rectangle> faces = new List<Rectangle>();
            List<Rectangle> eyes = new List<Rectangle>();
            //haarcascade_frontalcatface_extended
            //haarcascade_frontalface_default
            DetectFace.Detect(
              image, "haarcascade_frontalface_alt2.xml", "haarcascade_eye.xml",
              faces, eyes,
              out detectionTime);
            Bitmap tempBitmap;

            //if (faces.Count == 1 && eyes.Count == 2)
            if (faces.Count == 1)
            {
                //System.Drawing.Imaging.PixelFormat format = image.Bitmap.PixelFormat;
                Bitmap tempBitmapClone = image.Bitmap.Clone(faces[0], image.Bitmap.PixelFormat);
                Bitmap tempBitmapResize = new Bitmap(tempBitmapClone, 100, 100);
                tempBitmap = MakeGrayscale3(tempBitmapResize);
                tempBitmapClone.Dispose();
                tempBitmapResize.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            else
            {
                tempBitmap = null;
            }

            image.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            return tempBitmap;

        }

        public Bitmap getFaceFromBitmap2(Bitmap bmp)
        {
            //IImage image = new UMat(path, ImreadModes.Color); //UMat version
            Image<Bgr, Byte> image = new Image<Bgr, Byte>(bmp);
            long detectionTime;
            List<Rectangle> faces = new List<Rectangle>();
            List<Rectangle> eyes = new List<Rectangle>();
            //haarcascade_frontalcatface_extended
            //haarcascade_frontalface_default
            DetectFace.Detect(
              image, "haarcascade_frontalface_alt.xml", "haarcascade_eye.xml",
              faces, eyes,
              out detectionTime);
            Bitmap tempBitmap;

            //if (faces.Count == 1 && eyes.Count == 2)
            if (faces.Count == 1)
            {
                //System.Drawing.Imaging.PixelFormat format = image.Bitmap.PixelFormat;
                Bitmap tempBitmapClone = image.Bitmap.Clone(faces[0], image.Bitmap.PixelFormat);
                Bitmap tempBitmapResize = new Bitmap(tempBitmapClone, 100, 100);
                tempBitmap = MakeGrayscale3(tempBitmapResize);
                tempBitmapClone.Dispose();
                tempBitmapResize.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            else
            {
                tempBitmap = null;
            }

            image.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            return tempBitmap;

        }

        public Bitmap getFaceFromBitmap3(Bitmap bmp)
        {
            //IImage image = new UMat(path, ImreadModes.Color); //UMat version
            Image<Bgr, Byte> image = new Image<Bgr, Byte>(bmp);
            long detectionTime;
            List<Rectangle> faces = new List<Rectangle>();
            List<Rectangle> eyes = new List<Rectangle>();
            //haarcascade_frontalcatface_extended
            //haarcascade_frontalface_default
            DetectFace.Detect(
              image, "haarcascade_frontalface_default.xml", "haarcascade_eye.xml",
              faces, eyes,
              out detectionTime);
            Bitmap tempBitmap;

            //if (faces.Count == 1 && eyes.Count == 2)
            if (faces.Count == 1)
            {
                //System.Drawing.Imaging.PixelFormat format = image.Bitmap.PixelFormat;
                Bitmap tempBitmapClone = image.Bitmap.Clone(faces[0], image.Bitmap.PixelFormat);
                Bitmap tempBitmapResize = new Bitmap(tempBitmapClone, 100, 100);
                tempBitmap = MakeGrayscale3(tempBitmapResize);
                tempBitmapClone.Dispose();
                tempBitmapResize.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            else
            {
                tempBitmap = null;
            }

            image.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            return tempBitmap;

        }

        public Bitmap getFaceFromBitmap4(Bitmap bmp)
        {
            //IImage image = new UMat(path, ImreadModes.Color); //UMat version
            Image<Bgr, Byte> image = new Image<Bgr, Byte>(bmp);
            long detectionTime;
            List<Rectangle> faces = new List<Rectangle>();
            List<Rectangle> eyes = new List<Rectangle>();
            //haarcascade_frontalcatface_extended
            //haarcascade_frontalface_default
            DetectFace.Detect(
              image, "haarcascade_frontalface_alt_tree.xml", "haarcascade_eye.xml",
              faces, eyes,
              out detectionTime);
            Bitmap tempBitmap;

            //if (faces.Count == 1 && eyes.Count == 2)
            if (faces.Count == 1)
            {
                //System.Drawing.Imaging.PixelFormat format = image.Bitmap.PixelFormat;
                Bitmap tempBitmapClone = image.Bitmap.Clone(faces[0], image.Bitmap.PixelFormat);
                Bitmap tempBitmapResize = new Bitmap(tempBitmapClone, 100, 100);
                tempBitmap = MakeGrayscale3(tempBitmapResize);
                tempBitmapClone.Dispose();
                tempBitmapResize.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            else
            {
                tempBitmap = null;
            }

            image.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            return tempBitmap;

        }

        public void processFrame2(Bitmap bit)
        {
            captureImageBox.Image = bit;

            Bitmap face = getFaceFromBitmap(bit);
            if (face == null)
                face = getFaceFromBitmap2(bit);
            //if (face == null)
            //    face = getFaceFromBitmap3(bit);
            //if (face == null)
            //    face = getFaceFromBitmap4(bit);
            pictureBox2.Image = face;
            
            if (face != null)
            {
                Image<Bgr, Byte> img = new Image<Bgr, Byte>(face);
                pictureBox2.Image = face;
                if (connected)
                    try
                    {
                        sendBitmap = (Image)img.Bitmap.Clone();
                        sendImage();

                    }
                    catch
                    {

                    }
            }
                


            
        }

        public static Bitmap MakeGrayscale3(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][]
               {
         new float[] {.3f, .3f, .3f, 0, 0},
         new float[] {.59f, .59f, .59f, 0, 0},
         new float[] {.11f, .11f, .11f, 0, 0},
         new float[] {0, 0, 0, 1, 0},
         new float[] {0, 0, 0, 0, 1}
               });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            return newBitmap;
        }

        private void configureVideo()
        {
            TcpClient client = new TcpClient(ip, 9559);

            NetworkStream stream = client.GetStream();

            stream.Write(Instructions.video0, 0, Instructions.video0.Length);
            stream.Write(Instructions.video1, 0, Instructions.video1.Length);

            byte[] myReadBuffer = new byte[1460];
            int numberOfBytesRead = 0;
            StringBuilder myCompleteMessage = new StringBuilder();
            numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
            myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
            Console.WriteLine(myCompleteMessage.Length);
            myCompleteMessage.Clear();
            numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
            myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
            Console.WriteLine(myCompleteMessage.Length);
            myCompleteMessage.Clear();
            numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
            myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
            Console.WriteLine(myCompleteMessage.Length);
            stream.Write(Instructions.video2, 0, Instructions.video2.Length);
            myCompleteMessage.Clear();
            numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
            myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
            Console.WriteLine(myCompleteMessage.Length);
            stream.Write(Instructions.video3, 0, Instructions.video3.Length);
            myCompleteMessage.Clear();
            numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
            myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
            Console.WriteLine(myCompleteMessage.Length);
            stream.Write(Instructions.video4, 0, Instructions.video4.Length);
            myCompleteMessage.Clear();
            numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
            myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
            Console.WriteLine(myCompleteMessage.Length);
            stream.Write(Instructions.video5, 0, Instructions.video5.Length);
            myCompleteMessage.Clear();
            numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
            myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
            Console.WriteLine(myCompleteMessage);
            stream.Write(Instructions.video6, 0, Instructions.video6.Length);
            for (int i = 0; i < 15; i++)
            {
                myCompleteMessage.Clear();
                numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
                myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
                Console.WriteLine(myCompleteMessage.Length);

            }
            stream.Write(Instructions.video7, 0, Instructions.video7.Length);
            myCompleteMessage.Clear();
            numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
            myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
            Console.WriteLine(myCompleteMessage.Length);
            stream.Write(Instructions.video8, 0, Instructions.video8.Length);
            myCompleteMessage.Clear();
            numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
            myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
            Console.WriteLine(myCompleteMessage.Length);
            stream.Write(Instructions.video9, 0, Instructions.video9.Length);
            myCompleteMessage.Clear();
            numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
            myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
            Console.WriteLine(myCompleteMessage.Length);

            stream.Write(Instructions.video10, 0, Instructions.video10.Length);
            myCompleteMessage.Clear();
            numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
            myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
            Console.WriteLine(myCompleteMessage.Length);
        }

        private void DrawItem()
        {
            
            try
            {


                TcpClient client = new TcpClient(ip, 9559);
                //TcpClient client = new TcpClient("169.254.51.192", 9559);

                byte[] data = Instructions.askVideoMod(0x81, 0x00);

                NetworkStream stream = client.GetStream();

                stream.Write(data, 0, data.Length);

                int totalExpectedBytes = 107 + 1460 * 105 + 447;
                int totalBytes = 0;
                byte[] image = new byte[totalExpectedBytes];

                int globalindex = 0;
                int frametype = 0;
                while (totalBytes < totalExpectedBytes)
                {
                    byte[] myReadBuffer = new byte[1460];
                    int numberOfBytesRead = 0;
                    StringBuilder myCompleteMessage = new StringBuilder();

                    // Incoming message may be larger than the buffer size. 
                    do
                    {

                        numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);

                        myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
                        if (globalindex == 0 && myCompleteMessage.Length == 1460)
                        {
                            Console.WriteLine("Length " + myCompleteMessage.Length + " Total " + globalindex);
                            frametype = 1;
                        }


                        for (int i = 0; i < numberOfBytesRead; i++)
                        {
                            image[globalindex + i] = myReadBuffer[i];
                        }
                        globalindex += numberOfBytesRead;

                    }
                    while (stream.DataAvailable);

                    totalBytes += myCompleteMessage.Length;

                    //19454





                };

                if (totalBytes == totalExpectedBytes)
                {


                    if (frametype == 0)
                    {
                        LockBitmap lockBitmap = new LockBitmap(bitmap);
                        lockBitmap.LockBits();



                        byte[] bufferY = new byte[76800];
                        byte[] bufferU = new byte[76800];
                        byte[] bufferV = new byte[76800];

                        int indexY = 0;
                        int indexU = 0;
                        int indexV = 0;

                        int indexUV = 0;

                        byte tempU = 0x00;
                        byte tempV = 0x00;
                        for (int i = 0; i < 153600; i++)
                        {

                            byte temp = image[i + 102 + 107];



                            if (i % 2 == 0)
                            {
                                bufferY[indexY] = temp;
                                indexY++;
                            }
                            else
                            {
                                if (indexUV % 2 == 0)
                                {
                                    bufferU[indexU] = temp;
                                    bufferV[indexV] = tempV;
                                    tempU = temp;
                                    indexU++;
                                    indexV++;
                                }
                                else
                                {
                                    bufferV[indexV] = temp;
                                    bufferU[indexU] = tempU;
                                    tempV = temp;
                                    indexV++;
                                    indexU++;
                                }
                                indexUV++;

                            }

                        }

                        for (int i = 0; i < bufferY.GetLength(0); i++)
                        {
                            int R = (int)(1.164 * ((int)bufferY[i] - 16) + 1.596 * ((int)bufferV[i] - 128));
                            int G = (int)(1.164 * ((int)bufferY[i] - 16) - 0.813 * ((int)bufferV[i] - 128) - 0.391 * ((int)bufferU[i] - 128));
                            int B = (int)(1.164 * ((int)bufferY[i] - 16) + 2.018 * ((int)bufferU[i] - 128));

                            int y = i / 320;
                            int x = i % 320;

                            Color color = new Color();

                            R = Math.Min(255, R);
                            G = Math.Min(255, G);
                            B = Math.Min(255, B);

                            R = Math.Max(0, R);
                            G = Math.Max(0, G);
                            B = Math.Max(0, B);

                            color = Color.FromArgb(R, G, B);
                            lockBitmap.SetPixel(x, y, color);
                            //bitmap.SetPixel(x, y, color);

                        }
                        lockBitmap.UnlockBits();
                        processFrame2(bitmap);

                    }
                    if (frametype == 1)
                    {
                        LockBitmap lockBitmap = new LockBitmap(bitmap);
                        lockBitmap.LockBits();



                        byte[] bufferY = new byte[76800];
                        byte[] bufferU = new byte[76800];
                        byte[] bufferV = new byte[76800];

                        int indexY = 0;
                        int indexU = 0;
                        int indexV = 0;

                        int indexUV = 0;

                        byte tempU = 0x00;
                        byte tempV = 0x00;
                        for (int i = 0; i < 153600; i++)
                        {

                            byte temp = image[i + 102];



                            if (i % 2 == 0)
                            {
                                bufferY[indexY] = temp;
                                indexY++;
                            }
                            else
                            {
                                if (indexUV % 2 == 0)
                                {
                                    bufferU[indexU] = temp;
                                    bufferV[indexV] = tempV;
                                    tempU = temp;
                                    indexU++;
                                    indexV++;
                                }
                                else
                                {
                                    bufferV[indexV] = temp;
                                    bufferU[indexU] = tempU;
                                    tempV = temp;
                                    indexV++;
                                    indexU++;
                                }
                                indexUV++;

                            }

                        }

                        for (int i = 0; i < bufferY.GetLength(0); i++)
                        {
                            int R = (int)(1.164 * ((int)bufferY[i] - 16) + 1.596 * ((int)bufferV[i] - 128));
                            int G = (int)(1.164 * ((int)bufferY[i] - 16) - 0.813 * ((int)bufferV[i] - 128) - 0.391 * ((int)bufferU[i] - 128));
                            int B = (int)(1.164 * ((int)bufferY[i] - 16) + 2.018 * ((int)bufferU[i] - 128));

                            int y = i / 320;
                            int x = i % 320;

                            Color color = new Color();

                            R = Math.Min(255, R);
                            G = Math.Min(255, G);
                            B = Math.Min(255, B);

                            R = Math.Max(0, R);
                            G = Math.Max(0, G);
                            B = Math.Max(0, B);

                            color = Color.FromArgb(R, G, B);
                            lockBitmap.SetPixel(x, y, color);
                            //bitmap.SetPixel(x, y, color);

                        }
                        lockBitmap.UnlockBits();
                        //captureImageBox.Image = bitmap;
                        processFrame2(bitmap);
                        //Image test = (Image)bitmap.Clone();

                    }


                }


                // Close everything.
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

        private void captureButtonClick(object sender, EventArgs e)
      {
            date1 = DateTime.Now;
            new Thread(MyDraw).Start();
            video = true;
           
        }

      private void ReleaseData()
      {
         if (_capture != null)
            _capture.Dispose();
      }

     

      

        TcpClient tcpclnt = new TcpClient();

        //Declare and Initialize the IP Adress
        static IPAddress ipAd = IPAddress.Parse("127.0.0.10");

        //Declare and Initilize the Port Number;
        int PortNumber = 8888;

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Connecting.....");
            try
            {
                tcpclnt.Connect(ipAd, PortNumber);
                txtStatus.Text += Environment.NewLine + "Connected";
                txtStatus.Text += Environment.NewLine + "Enter the string to be transmitted";
                connected = true;
            }
            catch (System.Net.Sockets.SocketException exc)
            {
                Console.WriteLine(exc);
            }
        }

    

        private void button2_Click(object sender, EventArgs e)
        {
            String str = "Hola Mundo" + "$";
            System.IO.Stream stm = tcpclnt.GetStream();


            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] ba = asen.GetBytes(str);

            txtStatus.Text += Environment.NewLine + "Transmitting...";
            //Console.WriteLine("Transmitting.....");

            stm.Write(ba, 0, ba.Length);

            byte[] bb = new byte[100];
            int k = stm.Read(bb, 0, 100);

            string Response = Encoding.ASCII.GetString(bb);

            txtStatus.Text += Environment.NewLine + "Response from server: " + Response;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sendImage();
        }

        private void sendImage()
        {
            System.IO.Stream stm = tcpclnt.GetStream();

            
            //byte[] ba = ConvertBitMapToByteArray(pictureBox2.Image);
            byte[] ba = ConvertBitMapToByteArray(sendBitmap);
            Console.WriteLine(  "Transmitting...");

            Console.WriteLine(ba.Length.ToString());
            stm.Write(ba, 0, ba.Length);

            byte[] bb = new byte[100];
            int k = stm.Read(bb, 0, 100);

            string Response = Encoding.ASCII.GetString(bb);

            Console.WriteLine( "Response from server: " + Response);
            Response = new string(Response.Where(c => !char.IsControl(c)).ToArray());
            //Response =Regex.Replace(Response, "[^\\w\\._]", "");
            Response = Response.Replace("    ", " ");
            Response = Response.Replace("   ", " ");
            Response = Response.Replace("  ", " ");
            Response = Response.Replace("[", "");
            Response = Response.Replace("]", "");
            Response = Response.Replace("\t", " ");
            date2 = DateTime.Now;

            String timeStamp = (date1 - date2).ToString("mmssff");
            
        }

        public byte[] ConvertBitMapToByteArray(Image bitmap)
        {
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            Byte[] bytes = stream.ToArray();

            return bytes;
        }

        private void CameraCapture_Load(object sender, EventArgs e)
        {
            configureVideo();
            configureVideo();
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            naoControls naoWindow = new naoControls();
            naoWindow.Show();
        }
    }


}
