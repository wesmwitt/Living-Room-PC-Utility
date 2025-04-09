using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IniParser;
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

        public GlobalConfig() {
            this.SurroundSoundSetting = 0;
            this.HDRSetting = 0;
            this.AtmosSetting = 0;
            this.VolumeSetting = 0;
            this.DefaultVolumeSetting = 0;
        }

        public GlobalConfig(string surroundSoundSettingStr="0", string hdrSettingStr="0", string atmosSettingStr="0", string volumeSettingStr="0", string defaultVolumeSetting="0")
        {
            int surroundSoundSetting = 0;
            if (int.TryParse(surroundSoundSettingStr, out int surNum))
            {
                surroundSoundSetting = surNum;
            }

            int hdrSetting = 0;
            if (int.TryParse(hdrSettingStr, out int hdrNum))
            {
                hdrSetting = hdrNum;
            }

            int atmosSetting = 0;
            if (int.TryParse(atmosSettingStr, out int atmosNum))
            {
                atmosSetting = atmosNum;
            }

            int volumeSetting = 0;
            if (int.TryParse(volumeSettingStr, out int volNum))
            {
                volumeSetting = volNum;
            }
            int defaultVolume = 0;
            if (int.TryParse(defaultVolumeSetting, out int defVolNum))
            {
                defaultVolume = defVolNum;
            }

            this.SurroundSoundSetting = surroundSoundSetting;
            this.HDRSetting = hdrSetting;
            this.AtmosSetting = atmosSetting;
            this.VolumeSetting = volumeSetting;
            this.DefaultVolumeSetting = defaultVolume;
        }

        public GlobalConfig(int surroundSoundSetting=0, int hdrSetting=0, int atmosSetting=0, int volumeSetting=0, int defaultVolumeSetting=0)
        {
            this.SurroundSoundSetting = surroundSoundSetting;
            this.HDRSetting = hdrSetting;
            this.AtmosSetting = atmosSetting;
            this.VolumeSetting = volumeSetting;
            this.DefaultVolumeSetting = defaultVolumeSetting;
        }

        public static void UpdateConfigFile(int surroundSoundSetting, int hdrSetting, int atmosSetting, int volumeSetting, int defaultVolumeSetting)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"data\Config.ini");
            var parser = new FileIniDataParser();
            IniData configIni = parser.ReadFile(path);

            configIni["Settings"]["surroundType"] = surroundSoundSetting.ToString();
            configIni["Settings"]["hdr"] = ""+hdrSetting.ToString();
            configIni["Settings"]["atmos"] = ""+ atmosSetting.ToString();
            configIni["Settings"]["volume"] = ""+ volumeSetting.ToString();
            configIni["Settings"]["defaultVolume"] = "" + defaultVolumeSetting.ToString();

            parser.WriteFile(path, configIni);

        }

    }
}
