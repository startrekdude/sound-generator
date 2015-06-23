using System;
using com.leff.midi.@event;

namespace SoundGenerator.RandomMIDI
{
    public class RandomNote
    {
        public static MidiEvent[] GetEvents(ushort noteNum)
        {
            MidiEvent[] events = new MidiEvent[2];
            Random rand = new Random();
            int baseLength = rand.Next(100, 183);
            int pitch = rand.Next(20, 108);
            int velocity = rand.Next(80, 121);
            NoteOn on = new NoteOn(baseLength * noteNum, 0, pitch, velocity);
            double addLenMultipl = rand.Next(1, 5);
            double addLenMuliplMin = NextDouble(0.25, 0.75, rand);
            double fAddLenMulipl = addLenMultipl - addLenMuliplMin;
            velocity = rand.Next(80, 121);
            NoteOff off = new NoteOff(Convert.ToInt64(baseLength * fAddLenMulipl) * noteNum, 0, pitch, velocity);
            events[0] = on;
            events[1] = off;
            return events;
        }

        internal static double NextDouble(double minimum, double maximum, Random random)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
