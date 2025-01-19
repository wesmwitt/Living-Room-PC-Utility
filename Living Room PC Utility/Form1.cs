using System;
using System.Diagnostics;
using System.Management;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using IniParser;
using IniParser.Model;
using System.Reflection.Emit;
using Microsoft.VisualBasic.Devices;
using System.Runtime.CompilerServices;
using NAudio.CoreAudioApi;
using System.Text;


namespace Living_Room_PC_Utility
{
    public partial class Form1 : Form
    {
        private NotifyIcon trayIcon;
        private System.Windows.Forms.Timer processMonitorTimer;
        private HashSet<string> monitoredProcesses; // To track monitored processes
        private string specialProgram = "";
        private IniData globalConfigData;
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

            // Initialize the parser
            var parser = new FileIniDataParser();
            
            globalConfigData = parser.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), @"data\ProgramConfigListGlobal.ini"));

            // label1.Text = data.ToString();

            foreach (var item in globalConfigData["Audio"])
            {
                listBox1.Items.Add(item.KeyName + ": " + item.Value);
            }



            // Initialize the list of programs to monitor
            monitoredProcesses = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "notepad", // Example program
                //"chrome",  // Example program
                //"code"     // Example program (Visual Studio Code)
            };

            //get all running processes
            Process[] procs = Process.GetProcesses();
            foreach(Process proc in procs)
            {
                if(proc.MainWindowTitle != "")
                {
                    //label2.Text += "Process: " + proc.ProcessName + ".exe" + ", Window Title: " + proc.MainWindowTitle + "\n";
                }
                    
            }

            this.audioDeviceName = GetAudioDevice();

            ShowBaloonTip("Default Audio Device: " + this.audioDeviceName, true);

            // Start monitoring processes
            StartProcessMonitor();


            //TODO Re-enable this
            //this.WindowState = FormWindowState.Minimized;

            // Attach Resize event manually if not already done in the designer file
            this.Resize += new EventHandler(Form1_Resize);
            this.ShowInTaskbar = false;
            this.Hide(); // Hide window when user tries to close it
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

            KeyValuePair<string, int> tempProg = hasProgram("Audio", activeProcesses);

            if (tempProg.Key != "" && this.specialProgram == "")
            {
                this.specialProgram = tempProg.Key;
                this.label2.Text = this.specialProgram + ": " + tempProg.Value;
                this.ShowBaloonTip(specialProgram, true);
            } else if (tempProg.Key == "" && this.specialProgram != "" )
            {
                this.ShowBaloonTip(specialProgram, false);
                this.specialProgram = "";
                this.label2.Text = "";
            }

        }

        public string GetAudioDevice()
        {
            var enumerator = new MMDeviceEnumerator();
            var defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            return defaultDevice.FriendlyName;
        }

        private KeyValuePair<string, int> hasProgram(string configSection, Process[] activeProcesses)
        {

            foreach (var configProgram in globalConfigData[configSection])
            {

                foreach (Process activeProcess in activeProcesses)
                {
                    if (activeProcess.ProcessName + ".exe" == configProgram.KeyName
                        || activeProcess.MainWindowTitle == configProgram.KeyName)
                    {
                        return new KeyValuePair<string, int>(configProgram.KeyName, Int32.Parse(configProgram.Value));
                    }

                }

            }

            return new KeyValuePair<string, int>("",0);

        }

        private void ShowBaloonTip(string processName, bool opened)
        {
            if (opened)
            {
                trayIcon.BalloonTipTitle = $"{processName} Running";
                trayIcon.BalloonTipText = $"{processName} has started.";
                trayIcon.ShowBalloonTip(1000); // Show the balloon for 1 seconds
            } else
            {
                trayIcon.BalloonTipTitle = $"{processName} Closed";
                trayIcon.BalloonTipText = $"{processName} has been closed.";
                trayIcon.ShowBalloonTip(1000); // Show the balloon for 1s
            }

        }

        private void RunProg()
        {

            string paramString = "/SetSpeakersConfig";

            System.Diagnostics.Process.Start((Path.Combine(Directory.GetCurrentDirectory(), @"resources\programs\svcl.exe")),
                "/SetVolume AllAppVolume 100");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
                this.Hide(); // Hide the window from the taskbar and minimize to tray
                //turning this off because it runs a couple times when the program launches
                //trayIcon.ShowBalloonTip(3000, "App Running", "Click the tray icon to restore 1.", ToolTipIcon.Info); 
            }
        }

        //Override FormClosing to ensure it doesn't close the application
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                this.Hide(); // Hide window when user tries to close it
                trayIcon.ShowBalloonTip(3000, "App Minimized", "Click the tray icon to restore 2.", ToolTipIcon.Info);
            }
            base.OnFormClosing(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void IconClick(object sender, System.EventArgs e)
        {
            this.RestoreWindow(sender,e);
        }

    }
}
