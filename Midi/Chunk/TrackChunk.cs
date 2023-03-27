using MidiViewer.Midi.Chunk.Track;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiViewer.Midi.Chunk
{
    internal class TrackChunk
    {
        public List<Event> events;
        public TrackChunk()
        {
            events = new List<Event>();
        }
    }
}
