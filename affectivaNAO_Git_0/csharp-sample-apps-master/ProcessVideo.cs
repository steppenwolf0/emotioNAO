using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text;

namespace csharp_sample_app
{
    public partial class ProcessVideo : Form, Affdex.ProcessStatusListener, Affdex.ImageListener
    {
        string ip = "169.254.175.171";
        public bool video = false;
        Bitmap bitmap = new Bitmap(320, 240);
        public string emotion = "neutral";
        int countSurprise = 0;
        int countAngry = 0;
        int countSad = 0;
        int countDisgusted = 0;
        int countScared = 0;
        int countHappy = 0;
        public ProcessVideo(Affdex.PhotoDetector detector)
        {
            System.Console.WriteLine("Starting Interface...");
            this.detector = detector;
            detector.setImageListener(this);
            detector.setProcessStatusListener(this);
            InitializeComponent();
            rwLock = new ReaderWriterLock();
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            new Thread(MyDraw).Start();
        }

        public void MyDraw()
        {
            try
            {
                while (video)
                {
                    Invoke(new Action(DrawItem));
                    Thread.Sleep(40);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

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
                        detector.process(LoadFrameFromBitmap(bitmap));
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

                        //Image test = (Image)bitmap.Clone();
                        detector.process(LoadFrameFromBitmap(bitmap));
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

        public void onImageCapture(Affdex.Frame frame)
        {
            frame.Dispose();

        }

        public void onImageResults(Dictionary<int, Affdex.Face> faces, Affdex.Frame frame)
        {
            process_fps = 1.0f / (frame.getTimestamp() - process_last_timestamp);
            process_last_timestamp = frame.getTimestamp();
            //System.Console.WriteLine(" pfps: {0}", process_fps.ToString());

            byte[] pixels = frame.getBGRByteArray();
            this.img = new Bitmap(frame.getWidth(), frame.getHeight(), PixelFormat.Format24bppRgb);
            var bounds = new Rectangle(0, 0, frame.getWidth(), frame.getHeight());
            BitmapData bmpData = img.LockBits(bounds, ImageLockMode.WriteOnly, img.PixelFormat);
            IntPtr ptr = bmpData.Scan0;

            int data_x = 0;
            int ptr_x = 0;
            int row_bytes = frame.getWidth() * 3;

            // The bitmap requires bitmap data to be byte aligned.
            // http://stackoverflow.com/questions/20743134/converting-opencv-image-to-gdi-bitmap-doesnt-work-depends-on-image-size

            for (int y = 0; y < frame.getHeight(); y++)
            {
                Marshal.Copy(pixels, data_x, ptr + ptr_x, row_bytes);
                data_x += row_bytes;
                ptr_x += bmpData.Stride;
            }
            img.UnlockBits(bmpData);

            this.faces = faces;
            //rwLock.ReleaseWriterLock();

            this.Invalidate();
            frame.Dispose();
        }

        private void DrawResults(Graphics g, Dictionary<int, Affdex.Face> faces)
        {
            Pen whitePen = new Pen(Color.OrangeRed);
            Pen redPen = new Pen(Color.DarkRed);
            Pen bluePen = new Pen(Color.DarkBlue);
            Font aFont = new Font(FontFamily.GenericSerif, 14, FontStyle.Bold);
            float radius = 2;
            int spacing = 14;
            int left_margin = 30;

            foreach (KeyValuePair<int, Affdex.Face> pair in faces)
            {
                Affdex.Face face = pair.Value;
                foreach (Affdex.FeaturePoint fp in face.FeaturePoints)
                {
                    g.DrawCircle(whitePen, fp.X, fp.Y, radius);
                }

                Affdex.FeaturePoint tl = minPoint(face.FeaturePoints);
                Affdex.FeaturePoint br = maxPoint(face.FeaturePoints);

                int padding = (int)tl.Y;

                g.DrawString("EMOTIONS", aFont, bluePen.Brush, new PointF(br.X, padding += (spacing * 2)));

                double max = 0;
                int i = 0;
                int bestIndex = 0;
                string tempEmotion = "neutral";
                foreach (PropertyInfo prop in typeof(Affdex.Emotions).GetProperties())
                {
                    float value = (float)prop.GetValue(face.Emotions, null);
                    String c = String.Format("{0}: {1:0.00}", prop.Name, value);
                    if (i>2 && value>50 && value>max)
                    {
                        bestIndex = i;
                        max = value;
                        tempEmotion = prop.Name;
                    }
                    g.DrawString(c, aFont, (value > 50) ? 
                        whitePen.Brush : redPen.Brush, 
                        new PointF(br.X, padding += spacing));
                    i++; 
                }
                switch (bestIndex)
                {
                    case 3:
                        tempEmotion = "surprised";
                        countSurprise++;
                        break;
                    case 4:
                        tempEmotion = "angry";
                        countAngry++;
                        break;
                    case 5:
                        tempEmotion = "sad";
                        countSad++;
                        break;
                    case 6:
                        tempEmotion = "disgusted";
                        countDisgusted++;
                        break;
                    case 7:
                        tempEmotion = "scared";
                        countScared++;
                        break;
                    case 8:
                        tempEmotion = "happy";
                        countHappy++;
                        break;
                }

                if (max > 50)
                {
                    Console.WriteLine(tempEmotion);
                    
                    
                }
                    
                


            }
        }

        public void Talk(string text)
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

                stream.Write(Instructions.tts0, 0, Instructions.tts0.Length);
                stream.Write(Instructions.tts1, 0, Instructions.tts1.Length);

                byte[] message = Instructions.Message(text);

                stream.Write(message, 0, message.Length);
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

        public void onProcessingException(Affdex.AffdexException A_0)
        {
            System.Console.WriteLine("Encountered an exception while processing {0}", A_0.ToString());
        }

        public void onProcessingFinished()
        {
            //System.Console.WriteLine("Processing finished successfully");
        }

        Affdex.FeaturePoint minPoint(Affdex.FeaturePoint[] points)
        {
            Affdex.FeaturePoint ret = points[0];
            foreach (Affdex.FeaturePoint point in points)
            {
                if (point.X < ret.X) ret.X = point.X;
                if (point.Y < ret.Y) ret.Y = point.Y;
            }
            return ret;
        }

        Affdex.FeaturePoint maxPoint(Affdex.FeaturePoint[] points)
        {
            Affdex.FeaturePoint ret = points[0];
            foreach (Affdex.FeaturePoint point in points)
            {
                if (point.X > ret.X) ret.X = point.X;
                if (point.Y > ret.Y) ret.Y = point.Y;
            }
            return ret;
        }

        [HandleProcessCorruptedStateExceptions]
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                // rwLock.AcquireReaderLock(Timeout.Infinite);
                if (img != null)
                {
                    this.Width = img.Width;
                    this.Height = img.Height;
                    e.Graphics.DrawImage((Image)img, new Point(0, 0));
                }

                if (faces != null) DrawResults(e.Graphics, faces);


                e.Graphics.Flush();
            }
            catch (System.AccessViolationException exp)
            {
                System.Console.WriteLine("Encountered AccessViolationException.");
            }
        }

        private float process_last_timestamp = -1.0f;
        private float process_fps = -1.0f;
        public Affdex.PhotoDetector detector = null;
        private Bitmap img { get; set; }
        private Dictionary<int, Affdex.Face> faces { get; set; }
        //public Affdex.Detector detector { get; set; }
        private ReaderWriterLock rwLock { get; set; }

        static Affdex.Frame LoadFrameFromFile(string fileName)
        {
            Bitmap bitmap = new Bitmap(fileName);

            // Lock the bitmap's bits.
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap. 
            int numBytes = bitmap.Width * bitmap.Height * 3;
            byte[] rgbValues = new byte[numBytes];

            int data_x = 0;
            int ptr_x = 0;
            int row_bytes = bitmap.Width * 3;

            // The bitmap requires bitmap data to be byte aligned.
            // http://stackoverflow.com/questions/20743134/converting-opencv-image-to-gdi-bitmap-doesnt-work-depends-on-image-size

            for (int y = 0; y < bitmap.Height; y++)
            {
                Marshal.Copy(ptr + ptr_x, rgbValues, data_x, row_bytes);//(pixels, data_x, ptr + ptr_x, row_bytes);
                data_x += row_bytes;
                ptr_x += bmpData.Stride;
            }

            bitmap.UnlockBits(bmpData);

            return new Affdex.Frame(bitmap.Width, bitmap.Height, rgbValues, Affdex.Frame.COLOR_FORMAT.BGR);
        }

         Affdex.Frame LoadFrameFromBitmap(Bitmap bit)
        {
            Bitmap bitTemp = new Bitmap(bit, new Size(bit.Width * 2,
                bit.Height * 2));

            System.IO.MemoryStream stream = new System.IO.MemoryStream();

            // Save image to stream.
            bitTemp.Save(stream, ImageFormat.Jpeg);


            Bitmap bitmapAff =(Bitmap)Image.FromStream(stream);
            
            // Lock the bitmap's bits.
            Rectangle rect = new Rectangle(0, 0, bitmapAff.Width, bitmapAff.Height);
            BitmapData bmpData = bitmapAff.LockBits(rect, ImageLockMode.ReadWrite, bitmapAff.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap. 
            int numBytes = bitmapAff.Width * bitmapAff.Height * 3;
            byte[] rgbValues = new byte[numBytes];

            int data_x = 0;
            int ptr_x = 0;
            int row_bytes = bitmapAff.Width * 3;

            // The bitmap requires bitmap data to be byte aligned.
            // http://stackoverflow.com/questions/20743134/converting-opencv-image-to-gdi-bitmap-doesnt-work-depends-on-image-size

            for (int y = 0; y < bitmapAff.Height; y++)
            {
                Marshal.Copy(ptr + ptr_x, rgbValues, data_x, row_bytes);//(pixels, data_x, ptr + ptr_x, row_bytes);
                data_x += row_bytes;
                ptr_x += bmpData.Stride;
            }

            bitmapAff.UnlockBits(bmpData);

            return new Affdex.Frame(bitmapAff.Width, bitmapAff.Height, rgbValues, Affdex.Frame.COLOR_FORMAT.BGR);
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            configureVideo();
            configureVideo();
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (video)
                video = false;
            else
            {
                video = true;
                new Thread(MyDraw).Start();
            }

            timer1.Enabled = video;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            naoControls naoWindow = new naoControls(this);
            naoWindow.Show();
            //naoMovement naoWindow = new naoMovement(ip, 9559);
            //naoWindow.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int count = 3;
            if (countSurprise > count)
                Talk("they are surprised.");

            if (countAngry > count)
                Talk("Please calm down.");

            if (countSad > count)
                Talk("Do not be sad, everything will be fine.");

            if (countSurprise > count)
                Talk("Calm down.");

            if (countDisgusted > count)
                Talk("Is everything ok?");

            if (countScared > count)
                Talk("Do not be scared, everything will be fine.");

            if (countHappy > count)
                Talk("Are you having fun?");

             countSurprise = 0;
             countAngry = 0;
             countSad = 0;
             countDisgusted = 0;
             countScared = 0;
             countHappy = 0;

            Console.WriteLine( "now");
        }
    }
}

public static class GraphicsExtensions
{
    public static void DrawCircle(this Graphics g, Pen pen,
                                  float centerX, float centerY, float radius)
    {
        g.DrawEllipse(pen, centerX - radius, centerY - radius,
                      radius + radius, radius + radius);
    }
}
