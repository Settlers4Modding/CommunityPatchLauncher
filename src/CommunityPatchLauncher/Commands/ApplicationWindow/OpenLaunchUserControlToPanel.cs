using CommunityPatchLauncher.Commands.DataContainer;
using CommunityPatchLauncher.UserControls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace CommunityPatchLauncher.Commands.ApplicationWindow
{
    /// <summary>
    /// This class will open a launch user control in a given panel
    /// </summary>
    internal class OpenLaunchUserControlToPanel : OpenControlToPanel
    {
        /// <summary>
        /// The container to search the buttons in
        /// </summary>
        private readonly DependencyObject container;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="panelToUse">The panel to add the control to</param>
        /// <param name="userControl">The user control to add</param>
        public OpenLaunchUserControlToPanel(
            DockPanel panelToUse,
            UserControl userControl,
            DependencyObject container
            ) : base(panelToUse, userControl)
        {
            this.container = container;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return userControl is LaunchGameUserControl && parameter is PatchSelectionData;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            base.Execute(parameter);
            if (userControl is LaunchGameUserControl launchGameUserControl && parameter is PatchSelectionData data)
            {
                IEnumerable<ToggleButton> buttons = FindElementsOfType<ToggleButton>(container, "ControlButton");
                foreach (ToggleButton button in buttons)
                {
                    bool valueToSet = button == data.Button;

                    button.IsChecked = valueToSet;
                }
                launchGameUserControl.SetPatch(data.Patch);
            }
        }
    }
}
