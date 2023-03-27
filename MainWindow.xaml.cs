using Microsoft.Win32;
using MidiViewer.Log;
using MidiViewer.Midi;
using MidiViewer.Midi.Chunk.Track;
using MidiViewer.Processor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MidiViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenMidiButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "MIDI files (*.mid)|*.mid";
            op.FileOk += (sender, e) =>
            {
                ProcessFile(op.FileName);
            };
            op.ShowDialog();
        }

        private void ProcessFile(string file)
        {
            MidiFile? midi = MidiProcessor.ProcessFile(file);
            AddToText(Logger.Instance().DumpLogs());

            if (midi != null)
            {
                midiPathText.Text = "File Path: "+file;
                PrintNoteCount(midi);
            }
            else
            {
                fileInfoText.AppendText("Did not process file correctly. Information is unavailable.\n");
            }
        }

        private void PrintNoteCount(MidiFile midi)
        {
            fileInfoText.AppendText("Note count: " + MidiUtils.NoteCount(midi));
        }

        private void AddToText(string[] lines)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var line in lines)
            {
                sb.AppendLine(line);
            }
            midiLoadingDebugText.Text = sb.ToString();
        }
    }
}
