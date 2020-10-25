using CommunityPatchLauncher.Commands.ApplicationWindow;
using CommunityPatchLauncher.Settings.Factories;
using CommunityPatchLauncher.UserControls;
using CommunityPatchLauncherFramework.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    /// <summary>
    /// Main window model view
    /// </summary>
    internal class MainWindowModel : BaseViewModel
    {
        /// <summary>
        /// The command used to open the changelog
        /// </summary>
        public ICommand OpenChangelogCommand { get; private set; }

        /// <summary>
        /// The command used if you click on launch game
        /// </summary>
        public ICommand LaunchGameCommand { get; private set; }

        /// <summary>
        /// This command will open the settings page
        /// </summary>
        public ICommand OpenSettingCommand { get; private set; }

        /// <summary>
        /// The command used if something is coming soon
        /// </summary>
        public ICommand ComingSoonCommand { get; private set; }

        /// <summary>
        /// The command if a group visiblity changed
        /// </summary>
        public ICommand ChangeGroupVisiblity { get; private set; }

        /// <summary>
        /// The command to report a issue
        /// </summary>
        public ICommand ReportIssueCommand { get; private set; }

        /// <summary>
        /// The content dock to use
        /// </summary>
        private readonly DockPanel contentDock;

        /// <summary>
        /// Create a new instance of this model
        /// </summary>
        /// <param name="window"></param>
        public MainWindowModel(Window window) : base(window)
        {
            IconVisible = false;
            CloseWindowCommand = new CloseApplicationCommand();

            object dockArea = window.FindName("DP_ContentDock");
            if (dockArea is DockPanel panel)
            {
                ISettingFactory settingFactory = new WpfPropertySettingManagerFactory();
                SettingManager wpfSettings = settingFactory.GetSettingsManager();

                contentDock = panel;

                LaunchGameCommand = new OpenControlToPanel(contentDock, new PatchVersionSelectionUserControl());
                OpenSettingCommand = new OpenControlToPanel(contentDock, new SettingsUserControl(currentWindow));
                OpenChangelogCommand = new OpenControlToPanel(contentDock, new BrowserUserControl("Changelog.md"));
                ReportIssueCommand = new OpenLinkCommand(wpfSettings.GetValue<string>("ReportIssueLink"));
                ComingSoonCommand = new OpenControlToPanel(contentDock, new ComingSoonControl());
            }

            ChangeGroupVisiblity = new ToggleSubGroupVisibilityCommand(currentWindow);
        }
    }
}
