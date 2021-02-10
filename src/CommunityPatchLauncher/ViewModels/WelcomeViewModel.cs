using CommunityPatchLauncher.BindingData;
using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Commands.ApplicationWindow;
using CommunityPatchLauncher.Commands.Condition;
using CommunityPatchLauncher.Commands.DataCommands;
using CommunityPatchLauncher.Commands.Os;
using CommunityPatchLauncher.Commands.Settings;
using CommunityPatchLauncher.Documentation.Factories;
using CommunityPatchLauncher.UserControls;
using CommunityPatchLauncher.Windows;
using CommunityPatchLauncherFramework.Documentation.Factory;
using CommunityPatchLauncherFramework.Documentation.Manager;
using CommunityPatchLauncherFramework.Documentation.Strategy;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            get => aggreement;
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
        public LanguageItem SelectedLanguage
        {
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
        /// The current manager to use for loading documents
        /// </summary>
        private readonly DocumentManager documentManager;

        /// <summary>
        /// Public accessor if we should check for update on startup
        /// </summary>
        public bool CheckForUpdateOnStartup
        {
            get => checkForUpdateOnStartup;
            set
            {
                checkForUpdateOnStartup = value;
                RaisePropertyChanged("CheckForUpdateOnStartup");
            }
        }

        /// <summary>
        /// Private accessor if we should check for update on startup
        /// </summary>
        private bool checkForUpdateOnStartup;

        /// <summary>
        /// Current checksum
        /// </summary>
        public string Checksum
        {
            get => checksum;
            set
            {
                checksum = value;
                RaisePropertyChanged("Checksum");
            }
        }

        private ICommand errorPopup;

        /// <summary>
        /// Private checksum accessor
        /// </summary>
        private string checksum;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="window">The window this view belongs to</param>
        public WelcomeViewModel(Window window) : base(window, true)
        {
            RefreshGuiCommand = new MultiCommand(new List<ICommand>()
            {
                new SaveSettingsCommand(settingManager),
                new RefreshGuiLanguageCommand(currentWindow)
            });

            DependencyObject agreementDisplay = (DependencyObject)window.FindName("WB_Agreement");
            if (agreementDisplay is BrowserUserControl browserControl)
            {
                if (browserControl.DataContext is BrowserModelView modelView)
                {
                    modelView.ChangeDocument("Agreement.md");
                }
            }

            errorPopup = new OpenCustomPopupWindowCommand(
                             window,
                             FontAwesome.WPF.FontAwesomeIcon.Exclamation,
                             Properties.Resources.Default_FolderSelectionError,
                             new InfoPopup()
                             );

            firstStart = true;
            CloseWindowCommand = new CloseApplicationCommand();
            FolderSearch = new SelectFolderCommand(new SettlerFolderCondition());
            FolderSearch.Error += (sender, data) =>
            {
                errorPopup?.Execute(data.Message);
            };
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
            bool updateOnStartup = settingManager.GetValue<bool>("UpdateOnStartup");
            GameFolder = settingGameFolder ?? string.Empty;
            CheckForUpdateOnStartup = updateOnStartup;
            Window mainWindow = new MainWindow();
            AcceptAgreement = new AcceptAgreementCommand(settingManager, currentWindow, mainWindow);
            bool accepted = settingManager.GetValue<bool>("AgreementAccepted");

            IDocumentManagerFactory factory = new LocalDocumentManagerFactory();
            documentManager = factory.GetDocumentManager(Properties.Settings.Default.FallbackLanguage, new MarkdownHtmlConvertStrategy());
            string checkSumAgreement = documentManager.ReadConvertedDocument(Properties.Settings.Default.FallbackLanguage, "Agreement.md");
            Checksum = CreateChecksum(checkSumAgreement);
            PropertyChanged += (sender, data) =>
            {
                if (data.PropertyName == "SelectedLanguage")
                {
                    string agreementText = documentManager.ReadConvertedDocument(selectedLanguage.IsoCode, "Agreement.md");
                    string loadedChecksum = settingManager.GetValue<string>("AgreementChecksum");
                    AgreementText = agreementText;
                    if (accepted && FolderSet && Checksum == loadedChecksum)
                    {
                        currentWindow.Close();
                        mainWindow.Show();
                    }
                    if (!firstStart)
                    {
                        SwitchGuiLanguage(selectedLanguage.IsoCode);
                    }
                    firstStart = false;
                }
            };
        }

        /// <summary>
        /// Create a checksum from a given string
        /// </summary>
        /// <param name="inputData">The input string</param>
        /// <returns>The hashed output</returns>
        private string CreateChecksum(string inputData)
        {
            string returnString = string.Empty;
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                returnString = sb.ToString();
            }

            return returnString;
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
