using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiViewer.Midi.Header
{
    internal class HeaderChunk
    {
        public int chunkLength;
        public short format;
        public short nTracks;
        public short tickDiv;
        public int timingInterval;
        public int subdivisionCount;
    }
}
