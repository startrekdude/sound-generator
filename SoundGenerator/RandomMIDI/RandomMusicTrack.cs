using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.leff.midi;
using com.leff.midi.@event;

namespace SoundGenerator.RandomMIDI
{
    public class RandomMusicTrack
    {
        public static MidiTrack GetMusicTrack (ushort notes)
        {
            MidiEvent[] noteEvents = new MidiEvent[(notes*2)];
            noteEvents = PopulateNoteEvents(notes, noteEvents);
            MidiTrack musicTrk = new MidiTrack();
            foreach (MidiEvent noteEvnt in noteEvents)
            {
                musicTrk.insertEvent(noteEvnt);
            }
            Console.WriteLine("[" + String.Join(",", musicTrk.getEvents().toArray() as object[]) + "]");
            return musicTrk;
        }

        internal static MidiEvent[] PopulateNoteEvents(ushort noteNum, MidiEvent[] events)
        {
            int counter = 0;
            ushort noteNumber = 1;
            while (counter != noteNum*2)
            {
                try
                {
                    MidiEvent[] singleNote = RandomNote.GetEvents(noteNumber);
                    events[counter] = singleNote[0];
                    counter += 1;
                    events[counter] = singleNote[1];
                    counter += 1;
                    noteNum += 1;
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }
            return events;
        }
    }
}
