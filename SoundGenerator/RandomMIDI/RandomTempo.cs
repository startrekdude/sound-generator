using System;
using com.leff.midi.@event.meta;

namespace SoundGenerator.RandomMIDI
{
    public class RandomTempo
    {
        public static Tempo GetTempo()
        {
            Tempo tmp = new Tempo();
            Random rand = new Random();
            int bpm = rand.Next(80, 203);
            tmp.setBpm(bpm);
            return tmp;
        }
    }
}
