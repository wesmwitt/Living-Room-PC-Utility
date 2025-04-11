using IniParser.Model;

namespace Living_Room_PC_Utility
{
    internal class RecentPrograms
    {

        private static string iniSectionName = "RecentPrograms";

        public static Dictionary<string, string> GetRecentProgramsDictionary()
        {
            Dictionary<string, string> recentPrograms = new Dictionary<string, string>();
            
            IniData data = GetRecentPrograms();

            // Read the recent programs from the INI file
            foreach (var item in data[iniSectionName])
            {
                recentPrograms.Add(item.KeyName, item.Value);
            }

            return recentPrograms;
        }

        public static IniData GetRecentPrograms()
        {
            return IniHelper.GetIniFileData(IniNames.RecentProgramList);
        }

        //Requires special logic to add to the bottom of the 
        public static void AddRecentProgram(string name, string title)
        {

            var data = GetRecentPrograms();

            // Create a new section to reorder keys
            var newSection = new SectionData(iniSectionName);

            // Add all keys except the one to be moved
            foreach (var key in data[iniSectionName])
            {
                if (key.KeyName != name)
                {
                    newSection.Keys.AddKey(key.KeyName, key.Value);
                }
            }

            // Add the key to the bottom of the section
            newSection.Keys.AddKey(name, title);

            // Replace the old section with the new one
            data.Sections.RemoveSection(iniSectionName);
            data.Sections.Add(newSection);

            IniHelper.SetIniFileData(IniNames.RecentProgramList, data);
        }

    }

}