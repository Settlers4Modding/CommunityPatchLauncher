using CommunityPatchLauncherFramework.Settings.Container;

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
        /// <param name="key">The key of the setting to load</param>
        /// <param name="value">The value of the setting to write</param>
        /// <param name="connectionString">The connection string to use</param>
        /// <returns>True if saving was successful</returns>
        bool WriteSetting(string key, object value, string connectionString);

        /// <summary>
        /// Write a new setting to the setting file
        /// </summary>
        /// <param name="settingPair">The setting pair to write</param>
        /// <param name="connectionString">The connection string to use</param>
        /// <returns>True if saving was successful</returns>
        bool WriteSetting(SettingPair settingPair, string connectionString);
    }
}
