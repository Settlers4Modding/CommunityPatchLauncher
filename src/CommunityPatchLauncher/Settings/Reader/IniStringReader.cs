using CommunityPatchLauncherFramework.Settings.Container;
using CommunityPatchLauncherFramework.Settings.Reader;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CommunityPatchLauncher.Settings.Reader
{
    /// <summary>
    /// This class will interpret the connection string as a ini file, do not try to load a file from the disc
    /// Do not use this class with the settings manager!
    /// </summary>
    internal class IniStringReader : ISettingReader
    {
        /// <summary>
        /// Regex to find the scope in the ini file
        /// </summary>
        private const string SCOPE_REGEX = "\\[[a-zA-Z0-9]+\\]";

        /// <inheritdoc/>
        public HashSet<SettingPair> LoadSettings(string connectionString)
        {
            HashSet<SettingPair> returnSettings = new HashSet<SettingPair>();
            string[] lines = connectionString.Split(new[] { "\n" }, StringSplitOptions.None);
            string currentScope = "";
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (Regex.IsMatch(line, SCOPE_REGEX))
                {
                    currentScope = line.Replace("[", "").Replace("]", "").Trim();
                    continue;
                }

                if (!line.Contains("="))
                {
                    continue;
                }

                string[] data = line.Split('=');
                returnSettings.Add(new SettingPair(currentScope + "/" + data[0], data[1]));
            }
            return returnSettings;
        }
    }
}
