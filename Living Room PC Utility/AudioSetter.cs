using System.Diagnostics;
using NAudio.CoreAudioApi;

namespace Living_Room_PC_Utility
{

    public static class AudioSetter
    {

        //0 = Stereo, 1 = 5.1, 2 = 7.1
        public static void SetSurround(int settingNum=0)
        {

            string audioDeviceName = GetAudioDevice();
            string path = Path.Combine(Directory.GetCurrentDirectory(), @"resources\programs\svcl.exe");

            string soundStr = "0x3 0x3 0x3"; //Default Stereo
             if (settingNum == 1)
            {
                soundStr = "0x3f 0x3f 0x3f"; //5.1 Surround
            } else if (settingNum == 2)
            {
                soundStr = "0x63f 0x63f 0x63f"; //7.1 Surround
            }

            string command = "/SetSpeakersConfig \"" + audioDeviceName + "\" " + soundStr;

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = path,             // Path to the executable
                Arguments = command,         // Command-line arguments
                UseShellExecute = false,     // Required for CreateNoWindow to work
                CreateNoWindow = true,       // Do not create a window
                WindowStyle = ProcessWindowStyle.Hidden // Start hidden
            };

            Process.Start(startInfo);
        }

        public static void SetAtmos(bool enable=false)
        {

            string audioDeviceName = GetAudioDevice();
            string path = Path.Combine(Directory.GetCurrentDirectory(), @"resources\programs\svcl.exe");

            string soundStr = "";
            if (enable)
            {
                soundStr = "Dolby Atmos for home theater";
                //soundStr = "Windows Sonic for Headphones";
            }

            string command = "/SetSpatial \"" + audioDeviceName + "\" \"" + soundStr + "\"";

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = path,             // Path to the executable
                Arguments = command,         // Command-line arguments
                UseShellExecute = false,     // Required for CreateNoWindow to work
                CreateNoWindow = true,       // Do not create a window
                WindowStyle = ProcessWindowStyle.Hidden // Start hidden
            };

            Process.Start(startInfo);

            //run it again after a slight delay to make sure it takes effect
            Thread.Sleep(50);
            Process.Start(startInfo);
        }

        public static string GetAudioDevice()
        {
            var enumerator = new MMDeviceEnumerator();
            var defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            var properties = defaultDevice.Properties;

            return getMMDeviceDeviceDesc(defaultDevice);

            //string str = "Soundcore Speakers (Realtek(R) Audio)";
        }

        //Used to get PKEY_Device_DeviceDesc from MMDevice, this is the speaker name used to set the surround sound using svcl.exe
        public static string getMMDeviceDeviceDesc(MMDevice device)
        {
            var properties = device.Properties;

            properties[PropertyKeys.PKEY_Device_DeviceDesc].Value.ToString();
            if (properties.Contains(PropertyKeys.PKEY_Device_DeviceDesc))
            {
                return (string)properties[PropertyKeys.PKEY_Device_DeviceDesc].Value;
            }
            return "No Audio Device Description Found";
        }


    }
}
