using CommunityPatchLauncher.BindingData;
using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Enums;
using CommunityPatchLauncher.UserControls;
using CommunityPatchLauncherFramework.Settings.Manager;
using System;
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
        /// All the patches you can select
        /// </summary>
        public IReadOnlyList<Patch> AllPatches { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public PatchVersionSelectionViewModel(UserControl control)
        {
            Patches patches = new Patches();
            AllPatches = patches.GetPatches();

            object dockArea = control.FindName("DP_InnerDock");
            if (dockArea is DockPanel panel)
            {
                OpenLaunchGameCommand = new OpenLaunchUserControlToPanel(panel, new LaunchGameUserControl());
            }
        }
    }
}
