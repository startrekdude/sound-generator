using System;
using System.IO;
using SoundGenerator.WaveCreator;
using SoundGenerator.RandomWave;

namespace SoundGenerator.AACEncoder
{
    public class RandomAACGenerator
    {
        RandomWaveGenerator gen;
        String fname;
        WaveSampleType type;

        public RandomAACGenerator(WaveSampleType rate, ushort seconds, String fileName)
        {
            Console.WriteLine("Creating RandomWaveGenerator with arguments rate and seconds...");
            this.gen = new RandomWaveGenerator(rate, seconds);
            this.type = rate;
            this.fname = fileName;
        }

        public void GenerateAndSave()
        {
            Console.WriteLine("Populating RandomWaveGenerator");
            gen.Generate();
            Console.WriteLine("Proxying RandomWaveGenerator Output to \"temp.wav\"");
            gen.Save("temp.wav");
            Console.WriteLine("Encoding \"temp.wav\" as " + this.fname);
            AACEncoder.AACEncode("temp.wav", this.fname, this.type);
            Console.WriteLine("Invoking System.IO.File.Delete to delete \"temp.wav\".");
            File.Delete("temp.wav");
            Console.WriteLine("Operation Completed Sucessfully");
        }
    }
}
