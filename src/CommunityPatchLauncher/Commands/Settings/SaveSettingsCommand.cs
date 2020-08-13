using CommunityPatchLauncherFramework.Settings.Manager;
using System.IO;

namespace CommunityPatchLauncher.Commands.Settings
{
    internal class SaveSettingsCommand : BaseCommand
    {
        private readonly bool checkSettings;
        private readonly SettingManager settingManager;

        public SaveSettingsCommand(SettingManager settingManager) : this(true, settingManager)
        {

        }

        public SaveSettingsCommand(bool checkSettings, SettingManager settingManager)
        {
            this.checkSettings = checkSettings;
            this.settingManager = settingManager;
        }

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

        private SettingManager GetSettingManagerToUse(object parameter)
        {
            SettingManager manager = settingManager;
            if (parameter is SettingManager parameterManager)
            {
                manager = parameterManager;
            }
            return manager;
        }

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
