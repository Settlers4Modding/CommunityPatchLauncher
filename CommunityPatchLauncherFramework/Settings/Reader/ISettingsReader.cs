using CommunityPatchLauncherFramework.Settings.Container;
using System.Collections.Generic;

namespace CommunityPatchLauncherFramework.Settings.Reader
{
    /// <summary>
    /// This interface will define a settings reader
    /// </summary>
    public interface ISettingsReader
    {
        /// <summary>
        /// Load a specific value from a given connection string
        /// </summary>
        /// <param name="key">The key to search for</param>
        /// <param name="connectionString">The connection string to use</param>
        /// <returns></returns>
        SettingPair LoadSetting(string key, string connectionString);

        /// <summary>
        /// Load all the settings from the file
        /// </summary>
        /// <returns>All the settings in the file</returns>
        HashSet<SettingPair> GetAllSettings();
    }
}
