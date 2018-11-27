using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;

namespace csharp_sample_app
{
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            
            CmdOptions options = new CmdOptions();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                Affdex.PhotoDetector detector = null;
                List<string> imgExts = new List<string> { ".bmp", ".jpg", ".gif", ".png", ".jpe" };
                List<string> vidExts = new List<string> { ".avi", ".mov", ".flv", ".webm", ".wmv", ".mp4" };
                options.DataFolder = @"D:\NAO\NAO2018\data";
                options.Input = @"";
    

                
                    System.Console.WriteLine("Processing");
                    //detector = new Affdex.PhotoDetector((uint)options.numFaces, (Affdex.FaceDetectorMode)options.faceMode);
                detector = new Affdex.PhotoDetector((uint)2, (Affdex.FaceDetectorMode)options.faceMode);


                ProcessVideo videoForm = new ProcessVideo(detector);
                    detector.setClassifierPath(options.DataFolder);
                    detector.setDetectAllEmotions(true);
                    detector.setDetectAllExpressions(true);
                    detector.setDetectAllEmojis(true);
                    detector.setDetectAllAppearances(true);
                    detector.start();
                    System.Console.WriteLine("Face detector mode = " + detector.getFaceDetectorMode().ToString());
                    
                    videoForm.ShowDialog();
                    //detector.stop();
                

            }


        }

    }
}
