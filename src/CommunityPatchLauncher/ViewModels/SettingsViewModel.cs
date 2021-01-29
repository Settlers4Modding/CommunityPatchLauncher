using CommunityPatchLauncher.BindingData;
using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Commands.ApplicationWindow;
using CommunityPatchLauncher.Commands.DataCommands;
using CommunityPatchLauncher.Commands.Os;
using CommunityPatchLauncher.Commands.Settings;
using CommunityPatchLauncher.Enums;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    /// <summary>
    /// The view model for the settings view
    /// </summary>
    internal class SettingsViewModel : BaseViewModel
    {
        /// <summary>
        /// Command to open the setting folder
        /// </summary>
        public ICommand OpenSettingFolderCommand { get; private set; }

        /// <summary>
        /// Command to open the download folder
        /// </summary>
        public ICommand OpenDownloadFolderCommand { get; private set; }

        /// <summary>
        /// Command to open the game folder
        /// </summary>
        public ICommand OpenGameFolderCommand { get; private set; }

        /// <summary>
        /// Command to reset the agreement
        /// </summary>
        public ICommand ResetAgreementCommand { get; private set; }

        /// <summary>
        /// Command to save the settings
        /// </summary>
        public ICommand SaveSettingCommand { get; private set; }

        /// <summary>
        /// Command to reset the settings
        /// </summary>
        public ICommand ResetSettingCommand { get; private set; }

        /// <summary>
        /// This command will update the application
        /// </summary>
        public ICommand UpdateApplicationCommand { get; private set; }

        /// <summary>
        /// Command to auto detect the game folder
        /// </summary>
        public IDataCommand AutoDetectGameFolder { get; private set; }

        /// <summary>
        /// Command to select the game folder by hand
        /// </summary>
        public IDataCommand ManuelSelectGameFolder { get; private set; }

        /// <summary>
        /// Command to select the download folder
        /// </summary>
        public IDataCommand SelectFolder { get; private set; }

        /// <summary>
        /// All the selectable languages
        /// </summary>
        public IReadOnlyList<LanguageItem> SelectableLanguages { get; private set; }

        /// <summary>
        /// Currently selected language
        /// </summary>
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
        /// <summary>
        /// The selected language item
        /// </summary>
        private LanguageItem selectedItem;

        /// <summary>
        /// The selected index for the language dropdown
        /// </summary>
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                RaisePropertyChanged("SelectedIndex");
            }
        }
        /// <summary>
        /// Currently selected language index
        /// </summary>
        private int selectedIndex;

        /// <summary>
        /// Public accessor if we should check for update on startup
        /// </summary>
        public bool CheckForUpdateOnStartup
        {
            get => checkForUpdateOnStartup;
            set
            {
                checkForUpdateOnStartup = value;
                settingManager.AddValue("UpdateOnStartup", checkForUpdateOnStartup);
                RaisePropertyChanged("CheckForUpdateOnStartup");
            }
        }

        /// <summary>
        /// Private accessor if we should check for update on startup
        /// </summary>
        private bool checkForUpdateOnStartup;

        /// <summary>
        /// All the available update channels
        /// </summary>
        public IReadOnlyList<UpdateChannelContainer> UpdateChannels { get; private set; }

        /// <summary>
        /// Currently selected update channel
        /// </summary>
        public UpdateChannelContainer SelectedUpdateChannel
        {
            get => selectedUpdateChannel;
            set
            {
                selectedUpdateChannel = value;
                settingManager.AddValue("UpdateChannel", SelectedUpdateChannel.UpdateBranch.ToString());
                if (selectedUpdateChannel.UpdateBranch == UpdateBranchEnum.Release)
                {
                    settingManager.ClearValue("LauncherVersion");
                }
                RaisePropertyChanged("SelectedUpdateChannel");
            }
        }
        /// <summary>
        /// Currently selected private accessor for update channel
        /// </summary>
        private UpdateChannelContainer selectedUpdateChannel;

        /// <summary>
        /// The public accessor for the selected update index
        /// </summary>
        public int SelectedUpdateIndex
        {
            get => selecteUpdateIndex;
            set
            {
                selecteUpdateIndex = value;
                RaisePropertyChanged("SelectedUpdateIndex");
            }
        }
        /// <summary>
        /// The private accessor for the current update index
        /// </summary>
        private int selecteUpdateIndex;

        /// <summary>
        /// The path to the game folder
        /// </summary>
        public string GameFolder
        {
            get => gameFolder;
            set
            {
                gameFolder = value;
                if (gameFolder != null)
                {
                    gameFolder = gameFolder.Replace("\\", "/");
                    settingManager.AddValue("GameFolder", gameFolder);
                }
                RaisePropertyChanged("GameFolder");
            }
        }
        /// <summary>
        /// Private path to the game folder
        /// </summary>
        private string gameFolder;

        /// <summary>
        /// The path to the download folder
        /// </summary>
        public string DownloadFolder
        {
            get => downloadFolder;
            set
            {
                downloadFolder = value;
                if (downloadFolder != null)
                {
                    downloadFolder = downloadFolder.Replace("\\", "/");
                    settingManager.AddValue("DownloadFolder", downloadFolder);
                }

                RaisePropertyChanged("DownloadFolder");
            }
        }
        /// <summary>
        /// private download folder
        /// </summary>
        private string downloadFolder;

        /// <summary>
        /// Create new instance of this class
        /// </summary>
        /// <param name="window">The parent window</param>
        public SettingsViewModel(Window window)
        {
            AvailableLanguages availableLanguages = new AvailableLanguages();
            SelectableLanguages = availableLanguages.GetAvailableLanguages();
            Reload();

            UpdateChannelModel updateChannels = new UpdateChannelModel();
            UpdateChannels = updateChannels.GetUpdateChannels();

            OpenSettingFolderCommand = new OpenFolderCommand(settingManager.SettingFolderPath);
            OpenDownloadFolderCommand = new OpenFolderCommand(DownloadFolder);
            OpenGameFolderCommand = new OpenFolderCommand(GameFolder);
            AutoDetectGameFolder = new InstallationFromRegistryCommand();
            ManuelSelectGameFolder = new InstallationFromManuelSelectionCommand();
            ResetSettingCommand = new ReloadObjectCommand(this);
            UpdateApplicationCommand = new UpdateApplicationCommand(settingManager, window, true);
            SelectFolder = new SelectFolderCommand();

            ResetAgreementCommand = new MultiCommand(new List<ICommand>()
            {
                new ChangeSettingCommand(settingManager, "AgreementAccepted", true),
                new SaveSettingsCommand(false, settingManager),
                new RestartApplicationCommand()
            });
            SaveSettingCommand = new MultiCommand(new List<ICommand>()
            {
                new MoveFolderCommand(settingManager.GetValue<string>("DownloadFolder"), false),
                new SaveSettingsCommand(settingManager),
                new SwitchGuiLanguage(settingManager),
                new RefreshGuiLanguageCommand(window)
            });

            SelectFolder.Executed += DownloadFolderSelected_Executed;
            AutoDetectGameFolder.Executed += DetectGameFolder_Executed;
            ManuelSelectGameFolder.Executed += DetectGameFolder_Executed;
        }

        /// <summary>
        /// New download folder selected
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguemnts</param>
        private void DownloadFolderSelected_Executed(object sender, EventArguments.DataCommandEventArg e)
        {
            DownloadFolder = e.GetData<string>();
        }

        /// <summary>
        /// Game folder detected
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void DetectGameFolder_Executed(object sender, EventArguments.DataCommandEventArg e)
        {
            GameFolder = e.GetData<string>();
        }

        /// <inheritdoc>/>
        protected override void AddWindowResizeableCommand()
        {

        }

        /// <inheritdoc>/>
        public override void Reload()
        {
            base.Reload();
            string languageCode = string.Empty;
            if (settingManager != null)
            {
                languageCode = settingManager.GetValue<string>("Language");
                GameFolder = settingManager.GetValue<string>("GameFolder");
                DownloadFolder = settingManager.GetValue<string>("DownloadFolder");
                CheckForUpdateOnStartup = settingManager.GetValue<bool>("UpdateOnStartup");
            }
            for (int i = 0; i < SelectableLanguages.Count; i++)
            {
                if (SelectableLanguages[i].IsoCode == languageCode)
                {
                    SelectedIndex = i;
                    break;
                }
            }

            UpdateBranchEnum updateBranchEnum = UpdateBranchEnum.Release;
            string channelName = settingManager?.GetValue<string>("UpdateChannel");
            channelName = channelName ?? updateBranchEnum.ToString();

            Enum.TryParse(channelName, out updateBranchEnum);
            if (UpdateChannels == null)
            {
                return;
            }

            if (updateBranchEnum == UpdateBranchEnum.Release)
            {
                settingManager.ClearValue("LauncherVersion");
                settingManager.SaveSettings();
            }

            for (int i = 0; i < UpdateChannels.Count; i++)
            {
                if (UpdateChannels[i].UpdateBranch == updateBranchEnum)
                {
                    SelectedUpdateIndex = i;
                    break;
                }
            }
        }
    }
}
