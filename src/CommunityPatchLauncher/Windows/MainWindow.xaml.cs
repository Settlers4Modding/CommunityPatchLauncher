using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Container;
using CommunityPatchLauncherFramework.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CommunityPatchLauncher.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

            CB_LanguageSelector.SelectionChanged += CB_LanguageSelector_SelectionChanged;
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
            IDataCommand languageCommand = new GetCurrentLanguageCommand();
            languageCommand.Executed += (command, data) =>
            {
                string language = data.GetData<string>();
                if (language == string.Empty)
                {
                    return;
                }

                ICommand changeLanguage = new SwitchGuiLanguage();
                changeLanguage.Execute(language);
            };
            languageCommand.Execute(settingManager);
        }

        private void SwitchLanguage()
        {
            SwitchLanguage(false);
        }

        private void SwitchLanguage(bool restart)
        {

        }
    }
}
