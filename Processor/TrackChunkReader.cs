using BufferIO;
using MidiViewer.Logging;
using MidiViewer.Midi.Chunk;
using MidiViewer.Midi.Chunk.Track;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiViewer.Processor
{
    internal class TrackChunkReader
    {
        private static byte[] midiTrackHeader = { 77, 84, 114, 107 };

        public static TrackChunk? Read(ByteBuffer reader)
        {
            TrackChunk chunk = new TrackChunk();

            byte[] track = reader.Read(4);

            if (!VerifyByteArray(track, midiTrackHeader))
            {
                Logger.Instance().Log("Track header is not correct");
                return null;
            }

            Logger.Instance().Log("Track header is correct");

            int trackLength = reader.ReadInt();

            Logger.Instance().Log("Track length: " + trackLength);        

            int tracker = 0;

            byte lastStatus = 0x00;
            int lastChannel = -1;

            while(tracker < trackLength-1)
            {
                List<byte> deltaTimes = ReadDeltaTime(reader);
                tracker+=deltaTimes.Count;

                int deltaSum = 0;

                for(int i = 0; i < deltaTimes.Count; i++)
                {
                    deltaSum &= 0x7F;

                    deltaSum = (deltaSum << 7) + (deltaTimes[i] & 0x7F);
                }

                Logger.Instance().Log("Delta time: " + deltaSum);

                byte eventType = reader.ReadByte();
                tracker++;

                Event? ev = Event.BuildEventFromStatusByte(eventType);
                if (ev == null)
                {
                    Logger.Instance().Log("Error. Event type of "+eventType+" is not supported");
                    break;
                }

                if (ev is MetaEvent)
                {
                    byte type = reader.ReadByte();
                    tracker++;

                    byte length = reader.ReadByte();
                    tracker++;

                    byte[] data = reader.Read(length);
                    tracker += length;

                    ((MetaEvent)ev).type = type;
                    ((MetaEvent)ev).length = length;
                    ((MetaEvent)ev).data = data;

                }
                else if (ev is MidiEvent)
                {
                    //normal event
                    if (eventType > 127)
                    {
                        byte statusNibble = (byte)(eventType >> 4);

                        lastStatus = statusNibble;

                        byte b = (byte)(eventType << 4);

                        int midiChannel = b >> 4;

                        lastChannel = midiChannel;

                        byte value1 = reader.ReadByte();
                        tracker++;                      

                        byte data = 0x00;
                        if (statusNibble != 0x0C && statusNibble != 0x0D)
                        {
                            data = reader.ReadByte();
                            tracker++;
                        }

                        ((MidiEvent)ev).messageTypeNibble = statusNibble;
                        ((MidiEvent)ev).midiChannel = midiChannel;
                        ((MidiEvent)ev).value1 = value1;
                        ((MidiEvent)ev).data = data;
                    }
                    //optimization was done, so status is not specified (occurs in Bn status)
                    else
                    {
                        ((MidiEvent)ev).messageTypeNibble = lastStatus;
                        ((MidiEvent)ev).midiChannel = lastChannel;
                        

                        ((MidiEvent)ev).value1 = eventType;

                        byte data = reader.ReadByte();
                        tracker++;

                        //is last was note on and velocity value is 0, assume note off
                        if (data == 0x00 && lastStatus == 0x09)
                        {
                            ((MidiEvent)ev).messageTypeNibble = 0x08;
                        }

                        ((MidiEvent)ev).data = data;
                    }

                    
                }
                else
                {
                    break;
                }

                Logger.Instance().Log(ev.GetTextRepresentation());
                chunk.events.Add(ev);
            }



            return chunk;
        }

        private static List<byte> ReadDeltaTime(ByteBuffer reader)
        {
            List<byte> readAmount = new List<byte>();

            byte v = 0x00;
            do
            {
                v = reader.ReadByte();
                readAmount.Add(v);
            }
            while (v >> 7 == 1);

            return readAmount;
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
