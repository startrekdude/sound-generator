using System;
using System.IO;
using jio = java.io;
using SoundGenerator.WaveCreator;
using SoundGenerator.FileConverter;
using SoundGenerator.AACEncoder;
using SoundGenerator.RandomMIDI;

namespace SoundGenerator.CommandREPL
{
    public class SoundShell : CommandShell
    {
        public SoundShell()
            : base()
        {
            BGColor = ConsoleColor.Blue;
            Title = "SoundShell v1.0";
            TopMsg = ".NET SoundShell v1.0\nMAKE SURE YOU USE THE EXIT COMMAND TO CLOSE THIS\nA simple utility to create random Sound\nType \"lst\" for help.\n\n";
            Object[] randSndRes = new Object[] 
            {
                ResourceExtractor.ReadResourceAsString("SoundGenerator.RandomSoundHelpFile.txt").Trim(new char[] { '?' }),
                "All checks completed successfully, Starting random generation process\n"
            };
            Command randSnd = new Command(new CommandDelegate(RandomSoundCommand), "Generate Random Sound with this command.", randSndRes);
            Commands.Add("random-sound", randSnd);
            Object[] filConvrtRes = new Object[]
            {
                ResourceExtractor.ReadResourceAsString("SoundGenerator.ConvertAACHelpFile.txt").Trim(new char[] { '?' })
            };
            Command filConvrt = new Command(new CommandDelegate(ConvertAacCommand), "Convert the aac format to other formats with this command.", filConvrtRes);
            Commands.Add("convert-aac", filConvrt);
            Object[] randMidRes = new Object[]
            {
                ResourceExtractor.ReadResourceAsString("SoundGenerator.RandomMidiHelpFile.txt").Trim(new char[] { '?' } ),
                "This command is experimental and should be treated with caution.",
                "Starting experimental MIDI File Creation"
            };
            Command randMid = new Command(new CommandDelegate(RandomMidiCommand), "[EXPERIMENTAL] Generate a Random Midi File with this command.", randMidRes);
            Commands.Add("random-midi-experimental", randMid);
        }

        public void RandomMidiCommand(String[] argv, CommandShell cmd, Command com)
        {
            Console.WriteLine("Random MIDI Command. "+com.Data[1]);
            if (argv.Length < 2)
            {
                Console.Error.Write("Fail: Wrong Number of Arguments\n\n" + com.Data[0]);
                return;
            }
            ushort notes;
            if (!ushort.TryParse(argv[1], out notes))
            {
                Console.Error.Write("Fail: Could not determine number of notes\n\n" + com.Data[0]);
                return;
            }
            String fileName = argv[2];
            fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "\\" + fileName + ".mid";
            jio.File file = new jio.File(fileName);
            Console.WriteLine(com.Data[2]);
            RandomMidiGenerator.MakeMidiFile(file, notes);
        }

        public void ConvertAacCommand(String[] argv, CommandShell cmd, Command com)
        {
            if (argv.Length < 3)
            {
                Console.Error.Write("Fail: Wrong Number of Arguments\n\n" + com.Data[0]);
                return;
            }
            String inputPath = argv[1];
            inputPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "\\" + inputPath + ".aac";
            if (!File.Exists(inputPath))
            {
                Console.Error.Write("Fail: Input File Does Not Exist\n\n" + com.Data[0]);
                return;
            }
            String outputPath = argv[2];
            outputPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "\\" + outputPath + "." + argv[3];
            String format = argv[3];
            Boolean success = FileConvert.ConvertAAC(inputPath, outputPath, format);
            if (success)
            {
                Console.WriteLine("The command completed sucessfully.");
            }
            else
            {
                Console.Error.Write("Fail: Format not supported\n\n" + com.Data[0]);
            }
        }

        public void RandomSoundCommand(String[] argv, CommandShell cmd, Command com)
        {
            if (argv.Length < 2)
            {
                Console.Error.Write("Fail: Wrong Number of Arguments\n\n" + com.Data[0]);
                return;
            }
            WaveSampleType sampleType;
            if (Enum.TryParse(argv[1], out sampleType) == false)
            {
                Console.Error.Write("Fail: Invalid Sample Rate\n\n" + com.Data[0]);
                return;
            }
            ushort seconds;
            if (ushort.TryParse(argv[2], out seconds) == false)
            {
                Console.Error.Write("Fail: Could not determine number of seconds\n\n" + com.Data[0]);
                return;
            }
            String fileName = "default.aac";
            if (argv.Length > 2)
            {
                String usrInput = argv[3];
                fileName = usrInput + ".aac";
            }
            fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "\\" + fileName;
            Console.Write(com.Data[1]);
            Console.WriteLine("Invoking RandomAACGenerator...");
            RandomAACGenerator gen = new RandomAACGenerator(sampleType, seconds, fileName);
            gen.GenerateAndSave();
        }
    }
}
