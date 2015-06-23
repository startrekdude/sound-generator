using System;
using System.IO;
using SoundGenerator.WaveCreator;

namespace SoundGenerator.RandomWave
{
    public class RandomWaveGenerator
    {
        WaveSampleType rate;
        ushort seconds;
        WaveGenerator generator;

        public RandomWaveGenerator(WaveSampleType rate, ushort seconds)
        {
            Console.WriteLine("Synthesizing RandomWaveGenerator with WaveGenerator.");
            this.rate = rate;
            this.seconds = seconds;
            this.generator = new WaveGenerator(1, this.rate);
        }

        public void Generate()
        {
            Console.WriteLine("Synthesizing randomized PCM Data as Int16 Array.");
            RandomPcmGenerator gen = new RandomPcmGenerator(this.rate, this.seconds);
            short[] data = gen.GeneratePcm();
            Console.WriteLine("Retrieved randomized PCM data, beginning population of Wave.");
            this.generator.Generate(data);
        }

        public void Save(String name)
        {
            Console.WriteLine("Creating FileStream to \"" + name + "\".");
            FileStream fstream = new FileStream(name, FileMode.Create);
            Console.WriteLine("Serializing WaveGenerator Data to FileStream.");
            this.generator.Save(fstream);
            Console.WriteLine("Cleaning up file handles and closing FileStream.");
            fstream.Dispose();
        }
    }
}
