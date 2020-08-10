using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.ViewModels;
using CommunityPatchLauncherFramework.Settings.Manager;
using System;
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
            SetDefaultWindowStyle();
            this.DataContext = new MainWindowModel(this);

            this.MouseDown += MainWindow_MouseDown;
        }

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        protected void SetDefaultWindowStyle()
        {
            try
            {
                this.Style = this.Resources["WindowStyle"] as System.Windows.Style;
            }
            catch (Exception)
            {
                return;
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
