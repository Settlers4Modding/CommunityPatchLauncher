using CommunityPatchLauncher.BindingData;
using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Commands.ApplicationWindow;
using CommunityPatchLauncher.Commands.TaskCommands;
using CommunityPatchLauncher.UserControls;
using CommunityPatchLauncher.ViewModels.SpecialViews;
using FontAwesome.WPF;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    /// <summary>
    /// The view model for the launch game window
    /// </summary>
    class PatchVersionSelectionViewModel : BaseViewModel
    {
        /// <summary>
        /// Command to use to launch a game
        /// </summary>
        public ICommand OpenLaunchGameCommand { get; private set; }

        /// <summary>
        /// This command will download all the possible patches
        /// </summary>
        public ICommand DownloadAllPatches { get; private set; }

        /// <summary>
        /// All the patches you can select
        /// </summary>
        public IReadOnlyList<Patch> AllPatches { get; }

        private readonly DockPanel panelToUse;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public PatchVersionSelectionViewModel(Window parentWindow, UserControl control) : base(parentWindow)
        {
            Patches patches = new Patches();
            AllPatches = patches.GetPatches();

            IProgressCommand downloadPatchesCommand = new DownloadAllPatchesCommand(settingManager);
            //DownloadAllPatches = downloadPatchesCommand;
            ProgressBarDialog dialog = new ProgressBarDialog();
            if (dialog.DataContext is ProgressBarDialogViewModel progressViewModel)
            {
                downloadPatchesCommand.ProgressChanged += (sender, data) =>
                {
                    progressViewModel.UpdateProgress(data.CurrentWorkload, data.TotalWorkload);
                };
                downloadPatchesCommand.Executed += (sender, data) =>
                {
                    progressViewModel.UpdateProgress(100, 100);
                };
            }
            DownloadAllPatches = new MultiCommand(new List<ICommand>()
            {
                downloadPatchesCommand,
                 new OpenCustomPopupWindowCommand(currentWindow, FontAwesomeIcon.Hourglass, "Downloading all patches", dialog, false, null)
                 
            });

            object dockArea = control.FindName("DP_InnerDock");

            object controllerDock = control.FindName("IC_PatchSelectionControl");
            if (dockArea is DockPanel panel && controllerDock is ItemsControl controller)
            {
                panelToUse = panel;
                OpenLaunchGameCommand = new OpenLaunchUserControlToPanel(
                    panelToUse,
                    new LaunchGameUserControl(),
                    controller
                    );
            }
        }

        /// <inheritdoc/>
        public override void Reload()
        {
            base.Reload();
            foreach (UIElement panel in panelToUse.Children)
            {
                if (panel is UserControl control)
                {
                    if (control.DataContext is IViewModelReloadable reloadable)
                    {
                        reloadable.Reload();
                    }
                }
            }
        }
    }
}
