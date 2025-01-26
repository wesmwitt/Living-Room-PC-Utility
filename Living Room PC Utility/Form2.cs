﻿using System.Diagnostics;
using AutoActions.Displays;
using IniParser;
using IniParser.Model;
using Microsoft.VisualBasic;

namespace Living_Room_PC_Utility
{
    public partial class FormTest : Form
    {
        private SurroundSoundDetector detector = new SurroundSoundDetector();
        private Dictionary<string, bool> results;
        private int surroundFormatDetected = -1;
        FileIniDataParser parser = new FileIniDataParser();
        bool isTestingHdr = false;
        Form1 parentForm;

        public FormTest(Form1 parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
        }

        //Startup
        private void FormTest_Load(object sender, EventArgs e)
        {
            comboBoxTestHdr.SelectedIndex = 0;
        }

        private void setButtonAndLabelStatus(int status)
        {
            //1 = Default
            if (status == 1)
            {
                buttonStartTest.Enabled = true;
                buttonStopTest.Enabled = false;
                buttonCancel.Enabled = false;
                buttonSave.Enabled = false;
                comboBoxProgram.Items.Clear();
                comboBoxProgram.SelectedIndex = -1; //Select None
                comboBoxSoundMode.SelectedIndex = -1; //Select None
                comboBoxHdr.SelectedIndex = -1; //Select None
                comboBoxProgram.Enabled = false;
                comboBoxSoundMode.Enabled = false;
                comboBoxTestHdr.Enabled = true;
                comboBoxHdr.Enabled = false;
                labelResults.Text = "Press Start to begin...";
            }
            //2 = Testing
            if (status == 2)
            {
                buttonStartTest.Enabled = false;
                buttonStopTest.Enabled = true;
                comboBoxTestHdr.Enabled = false;
            }
            //3 = Test Complete
            if (status == 3)
            {
                buttonStartTest.Enabled = false;
                buttonStopTest.Enabled = false;
                buttonCancel.Enabled = true;
                comboBoxProgram.Enabled = true;
                comboBoxSoundMode.Enabled = true;
                comboBoxHdr.Enabled = true;
                comboBoxTestHdr.Enabled = false;
            }
            //All required fields filled
            if (status == 4)
            {
                buttonSave.Enabled = true;
                buttonCancel.Enabled = true;
            }
        }

        private void buttonStartTest_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(startTest);
            thread.Start();
        }

        private void startTest()
        {

            //TODO set surround to max level
            IniData config = parser.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), @"data\Config.ini"));
            AudioSetter.SetSurround(Int32.Parse(config["Settings"]["surroundType"]));

            //Set HDR if needed
            if (this.isTestingHdr)
            {
                HDRController.SetGlobalHDRState(true);
            }

            //wait for audio settings to update before starting test
            Thread.Sleep(500);

            this.setButtonAndLabelStatus(2);
            labelResults.Text = "Detecting active channels...";

            results = detector.DetectActiveChannels();
            this.ProcessResults(results);

        }

        private void ProcessResults(Dictionary<string, bool> results)
        {

            this.setButtonAndLabelStatus(3);

            comboBoxHdr.Text = (this.isTestingHdr) ? "Enabled" : "Disabled";

            int surroundFormatDetected = this.calculateSurroundFormat(results);
            comboBoxSoundMode.Text = ProgramConfig.getFriendlyNameForSetting("SurroundSoundSetting", surroundFormatDetected.ToString());


            Process[] activeProcesses = Process.GetProcesses();
            foreach (var process in activeProcesses)
            {
                if (process.MainWindowTitle != "")
                {   
                    comboBoxProgram.Items.Add(process.ProcessName + ".exe");
                    comboBoxProgram.Items.Add(process.MainWindowTitle);
                }
            }

            labelResults.Text = "";
            foreach (var result in results)
            {
                labelResults.Text += result.Key + ": " + result.Value + "\n";
            }

            this.parentForm.ShowBaloonTip("hello", true);

        }

        private int calculateSurroundFormat(Dictionary<string, bool> results)
        {
            if (results.Count == 8 && (results["Rear Left"] || results["Rear Right"]) && (results["Side Left"] || results["Side Right"]))
            {
                return 2; //7.1 Surround
            }
            else if (results.Count >= 6 && (results["Rear Left"] || results["Rear Right"] || results["Side Left"] || results["Side Right"]))
            {
                return 1; //5.1 Surround
            }
            return 0; //Stereo
        }

        private void buttonStopTest_Click(object sender, EventArgs e)
        {
            detector.StopDetection();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            detector.StopDetection();
            base.OnFormClosing(e);
        }

        //save button
        private void buttonSave_Click(object sender, EventArgs e)
        {

            var path = Path.Combine(Directory.GetCurrentDirectory(), @"data\ProgramConfigListUser.ini");
            IniData programConfigListUser = parser.ReadFile(path);

            string selectedProgram = comboBoxProgram.SelectedItem.ToString();
            string selectedSurround = comboBoxSoundMode.SelectedItem.ToString();

            if (selectedProgram != null)
            {

                var tempProgConfig = new ProgramConfig();


                var surroundSoundSetting = comboBoxSoundMode.SelectedItem;
                if (surroundSoundSetting != null)
                {
                    tempProgConfig.SurroundSoundSetting = ProgramConfig.getSettingForFriendlyName("SurroundSoundSetting", surroundSoundSetting.ToString());
                }

                var hdrSetting = comboBoxHdr.SelectedItem;
                if (hdrSetting != null)
                {
                    tempProgConfig.HDRSetting = ProgramConfig.getSettingForFriendlyName("HDRSetting", hdrSetting.ToString());
                }

                parentForm.ShowBaloonTip(tempProgConfig.toFriendlyString(), true);

                ProgramConfig.UpdateProgramConfigListUser(selectedProgram, tempProgConfig);
                
            }

            //refresh configs on main form
            parentForm.SetProgramConfigs();

            this.setButtonAndLabelStatus(1);
        }

        //Cancel Button
        private void buttonCancel_Click(object sender, EventArgs e)
        {

            parentForm.SetDefaultSoundDisplaySettings();

            //TODO reset form

            this.setButtonAndLabelStatus(1);

        }

        private void comboBoxProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.checkForCompletion();
        }

        private void comboBoxHdr_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.checkForCompletion();
        }

        private void checkForCompletion()
        {
            if (comboBoxProgram.SelectedIndex != -1
                && comboBoxSoundMode.SelectedIndex != -1
                && comboBoxHdr.SelectedIndex != -1)
            {
                this.setButtonAndLabelStatus(4);
            }
        }

        private void comboBoxTestHdr_SelectedIndexChanged(object sender, EventArgs e)
        {

            ComboBox comboBox = sender as ComboBox;

            if (comboBox != null)
            {
                // Get the selected item's value
                string selectedItem = comboBox.SelectedItem.ToString();

                if(selectedItem == "Yes")
                {
                    this.isTestingHdr = true;
                }

                // Display the selected item
                //MessageBox.Show($"Selected Item: {selectedItem}");
            }


        }
    }
}
