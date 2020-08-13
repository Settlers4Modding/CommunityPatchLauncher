using CommunityPatchLauncherFramework.Settings.Manager;
using System.IO;

namespace CommunityPatchLauncher.Commands
{
    internal class SaveSettingsCommand : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            bool canSave = false;
            if (parameter is SettingManager settingManager)
            {
                canSave = true;
                string gameFolder = settingManager.GetValue<string>("GameFolder");
                string DownloadFolder = settingManager.GetValue<string>("DownloadFolder");
                canSave &= File.Exists(gameFolder + "S4_Main.exe");
                canSave &= Directory.Exists(DownloadFolder);
            }
            return canSave;
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            if (parameter is SettingManager manager)
            {
                manager.SaveSettings();
            }
        }
    }
}
