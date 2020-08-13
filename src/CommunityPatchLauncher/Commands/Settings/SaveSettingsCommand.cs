using CommunityPatchLauncherFramework.Settings.Manager;
using System.IO;

namespace CommunityPatchLauncher.Commands.Settings
{
    internal class SaveSettingsCommand : BaseCommand
    {
        private readonly SettingManager settingManager;

        public SaveSettingsCommand() : this(null)
        {

        }

        public SaveSettingsCommand(SettingManager settingManager)
        {
            this.settingManager = settingManager;
        }

        public override bool CanExecute(object parameter)
        {
            bool canSave = false;
            if (GetSettingManagerToUse(parameter) is SettingManager manager)
            {
                canSave = true;
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
