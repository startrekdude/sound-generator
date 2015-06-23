using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SoundGenerator.FileConverter
{
    public static class FileConvert
    {
        public readonly static String[] FFMPEG_SUPPORTED_FORMATS = new String[] { "ac3", "g722", "g726", "alac", "eac3", "flac", "g723_1", "mp2", "tta", "aac"};
        public readonly static Dictionary<String, String> FFMPEG_CODEC_FILE_EXTENSION = new Dictionary<String, String>();
        const String AAC_FORMAT = "aac";

        static FileConvert()
        {
            FFMPEG_CODEC_FILE_EXTENSION.Add("adx", "adpcm_adx");
            FFMPEG_CODEC_FILE_EXTENSION.Add("gsm", "libgsm_ms");
            FFMPEG_CODEC_FILE_EXTENSION.Add("opus", "libopus");
            FFMPEG_CODEC_FILE_EXTENSION.Add("spx", "libspeex");
            FFMPEG_CODEC_FILE_EXTENSION.Add("ogg", "libvorbis");
            FFMPEG_CODEC_FILE_EXTENSION.Add("wma", "wmav2");
        }

        public static Boolean ConvertAAC (String path, String outputPath, String format)
        {
            return Convert(path, AAC_FORMAT, outputPath, format);
        }

        public static Boolean Convert(String path, String inputFormat, String outputPath, String outputFormat)
        {
            String ffmpeg_name = Program.FFMPEG_NAME;
            String inputCodec = FindCodec(inputFormat);
            if (inputCodec.Equals(""))
            {
                return false;
            }
            String outputCodec = FindCodec(outputFormat);
            String command = "-codec {{INCODEC}} -i \"{{INPATH}}\" -codec {{OUTCODEC}} \"{{OUTPATH}}\"";
            command = command.Replace("{{INCODEC}}", inputCodec).Replace("{{INPATH}}", path).Replace("{{OUTCODEC}}", outputCodec).Replace("{{OUTPATH}}", outputPath);
            ProcessStartInfo ffmpeg_inf = new ProcessStartInfo();
            ffmpeg_inf.CreateNoWindow = true;
            ffmpeg_inf.Arguments = command;
            ffmpeg_inf.FileName = ffmpeg_name;
            ffmpeg_inf.UseShellExecute = false;
            Console.WriteLine(ffmpeg_inf.FileName + " " + ffmpeg_inf.Arguments);
            Process ffmpeg = Process.Start(ffmpeg_inf);
            ffmpeg.WaitForExit();
            return true;
        }

        public static String FindCodec(String format)
        {
            if (FFMPEG_SUPPORTED_FORMATS.Contains(format))
            {
                return format;
            }
            if (FFMPEG_CODEC_FILE_EXTENSION.ContainsKey(format))
            {
                return FFMPEG_CODEC_FILE_EXTENSION[format];
            }
            return "";
        }
    }
}
