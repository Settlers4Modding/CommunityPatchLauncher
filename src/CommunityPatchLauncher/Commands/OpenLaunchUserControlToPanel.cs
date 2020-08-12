using CommunityPatchLauncher.Enums;
using CommunityPatchLauncher.UserControls;
using System.Windows.Controls;

namespace CommunityPatchLauncher.Commands
{
    /// <summary>
    /// This class will open a launch user control in a given panel
    /// </summary>
    internal class OpenLaunchUserControlToPanel : OpenControlToPanel
    {
        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="panelToUse">The panel to add the control to</param>
        /// <param name="userControl">The user control to add</param>
        public OpenLaunchUserControlToPanel(DockPanel panelToUse, UserControl userControl) : base(panelToUse, userControl)
        {
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return userControl is LaunchGameUserControl;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            base.Execute(parameter);
            if (userControl is LaunchGameUserControl launchGameUserControl && parameter is AvailablePatches patch)
            {
                launchGameUserControl.SetPatch(patch);
            }
        }
    }
}
