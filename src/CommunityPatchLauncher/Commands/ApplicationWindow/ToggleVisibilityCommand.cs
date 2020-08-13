using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CommunityPatchLauncher.Commands.ApplicationWindow
{
    /// <summary>
    /// This command will toggle the visiblity for a category on the main window
    /// </summary>
    public class ToggleVisibilityCommand : BaseDataCommand
    {
        /// <summary>
        /// The window to search the control on
        /// </summary>
        private readonly Window windowToSearchOn;

        /// <summary>
        /// Create a new instance of this command
        /// </summary>
        /// <param name="windowToSearchOn">The window to search in</param>
        public ToggleVisibilityCommand(Window windowToSearchOn)
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
            IEnumerable<StackPanel> panels = FindElementsOfType<StackPanel>(windowToSearchOn).Where(currentPanel =>
            {
                return currentPanel.Tag != null && currentPanel.Tag.ToString() == "SubGroup";
            });
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

        /// <summary>
        /// This method will find all the elements of a given type
        /// </summary>
        /// <typeparam name="T">The type of elemnts to find</typeparam>
        /// <param name="dependencyObject">The object to search elements in</param>
        /// <returns></returns>
        private IEnumerable<T> FindElementsOfType<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            if (dependencyObject == null)
            {
                yield return default;
            }
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
            {
                DependencyObject currentObject = VisualTreeHelper.GetChild(dependencyObject, i);
                if (currentObject != null && currentObject is T returnObject)
                {
                    yield return returnObject;
                }

                foreach (T childOfChild in FindElementsOfType<T>(currentObject))
                {
                    yield return childOfChild;
                }
            }
        }
    }
}
