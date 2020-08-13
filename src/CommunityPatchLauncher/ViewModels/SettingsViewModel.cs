using CommunityPatchLauncher.BindingData;
using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Commands.ApplicationWindow;
using CommunityPatchLauncher.Commands.DataCommands;
using CommunityPatchLauncher.Commands.Os;
using CommunityPatchLauncher.Commands.Settings;
using CommunityPatchLauncherFramework.Settings.Manager;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    internal class SettingsViewModel : BaseViewModel
    {
        private bool languageChanged;

        public ICommand OpenSettingFolderCommand { get; private set; }
        public ICommand OpenDownloadFolderCommand { get; private set; }
        public ICommand OpenGameFolderCommand { get; private set; }
        public ICommand ResetAgreementCommand { get; private set; }
        public ICommand SaveSettingCommand { get; private set; }
        public ICommand ResetSettingCommand { get; private set; }

        public IDataCommand AutoDetectGameFolder { get; private set; }
        public IDataCommand ManuelSelectGameFolder { get; private set; }

        public IDataCommand SelectFolder { get; private set; }

        public IReadOnlyList<LanguageItem> SelectableLanguages { get; private set; }

        public LanguageItem SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                settingManager.AddValue("Language", selectedItem.IsoCode);
                RaisePropertyChanged("SelectedItem");
            }
        }
        private LanguageItem selectedItem;

        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                RaisePropertyChanged("SelectedIndex");
            }
        }
        private int selectedIndex;

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
                }
                
                RaisePropertyChanged("DownloadFolder");
            }
        }
        private string downloadFolder;

        public SettingsViewModel(Window window)
        {
            AvailableLanguages availableLanguages = new AvailableLanguages();
            SelectableLanguages = availableLanguages.GetAvailableLanguages();
            Reload();

            OpenSettingFolderCommand = new OpenFolderCommand(settingManager.SettingFolderPath);
            OpenDownloadFolderCommand = new OpenFolderCommand(DownloadFolder);
            OpenGameFolderCommand = new OpenFolderCommand(GameFolder);
            AutoDetectGameFolder = new InstallationFromRegistryCommand();
            ManuelSelectGameFolder = new InstallationFromManuelSelectionCommand();
            ResetSettingCommand = new ReloadObjectCommand(this);
            SelectFolder = new SelectFolderCommand();
            
            ResetAgreementCommand = new MultiCommand(new List<ICommand>()
            {
                new ChangeSettingCommand(settingManager, "AgreementAccepted", true),
                new SaveSettingsCommand(false, settingManager),
                new RestartApplicationCommand()
            });
            SaveSettingCommand = new MultiCommand(new List<ICommand>()
            {
                new SaveSettingsCommand(settingManager),
                new SwitchGuiLanguage(settingManager),
                new RefreshGuiLanguageCommand(window)
            });

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

        public override void Reload()
        {
            base.Reload();
            string languageCode = settingManager?.GetValue<string>("Language");
            for (int i = 0; i < SelectableLanguages.Count; i++)
            {
                if (SelectableLanguages[i].IsoCode == languageCode)
                {
                    SelectedIndex = i;
                    break;
                }
            }
            GameFolder = settingManager?.GetValue<string>("GameFolder");
            DownloadFolder = settingManager?.GetValue<string>("DownloadFolder");
        }
    }
}
