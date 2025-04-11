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

        public GlobalConfig()
        {
            SurroundSoundSetting = 0;
            HDRSetting = 0;
            AtmosSetting = 0;
            VolumeSetting = 0;
            DefaultVolumeSetting = 0;
        }

        public GlobalConfig(string surroundSoundSettingStr = "0", string hdrSettingStr = "0", string atmosSettingStr = "0", string volumeSettingStr = "0", string defaultVolumeSetting = "0")
        {
            SurroundSoundSetting = ParseInt(surroundSoundSettingStr);
            HDRSetting = ParseInt(hdrSettingStr);
            AtmosSetting = ParseInt(atmosSettingStr);
            VolumeSetting = ParseInt(volumeSettingStr);
            DefaultVolumeSetting = ParseInt(defaultVolumeSetting);
        }

        public GlobalConfig(int surroundSoundSetting = 0, int hdrSetting = 0, int atmosSetting = 0, int volumeSetting = 0, int defaultVolumeSetting = 0)
        {
            SurroundSoundSetting = surroundSoundSetting;
            HDRSetting = hdrSetting;
            AtmosSetting = atmosSetting;
            VolumeSetting = volumeSetting;
            DefaultVolumeSetting = defaultVolumeSetting;
        }

        public static void SetGlobalConfigFileData(int surroundSoundSetting, int hdrSetting, int atmosSetting, int volumeSetting, int defaultVolumeSetting)
        {
            var configIni = new IniData();
            configIni["Settings"]["surroundType"] = surroundSoundSetting.ToString();
            configIni["Settings"]["hdr"] = hdrSetting.ToString();
            configIni["Settings"]["atmos"] = atmosSetting.ToString();
            configIni["Settings"]["volume"] = volumeSetting.ToString();
            configIni["Settings"]["defaultVolume"] = defaultVolumeSetting.ToString();

            IniHelper.SetIniFileData(IniNames.Config, configIni);
        }

        public static GlobalConfig GetGlobalConfigFileData()
        {
            var configIni = IniHelper.GetIniFileData(IniNames.Config);

            return new GlobalConfig(
                configIni["Settings"].ContainsKey("surroundType") ? configIni["Settings"]["surroundType"] : "0",
                configIni["Settings"].ContainsKey("hdr") ? configIni["Settings"]["hdr"] : "0",
                configIni["Settings"].ContainsKey("atmos") ? configIni["Settings"]["atmos"] : "0",
                configIni["Settings"].ContainsKey("volume") ? configIni["Settings"]["volume"] : "0",
                configIni["Settings"].ContainsKey("defaultVolume") ? configIni["Settings"]["defaultVolume"] : "0"
            );
        }

        private static int ParseInt(string value)
        {
            return int.TryParse(value, out int result) ? result : 0;
        }
    }
}
