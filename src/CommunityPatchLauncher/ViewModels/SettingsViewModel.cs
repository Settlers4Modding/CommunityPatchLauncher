using CommunityPatchLauncher.Commands;
using CommunityPatchLauncherFramework.Settings.Manager;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    internal class SettingsViewModel : BaseViewModel
    {
        public ICommand OpenSettingFolderCommand { get; private set; }
        public ICommand OpenDownloadFolderCommand { get; private set; }
        public ICommand OpenGameFolderCommand { get; private set; }
        public ICommand ResetAgreementCommand { get; private set; }
        public ICommand SaveSettingCommand { get; private set; }
        public ICommand ResetSettingCommand { get; private set; }

        public IDataCommand AutoDetectGameFolder { get; private set; }
        public IDataCommand ManuelSelectGameFolder { get; private set; }

        public IDataCommand SelectFolder { get; private set; }

        public SettingManager SettingManager => settingManager;

        public string GameFolder
        {
            get => gameFolder;
            set
            {
                gameFolder = value;
                if (downloadFolder != null)
                {
                    settingManager.AddValue("GameFolder", gameFolder);
                    RaisePropertyChanged("SettingManager");
                }
                RaisePropertyChanged("GameFolder");
            }
        }
        private string gameFolder;
        public string DownloadFolder
        {
            get => downloadFolder;
            set
            {
                downloadFolder = value;
                if (downloadFolder != null)
                {
                    settingManager.AddValue("DownloadFolder", downloadFolder);
                    RaisePropertyChanged("SettingManager");
                }
                
                RaisePropertyChanged("DownloadFolder");
            }
        }
        private string downloadFolder;

        public SettingsViewModel()
        {
            GameFolder = settingManager?.GetValue<string>("GameFolder");
            DownloadFolder = settingManager?.GetValue<string>("DownloadFolder");

            OpenSettingFolderCommand = new OpenFolderCommand(settingManager.SettingFolderPath);
            OpenDownloadFolderCommand = new OpenFolderCommand(DownloadFolder);
            OpenGameFolderCommand = new OpenFolderCommand(GameFolder);
            AutoDetectGameFolder = new InstallationFromRegistryCommand();
            ManuelSelectGameFolder = new InstallationFromManuelSelectionCommand();
            SelectFolder = new SelectFolderCommand();
            SaveSettingCommand = new SaveSettingsCommand();

            SelectFolder.Executed += SelectFolder_Executed;
            AutoDetectGameFolder.Executed += AutoDetectGameFolder_Executed;
            ManuelSelectGameFolder.Executed += AutoDetectGameFolder_Executed;
        }

        private void SelectFolder_Executed(object sender, EventArguments.DataCommandEventArg e)
        {
            DownloadFolder = e.GetData<string>();
        }

        private void AutoDetectGameFolder_Executed(object sender, EventArguments.DataCommandEventArg e)
        {
            GameFolder = e.GetData<string>();
        }

        protected override void AddWindowResizeableCommand()
        {
            
        }
    }
}
