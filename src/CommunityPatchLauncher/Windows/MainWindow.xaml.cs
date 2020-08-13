using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Commands.ApplicationWindow;
using CommunityPatchLauncher.Commands.Settings;
using CommunityPatchLauncher.ViewModels;
using CommunityPatchLauncherFramework.Settings.Manager;
using System.Windows;
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
            InitializeComponent();
            this.DataContext = new MainWindowModel(this);
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
        }
    }
}
