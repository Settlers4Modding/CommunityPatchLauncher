using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CommunityPatchLauncher.Commands.ApplicationWindow
{
    /// <summary>
    /// This command will toggle the visiblity for a category on the main window
    /// </summary>
    public class ToggleSubGroupVisibilityCommand : BaseDataCommand
    {
        /// <summary>
        /// The window to search the control on
        /// </summary>
        private readonly Window windowToSearchOn;

        /// <summary>
        /// Create a new instance of this command
        /// </summary>
        /// <param name="windowToSearchOn">The window to search in</param>
        public ToggleSubGroupVisibilityCommand(Window windowToSearchOn)
        {
            this.windowToSearchOn = windowToSearchOn;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return parameter is string && windowToSearchOn != null;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }
            string searchString = parameter as string;
            object element = windowToSearchOn.FindName(searchString);
            IEnumerable<StackPanel> panels = FindElementsOfType<StackPanel>(windowToSearchOn, "SubGroup");
            foreach (StackPanel currentPanel in panels)
            {
                if (currentPanel.Name == searchString)
                {
                    continue;
                }
                currentPanel.Visibility = Visibility.Collapsed;
            }
            if (element is StackPanel stackPanel)
            {
                Visibility visibility = stackPanel.Visibility;
                if (visibility == Visibility.Collapsed || visibility == Visibility.Hidden)
                {
                    stackPanel.Visibility = Visibility.Visible;
                    return;
                }
                stackPanel.Visibility = Visibility.Collapsed;
            }
        }
    }
}
