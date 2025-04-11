using System.Diagnostics;
using AutoActions.Displays;
using IniParser.Model;

namespace Living_Room_PC_Utility
{
    public partial class FormTest : Form
    {
        private SurroundSoundDetector detector = new SurroundSoundDetector();
        private Dictionary<string, bool> results;
        private int surroundFormatDetected = -1;
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

            if (this.InvokeRequired)
            {
                this.Invoke(new Action<int>(setButtonAndLabelStatus), status);
                return;
            }

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
                labelResults.Text = "Detecting active channels...";
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
            startTestThread();
        }

        public void startTestThread()
        {
            Thread thread = new Thread(startTest);
            thread.Start();
        }

        private void startTest()
        {

            GlobalConfig globalConfig = GlobalConfig.GetGlobalConfigFileData();
            AudioSetter.SetSurround(globalConfig.SurroundSoundSetting);

            //Set HDR if needed
            if (this.isTestingHdr)
            {
                HDRController.SetGlobalHDRState(true);
            }

            //wait for audio settings to update before starting test
            Thread.Sleep(500);

            this.setButtonAndLabelStatus(2);

            results = detector.DetectActiveChannels();
            this.ProcessResults(results);

        }

        private void ProcessResults(Dictionary<string, bool> results)
        {

            if (this.InvokeRequired)
            {
                this.Invoke(new Action<Dictionary<string, bool>>(ProcessResults), results);
                return;
            }

            this.setButtonAndLabelStatus(3);

            comboBoxHdr.Text = (this.isTestingHdr) ? "Enabled" : "Disabled";

            int surroundFormatDetected = this.calculateSurroundFormat(results);
            comboBoxSoundMode.Text = ProgramConfig.getFriendlyNameForSetting("SurroundSoundSetting", surroundFormatDetected.ToString());

            Dictionary<string, string> recentPrograms = RecentPrograms.GetRecentProgramsDictionary();
            //Add recent programs in reverse order
            int count = 0;
            int maxPrograms = 50; //limit to 50 programs max
            foreach (var prog in recentPrograms.Reverse())
            {

                if (count > maxPrograms)
                {
                    break;
                }

                //Add program name (.exe file) if it's not blank
                if(prog.Key != "")
                {
                    comboBoxProgram.Items.Add(prog.Key);
                }

                //Add program title if it's not blank
                if (prog.Value != "")
                {   
                    comboBoxProgram.Items.Add(prog.Value);
                }
                
                count++;
            }

            labelResults.Text = "";
            foreach (var result in results)
            {
                labelResults.Text += result.Key + ": " + result.Value + "\n";
            }

            //this.parentForm.ShowBaloonTip("hello", true);

        }

        private int calculateSurroundFormat(Dictionary<string, bool> results)
        {
            bool hasRearLeft = results.ContainsKey("Rear Left") && results["Rear Left"];
            bool hasRearRight = results.ContainsKey("Rear Right") && results["Rear Right"];
            bool hasSideLeft = results.ContainsKey("Side Left") && results["Side Left"];
            bool hasSideRight = results.ContainsKey("Side Right") && results["Side Right"];

            if (results.Count == 8 && (hasRearLeft || hasRearRight) && (hasSideLeft || hasSideRight))
            {
                return 2; //7.1 Surround
            }
            else if (results.Count >= 6 && (hasRearLeft || hasRearRight || hasSideLeft || hasSideRight))
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

            IniData programConfigListUser = IniHelper.GetIniFileData(IniNames.ProgramConfigListUser);

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

                //todo save delay and volume?

                string title = "Saving program: " + selectedProgram + ".";
                string text = tempProgConfig.toFriendlyStringConcise();
                parentForm.ShowBaloonTip(title, text);

                ProgramConfig.UpdateProgramConfigListUser(selectedProgram, tempProgConfig);

                //todo refresh? //todo what do to if program is no longer found? 
                
            }

            //refresh configs on main form
            parentForm.LoadAndSetProgramConfigs();

            //refresh to account for changes
            parentForm.TrySetActiveProgramOnDemand();

            this.setButtonAndLabelStatus(1);
        }

        //Cancel Button
        private void buttonCancel_Click(object sender, EventArgs e)
        {

            parentForm.SetDefaultSoundDisplaySettings();

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
        public void SetIsTestingHdr(bool status)
        {
            comboBoxTestHdr.Text = (status) ? "Yes" : "No";
            this.isTestingHdr = status;
            
        }

    }
}
