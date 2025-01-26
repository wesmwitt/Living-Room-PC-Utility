using System.Diagnostics;
using IniParser;
using IniParser.Model;
using NAudio.CoreAudioApi;
using AutoActions.Displays;


namespace Living_Room_PC_Utility
{
    public partial class Form1 : Form
    {
        private NotifyIcon trayIcon;
        private System.Windows.Forms.Timer processMonitorTimer;
        private string specialProgram = "";
        private IniData config;

        FileIniDataParser parser = new FileIniDataParser();

        private Dictionary<string, ProgramConfig> programConfigs = new Dictionary<string, ProgramConfig>();
        private string audioDeviceName = "";

        public Form1()
        {
            InitializeComponent();

            // Set up the NotifyIcon (system tray icon)
            trayIcon = new NotifyIcon()
            {
                Icon = SystemIcons.Application, // Use your custom icon if needed
                Visible = true,
                Text = "Taskbar Monitor"
            };

            // Add a right-click context menu to the tray icon
            trayIcon.ContextMenuStrip = new ContextMenuStrip();
            trayIcon.ContextMenuStrip.Items.Add("Show GUI", null, RestoreWindow);
            trayIcon.ContextMenuStrip.Items.Add("Exit", null, ExitApp);

            trayIcon.DoubleClick += new System.EventHandler(IconClick);

            // Build the absolute path to the DLL
            string dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources", "dll");

            // Add the DLL directory to the PATH environment variable at runtime
            Environment.SetEnvironmentVariable("PATH", $"{dllPath};{Environment.GetEnvironmentVariable("PATH")}");

            this.SetProgramConfigs();

            this.SetDefaultSoundDisplaySettings();

            this.PopulateConfigFields();

            this.label4.Text = AudioSetter.GetAudioDevice();

            // Start monitoring processes
            StartProcessMonitor();


            //TODO Re-enable this
            //this.WindowState = FormWindowState.Minimized;

            // Attach Resize event manually if not already done in the designer file
            this.Resize += new EventHandler(Form1_Resize);

            //todo re-enbale
            //this.ShowInTaskbar = false;

            this.Hide(); // Hide window when user tries to close it
        }

        public void SetProgramConfigs()
        {
            this.programConfigs = ProgramConfig.GetProgramConfigDictionary("both");
        }

        public void SetDefaultSoundDisplaySettings()
        {
            AudioSetter.SetSurround(0);
            HDRController.SetGlobalHDRState(false);
        }

        private void PopulateConfigFields()
        {
            config = parser.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), @"data\Config.ini"));

            //TODO more safely try to fetch this value

            comboBoxSurroundSound.SelectedIndex = Int32.Parse(config["Settings"]["surroundType"]);
            comboBoxHdr.SelectedIndex = Int32.Parse(config["Settings"]["hdr"]);
            comboBoxDolbyAtmos.SelectedIndex = Int32.Parse(config["Settings"]["atmos"]);
            comboBoxVolumeSwitching.SelectedIndex = Int32.Parse(config["Settings"]["volume"]);
        }

        // Start monitoring the processes
        private void StartProcessMonitor()
        {
            processMonitorTimer = new System.Windows.Forms.Timer();
            processMonitorTimer.Interval = 100; // Check every 1/10 second ;;TODO CHANGE BACK TO 100
            processMonitorTimer.Tick += ProcessMonitorTimer_Tick;
            processMonitorTimer.Start();
        }

        // Process monitoring logic
        private void ProcessMonitorTimer_Tick(object sender, EventArgs e)
        {

            Process[] activeProcesses = Process.GetProcesses();

            //TODO account for atmos and 5.1 maximum

            //Surround Sound Switching
            KeyValuePair<string, ProgramConfig> tempProg = hasProgram(this.programConfigs, activeProcesses);
            if (tempProg.Key != "" && this.specialProgram == "")
            {
                this.specialProgram = tempProg.Key;
                this.label2.Text = this.specialProgram + ": " + tempProg.Value.toFriendlyString();

                AudioSetter.SetSurround(Int32.Parse(tempProg.Value.SurroundSoundSetting));

                this.ShowBaloonTip(specialProgram, true);
                //todo increase tick delay
            }
            else if (tempProg.Key == "" && this.specialProgram != "")
            {
                AudioSetter.SetSurround(0);
                this.ShowBaloonTip(specialProgram, false);
                this.specialProgram = "";
                this.label2.Text = "";
            }

            //TODO HDR Switching

        }

        private KeyValuePair<string, ProgramConfig> hasProgram(Dictionary<string, ProgramConfig> config, Process[] activeProcesses)
        {

            foreach (Process activeProcess in activeProcesses)
            {

                ProgramConfig tempProgConfigByExe;
                bool hasConfigByEx = config.TryGetValue(activeProcess.ProcessName + ".exe", out tempProgConfigByExe);
                if (hasConfigByEx)
                {
                    return new KeyValuePair<string, ProgramConfig>(activeProcess.ProcessName + ".exe", tempProgConfigByExe);
                }

                ProgramConfig tempProgConfigByTitle;
                bool hasConfigByTitle = config.TryGetValue(activeProcess.MainWindowTitle, out tempProgConfigByTitle);
                if (hasConfigByTitle)
                {
                    return new KeyValuePair<string, ProgramConfig>(activeProcess.MainWindowTitle, tempProgConfigByTitle);
                }

            }

            return new KeyValuePair<string, ProgramConfig>("", new ProgramConfig()); //nothing found
        }


        public void ShowBaloonTip(string processName, bool opened)
        {
            if (opened)
            {
                trayIcon.BalloonTipTitle = $"{processName} Running";
                trayIcon.BalloonTipText = $"{processName} has started.";
                trayIcon.ShowBalloonTip(1000); // Show the balloon for 1 seconds
            }
            else
            {
                trayIcon.BalloonTipTitle = $"{processName} Closed";
                trayIcon.BalloonTipText = $"{processName} has been closed.";
                trayIcon.ShowBalloonTip(1000); // Show the balloon for 1s
            }

        }

        // Exit the application (right-click menu option)
        private void ExitApp(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }

        // Restore the window when clicked from the tray icon
        private void RestoreWindow(object sender, EventArgs e)
        {
            this.Show(); // Show the window again
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
        }

        // Minimize the window to the tray when the form is minimized
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                //TODO
                //this.Hide(); // Hide the window from the taskbar and minimize to tray
                //turning this off because it runs a couple times when the program launches
                //trayIcon.ShowBalloonTip(3000, "App Running", "Click the tray icon to restore 1.", ToolTipIcon.Info); 
            }
        }

        //Override FormClosing to ensure it doesn't close the application
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //TODO
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            //    e.Cancel = true;
            //    this.WindowState = FormWindowState.Minimized;
            //    this.Hide(); // Hide window when user tries to close it
            //    trayIcon.ShowBalloonTip(3000, "App Minimized", "Click the tray icon to restore 2.", ToolTipIcon.Info);
            //}
            //base.OnFormClosing(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormTest form2 = new FormTest(this);
            form2.Show();
        }

        private void IconClick(object sender, System.EventArgs e)
        {
            this.RestoreWindow(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            trayIcon.BalloonTipTitle = $"Running";
            trayIcon.BalloonTipText = this.audioDeviceName;
            AudioSetter.SetSurround(0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AudioSetter.SetSurround(1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AudioSetter.SetSurround(2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AudioSetter.SetAtmos(true);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AudioSetter.SetAtmos(false);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            HDRController.SetGlobalHDRState(true);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            HDRController.SetGlobalHDRState(false);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(this);
            form3.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
