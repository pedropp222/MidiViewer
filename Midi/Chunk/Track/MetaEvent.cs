using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiViewer.Midi.Chunk.Track
{
    internal class MetaEvent : Event
    {
        public byte type;
        public byte length;
        public byte[] data;

        public MetaEvent() : base(EventType.META)
        {
        }      

        public override string GetTextRepresentation()
        {
            return "Type: " + GetTypeValue() + ", length: " + length + ", data: " + FormatData();
        }
        private string GetDataValue()
        {
            return Encoding.UTF8.GetString(data);
        }
        private string GetTypeValue()
        {
            switch (type)
            {
                case 0x00:
                    return "Sequence Number";
                case 0x01:
                    return "Text";
                case 0x02:
                    return "Copyright";
                case 0x03:
                    return "Sequence / Track Name";
                case 0x04:
                    return "Instrument Name";
                case 0x05:
                    return "Lyric";
                case 0x06:
                    return "Marker";
                case 0x07:
                    return "Cue Point";
                case 0x08:
                    return "Program Name";
                case 0x09:
                    return "Device Name";
                case 0x20:
                    return "MIDI Channel Prefix";
                case 0x21:
                    return "MIDI Port";
                case 0x2F:
                    return "End of Track";
                case 0x51:
                    return "Tempo";
                case 0x54:
                    return "SMPTE Offset";
                case 0x58:
                    return "Time Signature";
                case 0x59:
                    return "Key Signature";
                case 0x7F:
                    return "Sequencer Specific Event";
                default:
                    return "Unknown META Event Type";
            }
        }

        private string FormatData()
        {
            switch (type)
            {
                //tempo
                case 0x51:
                    {
                        byte[] clone = (byte[])data.Clone();
                        byte[] rev = { 0x00, clone[0], clone[1], clone[2] };
                        Array.Reverse(rev);
                        return "Microseconds per quarter note: "+BitConverter.ToInt32(rev);
                    }
                //time signature
                case 0x58:
                    {
                        return "Numerator: " + data[0] + ", denominator: " + DenominatorToNoteName(data[1]) + ", MIDI clocks: " + data[2] + ", 32nd notes in 24 midi clocks: " + data[3];
                    }
                //key signature
                case 0x59:
                    {
                        return ByteToKeySignature(data[0])+", " + ByteToMode(data[1]);
                    }
                default:
                    return GetDataValue();
            }
        }

        private string ByteToKeySignature(byte num)
        {
            if (num == 0)
            {
                return "No sharps or flats";
            }
            else if (num > 127)
            {
                return Math.Abs(num-256) + " flats";
            }
            else
            {
                return num + " sharps";
            }
        }

        private string ByteToMode(byte num)
        {
            return num == 0 ? "Major" : num == 1 ? "Minor" : "Unspecified Mode " + num;
        }

        private string DenominatorToNoteName(byte value)
        {
            switch (value)
            {
                case 0x00:
                    return "Whole Notes";
                case 0x01:
                    return "Half Notes";
                case 0x02:
                    return "Quarter Notes";
                case 0x03:
                    return "Eighth Notes";
                default:
                    return Math.Pow(2, value) + "th Notes";
            }
        }
    }

}
