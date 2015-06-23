using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SoundGenerator.WaveCreator;

namespace SoundGenerator.AACEncoder
{
    public static class AACEncoder
    {
        public static void AACEncode(String input, String output, WaveSampleType rate)
        {
            Console.WriteLine("Deleting output path, if it exists.");
            File.Delete(output);
            Console.WriteLine("Preparing to start third-party program (FFMPEG)");
            String filename = Program.FFMPEG_NAME;
            String arguments = "-i \"{{INPUT}}\" -strict experimental -c:a aac -b:a {{SAMPLERATE}} \"{{OUTPUT}}\"";
            arguments = arguments.Replace("{{INPUT}}", input).Replace("{{OUTPUT}}", output).Replace("{{SAMPLERATE}}", ((uint) rate).ToString());
            Console.WriteLine("Starting FFMPEG to encode WAVE to AAC");
            ProcessStartInfo ffmpeg_inf = new ProcessStartInfo();
            ffmpeg_inf.Arguments = arguments;
            ffmpeg_inf.FileName = filename;
            ffmpeg_inf.CreateNoWindow = true;
            ffmpeg_inf.UseShellExecute = false;
            Process ffmpeg = Process.Start(ffmpeg_inf);
            ffmpeg.WaitForExit();
        }
    }
}
