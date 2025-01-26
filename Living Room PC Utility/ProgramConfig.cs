using IniParser;
using IniParser.Model;


namespace Living_Room_PC_Utility
{

    public struct ProgramConfig
    {
        //Global or User configurations
        public string SurroundSoundSetting;
        public string HDRSetting;
        public string DelaySetting;
        //User configurations only
        public string VolumeSetting;
        public string StartupScript;
        public string ShutdownScript;

        public ProgramConfig()
        {
            this.SurroundSoundSetting = "";
            this.HDRSetting = "";
            this.DelaySetting = "";
            this.VolumeSetting = "";
            this.StartupScript = "";
            this.ShutdownScript = "";
        }

        public ProgramConfig(string surroundSoundSetting="0", string hdrSetting="0", string delaySetting="0", string volumeSetting="", string startupScript="", string shutdownScript="")

        {
            SurroundSoundSetting = surroundSoundSetting;
            HDRSetting = hdrSetting;
            DelaySetting = delaySetting;
            VolumeSetting = volumeSetting;
            StartupScript = startupScript;
            ShutdownScript = shutdownScript;
        }

        public void setValue(string attributeName, string value)
        {
            if (attributeName == "SurroundSoundSetting"){
                SurroundSoundSetting = value;
            }else if (attributeName == "HDRSetting"){
                HDRSetting = value;
            }else if (attributeName == "DelaySetting"){
                DelaySetting = value;
            }else if (attributeName == "VolumeSetting"){
                VolumeSetting = value;
            }else if (attributeName == "StartupScript"){
                StartupScript = value; //keep this as a string
            }else if (attributeName == "ShutdownScript"){
                ShutdownScript = value; //keep this as a string
            }
        }

        public string getValue(string attributeName)
        {
            if (attributeName == "SurroundSoundSetting")
            {
                return SurroundSoundSetting;
            }
            else if (attributeName == "HDRSetting")
            {
                return HDRSetting;
            }
            else if (attributeName == "DelaySetting")
            {
                return DelaySetting;
            }
            else if (attributeName == "VolumeSetting")
            {
                return VolumeSetting;
            }
            else if (attributeName == "StartupScript")
            {
                return StartupScript;
            }
            else if (attributeName == "ShutdownScript")
            {
                return ShutdownScript;
            }
            return "Error";
        }

        public string getSurroundSoundFriendlyName()
        {
            return getFriendlyNameForSetting("SurroundSoundSetting", SurroundSoundSetting);

        }

        public string getHDRFriendlyName()
        {
            return getFriendlyNameForSetting("HDRSetting", HDRSetting);
        }

        public string getDelayFriendlyName()
        {
            return getFriendlyNameForSetting("DelaySetting", DelaySetting);
        }

        public string getVolumeFriendlyName()
        {
            return getFriendlyNameForSetting("VolumeSetting", VolumeSetting);
        }

        public string toString()
        {
            return "SurroundSoundSetting: " + SurroundSoundSetting + ", HDRSetting: " + HDRSetting + ", DelaySetting: " + DelaySetting + ", VolumeSetting: " + VolumeSetting;
        }

        public string toFriendlyString()
        {
            return "Surround Sound: " + getSurroundSoundFriendlyName() + ", HDR: " + getHDRFriendlyName() + ", Delay: " + getDelayFriendlyName() + ", Volume: " + getVolumeFriendlyName();
        }

        public Dictionary<string, string> toDictionary()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("SurroundSoundSetting", SurroundSoundSetting);
            dict.Add("HDRSetting", HDRSetting);
            dict.Add("DelaySetting", DelaySetting);
            dict.Add("VolumeSetting", VolumeSetting);
            dict.Add("StartupScript", StartupScript);
            dict.Add("ShutdownScript", ShutdownScript);
            return dict;
        }

        public static string getFriendlyNameForSetting(string setting, string value)
        {
            if(value == "")
            {
                return "";
            }
            if(setting == "SurroundSoundSetting")
            {
                if (value == "0"){return "Stereo (Disabled)";}
                else if (value == "1"){return "5.1 Surround";}
                else if (value == "2"){return "7.1 Surround";}
            }
            if(setting == "HDRSetting" || setting == "DelaySetting" || setting == "General")
            {
                if (value == "0"){return "Disabled";}
                else if (value == "1"){return "Enabled";}
            }
            if(setting == "VolumeSetting")
            {
                return "" + value;
            }
            return "Error";
        }   

        public static string getSettingForFriendlyName(string setting, string value)
        {
            if(value == "" || value == "Unset")
            {
                return "";
            }
            if (setting == "SurroundSoundSetting")
            {
                if (value == "Stereo (Disabled)") {return "0";}
                else if (value == "5.1 Surround") {return "1";}
                else if (value == "7.1 Surround") {return "2";}
            }
            if (setting == "HDRSetting" || setting == "DelaySetting" || setting == "General")
            {
                if (value == "Disabled"){return "0";}
                else if (value == "Enabled"){return "1";}
            }
            return "-1";
        }


        //private Dictionary<string, ProgramConfig> programConfigs = new Dictionary<string, ProgramConfig>();

        public static Dictionary<string, ProgramConfig> GetProgramConfigDictionary(string dict)
        {
            var parser = new FileIniDataParser();
            
            IniData programConfigData = new IniData();
            if (dict == "global"){
                programConfigData = parser.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), @"data\ProgramConfigListGlobal.ini"));
            }else if (dict == "user"){
                programConfigData = parser.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), @"data\ProgramConfigListUser.ini"));
            }else if (dict == "both"){
                programConfigData = parser.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), @"data\ProgramConfigListGlobal.ini"));
                var userConfigData = parser.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), @"data\ProgramConfigListUser.ini"));
                programConfigData.Merge(userConfigData);
            }

            Dictionary<string, ProgramConfig> programConfigs = new Dictionary<string, ProgramConfig>();

            foreach (var section in programConfigData.Sections)
            {
                foreach (var item in programConfigData[section.SectionName])
                {

                    //if(item.KeyName == "SurroundSoundSetting")
                    //{
                    //    var temp = 0;
                    //}

                    ProgramConfig tempProgConfig;
                    bool hasConfig = programConfigs.TryGetValue(item.KeyName, out tempProgConfig);
                    if (hasConfig)
                    {   //update existing entry
                        tempProgConfig.setValue(section.SectionName, item.Value);
                        programConfigs[item.KeyName] = tempProgConfig;
                    }
                    else
                    {   //add new entry
                        tempProgConfig = new ProgramConfig(); //by default sections evaluate to null
                        tempProgConfig.setValue(section.SectionName, item.Value);
                        programConfigs.Add(item.KeyName, tempProgConfig);
                    }

                }
            }

            return programConfigs;
        }

        public static void UpdateProgramConfigListUser(string name, ProgramConfig programConfig)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"data\ProgramConfigListUser.ini");
            var parser = new FileIniDataParser();
            IniData programConfigListUser = parser.ReadFile(path);
            var programConfigListUserDictionary = GetProgramConfigDictionary("user");

            //TODO logic to delete if receiving "" for value

            foreach (var section in programConfigListUser.Sections)
            {
                foreach(var property in programConfig.toDictionary())
                {
                    if(programConfig.getValue(section.SectionName) == "")
                    {
                        //delete key
                        programConfigListUser[section.SectionName].RemoveKey(name);
                    }
                    else
                    {
                        //add key
                        programConfigListUser[section.SectionName][name] = programConfig.getValue(section.SectionName);
                    }

                }
            }
                parser.WriteFile(path, programConfigListUser);
        }

    }
}
