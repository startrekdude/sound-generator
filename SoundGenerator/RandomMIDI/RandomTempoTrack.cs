using com.leff.midi;
using com.leff.midi.@event.meta;

namespace SoundGenerator.RandomMIDI
{
    public class RandomTempoTrack
    {
        public static MidiTrack GetTempoTrack()
        {
            MidiTrack tmpTrk = new MidiTrack();
            Tempo tmp = RandomTempo.GetTempo();
            TimeSignature sig = RandomTimeSignature.GetTimeSignature();
            tmpTrk.insertEvent(tmp);
            tmpTrk.insertEvent(sig);
            return tmpTrk;
        }
    }
}
