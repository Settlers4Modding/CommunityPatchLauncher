using System.Collections.Generic;

namespace CommunityPatchLauncherFramework.Settings.Container
{
    /// <summary>
    /// This class will alow you to serialize the setting to a file
    /// </summary>
    public class SerialaizeableSettingFile
    {
        /// <summary>
        /// All the settings in this file
        /// </summary>
        public List<SerializeableSettingsPair> Settings;

        /// <summary>
        /// Create a new instance of this setting file class
        /// </summary>
        public SerialaizeableSettingFile()
        {
            Settings = new List<SerializeableSettingsPair>();
        }

        /// <summary>
        /// Create a new instance of this setting file class
        /// </summary>
        /// <param name="settings">The settings to use as base data</param>
        public SerialaizeableSettingFile(HashSet<SettingPair> settings)
            : this()
        {
            foreach (SettingPair pair in settings)
            {
                Settings.Add(new SerializeableSettingsPair(pair));
            }
        }

        /// <summary>
        /// Get a useable setting pair data set
        /// </summary>
        /// <returns>A hash set with all the settings</returns>
        public HashSet<SettingPair> GetSettingPairs()
        {
            HashSet<SettingPair> settingPairs = new HashSet<SettingPair>();
            foreach (SerializeableSettingsPair pair in Settings)
            {
                settingPairs.Add(pair.GetSettingPair());
            }

            return settingPairs;
        }
    }
}
