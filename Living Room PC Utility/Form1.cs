using System.Diagnostics;
using IniParser.Model;
using NAudio.CoreAudioApi;
using AutoActions.Displays;
using System.Management;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System;
using System.Windows.Forms;
using TextBox = System.Windows.Forms.TextBox;


namespace Living_Room_PC_Utility
{
    public partial class Form1 : Form
    {
        private NotifyIcon trayIcon;
        private System.Windows.Forms.Timer processMonitorTimer;

        private string specialProgram = ""; //old

        private int activeProgramPID = 0;
        private string activeProgramStr = "";

        private GlobalConfig globalConfig = new GlobalConfig();

        private Dictionary<string, ProgramConfig> programConfigs = new Dictionary<string, ProgramConfig>();
        private string audioDeviceName = "";

        private ManagementEventWatcher processStartWatcher;
        private ManagementEventWatcher processStopWatcher;
        private HashSet<int> trackedProcesses = new HashSet<int>();

        string[] processBlockList = ["", "svcl.exe", "svchost.exe", "taskhostw.exe", "msedgewebview2.exe", "conhost.exe", "backgroundTaskHost.exe", "RuntimeBroker.exe", "sppsvc.exe", "ROUTE.EXE",
            "XboxPcTray.exe", "GameBar.exe", "XboxGameBarWidgets.exe", "GameBarFTServer.exe", "updater.exe", "dllhost.exe", "GameBarPresenceWriter.exe", "OAWrapper.exe",
            "StandardCollector.Service.exe", "steamwebhelper.exe", "consent.exe", "steamservice.exe", "cmd.exe", "SearchProtocolHost.exe", "SearchFilterHost.exe", "GameOverlayUI.exe",
            "CompPkgSrv.exe", "x64launcher.exe", "Living Room PC Utility.exe", "git.exe", "smartscreen.exe", "Taskmgr.exe", "WindowsPackageManagerServer.exe", "CompatTelRunner.exe",
            "Living Room PC Utility.exe", "MoUsoCoreWorker.exe", "csc.exe", "cvtres.exe", "WmiPrvSE.exe", "OfficeClickToRun.exe", "PerfBoost.exe", "AppVShNotify.exe", "Integrator.exe",
            "SearchIndexer.exe", "wevtutil.exe", "rundll32.exe", "schtasks.exe", "msiexec.exe", "BackgroundDownload.exe", "OfficeC2RClient.exe", "SDXHelper.exe", "sdbinst.exe", "audiodg.exe",
            "SystemSettings.exe", "nvngx_update.exe", "powershell.exe", "XboxPcApp.exe", "mmc.exe", "7zFM.exe", "setup.exe", "sc.exe", "NvOAWrapperCache.exe", "drvinst.exe", "nvcontainer.exe",
            "NVIDIA App.exe", "nvcplui.exe", "OpenConsole.exe", "Time.exe", "Copilot.exe", "PilotshubApp.exe", "MicrosoftSecurityApp.exe", "SecHealthUI.exe", "python.exe",
            "SecurityHealthHost.exe", "ms-teamsupdate.exe", "QuickAssist.exe", "PickerHost.exe", "SnippingTool.exe", "control.exe", "explorer.exe", "AppHostRegistrationVerifier.exe",
            "WindowsBackupClient.exe", "crashpad_handler.exe", "GoogleDriveFS.exe", "osk.exe", "AsusUpdate.exe", "Get-AppxVersion.exe", "WebExperienceHostApp.exe", "git-remote-https.exe",
            "git-credential-manager.exe", "sh.exe", "VSSVC.exe", "SrTasks.exe", "tzsync.exe", "UsoClient.exe", "dmclient.exe", "UCPDMgr.exe", "mscorsvw.exe", "ngen.exe", "DismHost.exe",
            "DataExchangeHost.exe", "ShellHost.exe", "WindowsTerminal.exe", "UbisoftExtension.exe", "EAConnect_microsoft.exe", "upc.exe", "UplayService.exe", "UplayWebCore.exe",
            "EACefSubProcess.exe", "MicrosoftEdge_X64_135.0.3179.66_135.0.3179.54.exe", "BackgroundTransferHost.exe", "pingsender.exe", "upfc.exe", "SpatialAudioLicenseSrv.exe", "TiWorker.exe",
            "LogiOverlay.exe", "AutoHotkey64.exe", "SIHClient.exe", "UCConfigTask.exe", "wuaucltcore.exe", "wsqmcons.exe", "curl.exe", "AutoHotkeyU64.exe", "USBDeview.exe", "AutoHotkeyUX.exe",
            "wscript.exe", "dxgiadaptercache.exe", "WMIADAP.exe", "ngen.exe", "APSDaemon.exe", "taskkill.exe", "mscorsvw.exe", "MoNotificationUx.exe", "UIEOrchestrator.exe", "NZXT CAM.exe",
            "EasyAntiCheat_EOS.exe", "ShellExperienceHost.exe", "CrashReportClient.exe", "MpCmdRun.exe", "MpSigStub.exe", "LogiLuUpdater.exe", "UsoClient.exe", "TrustedInstaller.exe", "dmclient.exe",
            "MicrosoftEdgeUpdate.exe", "wermgr.exe", "EpicWebHelper.exe", "iCloudFirefox.exe", "chrome_proxy.exe", "NgcIso.exe", "provtool.exe", "Defrag.exe", "EpicGamesUpdater.exe", "DeviceCensus.exe",
            "bootstrapper.exe", "dxdiag.exe", "codCrashHandler.exe", "WmiApSrv.exe", "elevation_service.exe", "inno_updater.exe", "remoting_native_messaging_host.exe", "EALaunchHelper.exe",
            "EALocalHostSvc.exe", "IGOProxy32.exe", "EAAntiCheat.Installer.exe", "EAAntiCheat.GameService.dll", "EAAntiCheat.GameService.exe", "OpenWith.exe", "AM_Delta_Patch_1.427.205.0.exe",
            "makecab.exe", "UpdaterSetup.exe", "qualification_app.exe", "PING.EXE", "ControllerCompanion.exe", "default-browser-agent.exe", "WaaSMedicAgent.exe", "gifsicle.exe", "dbInstaller.exe",
            "appidcertstorecheck.exe", "TcNo-Acc-Switcher.exe", "TcNo-Acc-Switcher_main.exe", "opushutil.exe", "runas.exe", "WerFault.exe", "SpeechModelDownload.exe", "DiskSnapshot.exe", "dstokenclean.exe",
            "cleanmgr.exe", "ngentask.exe", "reg.exe", "doff.exe", "identity_helper.exe", "code-tunnel.exe", "wsl.exe", "icacls.exe", "DesignToolsServer.exe"
        ];

        public Form1()
        {
            InitializeComponent();

            // Set up the NotifyIcon (system tray icon)
            trayIcon = new NotifyIcon()
            {
                Icon = new System.Drawing.Icon(@"resources\icons\tray-icon.ico"), // Use your custom icon if needed
                Visible = true,
                Text = "Living Room PC Utility"
            };

            // Add a right-click context menu to the tray icon
            trayIcon.ContextMenuStrip = new ContextMenuStrip();
            trayIcon.ContextMenuStrip.Items.Add("Show Main GUI", null, RestoreWindow);
            trayIcon.ContextMenuStrip.Items.Add("-");
            trayIcon.ContextMenuStrip.Items.Add("Test Program (HDR Off)", null, TestProgram);
            trayIcon.ContextMenuStrip.Items.Add("Test Program (HDR On)", null, TestProgramWithHdr);
            trayIcon.ContextMenuStrip.Items.Add("-");
            trayIcon.ContextMenuStrip.Items.Add("Stereo", null, (s, e) => AudioSetter.SetSurround(0));
            trayIcon.ContextMenuStrip.Items.Add("5.1 Surround", null, (s, e) => AudioSetter.SetSurround(1));
            trayIcon.ContextMenuStrip.Items.Add("7.1 Surround", null, (s, e) => AudioSetter.SetSurround(2));
            trayIcon.ContextMenuStrip.Items.Add("-");
            trayIcon.ContextMenuStrip.Items.Add("Atmos On", null, (s, e) => AudioSetter.SetAtmos(true));
            trayIcon.ContextMenuStrip.Items.Add("Atmos Off", null, (s, e) => AudioSetter.SetAtmos(true));
            trayIcon.ContextMenuStrip.Items.Add("-");
            trayIcon.ContextMenuStrip.Items.Add("HDR On", null, (s, e) => HDRController.SetGlobalHDRState(true));
            trayIcon.ContextMenuStrip.Items.Add("HDR Off", null, (s, e) => HDRController.SetGlobalHDRState(false));
            trayIcon.ContextMenuStrip.Items.Add("-");
            trayIcon.ContextMenuStrip.Items.Add("Exit", null, ExitApp);

            trayIcon.DoubleClick += new System.EventHandler(IconClick);

            // Build the absolute path to the DLL
            string dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources", "dll");

            // Add the DLL directory to the PATH environment variable at runtime
            Environment.SetEnvironmentVariable("PATH", $"{dllPath};{Environment.GetEnvironmentVariable("PATH")}");

            this.LoadAndSetProgramConfigs();

            this.SetDefaultSoundDisplaySettings();

            this.ProcessGlobalConfig();

            this.label4.Text = AudioSetter.GetAudioDevice();

            StartProcessMonitoring();

            Process[] activeProcesses = Process.GetProcesses();

            // Start monitoring processes
            //StartProcessMonitor();

            //TODO Re-enable this
            this.WindowState = FormWindowState.Minimized;

            // Attach Resize event manually if not already done in the designer file
            //this.Resize += new EventHandler(Form1_Resize);

            //todo re-enbale
            this.ShowInTaskbar = false;

            var recentPrograms = RecentPrograms.GetRecentProgramsDictionary();
            Debug.WriteLine("Recent Programs:");
            foreach (var item in recentPrograms)
            {
                Debug.WriteLine(item.Key + ": " + item.Value);
            }

            this.Hide(); // Hide window when user tries to close it
        }

        public void LoadAndSetProgramConfigs()
        {
            this.programConfigs = ProgramConfig.GetProgramConfigDictionary("both");
        }

        public void SetDefaultSoundDisplaySettings()
        {
            if (this.globalConfig.SurroundSoundSetting > 0)
            {
                AudioSetter.SetSurround(0);
            }

            if (this.globalConfig.AtmosSetting > 0)
            {
                AudioSetter.SetAtmos(false);
            }

            if (this.globalConfig.HDRSetting > 0)
            {
                HDRController.SetGlobalHDRState(false);
            }

            if (this.globalConfig.VolumeSetting > 0)
            {
                VolumeSetter.SetReceiverVolume(this.globalConfig.DefaultVolumeSetting);
            }

        }

        private void ProcessGlobalConfig()
        {
            this.globalConfig = GlobalConfig.GetGlobalConfigFileData();
            this.PopulateConfigFields();
        }
        private void PopulateConfigFields()
        {
            comboBoxSurroundSound.SelectedIndex = this.globalConfig.SurroundSoundSetting;
            comboBoxHdr.SelectedIndex = this.globalConfig.HDRSetting;
            comboBoxDolbyAtmos.SelectedIndex = this.globalConfig.AtmosSetting;
            comboBoxVolumeSwitching.SelectedIndex = this.globalConfig.VolumeSetting;
            numericUpDownDefaultVolume.Value = this.globalConfig.DefaultVolumeSetting;
            textBoxStartupScript.Text = this.globalConfig.StartupScript;
            textBoxShutdownScript.Text = this.globalConfig.ShutdownScript;

            this.buttonSaveConfig.Enabled = false;
            this.buttonCancelConfig.Enabled = false;
        }

        // -- NEW METHOD -----------------------------

        private void StartProcessMonitoring()
        {
            try
            {
                // WMI Query to detect new processes
                WqlEventQuery startQuery = new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace");
                processStartWatcher = new ManagementEventWatcher(startQuery);
                processStartWatcher.EventArrived += new EventArrivedEventHandler(OnProcessStarted);
                processStartWatcher.Start();

                // WMI Query to detect closed processes
                WqlEventQuery stopQuery = new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace");
                processStopWatcher = new ManagementEventWatcher(stopQuery);
                processStopWatcher.EventArrived += new EventArrivedEventHandler(OnProcessStopped);
                processStopWatcher.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting process monitor: " + ex.Message);
            }
        }

        private void OnProcessStarted(object sender, EventArrivedEventArgs e)
        {
            try
            {

                int processId = Convert.ToInt32(e.NewEvent.Properties["ProcessID"].Value);
                string processName = e.NewEvent.Properties["ProcessName"].Value.ToString();
                //"ParentProcessID" is the other helpful attribute
                int parentProcessId = Convert.ToInt32(e.NewEvent.Properties["ParentProcessID"].Value);


                if (!processBlockList.Contains(processName))
                {
                    trackedProcesses.Add(processId);

                    Task.Run(() =>
                    {
                        string windowTitle = GetWindowTitle(processId);
                        Debug.WriteLine($"New Process Detected: {processName} - {windowTitle} - {processId}");
                        RecentPrograms.AddRecentProgram(processName, windowTitle);
                        TrySetActiveProgram(processName, windowTitle, processId, this.programConfigs);
                    });
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error handling process start event: " + ex.Message);
            }
        }

        private void OnProcessStopped(object sender, EventArrivedEventArgs e)
        {
            try
            {
                int processId = Convert.ToInt32(e.NewEvent.Properties["ProcessID"].Value);
                string processName = e.NewEvent.Properties["ProcessName"].Value.ToString();

                if (trackedProcesses.Contains(processId))
                {
                    trackedProcesses.Remove(processId);
                    Debug.WriteLine($"Process Closed: {processName} - {processId}");
                    TryUnsetActiveProgram(processId);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error handling process stop event: " + ex.Message);
            }
        }

        private string GetWindowTitle(int processId)
        {
            try
            {

                Process process = Process.GetProcessById(processId);
                Debug.WriteLine("GetWindowTitle() pid: ", processId, "process found: ", process.ToString());

                if (process.MainWindowHandle != IntPtr.Zero)
                {
                    return process.MainWindowTitle;
                }

            }
            catch
            {
                // Process might have exited
            }
            return string.Empty;
        }

        public void TrySetActiveProgramOnDemand()
        {

            bool hasActiveProgram = false;

            Process[] activeProcesses = Process.GetProcesses();

            foreach (Process activeProcess in activeProcesses)
            {

                ProgramConfig tempProgConfigByExe;
                bool hasConfigByExe = (this.programConfigs.TryGetValue(activeProcess.ProcessName + ".exe", out tempProgConfigByExe));
                ProgramConfig tempProgConfigByTitle;
                bool hasConfigByTitle = (this.programConfigs.TryGetValue(activeProcess.MainWindowTitle, out tempProgConfigByTitle));

                if (hasConfigByExe)
                {
                    this.activeProgramStr = activeProcess.ProcessName + ".exe";
                }
                else if (hasConfigByTitle)
                {
                    this.activeProgramStr = activeProcess.MainWindowTitle;
                }

                if (hasConfigByExe || hasConfigByTitle)
                {

                    hasActiveProgram = true;

                    //set pid
                    this.activeProgramPID = activeProcess.Id;
                    this.trackedProcesses.Add(activeProcess.Id);

                    //set friendly name
                    label2.Invoke((Action)delegate
                    {
                        label2.Text = this.activeProgramStr;
                    });

                    //set attributes
                    KeyValuePair<string, ProgramConfig> tempProg = GetProgramFromConfig(this.programConfigs, this.activeProgramStr);

                    this.ShowProgramStatusBaloon(true, this.activeProgramStr, tempProg.Value);

                    this.SetSoundDisplaySettingsForProgramConfig(tempProg.Value);
                }

            }

            //if we don't have a program, set the default settings
            if (!hasActiveProgram)
            {
                this.activeProgramPID = 0;
                this.activeProgramStr = "";
                this.SetDefaultSoundDisplaySettings();
                label2.Invoke((Action)delegate
                {
                    label2.Text = "";
                });
                this.ShowProgramStatusBaloon(false);
            }

        }

        public void TrySetActiveProgram(string programName, string programTitle, int programPID, Dictionary<string, ProgramConfig> config)
        {

            //only set if there's not an active program still running
            if (this.activeProgramPID != 0)
            {
                Debug.WriteLine("There is already a program active, skipping for now");
                return;
            }

            if ((programName == "" && programTitle == "") || programPID == 0)
            {
                Debug.WriteLine("TrySetActiveProgram Error: Missing Param");
                return;
            }

            ProgramConfig tempProgConfigByExe;
            bool hasConfigByExe = config.TryGetValue(programName, out tempProgConfigByExe);

            ProgramConfig tempProgConfigByTitle;
            bool hasConfigByTitle = false;
            if (programTitle != "")
            {
                hasConfigByTitle = config.TryGetValue(programTitle, out tempProgConfigByTitle);
            }

            if (hasConfigByExe)
            {
                Debug.WriteLine($"Has config by name (exe): {programName} with pid: {programPID}");
                this.activeProgramStr = programName;
            }
            else if (hasConfigByTitle)
            {
                Debug.WriteLine($"Has config by title: {programTitle} with pid: {programPID}");
                this.activeProgramStr = programTitle;

            }

            if (hasConfigByExe || hasConfigByTitle)
            {
                //set pid
                this.activeProgramPID = programPID;

                //set friendly name
                label2.Invoke((Action)delegate
                {
                    label2.Text = this.activeProgramStr;
                });

                //set attributes
                KeyValuePair<string, ProgramConfig> tempProg = GetProgramFromConfig(this.programConfigs, this.activeProgramStr);

                this.ShowProgramStatusBaloon(true, this.activeProgramStr, tempProg.Value);

                this.SetSoundDisplaySettingsForProgramConfig(tempProg.Value);
            }

        }

        public void TryUnsetActiveProgram(int pid)
        {
            if (this.activeProgramPID == pid)
            {
                ShowProgramStatusBaloon(false, this.activeProgramStr);
                this.activeProgramPID = 0;
                this.activeProgramStr = "";
                RunShutdownScript();

                this.SetDefaultSoundDisplaySettings();
                label2.Invoke((Action)delegate
                {
                    label2.Text = "";
                });
            }
        }

        private void RunShutdownScript()
        {
            if (this.globalConfig.ShutdownScript != "")
            {
                // Run startup script with the default associated application
                Debug.WriteLine("Has startup script.");
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = this.globalConfig.ShutdownScript;
                startInfo.UseShellExecute = true; // Use the default application

                try
                {
                    Process.Start(startInfo);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to start process: {ex.Message}");
                    // Optionally, you can show a message box or log the error
                    MessageBox.Show($"Failed to start process: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void SetSoundDisplaySettingsForProgramConfig(ProgramConfig prog)
        {

            int programSurroundValue = Int32.Parse(prog.SurroundSoundSetting);
            //if surround sound switching is enabled
            if (this.globalConfig.SurroundSoundSetting > 0)
            {
                //if the program supports a higher value than the user's computer
                if (programSurroundValue > this.globalConfig.SurroundSoundSetting)
                {
                    //we only set it to the highest supported setting
                    programSurroundValue = this.globalConfig.SurroundSoundSetting;
                }
                AudioSetter.SetSurround(programSurroundValue);
            }

            //if atmos is enabled
            //only enable atmos if the program is at the highest supported global surround sound setting
            //for example, if we have a 7.1 system and a 5.1 program, we do not enable atmos
            if (this.globalConfig.AtmosSetting > 0 && programSurroundValue == this.globalConfig.SurroundSoundSetting)
            {
                AudioSetter.SetAtmos(true);
            }

            if (this.globalConfig.HDRSetting > 0)
            {
                bool hdrState = false;
                if (prog.HDRSetting == "1")
                {
                    hdrState = true;
                }
                HDRController.SetGlobalHDRState(hdrState);
            }

            if (this.globalConfig.VolumeSetting > 0) //volume switching is enabled in the global settings
            {
                if (prog.VolumeSetting != "" && prog.VolumeSetting != "0") //program config has a volume setting
                {
                    VolumeSetter.SetReceiverVolume(Int32.Parse(prog.VolumeSetting));
                }

            }

            RunStartupScript();

        }

        private void RunStartupScript()
        {
            if (this.globalConfig.StartupScript != "")
            {
                // Run startup script with the default associated application
                Debug.WriteLine("Has startup script.");
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = this.globalConfig.StartupScript;
                startInfo.UseShellExecute = true; // Use the default application

                try
                {
                    Process.Start(startInfo);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to start process: {ex.Message}");
                    // Optionally, you can show a message box or log the error
                    MessageBox.Show($"Failed to start process: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //-------------------------------------------

        // Start monitoring the processes
        //private void StartProcessMonitor()
        //{
        //    processMonitorTimer = new System.Windows.Forms.Timer();
        //    processMonitorTimer.Interval = 10000; // Check every 1/10 second ;;TODO CHANGE BACK TO 100
        //    processMonitorTimer.Tick += ProcessMonitorTimer_Tick;
        //    processMonitorTimer.Start();
        //}

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

                //this.ShowBaloonTip(specialProgram, true);
                //todo increase tick delay
            }
            else if (tempProg.Key == "" && this.specialProgram != "")
            {
                AudioSetter.SetSurround(0);
                //this.ShowBaloonTip(specialProgram, false);
                this.specialProgram = "";
                this.label2.Text = "";
            }

            //TODO HDR Switching

        }

        private KeyValuePair<string, ProgramConfig> GetProgramFromConfig(Dictionary<string, ProgramConfig> config, string progLabel)
        {

            ProgramConfig tempProgConfigByExe;
            bool hasConfig = config.TryGetValue(progLabel, out tempProgConfigByExe);
            if (hasConfig)
            {
                return new KeyValuePair<string, ProgramConfig>(progLabel, tempProgConfigByExe);
            }

            return new KeyValuePair<string, ProgramConfig>("", new ProgramConfig()); //nothing found
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

        public void ShowProgramStatusBaloon(bool openStatus)
        {
            string status = (openStatus) ? "Configured process open" : "No configured process open";
            string settingChange = (openStatus) ? "Adjusting settings accordingly." : "Restoring default settings.";
            trayIcon.BalloonTipTitle = $"{status}";
            trayIcon.BalloonTipText = $"{settingChange}";
            trayIcon.ShowBalloonTip(1000);

        }

        public void ShowProgramStatusBaloon(bool openStatus, string process)
        {
            string status = (openStatus) ? "opened" : "closed";
            string settingChange = (openStatus) ? "Adjusting settings accordingly." : "Restoring default settings.";
            trayIcon.BalloonTipTitle = $"{process} has {status}";
            trayIcon.BalloonTipText = $"{settingChange}";
            trayIcon.ShowBalloonTip(1000);

        }

        public void ShowProgramStatusBaloon(bool openStatus, string process, ProgramConfig programConfig)
        {

            string status = (openStatus) ? "opened" : "closed";

            trayIcon.BalloonTipTitle = $"{process} has {status}.";
            trayIcon.BalloonTipText = $"{programConfig.toFriendlyStringConcise()}.";
            trayIcon.ShowBalloonTip(1000);
        }

        public void ShowBaloonTip(string title, string text)
        {
            trayIcon.BalloonTipTitle = title;
            trayIcon.BalloonTipText = text;
            trayIcon.ShowBalloonTip(1000);
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
                this.Hide(); // Hide the window from the taskbar and minimize to tray
                //turning this off because it runs a couple times when the program launches
                //todo figure out
                //trayIcon.ShowBalloonTip(3000, "App Running", "Click the tray icon to restore 1.", ToolTipIcon.Info);
            }
        }

        //Override FormClosing to ensure it doesn't close the application
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Debug.WriteLine("Closing Form: " + e.CloseReason.ToString());
            //TODO
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                this.Hide(); // Hide window when user tries to close it
                trayIcon.ShowBalloonTip(3000, "Living Room PC Utility Minimized to Tray", "Double-click the tray icon to restore or right click the icon and select Exit to close.", ToolTipIcon.Info);
            }
            else if (e.CloseReason == CloseReason.ApplicationExitCall)
            {
                processStartWatcher?.Stop();
                processStartWatcher?.Dispose();
            }
            base.OnFormClosing(e);
        }

        private void IconClick(object sender, System.EventArgs e)
        {
            this.RestoreWindow(sender, e);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.buttonSaveConfig.Enabled = true;
            this.buttonCancelConfig.Enabled = true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.buttonSaveConfig.Enabled = true;
            this.buttonCancelConfig.Enabled = true;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.buttonSaveConfig.Enabled = true;
            this.buttonCancelConfig.Enabled = true;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.buttonSaveConfig.Enabled = true;
            this.buttonCancelConfig.Enabled = true;
        }

        private void numericUpDownDefaultVolume_ValueChanged(object sender, EventArgs e)
        {
            this.buttonSaveConfig.Enabled = true;
            this.buttonCancelConfig.Enabled = true;
        }

        private void buttonSaveConfig_Click(object sender, EventArgs e)
        {
            this.buttonSaveConfig.Enabled = false;
            this.buttonCancelConfig.Enabled = false;

            //convert from decimal to int
            int defaultVolume = (int)numericUpDownDefaultVolume.Value;

            GlobalConfig.SetGlobalConfigFileData(
                comboBoxSurroundSound.SelectedIndex,
                comboBoxDolbyAtmos.SelectedIndex,
                comboBoxHdr.SelectedIndex,
                comboBoxVolumeSwitching.SelectedIndex,
                defaultVolume,
                textBoxStartupScript.Text,
                textBoxShutdownScript.Text
                );

            ProcessGlobalConfig();

            //do a refresh in case the active settings need to change
            TrySetActiveProgramOnDemand();
        }

        private void buttonCancelConfig_Click(object sender, EventArgs e)
        {
            this.PopulateConfigFields();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TrySetActiveProgramOnDemand();
            //this.getOpenPrograms();
        }

        private void programListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(this);
            form3.Show();
        }

        private void testProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTest form2 = new FormTest(this);
            form2.Show();
        }

        private void TestProgram(object sender, EventArgs e)
        {
            this.RestoreWindow(null, null);
            FormTest form2 = new FormTest(this);
            form2.Show();
            form2.SetIsTestingHdr(false);
            form2.startTestThread();
        }

        private void TestProgramWithHdr(object sender, EventArgs e)
        {
            this.RestoreWindow(null, null);
            FormTest form2 = new FormTest(this);
            form2.Show();
            form2.SetIsTestingHdr(true);
            form2.startTestThread();
        }

        //test code, not currently used
        private void getOpenPrograms()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Process");

            Debug.WriteLine($"----------getOpenPrograms()----------");
            foreach (ManagementObject obj in searcher.Get())
            {
                Debug.WriteLine($"PID: {obj["ProcessId"]}, Name: {obj["Name"]}");
            }
            Debug.WriteLine($"----------getOpenPrograms()----------");
        }

        private void buttonStartupScript_Click(object sender, EventArgs e)
        {
            SelectFile(textBoxStartupScript);
        }

        private void textBoxStartupScript_TextChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("textBoxStartupScript_TextChanged: " + textBoxStartupScript.Text);
            this.buttonSaveConfig.Enabled = true;
            this.buttonCancelConfig.Enabled = true;
        }

        private void buttonShutdownScript_Click(object sender, EventArgs e)
        {
            SelectFile(textBoxShutdownScript);
        }

        private void textBoxShutdownScript_TextChanged(object sender, EventArgs e)
        {
            this.buttonSaveConfig.Enabled = true;
            this.buttonCancelConfig.Enabled = true;
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
