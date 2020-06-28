using CommunityPatchLauncher.Commands;
using CommunityPatchLauncherFramework.Settings.Container;
using CommunityPatchLauncherFramework.Settings.Manager;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CommunityPatchLauncher.Windows
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        private SettingManager settingManager;

        /// <summary>
        /// welcome window for the application
        /// </summary>
        public WelcomeWindow()
        {
            IDataCommand settingManagerCommand = new GetSettingManagerCommand();
            settingManagerCommand.Executed += (sender, data) =>
            {
                settingManager = data.GetData<SettingManager>();
                if (settingManager == null)
                {
                    return;
                }
            };
            settingManagerCommand.Execute(null);
            ShowIfNeeded();
            InitializeComponent();

            for (int i = 0; i < CB_LanguageSelector.Items.Count; i++)
            {
                ComboBoxItem item = CB_LanguageSelector.Items[i] as ComboBoxItem;
                if (item.Tag.ToString() == CultureInfo.CurrentUICulture.Name.ToString())
                {
                    CB_LanguageSelector.SelectedIndex = i;
                    break;
                }
            }

            CB_LanguageSelector.SelectionChanged += (sender, data) =>
            {
                if (sender is ComboBox box)
                {
                    if (box.SelectedItem is ComboBoxItem item)
                    {
                        ICommand switchLanguage = new SwitchGuiLanguage();
                        switchLanguage.Execute(item.Tag.ToString());

                        ICommand refreshUiCommand = new RefreshGuiLanguageCommand();
                        refreshUiCommand.Execute(this);
                    }
                }
            };
            B_Ok.Click += B_Ok_Click;
        }

        /// <summary>
        /// If the okay button is clicked
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void B_Ok_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedLanguage = CB_LanguageSelector.SelectedItem as ComboBoxItem;
            settingManager.AddValue("language", selectedLanguage.Tag.ToString());
            settingManager.AddValue("welcomeShown", CB_Agree.IsChecked);
            settingManager.SaveSettings();
            ShowIfNeeded();
        }

        /// <summary>
        /// Hide this form if it was already shown and agreed
        /// </summary>
        private void ShowIfNeeded()
        {
            if (settingManager == null)
            {
                return;
            }
            SettingPair showWelcome = settingManager.GetValue("welcomeShown");
            bool welcomeShown = showWelcome != null && showWelcome.GetValue<bool>();
            if (welcomeShown)
            {
                Window mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }
    }
}
