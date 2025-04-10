namespace Living_Room_PC_Utility
{
    partial class FormTest
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
            buttonStartTest = new Button();
            buttonStopTest = new Button();
            labelResults = new Label();
            labelHeader = new Label();
            comboBoxProgram = new ComboBox();
            labelSoundMode = new Label();
            labelProgram = new Label();
            comboBoxSoundMode = new ComboBox();
            buttonSave = new Button();
            buttonCancel = new Button();
            labelHdr = new Label();
            comboBoxHdr = new ComboBox();
            labelTestHdr = new Label();
            comboBoxTestHdr = new ComboBox();
            SuspendLayout();
            // 
            // buttonStartTest
            // 
            buttonStartTest.Location = new Point(25, 405);
            buttonStartTest.Name = "buttonStartTest";
            buttonStartTest.Size = new Size(112, 34);
            buttonStartTest.TabIndex = 0;
            buttonStartTest.Text = "Start";
            buttonStartTest.UseVisualStyleBackColor = true;
            buttonStartTest.Click += buttonStartTest_Click;
            // 
            // buttonStopTest
            // 
            buttonStopTest.Enabled = false;
            buttonStopTest.Location = new Point(143, 405);
            buttonStopTest.Name = "buttonStopTest";
            buttonStopTest.Size = new Size(112, 34);
            buttonStopTest.TabIndex = 1;
            buttonStopTest.Text = "Stop";
            buttonStopTest.UseVisualStyleBackColor = true;
            buttonStopTest.Click += buttonStopTest_Click;
            // 
            // labelResults
            // 
            labelResults.AutoSize = true;
            labelResults.Location = new Point(25, 72);
            labelResults.Name = "labelResults";
            labelResults.Size = new Size(178, 25);
            labelResults.TabIndex = 2;
            labelResults.Text = "Press Start to begin...";
            // 
            // labelHeader
            // 
            labelHeader.AutoSize = true;
            labelHeader.Font = new Font("Segoe UI", 20.1428585F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelHeader.Location = new Point(18, 18);
            labelHeader.Margin = new Padding(2, 0, 2, 0);
            labelHeader.Name = "labelHeader";
            labelHeader.Size = new Size(476, 55);
            labelHeader.TabIndex = 3;
            labelHeader.Text = "Surround Sound Tester:";
            // 
            // comboBoxProgram
            // 
            comboBoxProgram.Enabled = false;
            comboBoxProgram.FormattingEnabled = true;
            comboBoxProgram.Location = new Point(518, 54);
            comboBoxProgram.Margin = new Padding(2);
            comboBoxProgram.Name = "comboBoxProgram";
            comboBoxProgram.Size = new Size(525, 33);
            comboBoxProgram.TabIndex = 28;
            comboBoxProgram.SelectedIndexChanged += comboBoxProgram_SelectedIndexChanged;
            // 
            // labelSoundMode
            // 
            labelSoundMode.AutoSize = true;
            labelSoundMode.Location = new Point(518, 127);
            labelSoundMode.Margin = new Padding(2, 0, 2, 0);
            labelSoundMode.Name = "labelSoundMode";
            labelSoundMode.Size = new Size(147, 25);
            labelSoundMode.TabIndex = 27;
            labelSoundMode.Text = "Surround Sound:";
            // 
            // labelProgram
            // 
            labelProgram.AutoSize = true;
            labelProgram.Location = new Point(518, 27);
            labelProgram.Margin = new Padding(2, 0, 2, 0);
            labelProgram.Name = "labelProgram";
            labelProgram.Size = new Size(428, 25);
            labelProgram.TabIndex = 29;
            labelProgram.Text = "Program Name or Title (Using .exe name suggested):";
            // 
            // comboBoxSoundMode
            // 
            comboBoxSoundMode.Enabled = false;
            comboBoxSoundMode.FormattingEnabled = true;
            comboBoxSoundMode.Items.AddRange(new object[] { "Stereo (Disabled)", "5.1 Surround", "7.1 Surround" });
            comboBoxSoundMode.Location = new Point(518, 154);
            comboBoxSoundMode.Margin = new Padding(2);
            comboBoxSoundMode.Name = "comboBoxSoundMode";
            comboBoxSoundMode.Size = new Size(177, 33);
            comboBoxSoundMode.TabIndex = 30;
            // 
            // buttonSave
            // 
            buttonSave.Enabled = false;
            buttonSave.Location = new Point(518, 405);
            buttonSave.Margin = new Padding(2);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(109, 33);
            buttonSave.TabIndex = 33;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Enabled = false;
            buttonCancel.Location = new Point(632, 405);
            buttonCancel.Margin = new Padding(2);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(109, 33);
            buttonCancel.TabIndex = 34;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // labelHdr
            // 
            labelHdr.AutoSize = true;
            labelHdr.Location = new Point(518, 228);
            labelHdr.Margin = new Padding(2, 0, 2, 0);
            labelHdr.Name = "labelHdr";
            labelHdr.Size = new Size(53, 25);
            labelHdr.TabIndex = 35;
            labelHdr.Text = "HDR:";
            // 
            // comboBoxHdr
            // 
            comboBoxHdr.Enabled = false;
            comboBoxHdr.FormattingEnabled = true;
            comboBoxHdr.Items.AddRange(new object[] { "Disabled", "Enabled" });
            comboBoxHdr.Location = new Point(518, 255);
            comboBoxHdr.Margin = new Padding(2);
            comboBoxHdr.Name = "comboBoxHdr";
            comboBoxHdr.Size = new Size(177, 33);
            comboBoxHdr.TabIndex = 36;
            comboBoxHdr.SelectedIndexChanged += comboBoxHdr_SelectedIndexChanged;
            // 
            // labelTestHdr
            // 
            labelTestHdr.AutoSize = true;
            labelTestHdr.Location = new Point(25, 315);
            labelTestHdr.Margin = new Padding(2, 0, 2, 0);
            labelTestHdr.Name = "labelTestHdr";
            labelTestHdr.Size = new Size(92, 25);
            labelTestHdr.TabIndex = 37;
            labelTestHdr.Text = "Test HDR?";
            // 
            // comboBoxTestHdr
            // 
            comboBoxTestHdr.FormattingEnabled = true;
            comboBoxTestHdr.Items.AddRange(new object[] { "No", "Yes" });
            comboBoxTestHdr.Location = new Point(29, 342);
            comboBoxTestHdr.Margin = new Padding(2);
            comboBoxTestHdr.Name = "comboBoxTestHdr";
            comboBoxTestHdr.Size = new Size(177, 33);
            comboBoxTestHdr.TabIndex = 38;
            comboBoxTestHdr.SelectedIndexChanged += comboBoxTestHdr_SelectedIndexChanged;
            // 
            // FormTest
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1068, 450);
            Controls.Add(comboBoxTestHdr);
            Controls.Add(labelTestHdr);
            Controls.Add(comboBoxHdr);
            Controls.Add(labelHdr);
            Controls.Add(buttonCancel);
            Controls.Add(buttonSave);
            Controls.Add(comboBoxSoundMode);
            Controls.Add(labelProgram);
            Controls.Add(comboBoxProgram);
            Controls.Add(labelSoundMode);
            Controls.Add(labelHeader);
            Controls.Add(labelResults);
            Controls.Add(buttonStopTest);
            Controls.Add(buttonStartTest);
            Name = "FormTest";
            Text = "Surround Sound Tester";
            Load += FormTest_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonStartTest;
        private Button buttonStopTest;
        private Label labelResults;
        private Label labelHeader;
        private ComboBox comboBoxProgram;
        private Label labelSoundMode;
        private Label labelProgram;
        private ComboBox comboBoxSoundMode;
        private Button buttonSave;
        private Button buttonCancel;
        private Label labelHdr;
        private ComboBox comboBoxHdr;
        private Label labelTestHdr;
        private ComboBox comboBoxTestHdr;
    }
}