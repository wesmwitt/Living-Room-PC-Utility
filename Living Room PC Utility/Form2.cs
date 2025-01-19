using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Utils;

namespace Living_Room_PC_Utility
{
    public partial class Form2 : Form
    {
        private SurroundSoundDetector detector = new SurroundSoundDetector();
        private Dictionary<string, bool> results;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(startTest);
            thread.Start();
        }

        private void startTest()
        {
            label1.Text = "";
            results = detector.DetectActiveChannels();
            foreach (var result in results)
            {
                label1.Text += result.Key + ": " + result.Value + "\n";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            detector.StopDetection();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
