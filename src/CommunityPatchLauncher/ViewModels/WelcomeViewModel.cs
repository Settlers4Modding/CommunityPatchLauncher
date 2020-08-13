using CommunityPatchLauncher.BindingData;
using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Factories;
using CommunityPatchLauncher.Windows;
using CommunityPatchLauncherFramework.Documentation.Factory;
using CommunityPatchLauncherFramework.Documentation.Manager;
using CommunityPatchLauncherFramework.Documentation.Strategy;
using CommunityPatchLauncherFramework.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    /// <summary>
    /// This view model is used on the welcome screen
    /// </summary>
    public class WelcomeViewModel : BaseViewModel
    {
        /// <summary>
        /// Command to use to search the game path in the reged
        /// </summary>
        public IDataCommand RegexSearch { get; private set; }

        /// <summary>
        /// Command to use to search the game path on your own
        /// </summary>
        public IDataCommand FolderSearch { get; private set; }

        /// <summary>
        /// Command to use if you close this window
        /// </summary>
        public ICommand CloseWindow { get; set; }

        /// <summary>
        /// Command to use if you accept the agreement
        /// </summary>
        public ICommand AcceptAgreement { get; set; }
        
        /// <summary>
        /// The game folder to use
        /// </summary>
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
                if (gameFolder.Length > 1)
                {
                    char lastChar = gameFolder.Last();
                    if (lastChar != '/' && lastChar != '\\')
                    {
                        gameFolder += "/";
                    }
                }
                
                FolderSet = Directory.Exists(gameFolder) && File.Exists(gameFolder + "S4_Main.exe");
                RaisePropertyChanged("GameFolder");
            }
        }
        /// <summary>
        /// Private game folder to use
        /// </summary>
        private string gameFolder;

        /// <summary>
        /// Was the agreement accepted
        /// </summary>
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
        /// <summary>
        /// Private variable if the agreement was accepted
        /// </summary>
        private bool aggreement;

        /// <summary>
        /// Is the selected folder correct
        /// </summary>
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
        /// <summary>
        /// Private variable if the selected folder is correct
        /// </summary>
        private bool folderSet;

        /// <summary>
        /// All the available languages
        /// </summary>
        public IReadOnlyList<LanguageItem> Languages { get; private set; }

        /// <summary>
        /// The currently selected language
        /// </summary>
        public LanguageItem SelectedLanguage {
            get => selectedLanguage;
            set
            {
                selectedLanguage = value;
                RaisePropertyChanged("SelectedLanguage");
            }
        }
        /// <summary>
        /// Private access to the current sleected language
        /// </summary>
        private LanguageItem selectedLanguage;

        /// <summary>
        /// Index of the currently selected language
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
        /// Private variable of the current selected language
        /// </summary>
        private int selectedIndex;

        /// <summary>
        /// Is this the first start of this window
        /// </summary>
        private bool firstStart;

        /// <summary>
        /// The text to display in the agreement web browser
        /// </summary>
        public string AgreementText 
        {
            get => agreementText;
            set 
            {
                agreementText = value;
                RaisePropertyChanged("AgreementText");
            } 
        }
        /// <summary>
        /// Private variable for the text to show in the agreement web browser
        /// </summary>
        private string agreementText;

        /// <summary>
        /// The setting manager to use
        /// </summary>
        private SettingManager settingManager;

        /// <summary>
        /// The current manager to use for loading documents
        /// </summary>
        private readonly DocumentManager documentManager;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="window">The window this view belongs to</param>
        public WelcomeViewModel(Window window) : base(window)
        {
            firstStart = true;
            CloseWindowCommand = new CloseApplicationCommand();
            FolderSearch = new InstallationFromManuelSelectionCommand();
            RegexSearch = new InstallationFromRegistryCommand();

            CloseWindow = new CloseApplicationCommand();
            

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

            if (settingManager == null)
            {
                return;
            }

            string settingGameFolder = settingManager.GetValue<string>("GameFolder");
            GameFolder = settingGameFolder ?? string.Empty;
            AcceptAgreement = new AcceptAgreementCommand(settingManager, currentWindow, new MainWindow());
            bool accepted = settingManager.GetValue<bool>("AgreementAccepted");

            if (accepted && FolderSet)
            {
                currentWindow.Close();
                Window mainWindow = new MainWindow();
                mainWindow.Show();
            }

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

        /// <summary>
        /// The game folder did change
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void GameFolderChanged_Executed(object sender, EventArguments.DataCommandEventArg e)
        {
            FolderSet = false;
            string gameFolder = e.GetData<string>();
            if (gameFolder != null && gameFolder != "")
            {
                GameFolder = gameFolder;
                FolderSet = true;
            }
        }
    }
}
