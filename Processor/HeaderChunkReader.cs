using BufferIO;
using MidiViewer.Log;
using MidiViewer.Midi.Header;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiViewer.Processor
{
    internal class HeaderChunkReader
    {
        private static byte[] midiStarterHeader = { 77, 84, 104, 100 };

        public static HeaderChunk? Read(ByteBuffer reader)
        {
            HeaderChunk header;

            byte[] hd = reader.Read(4);

            if (!VerifyByteArray(hd, midiStarterHeader))
            {
                Logger.Instance().Log("The starter header chunk is incorrect");
                return null;
            }
            Logger.Instance().Log("The starter header chunk is correct");

            int chunkLength = reader.ReadInt();

            Logger.Instance().Log("Chunk length: " + chunkLength);

            short format = reader.ReadShort();

            Logger.Instance().Log("Midi format: " + format);

            short nTracks = reader.ReadShort();

            Logger.Instance().Log("Number of tracks: " + nTracks);

            short tickDiv = reader.ReadShort();

            int timingInterval = tickDiv >> 15;

            Logger.Instance().Log("Timing interval flag: " + timingInterval);

            if (timingInterval == 1)
            {
                Logger.Instance().Log("Timing interval of 1 is not supported");
                return null;
            }

            int subdivisionCount = tickDiv;

            Logger.Instance().Log("Subdivision count: " + subdivisionCount);

            header = new HeaderChunk
            {
                chunkLength = chunkLength,
                format = format,
                nTracks = nTracks,
                tickDiv = tickDiv,
                timingInterval = timingInterval,
                subdivisionCount = subdivisionCount
            };

            return header;
        }

        private static bool VerifyByteArray(byte[] array, byte[] compArray)
        {
            if (array.Length != compArray.Length)
            {
                return false;
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != compArray[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
