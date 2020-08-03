using CommunityPatchLauncher.Commands;
using CommunityPatchLauncherFramework.Settings.Manager;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CommunityPatchLauncher.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The setting manager to use
        /// </summary>
        private SettingManager settingManager;

        /// <summary>
        /// Create a new main window
        /// </summary>
        public MainWindow()
        {
            IDataCommand settingManagerCommand = new GetSettingManagerCommand();
            settingManagerCommand.Executed += SettingManagerCommand_Executed;
            settingManagerCommand.Execute(null);
            InitializeComponent();

            SetCurrentLanguage();
            CB_LanguageSelector.SelectionChanged += CB_LanguageSelector_SelectionChanged;
        }

        /// <summary>
        /// Set the current window language
        /// </summary>
        private void SetCurrentLanguage()
        {
            IDataCommand languageCommand = new GetCurrentLanguageCommand();
            languageCommand.Executed += (command, data) =>
            {
                string language = data.GetData<string>();

                for (int i = 0; i < CB_LanguageSelector.Items.Count; i++)
                {
                    ComboBoxItem boxItem = CB_LanguageSelector.Items[i] as ComboBoxItem;
                    if (boxItem.Tag.ToString() == language)
                    {
                        CB_LanguageSelector.SelectedIndex = i;
                        break;
                    }
                }
            };
            languageCommand.Execute(settingManager);
        }

        /// <summary>
        /// Language selection did change
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Data of the event</param>
        private void CB_LanguageSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox box)
            {
                if (box.SelectedItem is ComboBoxItem item)
                {
                    settingManager?.AddValue("Language", item.Tag.ToString());
                    settingManager.SaveSettings();
                    SwitchLanguage(true);
                }
            }
        }

        /// <summary>
        /// Was the setting command executed
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">The event arugments</param>
        private void SettingManagerCommand_Executed(object sender, EventArguments.DataCommandEventArg e)
        {
            settingManager = e.GetData<SettingManager>();
            if (settingManager == null)
            {
                return;
            }
            SwitchLanguage();
        }

        /// <summary>
        /// Switch the language of this window
        /// </summary>
        private void SwitchLanguage()
        {
            SwitchLanguage(false);
        }

        /// <summary>
        /// Switch the language of this window
        /// </summary>
        /// <param name="refresh">Should the gui get a refresh</param>
        private void SwitchLanguage(bool refresh)
        {
            IDataCommand languageCommand = new GetCurrentLanguageCommand();
            languageCommand.Executed += (command, data) =>
            {
                string language = data.GetData<string>();
                if (language == null)
                {
                    return;
                }

                ICommand changeLanguage = new SwitchGuiLanguage();
                changeLanguage.Execute(language);
                if (refresh)
                {
                    ICommand refreshUiCommand = new RefreshGuiLanguageCommand();
                    refreshUiCommand.Execute(this);
                }
            };
            languageCommand.Execute(settingManager);
        }
    }
}
