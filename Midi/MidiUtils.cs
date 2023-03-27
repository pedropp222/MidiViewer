using MidiViewer.Midi.Chunk.Track;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiViewer.Midi
{
    internal class MidiUtils
    {
        public static int NoteCount(MidiFile? midi)
        {
            if (midi == null) return -1; 

            int noteCount = 0;
            foreach (var o in midi.trackChunks)
            {
                foreach (var ev in o.events)
                {
                    if (ev.eventType == Midi.Chunk.Track.Event.EventType.MIDI)
                    {
                        MidiEvent mid = (MidiEvent)ev;

                        if (mid.messageTypeNibble == 0x09)
                        {
                            noteCount++;
                        }
                    }
                }
            }
            return noteCount;
        }
    }
}
