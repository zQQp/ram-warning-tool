using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Diagnostics;

namespace RamWarningTool
{
    public partial class Form1 : Form
    {

        public static PerformanceCounter ramInfo = new PerformanceCounter();

        public Form1()
        {
            InitializeComponent();
            ramInfo.CounterName = "% Committed Bytes In Use";
            ramInfo.CategoryName = "Memory";
            pullData();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pullData();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("RAM Warning Tool by jens@nierha.us\r\nChecking interval: 10 seconds.");
        }

        public void pullData()
        {
            int ramPercent = (int)(ramInfo.NextValue());
            progressBar1.Value = ramPercent;
            label2.Text = ramPercent + "%";

            if (ramPercent > 90 && checkBox1.Checked)
            {
                //speakWarning();
                Task.Factory.StartNew(speakWarning);
            }

        }

        private static void speakWarning()
        {
            // Initialize a new instance of the SpeechSynthesizer.
            SpeechSynthesizer synth = new SpeechSynthesizer();
        
            // Configure the audio output. 
            synth.SetOutputToDefaultAudioDevice();

            // Speak a string.
            synth.Speak("Warning, your RAM has reached over 90% usage!");

        }

        private void testWarningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //speakWarning();
            Task.Factory.StartNew(speakWarning);
        }

    }
}
