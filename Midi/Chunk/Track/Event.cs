using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiViewer.Midi.Chunk.Track
{
    abstract class Event
    {
        public EventType eventType { get; private set; }

        public Event(EventType eventType)
        {
            this.eventType = eventType;
        }

        public abstract string GetTextRepresentation();

        public enum EventType
        {
            MIDI,
            SYSEX,
            META,
            CONTROLLER
        }

        public enum DataType
        {
            TEXT,
            BYTE,
            NUMBER,
            OTHER
        }

        private static MidiEvent sampleMidi = new MidiEvent();

        public static Event? BuildEventFromStatusByte(byte statusByte)
        {
            if (statusByte == 0xFF)
            {
                return new MetaEvent();
            }
            else if (statusByte == 0xF0 || statusByte == 0xF7)
            {
                //SysEx Event
                return null;
            }
            else
            {
                return new MidiEvent();
            }
        }
    }
}
