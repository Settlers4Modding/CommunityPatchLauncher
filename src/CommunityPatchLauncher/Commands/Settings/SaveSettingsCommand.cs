using CommunityPatchLauncherFramework.Settings.Manager;
using System.IO;

namespace CommunityPatchLauncher.Commands.Settings
{
    /// <summary>
    /// This command will save the settings
    /// </summary>
    internal class SaveSettingsCommand : BaseCommand
    {
        /// <summary>
        /// Check the settings before saving
        /// </summary>
        private readonly bool checkSettings;
        
        /// <summary>
        /// The settings manager to use
        /// </summary>
        private readonly SettingManager settingManager;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="settingManager">The setting manager to use</param>
        public SaveSettingsCommand(SettingManager settingManager) : this(true, settingManager)
        {

        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="checkSettings">Check the settings before saving</param>
        /// <param name="settingManager">The setting manager to use</param>
        public SaveSettingsCommand(bool checkSettings, SettingManager settingManager)
        {
            this.checkSettings = checkSettings;
            this.settingManager = settingManager;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            bool canSave = true;
            if (checkSettings && GetSettingManagerToUse(parameter) is SettingManager manager)
            {
                string gameFolder = manager.GetValue<string>("GameFolder");
                string DownloadFolder = manager.GetValue<string>("DownloadFolder");
                canSave &= File.Exists(gameFolder + "S4_Main.exe");
                canSave &= Directory.Exists(DownloadFolder);
            }
            return canSave;
        }

        /// <summary>
        /// Get the setting manager to use
        /// </summary>
        /// <param name="parameter">The possible other setting manager</param>
        /// <returns>The correct setting manager</returns>
        private SettingManager GetSettingManagerToUse(object parameter)
        {
            SettingManager manager = settingManager;
            if (parameter is SettingManager parameterManager)
            {
                manager = parameterManager;
            }
            return manager;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            if (GetSettingManagerToUse(parameter) is SettingManager manager)
            {
                manager.SaveSettings();
            }
        }
    }
}
