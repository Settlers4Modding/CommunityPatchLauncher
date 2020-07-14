using CommunityPatchLauncherFramework.Settings.Container;
using CommunityPatchLauncherFramework.Settings.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommunityPatchLauncher.Settings.Reader
{
    class IniStringReader : ISettingReader
    {
        private const string SCOPE_REGEX = "\\[[a-zA-Z0-9]+\\]";

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
