using MidiViewer.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BufferIO;
using MidiViewer.Midi;
using MidiViewer.Midi.Header;
using System.Security.Cryptography;
using MidiViewer.Midi.Chunk;
using System.Windows.Controls.Primitives;

namespace MidiViewer.Processor
{
    internal class MidiProcessor
    {
        public static MidiFile? ProcessFile(string file)
        {
            MidiFile mid = new MidiFile();

            if (!file.EndsWith(".mid"))
            {
                Logger.Instance().Log("Error. The opened file is not a .mid file.");
                return null;
            }

            ByteBuffer reader = new ByteBuffer(File.ReadAllBytes(file));

            HeaderChunk? pr = HeaderChunkReader.Read(reader);

            if (pr == null)
            {
                Logger.Instance().Log("Error. Processing stopped because of invalid header chunk");
                return null;
            }

            mid.headerChunk = pr;

            int count = 1;

            while(reader.Position < reader.Length-1)
            {
                Logger.Instance().Log("Reading track "+count+"/"+ pr.nTracks);
                TrackChunk? tc = TrackChunkReader.Read(reader);
                if (tc == null)
                {
                    Logger.Instance().Log("Error. Processing stopped because of invalid track chunk");
                    return mid;
                }
                mid.trackChunks.Add(tc);
                count++;
            }

            return mid;
        }
        
    }
}
