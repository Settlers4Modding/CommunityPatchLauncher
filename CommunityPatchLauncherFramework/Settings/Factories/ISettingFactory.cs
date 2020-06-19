using CommunityPatchLauncherFramework.Settings.Manager;

namespace CommunityPatchLauncherFramework.Settings.Factories
{
    /// <summary>
    /// A factory to create a new settings manager
    /// </summary>
    public interface ISettingFactory
    {
        /// <summary>
        /// This method will return you a ready to use Settings manager
        /// </summary>
        /// <returns>A instance of a settings manager</returns>
        SettingManager GetSettingsManager();

        /// <summary>
        /// This method will return you a ready to use Settings manager
        /// </summary>
        /// <param name="settingsConnectionString">The connection stream for the settings manager</param>
        /// <returns>A instance of a settings manager</returns>
        SettingManager GetSettingsManager(string settingsConnectionString);
    }
}
