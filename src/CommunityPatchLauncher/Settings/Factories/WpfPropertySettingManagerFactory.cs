using CommunityPatchLauncher.Settings.Reader;
using CommunityPatchLauncherFramework.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;
using CommunityPatchLauncherFramework.Settings.Writer;

namespace CommunityPatchLauncher.Settings.Factories
{
    /// <summary>
    /// This factory will create a read only manager for the wpf setting file
    /// </summary>
    class WpfPropertySettingManagerFactory : ISettingFactory
    {
        /// <inheritdoc/>
        public SettingManager GetSettingsManager()
        {
            return new SettingManager(new WpfPropertyReader(), new DummySettingWriter(), string.Empty);
        }

        /// <inheritdoc/>
        public SettingManager GetSettingsManager(string settingsConnectionString)
        {
            return GetSettingsManager();
        }
    }
}
