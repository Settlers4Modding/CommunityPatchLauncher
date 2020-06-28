using CommunityPatchLauncherFramework.Settings.Container;
using System.Collections.Generic;

namespace CommunityPatchLauncherFramework.Settings.Writer
{
    /// <summary>
    /// This interface will define a setting writer which will save a given value
    /// </summary>
    public interface ISettingWriter
    {
        /// <summary>
        /// Write a new setting to the setting file
        /// </summary>
        /// <param name="settings">The settings to write</param>
        /// <param name="connectionString">The connection string to use</param>
        /// <returns>True if saving was successful</returns>
        bool WriteSettings(HashSet<SettingPair> settings, string connectionString);
    }
}
