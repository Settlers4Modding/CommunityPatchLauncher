using CommunityPatchLauncherFramework.Settings.Container;
using System.Collections.Generic;

namespace CommunityPatchLauncherFramework.Settings.Reader
{
    /// <summary>
    /// This interface will define a settings reader
    /// </summary>
    public interface ISettingReader
    {
        /// <summary>
        /// Load all the settings from the file
        /// </summary>
        /// <returns>All the settings in the file</returns>
        HashSet<SettingPair> LoadSettings(string connectionString);
    }
}
