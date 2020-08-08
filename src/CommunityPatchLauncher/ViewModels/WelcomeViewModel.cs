using CommunityPatchLauncher.BindingData;
using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Factories;
using CommunityPatchLauncherFramework.Documentation.Factory;
using CommunityPatchLauncherFramework.Documentation.Manager;
using CommunityPatchLauncherFramework.Documentation.Strategy;
using CommunityPatchLauncherFramework.Settings.Manager;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents.Serialization;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    public class WelcomeViewModel : BaseViewModel
    {
        public IDataCommand RegexSearch { get; private set; }
        public IDataCommand FolderSearch { get; private set; }
        public ICommand AcceptAgreement { get; set; }

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

        public LanguageItem SelectedLanguage {
            get => selectedLanguage;
            set
            {
                selectedLanguage = value;
                RaisePropertyChanged("SelectedLanguage");
            }
        }
        private LanguageItem selectedLanguage;
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
        private bool firstStart;

        public string AgreementText 
        {
            get => agreementText;
            set 
            {
                agreementText = value;
                RaisePropertyChanged("AgreementText");
            } 
        }
        private string agreementText;

        /// <summary>
        /// The setting manager to use
        /// </summary>
        private SettingManager settingManager;
        private readonly DocumentManager documentManager;

        public WelcomeViewModel(Window window) : base(window)
        {
            firstStart = true;
            FolderSearch = new InstallationFromManuelSelectionCommand();
            RegexSearch = new InstallationFromRegistryCommand();
            AcceptAgreement = new AcceptAgreementCommand();

            FolderSearch.Executed += GameFolderChanged_Executed;
            RegexSearch.Executed += GameFolderChanged_Executed;

            Languages = new AvailableLanguages().GetAvailableLanguages();
            string languageIsoCode = CultureInfo.CurrentUICulture.IetfLanguageTag;
            for (int i = 0; i < Languages.Count; i++)
            {
                if (Languages[i].IsoCode == languageIsoCode)
                {
                    selectedIndex = i;
                    break;
                }
            }


            //SelectedLanguage = Languages[0];

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
            IDocumentManagerFactory factory = new LocalDocumentManagerFactory();
            documentManager = factory.GetDocumentManager("en-EN", new MarkdownHtmlConvertStrategy());
            PropertyChanged += (sender, data) =>
            {
                if (data.PropertyName == "SelectedLanguage")
                {
                    AgreementText = documentManager.ReadConvertedDocument(selectedLanguage.IsoCode, "Agreement.md");
                    ICommand switchLanguage = new SwitchGuiLanguage();
                    ICommand refreshGui = new RefreshGuiLanguageCommand();
                    switchLanguage.Execute(selectedLanguage.IsoCode);
                    if (!firstStart)
                    {
                        refreshGui.Execute(currentWindow);
                    }
                    firstStart = false;
                }
            };
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
