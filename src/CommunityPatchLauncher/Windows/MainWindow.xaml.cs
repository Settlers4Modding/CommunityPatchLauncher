using CommunityPatchLauncher.ViewModels;
using CommunityPatchLauncherFramework.Settings.Manager;
using System.Windows;

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
    }
}
