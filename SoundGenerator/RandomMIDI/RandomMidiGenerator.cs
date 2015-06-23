using jio = java.io;
using com.leff.midi;

namespace SoundGenerator.RandomMIDI
{
    public class RandomMidiGenerator
    {
        public static void MakeMidiFile(jio.File file, ushort noteNum)
        {
            MidiFile midFil = GetMidiFile(noteNum);
            midFil.writeToFile(file);
        }

        public static MidiFile GetMidiFile(ushort noteNum)
        {
            MidiFile midFil = new MidiFile();
            MidiTrack tempTrk = RandomTempoTrack.GetTempoTrack();
            MidiTrack musicTrk = RandomMusicTrack.GetMusicTrack(noteNum);
            midFil.addTrack(musicTrk, 1);
            midFil.addTrack(tempTrk, 0);
            midFil.setResolution(MidiFile.DEFAULT_RESOLUTION);
            return midFil;
        }
    }
}
