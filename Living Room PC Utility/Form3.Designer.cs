namespace Living_Room_PC_Utility
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            ProgName = new DataGridViewTextBoxColumn();
            Sound = new DataGridViewTextBoxColumn();
            HDR = new DataGridViewTextBoxColumn();
            Volume = new DataGridViewTextBoxColumn();
            Delay = new DataGridViewTextBoxColumn();
            startupScriptEnabled = new DataGridViewTextBoxColumn();
            shutdownScriptEnabled = new DataGridViewTextBoxColumn();
            userConfigured = new DataGridViewTextBoxColumn();
            comboBoxHdr = new ComboBox();
            labelHdr = new Label();
            buttonCancel = new Button();
            buttonSave = new Button();
            comboBoxSoundMode = new ComboBox();
            labelSoundMode = new Label();
            labelDelay = new Label();
            comboBoxDelay = new ComboBox();
            labelGlobalSettings = new Label();
            labelUserSettings = new Label();
            comboBoxDelayGlobal = new ComboBox();
            label1 = new Label();
            comboBoxHdrGlobal = new ComboBox();
            label2 = new Label();
            comboBoxSoundModeGlobal = new ComboBox();
            label3 = new Label();
            labelVolume = new Label();
            numericUpDownVolume = new NumericUpDown();
            buttonShutdownScript = new Button();
            buttonStartupScript = new Button();
            textBoxShutdownScript = new TextBox();
            textBoxStartupScript = new TextBox();
            labelShutdownScript = new Label();
            labelStartupScript = new Label();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownVolume).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { ProgName, Sound, HDR, Volume, Delay, startupScriptEnabled, shutdownScriptEnabled, userConfigured });
            dataGridView1.Location = new Point(11, 11);
            dataGridView1.Margin = new Padding(2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 72;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(1386, 320);
            dataGridView1.TabIndex = 18;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
            // 
            // ProgName
            // 
            ProgName.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ProgName.HeaderText = "Name";
            ProgName.MinimumWidth = 9;
            ProgName.Name = "ProgName";
            ProgName.ReadOnly = true;
            ProgName.Width = 95;
            // 
            // Sound
            // 
            Sound.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Sound.HeaderText = "Sound";
            Sound.MinimumWidth = 9;
            Sound.Name = "Sound";
            Sound.ReadOnly = true;
            // 
            // HDR
            // 
            HDR.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            HDR.HeaderText = "HDR";
            HDR.MinimumWidth = 9;
            HDR.Name = "HDR";
            HDR.ReadOnly = true;
            HDR.Width = 85;
            // 
            // Volume
            // 
            Volume.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Volume.HeaderText = "Volume";
            Volume.MinimumWidth = 8;
            Volume.Name = "Volume";
            Volume.ReadOnly = true;
            Volume.Width = 108;
            // 
            // Delay
            // 
            Delay.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Delay.HeaderText = "Delay";
            Delay.MinimumWidth = 9;
            Delay.Name = "Delay";
            Delay.ReadOnly = true;
            Delay.Width = 92;
            // 
            // startupScriptEnabled
            // 
            startupScriptEnabled.HeaderText = "Startup Script";
            startupScriptEnabled.MinimumWidth = 8;
            startupScriptEnabled.Name = "startupScriptEnabled";
            startupScriptEnabled.ReadOnly = true;
            startupScriptEnabled.Width = 150;
            // 
            // shutdownScriptEnabled
            // 
            shutdownScriptEnabled.HeaderText = "Shutdown Script";
            shutdownScriptEnabled.MinimumWidth = 8;
            shutdownScriptEnabled.Name = "shutdownScriptEnabled";
            shutdownScriptEnabled.ReadOnly = true;
            shutdownScriptEnabled.Width = 150;
            // 
            // userConfigured
            // 
            userConfigured.HeaderText = "Has User Config";
            userConfigured.MinimumWidth = 8;
            userConfigured.Name = "userConfigured";
            userConfigured.ReadOnly = true;
            userConfigured.Width = 120;
            // 
            // comboBoxHdr
            // 
            comboBoxHdr.Enabled = false;
            comboBoxHdr.FormattingEnabled = true;
            comboBoxHdr.Items.AddRange(new object[] { "Unset", "Disabled", "Enabled" });
            comboBoxHdr.Location = new Point(650, 484);
            comboBoxHdr.Margin = new Padding(2);
            comboBoxHdr.Name = "comboBoxHdr";
            comboBoxHdr.Size = new Size(177, 33);
            comboBoxHdr.TabIndex = 42;
            comboBoxHdr.SelectedIndexChanged += comboBoxHdr_SelectedIndexChanged;
            // 
            // labelHdr
            // 
            labelHdr.AutoSize = true;
            labelHdr.Location = new Point(650, 457);
            labelHdr.Margin = new Padding(2, 0, 2, 0);
            labelHdr.Name = "labelHdr";
            labelHdr.Size = new Size(53, 25);
            labelHdr.TabIndex = 41;
            labelHdr.Text = "HDR:";
            // 
            // buttonCancel
            // 
            buttonCancel.Enabled = false;
            buttonCancel.Location = new Point(759, 746);
            buttonCancel.Margin = new Padding(2);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(109, 33);
            buttonCancel.TabIndex = 40;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // buttonSave
            // 
            buttonSave.Enabled = false;
            buttonSave.Location = new Point(646, 746);
            buttonSave.Margin = new Padding(2);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(109, 33);
            buttonSave.TabIndex = 39;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // comboBoxSoundMode
            // 
            comboBoxSoundMode.Enabled = false;
            comboBoxSoundMode.FormattingEnabled = true;
            comboBoxSoundMode.Items.AddRange(new object[] { "Unset", "Stereo (Disabled)", "5.1 Surround", "7.1 Surround" });
            comboBoxSoundMode.Location = new Point(650, 422);
            comboBoxSoundMode.Margin = new Padding(2);
            comboBoxSoundMode.Name = "comboBoxSoundMode";
            comboBoxSoundMode.Size = new Size(177, 33);
            comboBoxSoundMode.TabIndex = 38;
            comboBoxSoundMode.SelectedIndexChanged += comboBoxSoundMode_SelectedIndexChanged;
            // 
            // labelSoundMode
            // 
            labelSoundMode.AutoSize = true;
            labelSoundMode.Location = new Point(650, 395);
            labelSoundMode.Margin = new Padding(2, 0, 2, 0);
            labelSoundMode.Name = "labelSoundMode";
            labelSoundMode.Size = new Size(147, 25);
            labelSoundMode.TabIndex = 37;
            labelSoundMode.Text = "Surround Sound:";
            // 
            // labelDelay
            // 
            labelDelay.AutoSize = true;
            labelDelay.Location = new Point(650, 519);
            labelDelay.Name = "labelDelay";
            labelDelay.Size = new Size(60, 25);
            labelDelay.TabIndex = 43;
            labelDelay.Text = "Delay:";
            // 
            // comboBoxDelay
            // 
            comboBoxDelay.Enabled = false;
            comboBoxDelay.FormattingEnabled = true;
            comboBoxDelay.Items.AddRange(new object[] { "Unset", "Disabled", "Enabled" });
            comboBoxDelay.Location = new Point(650, 547);
            comboBoxDelay.Name = "comboBoxDelay";
            comboBoxDelay.Size = new Size(177, 33);
            comboBoxDelay.TabIndex = 44;
            comboBoxDelay.SelectedIndexChanged += comboBoxDelay_SelectedIndexChanged;
            // 
            // labelGlobalSettings
            // 
            labelGlobalSettings.AutoSize = true;
            labelGlobalSettings.Font = new Font("Segoe UI", 14F);
            labelGlobalSettings.Location = new Point(162, 344);
            labelGlobalSettings.Name = "labelGlobalSettings";
            labelGlobalSettings.Size = new Size(209, 38);
            labelGlobalSettings.TabIndex = 45;
            labelGlobalSettings.Text = "Global Settings:";
            // 
            // labelUserSettings
            // 
            labelUserSettings.AutoSize = true;
            labelUserSettings.Font = new Font("Segoe UI", 14F);
            labelUserSettings.Location = new Point(813, 344);
            labelUserSettings.Name = "labelUserSettings";
            labelUserSettings.Size = new Size(186, 38);
            labelUserSettings.TabIndex = 46;
            labelUserSettings.Text = "User Settings:";
            // 
            // comboBoxDelayGlobal
            // 
            comboBoxDelayGlobal.Enabled = false;
            comboBoxDelayGlobal.FormattingEnabled = true;
            comboBoxDelayGlobal.Items.AddRange(new object[] { "Disabled", "Enabled" });
            comboBoxDelayGlobal.Location = new Point(162, 547);
            comboBoxDelayGlobal.Name = "comboBoxDelayGlobal";
            comboBoxDelayGlobal.Size = new Size(177, 33);
            comboBoxDelayGlobal.TabIndex = 52;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(162, 519);
            label1.Name = "label1";
            label1.Size = new Size(60, 25);
            label1.TabIndex = 51;
            label1.Text = "Delay:";
            // 
            // comboBoxHdrGlobal
            // 
            comboBoxHdrGlobal.Enabled = false;
            comboBoxHdrGlobal.FormattingEnabled = true;
            comboBoxHdrGlobal.Items.AddRange(new object[] { "Disabled", "Enabled" });
            comboBoxHdrGlobal.Location = new Point(162, 484);
            comboBoxHdrGlobal.Margin = new Padding(2);
            comboBoxHdrGlobal.Name = "comboBoxHdrGlobal";
            comboBoxHdrGlobal.Size = new Size(177, 33);
            comboBoxHdrGlobal.TabIndex = 50;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(162, 457);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(53, 25);
            label2.TabIndex = 49;
            label2.Text = "HDR:";
            // 
            // comboBoxSoundModeGlobal
            // 
            comboBoxSoundModeGlobal.Enabled = false;
            comboBoxSoundModeGlobal.FormattingEnabled = true;
            comboBoxSoundModeGlobal.Items.AddRange(new object[] { "Stereo (Disabled)", "5.1 Surround", "7.1 Surround" });
            comboBoxSoundModeGlobal.Location = new Point(162, 422);
            comboBoxSoundModeGlobal.Margin = new Padding(2);
            comboBoxSoundModeGlobal.Name = "comboBoxSoundModeGlobal";
            comboBoxSoundModeGlobal.Size = new Size(177, 33);
            comboBoxSoundModeGlobal.TabIndex = 48;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(162, 395);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(147, 25);
            label3.TabIndex = 47;
            label3.Text = "Surround Sound:";
            // 
            // labelVolume
            // 
            labelVolume.AutoSize = true;
            labelVolume.Location = new Point(877, 396);
            labelVolume.Name = "labelVolume";
            labelVolume.Size = new Size(168, 25);
            labelVolume.TabIndex = 56;
            labelVolume.Text = "Volume (0 is Unset):";
            // 
            // numericUpDownVolume
            // 
            numericUpDownVolume.Location = new Point(877, 424);
            numericUpDownVolume.Name = "numericUpDownVolume";
            numericUpDownVolume.Size = new Size(180, 31);
            numericUpDownVolume.TabIndex = 57;
            numericUpDownVolume.ValueChanged += numericUpDownDefaultVolume_ValueChanged;
            // 
            // buttonShutdownScript
            // 
            buttonShutdownScript.Location = new Point(1110, 699);
            buttonShutdownScript.Name = "buttonShutdownScript";
            buttonShutdownScript.Size = new Size(112, 34);
            buttonShutdownScript.TabIndex = 63;
            buttonShutdownScript.Text = "Choose File";
            buttonShutdownScript.UseVisualStyleBackColor = true;
            buttonShutdownScript.Click += buttonShutdownScript_Click;
            // 
            // buttonStartupScript
            // 
            buttonStartupScript.Location = new Point(1110, 626);
            buttonStartupScript.Name = "buttonStartupScript";
            buttonStartupScript.Size = new Size(112, 34);
            buttonStartupScript.TabIndex = 62;
            buttonStartupScript.Text = "Choose File";
            buttonStartupScript.UseVisualStyleBackColor = true;
            buttonStartupScript.Click += buttonStartupScript_Click;
            // 
            // textBoxShutdownScript
            // 
            textBoxShutdownScript.Location = new Point(646, 701);
            textBoxShutdownScript.Name = "textBoxShutdownScript";
            textBoxShutdownScript.Size = new Size(458, 31);
            textBoxShutdownScript.TabIndex = 61;
            textBoxShutdownScript.TextChanged += textBoxShutdownScript_TextChanged;
            // 
            // textBoxStartupScript
            // 
            textBoxStartupScript.Location = new Point(646, 626);
            textBoxStartupScript.Name = "textBoxStartupScript";
            textBoxStartupScript.Size = new Size(458, 31);
            textBoxStartupScript.TabIndex = 60;
            textBoxStartupScript.TextChanged += textBoxStartupScript_TextChanged;
            // 
            // labelShutdownScript
            // 
            labelShutdownScript.AutoSize = true;
            labelShutdownScript.Location = new Point(646, 673);
            labelShutdownScript.Name = "labelShutdownScript";
            labelShutdownScript.Size = new Size(286, 25);
            labelShutdownScript.TabIndex = 59;
            labelShutdownScript.Text = "Program Specific Shutdown Script:";
            // 
            // labelStartupScript
            // 
            labelStartupScript.AutoSize = true;
            labelStartupScript.Location = new Point(646, 598);
            labelStartupScript.Name = "labelStartupScript";
            labelStartupScript.Size = new Size(262, 25);
            labelStartupScript.TabIndex = 58;
            labelStartupScript.Text = "Program Specific Startup Script:";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlDark;
            panel1.Location = new Point(487, 358);
            panel1.Name = "panel1";
            panel1.Size = new Size(4, 408);
            panel1.TabIndex = 64;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1408, 788);
            Controls.Add(panel1);
            Controls.Add(buttonShutdownScript);
            Controls.Add(buttonStartupScript);
            Controls.Add(textBoxShutdownScript);
            Controls.Add(textBoxStartupScript);
            Controls.Add(labelShutdownScript);
            Controls.Add(labelStartupScript);
            Controls.Add(numericUpDownVolume);
            Controls.Add(labelVolume);
            Controls.Add(comboBoxDelayGlobal);
            Controls.Add(label1);
            Controls.Add(comboBoxHdrGlobal);
            Controls.Add(label2);
            Controls.Add(comboBoxSoundModeGlobal);
            Controls.Add(label3);
            Controls.Add(labelUserSettings);
            Controls.Add(labelGlobalSettings);
            Controls.Add(comboBoxDelay);
            Controls.Add(labelDelay);
            Controls.Add(comboBoxHdr);
            Controls.Add(labelHdr);
            Controls.Add(buttonCancel);
            Controls.Add(buttonSave);
            Controls.Add(comboBoxSoundMode);
            Controls.Add(labelSoundMode);
            Controls.Add(dataGridView1);
            Margin = new Padding(2);
            Name = "Form3";
            Text = "Program Settings";
            Load += Form3_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownVolume).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private ComboBox comboBoxHdr;
        private Label labelHdr;
        private Button buttonCancel;
        private Button buttonSave;
        private ComboBox comboBoxSoundMode;
        private Label labelSoundMode;
        private Label labelDelay;
        private ComboBox comboBoxDelay;
        private Label labelGlobalSettings;
        private Label labelUserSettings;
        private ComboBox comboBoxDelayGlobal;
        private Label label1;
        private ComboBox comboBoxHdrGlobal;
        private Label label2;
        private ComboBox comboBoxSoundModeGlobal;
        private Label label3;
        private Label labelVolume;
        private TextBox textBox1;
        private NumericUpDown numericUpDownVolume;
        private Button buttonShutdownScript;
        private Button buttonStartupScript;
        private TextBox textBoxShutdownScript;
        private TextBox textBoxStartupScript;
        private Label labelShutdownScript;
        private Label labelStartupScript;
        private DataGridViewTextBoxColumn ProgName;
        private DataGridViewTextBoxColumn Sound;
        private DataGridViewTextBoxColumn HDR;
        private DataGridViewTextBoxColumn Volume;
        private DataGridViewTextBoxColumn Delay;
        private DataGridViewTextBoxColumn startupScriptEnabled;
        private DataGridViewTextBoxColumn shutdownScriptEnabled;
        private DataGridViewTextBoxColumn userConfigured;
        private Panel panel1;
    }
}