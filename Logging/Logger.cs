using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiViewer.Logging
{
    public class Logger
    {
        private List<string> logs = new List<string>();

        private static Logger? _logger;

        public static Logger Instance()
        {
            if (_logger == null)
            {
                _logger = new Logger();
            }

            return _logger;
        }

        public void Log(string message)
        {
            logs.Add(message);
        }

        public string[] DumpLogs()
        {
            string[] lgs = logs.ToArray();
            logs.Clear();
            return lgs;
        }
    }
}
