using System.IO;
using IniParser;
using IniParser.Model;

namespace Living_Room_PC_Utility
{

    public enum IniNames
    {
        Config,
        ProgramConfigListGlobal,
        ProgramConfigListUser,
        RecentProgramList
    }

    internal class IniHelper
    {
        public static readonly string ConfigPath = @"data\Config.ini";
        public static readonly string ProgramConfigGlobalPath = @"data\ProgramConfigListGlobal.ini";
        public static readonly string ProgramConfigUserPath = @"data\ProgramConfigListUser.ini";
        public static readonly string RecentProgramPath = @"data\RecentProgramList.ini";

        private static readonly Dictionary<IniNames, string> iniPaths = new Dictionary<IniNames, string>
            {
                { IniNames.Config, ConfigPath },
                { IniNames.ProgramConfigListGlobal, ProgramConfigGlobalPath },
                { IniNames.ProgramConfigListUser, ProgramConfigUserPath },
                { IniNames.RecentProgramList, RecentProgramPath }
            };

        public static readonly Dictionary<IniNames, string[]> iniDefaultSections = new Dictionary<IniNames, string[]>
            {
                { IniNames.Config, new string[] { "Settings" } },
                { IniNames.ProgramConfigListGlobal, new string[] { "SurroundSoundSetting", "HDRSetting", "DelaySetting" } },
                { IniNames.ProgramConfigListUser, new string[] { "SurroundSoundSetting", "HDRSetting", "DelaySetting", "VolumeSetting", "StartupScript", "ShutdownScript" } },
                { IniNames.RecentProgramList, new string[] { "RecentPrograms" } }
            };

        private static readonly object fileLock = new object();

        public static string GetFullPath(string filePath)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), filePath);
        }

        public static string GetFullPath(IniNames iniName)
        {
            if (!iniPaths.TryGetValue(iniName, out string filePath))
            {
                throw new ArgumentException($"Invalid INI file name: {iniName}.");
            }
            return GetFullPath(filePath);
        }

        public static IniData GetIniFileData(IniNames iniName)
        {
            if (!iniPaths.TryGetValue(iniName, out string filePath))
            {
                throw new ArgumentException($"Invalid INI file name: {iniName}.");
            }

            CreateFileIfNeeded(iniName);

            IniData data = new IniData();
            var fullPath = GetFullPath(filePath);

            lock (fileLock)
            {
                bool fileRead = false;
                int retryCount = 0;
                while (!fileRead && retryCount < 5)
                {
                    try
                    {
                        FileIniDataParser parser = new FileIniDataParser();
                        data = parser.ReadFile(fullPath);
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
            }

            return data;
        }

        public static void SetIniFileData(IniNames iniName, IniData data)
        {
            if (!iniPaths.TryGetValue(iniName, out string filePath))
            {
                throw new ArgumentException($"Invalid INI file name: {iniName}.");
            }

            var fullPath = GetFullPath(filePath);
            lock (fileLock)
            {
                bool fileWritten = false;
                int retryCount = 0;
                while (!fileWritten && retryCount < 5)
                {
                    try
                    {
                        FileIniDataParser parser = new FileIniDataParser();
                        parser.WriteFile(fullPath, data);
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

        public static bool CheckFileExists(IniNames iniName)
        {
            if (!iniPaths.TryGetValue(iniName, out string filePath))
            {
                throw new ArgumentException($"Invalid INI file name: {iniName}.");
            }
            return CheckFileExists(GetFullPath(filePath));
        }

        public static bool CheckFileExists(string fullPath)
        {
            return File.Exists(fullPath);
        }

        public static void CreateFileIfNeeded(IniNames iniName)
        {
            if (!iniPaths.TryGetValue(iniName, out string filePath))
            {
                throw new ArgumentException($"Invalid INI file name: {iniName}.");
            }

            var fullPath = GetFullPath(filePath);

            lock (fileLock)
            {
                if (!CheckFileExists(fullPath))
                {
                    bool fileCreated = false;
                    int retryCount = 0;
                    while (!fileCreated && retryCount < 5)
                    {
                        try
                        {
                            // Create an empty INI file
                            FileIniDataParser parser = new FileIniDataParser();
                            IniData data = new IniData();

                            // Populate with default sections
                            if (iniDefaultSections.TryGetValue(iniName, out string[] sections))
                            {
                                foreach (var section in sections)
                                {
                                    data.Sections.AddSection(section);
                                }
                            }

                            parser.WriteFile(fullPath, data);
                            fileCreated = true;
                        }
                        catch (IOException)
                        {
                            retryCount++;
                            Thread.Sleep(100); // Wait for 100ms before retrying
                        }
                    }

                    if (!fileCreated)
                    {
                        throw new IOException("Failed to create the file after multiple attempts.");
                    }
                }
            }
        }

        public static void CreateAllFilesIfNeeded()
        {
            foreach (var iniName in iniPaths.Keys)
            {
                CreateFileIfNeeded(iniName);
            }
        }
    }
}
