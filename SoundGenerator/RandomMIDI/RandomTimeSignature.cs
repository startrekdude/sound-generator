using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.leff.midi.@event.meta;

namespace SoundGenerator.RandomMIDI
{
    public class RandomTimeSignature
    {
        readonly static TimeSignature FourFour = RandomTimeSignature.NewTimeSignature(4, 4);
        readonly static TimeSignature ThreeFour = RandomTimeSignature.NewTimeSignature(3, 4);
        readonly static TimeSignature SixEight = RandomTimeSignature.NewTimeSignature(6, 8);

        public static TimeSignature GetTimeSignature()
        {
            Random rand = new Random();
            int select = rand.Next(0, 4);
            TimeSignature selection = null;
            switch (select)
            {
                case 1: selection = RandomTimeSignature.FourFour;
                    break;
                case 2: selection = RandomTimeSignature.ThreeFour;
                    break;
                case 3: selection = RandomTimeSignature.SixEight;
                    break;
            }
            return selection;
        }

        internal static TimeSignature NewTimeSignature (int top, int bottom)
        {
            TimeSignature sig = new TimeSignature();
            sig.setTimeSignature(top, bottom, TimeSignature.DEFAULT_METER, TimeSignature.DEFAULT_DIVISION);
            return sig;
        }
    }
}
