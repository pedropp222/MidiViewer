using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiViewer.Midi.Chunk.Track
{
    internal class MidiEvent : Event
    {
        public int messageTypeNibble;
        public int midiChannel;
        public byte value1;
        public byte data;

        public MidiEvent() : base(EventType.MIDI)
        {
            
        }

        public override string GetTextRepresentation()
        {
            return "MessageType: "+GetMessageType()+", Midi Channel: "+midiChannel+", "+GetValue1()+", "+GetData();
        }

        private string GetData()
        {
            switch(messageTypeNibble)
            {
                case 0x08:
                case 0x09:
                    return "Velocity: " + data;
                case 0x0A:
                    return "Pressure: " + data;
                case 0x0B:
                    return "Value: " + data;
                default:
                    return "";

            }
        }

        private string GetValue1()
        {
            switch(messageTypeNibble)
            {
                case 0x08:
                case 0x09:
                case 0x0A:
                    return "Note: " + value1;
                case 0x0B:
                    return GetController();
                case 0x0C:
                    return "Program: " + value1;
                case 0x0D:
                    return "Pressure: " + value1;
                case 0x0E:
                    return "lsb: " + value1 + "| msb: " + data;
                default:
                    return "";
            }
        }

        public string GetController()
        {
            switch (value1)
            {
                //High resolution continuous controllers (MSB)
                case 0:
                    return "Bank Select";
                case 1:
                    return "Modulation Wheel";
                case 2:
                    return "Breath Controller";
                case 4:
                    return "Foot Controller";
                case 5:
                    return "Portamento Time";
                case 6:
                    return "Data Entry";
                case 7:
                    return "Channel Volume";
                case 8:
                    return "Balance";
                case 10:
                    return "Pan";
                case 11:
                    return "Expression Controller";
                case 12:
                    return "Effect Control 1";
                case 13:
                    return "Effect Control 2";
                case 16:
                    return "Gen Purpose Controller 1";
                case 17:
                    return "Gen Purpose Controller 2";
                case 18:
                    return "Gen Purpose Controller 3";
                case 19:
                    return "Gen Purpose Controller 4";
                //High resolution continuous controllers (LSB)
                case 32:
                    return "Bank Select";
                case 33:
                    return "Modulation Wheel";
                case 34:
                    return "Breath Controller";
                case 36:
                    return "Foot Controller";
                case 37:
                    return "Portamento Time";
                case 38:
                    return "Data Entry";
                case 39:
                    return "Channel Volume";
                case 40:
                    return "Balance";
                case 42:
                    return "Pan";
                case 43:
                    return "Expression Controller";
                case 44:
                    return "Effect Control 1";
                case 45:
                    return "Effect Control 2";
                case 48:
                    return "Gen Purpose Controller 1";
                case 49:
                    return "Gen Purpose Controller 2";
                case 50:
                    return "Gen Purpose Controller 3";
                case 51:
                    return "Gen Purpose Controller 4";
                //SWITCHES
                case 64:
                    return "Sustain On/Off";
                case 65:
                    return "Portamento On/Off";
                case 66:
                    return "Sostenuto On/Off";
                case 67:
                    return "Soft Pedal On/Off";
                case 68:
                    return "Legato On/Off";
                case 69:
                    return "Hold 2 On/Off";
                //Low resolution continuous controllers
                case 70:
                    return "Sound Controller 1";
                case 71:
                    return "Sound Controller 2";
                case 72:
                    return "Sound Controller 3";
                case 73:
                    return "Sound Controller 4";
                case 74:
                    return "Sound Controller 5";
                case 75:
                    return "Sound Controller 6";
                case 76:
                    return "Sound Controller 7";
                case 77:
                    return "Sound Controller 8";
                case 78:
                    return "Sound Controller 9";
                case 79:
                    return "Sound Controller 10";
                case 80:
                    return "General Purpose Controller 5";
                case 81:
                    return "General Purpose Controller 6";
                case 82:
                    return "General Purpose Controller 7";
                case 83:
                    return "General Purpose Controller 8";
                case 84:
                    return "Portamento Control (PTC)";
                case 88:
                    return "High Resolution Velocity Prefix";
                case 91:
                    return "Effects 1 Depth (Reverb Send Level)";
                case 92:
                    return "Effects 1 Depth (Tremolo Depth)";
                case 93:
                    return "Effects 1 Depth (Chorus Send Level)";
                case 94:
                    return "Effects 1 Depth (Celeste Depth)";
                case 95:
                    return "Effects 1 Depth (Phaser Depth)";
                //RPNs / NRPNs 
                case 96:
                    return "Data Increment";
                case 97:
                    return "Data Decrement";
                case 98:
                    return "Non Registered Parameter Number (LSB)";
                case 99:
                    return "Non Registered Parameter Number (MSB)";
                case 100:
                    return "Registered Parameter Number (LSB)";
                case 101:
                    return "Registered Parameter Number (MSB)";
                //Channel Mode messages
                case 120:
                    return "All Sound Off";
                case 121:
                    return "Reset All Controllers";
                case 122:
                    return "Local Control On/Off";
                case 123:
                    return "All Notes Off";
                case 124:
                    return "Omni Mode Off";
                case 125:
                    return "Omni Mode On";
                case 126:
                    return "Mono Mode On";
                case 127:
                    return "Poly Mode On";
                default:
                    return "Undefined controller: " + value1;
            }
        }

        private string GetMessageType()
        {
            switch(messageTypeNibble)
            {
                case 0x08:
                    return "Note Off";
                case 0x09:
                    return "Note On";
                case 0x0A:
                    return "Polyphonic Pressure";
                case 0x0B:
                    return "Controller";
                case 0x0C:
                    return "Program Change";
                case 0x0D:
                    return "Channel Pressure";
                case 0x0E:
                    return "Pitch Bend";
                default:
                    return "Unknown Message Type " + messageTypeNibble;
            }
        }
    }
}
