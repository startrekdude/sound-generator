using System;
using System.IO;
using System.Threading;
using com.leff.midi;
using jre = java.lang;
using SoundGenerator.CommandREPL;

namespace SoundGenerator
{
    public class Program
    {
        public static String FFMPEG_NAME;

        public static void Main(string[] args)
        {
            Console.WriteLine("Sam Haskins's Random Noise Generator");
            Console.WriteLine("Note: Sometimes, Windows Media Player can't play WAV Files generated when using different sampling rates, so use VLC!");
            Environment.CurrentDirectory = Path.GetTempPath();
            Console.WriteLine("Extracting all necessary resource files...");
            Program.ExtractAllResources();
            Console.WriteLine("_100%_ Done!");
            Console.WriteLine("Loading all necessary IKVM classes...");
            Program.LoadIKVMClasses();
            Console.WriteLine("_100%_ Done!");
            Console.WriteLine("Starting SoundShell...");
            ConsoleSpinner spinner = new ConsoleSpinner();
            short i = 0;
            while (i < 25)
            {
                spinner.Turn();
                Thread.Sleep(200);
                i += 1;
            }
            Console.WriteLine("SoundShell Start!");
            SoundShell cmd = new SoundShell();
            cmd.Run();
            Program.KillAllExternalResources();
        }

        public static void ExtractAllResources()
        {
            Program.FFMPEG_NAME = Environment.CurrentDirectory + "\\" + Path.GetRandomFileName() + ".exe";
            ResourceExtractor.ExtractResourceToFile("SoundGenerator.ffmpeg.exe", Program.FFMPEG_NAME);
            File.SetAttributes(Program.FFMPEG_NAME, FileAttributes.Hidden);
        }

        public static void KillAllExternalResources()
        {
            File.Delete(Program.FFMPEG_NAME);
        }

        internal static void LoadIKVMClasses ()
        {
            jre.Class clazz = typeof(MidiFile);
            jre.Thread.currentThread().setContextClassLoader(clazz.getClassLoader());
        }
    }
}
