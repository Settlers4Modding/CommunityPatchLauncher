using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Commands.ApplicationWindow;
using CommunityPatchLauncher.Commands.Os;
using CommunityPatchLauncher.Enums;
using CommunityPatchLauncher.Settings.Factories;
using CommunityPatchLauncher.UserControls;
using CommunityPatchLauncherFramework.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;
using System;
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
        /// The command used to open the about page
        /// </summary>
        public ICommand OpenAboutCommand { get; private set; }

        /// <summary>
        /// The command used to open the disclamer
        /// </summary>
        public ICommand OpenDisclamerCommand { get; private set; }

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
        /// Open the s4 editor
        /// </summary>
        public ICommand OpenEditorCommand { get; private set; }

        /// <summary>
        /// Oopen the texture changer tool
        /// </summary>
        public ICommand OpenTextureChangerCommand { get; private set; }

        /// <summary>
        /// The content dock to use
        /// </summary>
        private readonly DockPanel contentDock;

        /// <summary>
        /// This will be set to true if the update was searched once
        /// </summary>
        private bool updateSearched;

        /// <summary>
        /// Create a new instance of this model
        /// </summary>
        /// <param name="window"></param>
        public MainWindowModel(Window window) : base(window)
        {
            IconVisible = false;
            updateSearched = false;
            CloseWindowCommand = new CloseApplicationCommand();

            object dockArea = window.FindName("DP_ContentDock");
            if (dockArea is DockPanel panel)
            {
                ISettingFactory settingFactory = new WpfPropertySettingManagerFactory();
                SettingManager wpfSettings = settingFactory.GetSettingsManager();

                contentDock = panel;

                LaunchGameCommand = new OpenControlToPanel(contentDock, new PatchVersionSelectionUserControl(window));
                OpenSettingCommand = new OpenControlToPanel(contentDock, new SettingsUserControl(currentWindow));
                OpenChangelogCommand = new OpenControlToPanel(contentDock, new BrowserUserControl("Changelog.md"));
                OpenDisclamerCommand = new OpenControlToPanel(contentDock, new BrowserUserControl("Agreement.md"));
                OpenAboutCommand = new OpenControlToPanel(contentDock, new BrowserUserControl("About.md"));
                ReportIssueCommand = new OpenLinkCommand(wpfSettings.GetValue<string>("ReportIssueLink"));
                ComingSoonCommand = new OpenControlToPanel(contentDock, new ComingSoonControl());

                OpenEditorCommand = new StartEditorCommand(settingManager);

                string gameFolder = settingManager.GetValue<string>("GameFolder");
                string textureChange = gameFolder + "Texturenwechsler.bat";
                OpenTextureChangerCommand = new StartProgramCommand(textureChange);
            }

            ChangeGroupVisiblity = new ToggleSubGroupVisibilityCommand(currentWindow);
            window.ContentRendered += (sender, data) =>
            {
                if (updateSearched)
                {
                    return;
                }
                updateSearched = true;
                CheckForUpdateIfNeeded(window);
            };

        }

        /// <summary>
        /// This method will check for updates if needed
        /// </summary>
        private void CheckForUpdateIfNeeded(Window parentWindow)
        {
            if (settingManager?.GetValue<bool>("UpdateOnStartup") == true)
            {
                UpdateBranchEnum updateBranch = UpdateBranchEnum.Release;
                string updateChannel = settingManager.GetValue<string>("UpdateChannel");
                if (!Enum.TryParse(updateChannel, out updateBranch))
                {
                    return;
                }
                ICommand updateApplication = new UpdateApplicationCommand(settingManager, parentWindow);
                updateApplication.Execute(new UpdateChannelContainer(updateBranch));
            }
        }
    }
}
