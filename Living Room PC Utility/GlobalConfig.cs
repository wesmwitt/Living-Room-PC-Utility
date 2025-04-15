using IniParser.Model;

namespace Living_Room_PC_Utility
{
    public struct GlobalConfig
    {
        public int SurroundSoundSetting;
        public int HDRSetting;
        public int AtmosSetting;
        public int VolumeSetting;
        public int DefaultVolumeSetting;
        public string StartupScript;
        public string ShutdownScript;

        public GlobalConfig()
        {
            SurroundSoundSetting = 0;
            HDRSetting = 0;
            AtmosSetting = 0;
            VolumeSetting = 0;
            DefaultVolumeSetting = 0;
            StartupScript = "";
            ShutdownScript = "";
        }

        public GlobalConfig(string surroundSoundSettingStr = "0", string hdrSettingStr = "0", string atmosSettingStr = "0", string volumeSettingStr = "0", string defaultVolumeSetting = "0", string startupScript="", string shutdownScript="")
        {
            SurroundSoundSetting = ParseInt(surroundSoundSettingStr);
            HDRSetting = ParseInt(hdrSettingStr);
            AtmosSetting = ParseInt(atmosSettingStr);
            VolumeSetting = ParseInt(volumeSettingStr);
            DefaultVolumeSetting = ParseInt(defaultVolumeSetting);
            StartupScript = startupScript;
            ShutdownScript = shutdownScript;

        }

        public GlobalConfig(int surroundSoundSetting = 0, int hdrSetting = 0, int atmosSetting = 0, int volumeSetting = 0, int defaultVolumeSetting = 0, string startupScript = "", string shutdownScript = "")
        {
            SurroundSoundSetting = surroundSoundSetting;
            HDRSetting = hdrSetting;
            AtmosSetting = atmosSetting;
            VolumeSetting = volumeSetting;
            DefaultVolumeSetting = defaultVolumeSetting;
            StartupScript = startupScript;
            ShutdownScript = shutdownScript;

        }

        public static void SetGlobalConfigFileData(int surroundSoundSetting, int hdrSetting, int atmosSetting, int volumeSetting, int defaultVolumeSetting, string startupScript, string shutdownScript)
        {
            var configIni = new IniData();
            configIni["Settings"]["surroundType"] = surroundSoundSetting.ToString();
            configIni["Settings"]["hdr"] = hdrSetting.ToString();
            configIni["Settings"]["atmos"] = atmosSetting.ToString();
            configIni["Settings"]["volume"] = volumeSetting.ToString();
            configIni["Settings"]["defaultVolume"] = defaultVolumeSetting.ToString();
            configIni["Settings"]["startupScript"] = startupScript;
            configIni["Settings"]["shutdownScript"] = shutdownScript;

            IniHelper.SetIniFileData(IniNames.Config, configIni);
        }

        public static void SetGlobalConfigFileData(GlobalConfig globalConfig)
        {
            SetGlobalConfigFileData(globalConfig.SurroundSoundSetting, globalConfig.HDRSetting, globalConfig.AtmosSetting, globalConfig.VolumeSetting, globalConfig.DefaultVolumeSetting, globalConfig.StartupScript, globalConfig.ShutdownScript);
        }

        public static GlobalConfig GetGlobalConfigFileData()
        {
            var configIni = IniHelper.GetIniFileData(IniNames.Config);

            return new GlobalConfig(
                configIni["Settings"].ContainsKey("surroundType") ? configIni["Settings"]["surroundType"] : "0",
                configIni["Settings"].ContainsKey("hdr") ? configIni["Settings"]["hdr"] : "0",
                configIni["Settings"].ContainsKey("atmos") ? configIni["Settings"]["atmos"] : "0",
                configIni["Settings"].ContainsKey("volume") ? configIni["Settings"]["volume"] : "0",
                configIni["Settings"].ContainsKey("defaultVolume") ? configIni["Settings"]["defaultVolume"] : "0",
                configIni["Settings"].ContainsKey("startupScript") ? configIni["Settings"]["startupScript"] : "",
                configIni["Settings"].ContainsKey("shutdownScript") ? configIni["Settings"]["shutdownScript"] : ""
            );
        }

        private static int ParseInt(string value)
        {
            return int.TryParse(value, out int result) ? result : 0;
        }
    }
}
