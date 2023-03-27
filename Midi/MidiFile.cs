using MidiViewer.Midi.Chunk;
using MidiViewer.Midi.Header;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiViewer.Midi
{
    internal class MidiFile
    {
        public HeaderChunk headerChunk;
        public List<TrackChunk> trackChunks;

        public MidiFile()
        {
            trackChunks = new List<TrackChunk>();
        }
    }
}
