using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoundGenerator.WaveCreator;

namespace SoundGenerator.RandomWave
{
    public class RandomPcmGenerator
    {
        WaveSampleType rate;
        ushort seconds;
        Random random;

        public RandomPcmGenerator(WaveSampleType rate, ushort seconds)
        {
            Console.WriteLine("Creating randomized PCM Synthesizer.");
            this.rate = rate;
            this.seconds = seconds;
            Console.WriteLine("Generating Seeded Random");
            this.random = this.GenerateRandom();
        }

        public short[] GeneratePcm()
        {
            Console.WriteLine("Generating " + (uint)this.rate * this.seconds + " Int16 of Randomized PCM data.");
            ushort counter = this.seconds;
            List<short[]> inst = new List<short[]>((int) this.seconds);
            while (counter != 0)
            {
                short[] second = this.GenerateSingleSecond();
                inst.Add(second);
                counter -= 1;
            }
            short[] retrVal = new short[0];
            foreach (short[] arr in inst)
            {
                retrVal = retrVal.Concat(arr).ToArray();
            }
            Console.WriteLine("Generation complete " + (uint)this.rate * this.seconds * 2 + " bytes of PCM Data Generated."); 
            return retrVal;
        }

        internal short[] GenerateSingleSecond()
        {
            Console.WriteLine("Generating ~ a single second of PCM Data. Generating " + (uint)this.rate + "units of PCM data.");
            short[] arr = new short[(int) this.rate + 1];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = (short) this.random.Next(Int16.MinValue, Int16.MaxValue);
            }
            return arr;
        }

        internal Random GenerateRandom()
        {
            return new Random(new Random().Next());
        }
    }
}
