using IniParser;
using IniParser.Model;

namespace Living_Room_PC_Utility
{
    internal class RecentPrograms
    {

        private static string iniSectionName = "RecentPrograms";
        private static string filePath = @"data\RecentProgramList.ini";

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

            // Load the INI file
            FileIniDataParser parser = new FileIniDataParser();
            var path = Path.Combine(Directory.GetCurrentDirectory(), filePath);
            IniData data = new IniData();

            bool fileRead = false;
            int retryCount = 0;
            while (!fileRead && retryCount < 5)
            {
                try
                {
                    data = parser.ReadFile(path);
                    fileRead = true;
                }
                catch (IOException)
                {
                    retryCount++;
                    Thread.Sleep(100); // Wait for 100ms before retrying
                }
            }

            if (!fileRead)
            {
                throw new IOException("Failed to read the file after multiple attempts.");
            }

            return data;
        }

        //Requires special logic to add to the bottom of the 
        public static void AddRecentProgram(string name, string title)
        {
            FileIniDataParser parser = new FileIniDataParser();
            var path = Path.Combine(Directory.GetCurrentDirectory(), filePath);
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


            bool fileWritten = false;
            int retryCount = 0;
            while (!fileWritten && retryCount < 5)
            {
                try
                {
                    parser.WriteFile(path, data);
                    fileWritten = true;
                }
                catch (IOException)
                {
                    retryCount++;
                    Thread.Sleep(100); // Wait for 100ms before retrying
                }
            }

            if (!fileWritten)
            {
                throw new IOException("Failed to write to the file after multiple attempts.");
            }
        }


    }

}