using CommunityPatchLauncher.BindingData;
using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Commands;
using CommunityPatchLauncherFramework.Settings.Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    public class WelcomeViewModel : BaseViewModel
    {
        public IDataCommand RegexSearch { get; private set; }
        public IDataCommand FolderSearch { get; private set; }
        public IDataCommand AcceptAgreement { get; set; }

        public string GameFolder
        {
            get
            {
                return gameFolder;
            }
            set
            {
                gameFolder = value;
                gameFolder = gameFolder.Replace("\\", "/");
                char lastChar = gameFolder.Last();
                if (lastChar != '/' && lastChar != '\\')
                {
                    gameFolder += "/";
                }
                FolderSet = Directory.Exists(gameFolder) && File.Exists(gameFolder + "S4_Main.exe");
                RaisePropertyChanged("GameFolder");
            }
        }
        private string gameFolder;
        public bool Agreement
        {
            get
            {
                return aggreement;
            }
            set
            {
                aggreement = value;
                RaisePropertyChanged("Agreement");
            }
        }
        private bool aggreement;
        public bool FolderSet
        {
            get
            {
                return folderSet;
            }
            set
            {
                folderSet = value;
                RaisePropertyChanged("FolderSet");
            }
        }
        private bool folderSet;


        public IReadOnlyList<LanguageItem> Languages { get; private set; }
        public LanguageItem SelectedLanguage { get; set; }

        /// <summary>
        /// The setting manager to use
        /// </summary>
        private SettingManager settingManager;

        public WelcomeViewModel(Window window) : base(window)
        {
            FolderSearch = new InstallationFromManuelSelectionCommand();
            RegexSearch = new InstallationFromRegistryCommand();
            FolderSearch.Executed += GameFolderChanged_Executed;
            RegexSearch.Executed += GameFolderChanged_Executed;

            Languages = new AvailableLanguages().GetAvailableLanguages();
            SelectedLanguage = Languages[0];

            IDataCommand settingManagerCommand = new GetSettingManagerCommand();
            settingManagerCommand.Executed += (sender, data) =>
            {
                settingManager = data.GetData<SettingManager>();
                if (settingManager == null)
                {
                    return;
                }
                //ShowIfNeeded();
            };
            settingManagerCommand.Execute(null);
        }

        private void GameFolderChanged_Executed(object sender, EventArguments.DataCommandEventArg e)
        {
            FolderSet = false;
            string gameFolder = e.GetData<string>();
            if (gameFolder != null || gameFolder != "")
            {
                GameFolder = gameFolder;
                FolderSet = true;
            }
        }
    }
}
