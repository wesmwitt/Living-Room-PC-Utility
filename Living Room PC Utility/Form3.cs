using System.Diagnostics;

namespace Living_Room_PC_Utility
{
    public partial class Form3 : Form
    {

        private Dictionary<string, ProgramConfig> programConfigs;
        private Dictionary<string, ProgramConfig> programConfigsGlobal;
        private Dictionary<string, ProgramConfig> programConfigsUser;
        private Form1 parentForm;

        public Form3(Form1 parentForm)
        {

            this.parentForm = parentForm;

            InitializeComponent();

            LoadProgramConfigs();

            SetTableFromProgramConfigs();

        }

        private void LoadProgramConfigs()
        {
            programConfigs = ProgramConfig.GetProgramConfigDictionary("both");
            programConfigsGlobal = ProgramConfig.GetProgramConfigDictionary("global");
            programConfigsUser = ProgramConfig.GetProgramConfigDictionary("user");
        }

        private void SetTableFromProgramConfigs()
        {
            dataGridView1.Rows.Clear();
            foreach (var item in this.programConfigs)
            {
                dataGridView1.Rows.Add(item.Key,
                    item.Value.getSurroundSoundFriendlyName(),
                    item.Value.getHDRFriendlyName(),
                    item.Value.getVolumeFriendlyName(),
                    item.Value.getDelayFriendlyName(),
                    (item.Value.StartupScript != "") ? "Enabled" : "",
                    (item.Value.ShutdownScript != "") ? "Enabled" : "",
                    this.programConfigsUser.ContainsKey(item.Key) ? "Yes" : ""
                    );
            }
            dataGridView1.Refresh();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            this.SetBoxesFromSelectedRow();

            buttonSave.Enabled = false;
            buttonCancel.Enabled = false;

            comboBoxSoundMode.Enabled = true;
            comboBoxHdr.Enabled = true;
            comboBoxDelay.Enabled = true;
        }

        private void SetBoxesFromSelectedRow()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                var selectedProgram = selectedRow.Cells[0].Value.ToString();

                //TODO needs to support unset configs

                if (programConfigsUser.ContainsKey(selectedProgram))
                {
                    var selectedConfigUser = programConfigsUser[selectedProgram];
                    comboBoxSoundMode.Text = selectedConfigUser.getSurroundSoundFriendlyName();
                    comboBoxHdr.Text = selectedConfigUser.getHDRFriendlyName();

                    int defaultVolume = 0;
                    if (int.TryParse(selectedConfigUser.VolumeSetting, out int defVolNum))
                    {
                        defaultVolume = defVolNum;
                    }
                    numericUpDownVolume.Value = defaultVolume;

                    comboBoxDelay.Text = selectedConfigUser.getDelayFriendlyName();

                    textBoxStartupScript.Text = selectedConfigUser.StartupScript;
                    textBoxShutdownScript.Text = selectedConfigUser.ShutdownScript;
                }
                else
                {
                    comboBoxSoundMode.Text = "";
                    comboBoxHdr.Text = "";
                    numericUpDownVolume.Value = 0;
                    comboBoxDelay.Text = "";
                    textBoxStartupScript.Text = "";
                    textBoxShutdownScript.Text = "";
                }

                if (programConfigsGlobal.ContainsKey(selectedProgram))
                {
                    var selectedConfigGlobal = programConfigsGlobal[selectedProgram];
                    comboBoxSoundModeGlobal.Text = selectedConfigGlobal.getSurroundSoundFriendlyName();
                    comboBoxHdrGlobal.Text = selectedConfigGlobal.getHDRFriendlyName();
                    comboBoxDelayGlobal.Text = selectedConfigGlobal.getDelayFriendlyName();
                    //global does not have a volume setting or startup/shutdown
                }
                else
                {
                    comboBoxSoundModeGlobal.Text = "";
                    comboBoxHdrGlobal.Text = "";
                    comboBoxDelayGlobal.Text = "";
                }

            }
        }

        private void comboBoxSoundMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetSaveCancelButtons(true);
        }

        private void comboBoxHdr_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetSaveCancelButtons(true);
        }

        private void comboBoxDelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetSaveCancelButtons(true);
        }

        private void numericUpDownDefaultVolume_ValueChanged(object sender, EventArgs e)
        {
            this.SetSaveCancelButtons(true);
        }

        private void textBoxStartupScript_TextChanged(object sender, EventArgs e)
        {
            this.SetSaveCancelButtons(true);
        }

        private void textBoxShutdownScript_TextChanged(object sender, EventArgs e)
        {
            this.SetSaveCancelButtons(true);
        }

        private void SetSaveCancelButtons(bool enable)
        {
            if (enable)
            {
                buttonSave.Enabled = true;
                buttonCancel.Enabled = true;
            }
            else
            {
                buttonSave.Enabled = false;
                buttonCancel.Enabled = false;
            }

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.SetBoxesFromSelectedRow();
            this.SetSaveCancelButtons(false);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var selectedProgram = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

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

                var delaySetting = comboBoxDelay.SelectedItem;
                if (delaySetting != null)
                {
                    tempProgConfig.DelaySetting = ProgramConfig.getSettingForFriendlyName("DelaySetting", delaySetting.ToString());
                }

                var volumeSetting = numericUpDownVolume.Value;
                if (volumeSetting != 0)
                {
                    tempProgConfig.VolumeSetting = volumeSetting.ToString();
                }

                var startupScript = textBoxStartupScript.Text;
                if (startupScript != "")
                {
                    tempProgConfig.StartupScript = startupScript;
                }

                var shutdownScript = textBoxShutdownScript.Text;
                if (shutdownScript != "")
                {
                    tempProgConfig.ShutdownScript = shutdownScript;
                }

                ProgramConfig.UpdateProgramConfigListUser(selectedProgram, tempProgConfig);
                LoadProgramConfigs();
                parentForm.LoadAndSetProgramConfigs();

                //refresh to account for changes
                parentForm.TrySetActiveProgramOnDemand();

                SetTableFromProgramConfigs();
                SetBoxesFromSelectedRow();
                this.SetSaveCancelButtons(false);

            }


        }

        private void labelVolume_Click(object sender, EventArgs e)
        {

        }

        private void buttonStartupScript_Click(object sender, EventArgs e)
        {
            SelectFile(textBoxStartupScript);
        }

        private void buttonShutdownScript_Click(object sender, EventArgs e)
        {
            SelectFile(textBoxShutdownScript);
        }

        private void SelectFile(TextBox textBox)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Select File";
            openFileDialog1.InitialDirectory = @"C:\";//--"C:\\";
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            { textBox.Text = openFileDialog1.FileName; }
        }

    }
}
