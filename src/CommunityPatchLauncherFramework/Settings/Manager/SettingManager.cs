using CommunityPatchLauncherFramework.Settings.Container;
using CommunityPatchLauncherFramework.Settings.Reader;
using CommunityPatchLauncherFramework.Settings.Writer;
using System.Collections.Generic;
using System.Linq;

namespace CommunityPatchLauncherFramework.Settings.Manager
{
    /// <summary>
    /// A class to load and save application settings
    /// </summary>
    public class SettingManager
    {
        /// <summary>
        /// Settings reader to use
        /// </summary>
        private readonly ISettingReader reader;

        /// <summary>
        /// Settings writer to use
        /// </summary>
        private readonly ISettingWriter writer;

        /// <summary>
        /// The default settings path to use for loading
        /// </summary>
        private readonly string settingsPath;

        /// <summary>
        /// All the settings in the manager
        /// </summary>
        private HashSet<SettingPair> settings;

        /// <summary>
        /// Setting are loaded already
        /// </summary>
        private bool settingsLoaded;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="reader">The reader to use</param>
        /// <param name="writer">The writer to use</param>
        /// <param name="defaultSettingsPath">The path to use as default setting path</param>
        public SettingManager(ISettingReader reader, ISettingWriter writer, string settingsPath)
        {
            this.reader = reader;
            this.writer = writer;
            this.settingsPath = settingsPath;

            settingsLoaded = false;
            Reload();
        }

        /// <summary>
        /// Load a setting from the default setting path
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public SettingPair GetValue(string key)
        {
            if (!settingsLoaded)
            {
                Reload();
            }

            return settings?.Where(o => o.Key == key).FirstOrDefault();
        }

        /// <summary>
        /// This will directly get you the value from a setting pair
        /// </summary>
        /// <typeparam name="T">The type to load</typeparam>
        /// <param name="key">The key to search for</param>
        /// <returns>The requested setting</returns>
        public T GetValue<T>(string key)
        {
            SettingPair pair = GetValue(key);
            return pair == null ? default : pair.GetValue<T>();
        }

        /// <summary>
        /// Write a value to the setting file using the default connection string
        /// </summary>
        /// <param name="key">The key of the setting to use</param>
        /// <param name="value">The value to use for the setting</param>
        /// <returns>True if writing was successful</returns>
        public void AddValue(string key, object value)
        {
            if (settings == null)
            {
                settings = new HashSet<SettingPair>();
            }
            SettingPair loadedValue = GetValue(key);
            if (loadedValue == null)
            {
                settings.Add(new SettingPair(key, value));
                return;
            }
            loadedValue.ChangeValue(value);
        }

        /// <summary>
        /// Reload all the settings
        /// </summary>
        public void Reload()
        {
            settings = reader.LoadSettings(settingsPath);
            settingsLoaded = true;
        }

        /// <summary>
        /// Save the cached settings
        /// </summary>
        /// <returns>True if saving was successful</returns>
        public bool SaveSettings()
        {
            settingsLoaded = false;
            return writer.WriteSettings(settings, settingsPath);
        }
    }
}
