using CommunityPatchLauncherFramework.Settings.Container;
using CommunityPatchLauncherFramework.Settings.Reader;
using System.Collections.Generic;
using System.Configuration;

namespace CommunityPatchLauncher.Settings.Reader
{
    /// <summary>
    /// This class will read the wpf setting file
    /// </summary>
    class WpfPropertyReader : ISettingReader
    {
        /// <inheritdoc/>
        public HashSet<SettingPair> LoadSettings(string connectionString)
        {
            HashSet<SettingPair> returnSettings = new HashSet<SettingPair>();
            foreach (SettingsProperty property in Properties.Settings.Default.Properties)
            {
                returnSettings.Add(new SettingPair(property.Name, property.DefaultValue, property.PropertyType));
            }

            return returnSettings;
        }
    }
}
