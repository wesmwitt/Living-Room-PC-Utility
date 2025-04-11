namespace Living_Room_PC_Utility
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label7 = new Label();
            comboBoxDolbyAtmos = new ComboBox();
            label9 = new Label();
            comboBoxVolumeSwitching = new ComboBox();
            comboBoxHdr = new ComboBox();
            label8 = new Label();
            comboBoxSurroundSound = new ComboBox();
            label6 = new Label();
            buttonSaveConfig = new Button();
            buttonCancelConfig = new Button();
            menuStrip2 = new MenuStrip();
            programListToolStripMenuItem = new ToolStripMenuItem();
            testProgramToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            openFileDialog1 = new OpenFileDialog();
            labelDefaultVolume = new Label();
            numericUpDownDefaultVolume = new NumericUpDown();
            menuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDefaultVolume).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(258, 44);
            label1.Name = "label1";
            label1.Size = new Size(129, 25);
            label1.TabIndex = 5;
            label1.Text = "Active Process:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(381, 44);
            label2.Name = "label2";
            label2.Size = new Size(55, 25);
            label2.TabIndex = 6;
            label2.Text = "None";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(258, 20);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(121, 25);
            label3.TabIndex = 13;
            label3.Text = "Audio Device:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(380, 20);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(88, 25);
            label4.TabIndex = 14;
            label4.Text = "Loading...";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 20.1428585F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(247, 82);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(192, 55);
            label5.TabIndex = 19;
            label5.Text = "Settings:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(453, 154);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(122, 25);
            label7.TabIndex = 23;
            label7.Text = "Dolby Atmos:";
            // 
            // comboBoxDolbyAtmos
            // 
            comboBoxDolbyAtmos.FormattingEnabled = true;
            comboBoxDolbyAtmos.Items.AddRange(new object[] { "Disabled", "Enabled" });
            comboBoxDolbyAtmos.Location = new Point(453, 181);
            comboBoxDolbyAtmos.Margin = new Padding(2);
            comboBoxDolbyAtmos.Name = "comboBoxDolbyAtmos";
            comboBoxDolbyAtmos.Size = new Size(177, 33);
            comboBoxDolbyAtmos.TabIndex = 24;
            comboBoxDolbyAtmos.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(453, 224);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(157, 25);
            label9.TabIndex = 27;
            label9.Text = "Volume Switching:";
            // 
            // comboBoxVolumeSwitching
            // 
            comboBoxVolumeSwitching.FormattingEnabled = true;
            comboBoxVolumeSwitching.Items.AddRange(new object[] { "Disabled", "Enabled" });
            comboBoxVolumeSwitching.Location = new Point(453, 252);
            comboBoxVolumeSwitching.Margin = new Padding(2);
            comboBoxVolumeSwitching.Name = "comboBoxVolumeSwitching";
            comboBoxVolumeSwitching.Size = new Size(177, 33);
            comboBoxVolumeSwitching.TabIndex = 28;
            comboBoxVolumeSwitching.SelectedIndexChanged += comboBox4_SelectedIndexChanged;
            // 
            // comboBoxHdr
            // 
            comboBoxHdr.FormattingEnabled = true;
            comboBoxHdr.Items.AddRange(new object[] { "Disabled", "Enabled" });
            comboBoxHdr.Location = new Point(258, 252);
            comboBoxHdr.Margin = new Padding(2);
            comboBoxHdr.Name = "comboBoxHdr";
            comboBoxHdr.Size = new Size(177, 33);
            comboBoxHdr.TabIndex = 26;
            comboBoxHdr.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(258, 224);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(53, 25);
            label8.TabIndex = 25;
            label8.Text = "HDR:";
            // 
            // comboBoxSurroundSound
            // 
            comboBoxSurroundSound.FormattingEnabled = true;
            comboBoxSurroundSound.Items.AddRange(new object[] { "Stereo (Disabled)", "5.1 Surround", "7.1 Surround" });
            comboBoxSurroundSound.Location = new Point(258, 181);
            comboBoxSurroundSound.Margin = new Padding(2);
            comboBoxSurroundSound.Name = "comboBoxSurroundSound";
            comboBoxSurroundSound.Size = new Size(177, 33);
            comboBoxSurroundSound.TabIndex = 22;
            comboBoxSurroundSound.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(258, 154);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(147, 25);
            label6.TabIndex = 20;
            label6.Text = "Surround Sound:";
            // 
            // buttonSaveConfig
            // 
            buttonSaveConfig.Enabled = false;
            buttonSaveConfig.Location = new Point(258, 394);
            buttonSaveConfig.Name = "buttonSaveConfig";
            buttonSaveConfig.Size = new Size(112, 34);
            buttonSaveConfig.TabIndex = 29;
            buttonSaveConfig.Text = "Save";
            buttonSaveConfig.UseVisualStyleBackColor = true;
            buttonSaveConfig.Click += buttonSaveConfig_Click;
            // 
            // buttonCancelConfig
            // 
            buttonCancelConfig.Enabled = false;
            buttonCancelConfig.Location = new Point(376, 394);
            buttonCancelConfig.Name = "buttonCancelConfig";
            buttonCancelConfig.Size = new Size(112, 34);
            buttonCancelConfig.TabIndex = 30;
            buttonCancelConfig.Text = "Cancel";
            buttonCancelConfig.UseVisualStyleBackColor = true;
            buttonCancelConfig.Click += buttonCancelConfig_Click;
            // 
            // menuStrip2
            // 
            menuStrip2.BackColor = Color.LightGray;
            menuStrip2.Dock = DockStyle.Left;
            menuStrip2.ImageScalingSize = new Size(24, 24);
            menuStrip2.Items.AddRange(new ToolStripItem[] { programListToolStripMenuItem, testProgramToolStripMenuItem, toolStripMenuItem1 });
            menuStrip2.Location = new Point(0, 0);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Size = new Size(240, 455);
            menuStrip2.TabIndex = 32;
            menuStrip2.Text = "menuStrip2";
            // 
            // programListToolStripMenuItem
            // 
            programListToolStripMenuItem.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            programListToolStripMenuItem.ForeColor = Color.Black;
            programListToolStripMenuItem.Image = (Image)resources.GetObject("programListToolStripMenuItem.Image");
            programListToolStripMenuItem.Name = "programListToolStripMenuItem";
            programListToolStripMenuItem.Padding = new Padding(8, 16, 8, 16);
            programListToolStripMenuItem.Size = new Size(227, 74);
            programListToolStripMenuItem.Text = "Program List";
            programListToolStripMenuItem.TextAlign = ContentAlignment.MiddleLeft;
            programListToolStripMenuItem.Click += programListToolStripMenuItem_Click;
            // 
            // testProgramToolStripMenuItem
            // 
            testProgramToolStripMenuItem.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            testProgramToolStripMenuItem.ForeColor = Color.Black;
            testProgramToolStripMenuItem.Image = (Image)resources.GetObject("testProgramToolStripMenuItem.Image");
            testProgramToolStripMenuItem.Name = "testProgramToolStripMenuItem";
            testProgramToolStripMenuItem.Padding = new Padding(8, 16, 8, 16);
            testProgramToolStripMenuItem.Size = new Size(227, 74);
            testProgramToolStripMenuItem.Text = "Test Program";
            testProgramToolStripMenuItem.Click += testProgramToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(227, 4);
            // 
            // labelDefaultVolume
            // 
            labelDefaultVolume.AutoSize = true;
            labelDefaultVolume.Location = new Point(453, 295);
            labelDefaultVolume.Name = "labelDefaultVolume";
            labelDefaultVolume.Size = new Size(220, 25);
            labelDefaultVolume.TabIndex = 33;
            labelDefaultVolume.Text = "Default Volume (Desktop):";
            // 
            // numericUpDownDefaultVolume
            // 
            numericUpDownDefaultVolume.Location = new Point(453, 323);
            numericUpDownDefaultVolume.Name = "numericUpDownDefaultVolume";
            numericUpDownDefaultVolume.Size = new Size(180, 31);
            numericUpDownDefaultVolume.TabIndex = 35;
            numericUpDownDefaultVolume.ValueChanged += numericUpDownDefaultVolume_ValueChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(668, 455);
            Controls.Add(numericUpDownDefaultVolume);
            Controls.Add(labelDefaultVolume);
            Controls.Add(buttonCancelConfig);
            Controls.Add(buttonSaveConfig);
            Controls.Add(comboBoxVolumeSwitching);
            Controls.Add(label9);
            Controls.Add(comboBoxHdr);
            Controls.Add(label8);
            Controls.Add(comboBoxDolbyAtmos);
            Controls.Add(label7);
            Controls.Add(comboBoxSurroundSound);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(menuStrip2);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Living Room PC Utility";
            Load += Form1_Load;
            Resize += Form1_Resize;
            menuStrip2.ResumeLayout(false);
            menuStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDefaultVolume).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label7;
        private ComboBox comboBoxDolbyAtmos;
        private Label label9;
        private ComboBox comboBoxVolumeSwitching;
        private ComboBox comboBoxHdr;
        private Label label8;
        private ComboBox comboBoxSurroundSound;
        private Label label6;
        private Button buttonSaveConfig;
        private Button buttonCancelConfig;
        private MenuStrip menuStrip2;
        private ToolStripMenuItem programListToolStripMenuItem;
        private ToolStripMenuItem testProgramToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private OpenFileDialog openFileDialog1;
        private Button button1;
        private Label labelDefaultVolume;
        private NumericUpDown numericUpDownDefaultVolume;
    }
}
