using CommunityPatchLauncherFramework.Settings.Container;
using CommunityPatchLauncherFramework.Settings.Reader;
using CommunityPatchLauncherFramework.Settings.Writer;

namespace CommunityPatchLauncherFramework.Settings.Manager
{
    /// <summary>
    /// A class to load and save application settings
    /// </summary>
    public class SettingsManager
    {
        /// <summary>
        /// Settings reader to use
        /// </summary>
        private readonly ISettingsReader reader;

        /// <summary>
        /// Settings writer to use
        /// </summary>
        private readonly ISettingsWriter writer;

        /// <summary>
        /// The default settings path to use for loading
        /// </summary>
        private readonly string defaultSettingsPath;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="reader">The reader to use</param>
        /// <param name="writer">The writer to use</param>
        public SettingsManager(ISettingsReader reader, ISettingsWriter writer)
            : this(reader, writer, string.Empty)
        {

        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="reader">The reader to use</param>
        /// <param name="writer">The writer to use</param>
        /// <param name="defaultSettingsPath">The path to use as default setting path</param>
        public SettingsManager(ISettingsReader reader, ISettingsWriter writer, string defaultSettingsPath)
        {
            this.reader = reader;
            this.writer = writer;
            this.defaultSettingsPath = defaultSettingsPath;
        }

        /// <summary>
        /// Load a setting from the default setting path
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public SettingPair GetValue(string key)
        {
            return GetValue(key, defaultSettingsPath);
        }

        /// <summary>
        /// Get a setting from a given setting path
        /// </summary>
        /// <param name="key">The key of the setting to load</param>
        /// <param name="connectionString">The connection string to use</param>
        /// <returns></returns>
        public SettingPair GetValue(string key, string connectionString)
        {
            return reader.LoadSetting(key, connectionString);
        }

        /// <summary>
        /// Write a value to the setting file using the default connection string
        /// </summary>
        /// <param name="key">The key of the setting to use</param>
        /// <param name="value">The value to use for the setting</param>
        /// <returns>True if writing was successful</returns>
        public bool WriteValue(string key, object value)
        {
            return WriteValue(key, value, defaultSettingsPath);
        }

        /// <summary>
        /// Write a value to the setting file
        /// </summary>
        /// <param name="key">The key of the setting to use</param>
        /// <param name="value">The value to use for the setting</param>
        /// <param name="connectionString">The connection string to use</param>
        /// <returns>True if writing was successful</returns>
        public bool WriteValue(string key, object value, string connectionString)
        {
            return writer.WriteSetting(key, value, connectionString);
        }
    }
}
