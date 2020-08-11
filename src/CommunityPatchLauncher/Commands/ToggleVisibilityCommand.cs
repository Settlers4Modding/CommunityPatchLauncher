using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CommunityPatchLauncher.Commands
{
    public class ToggleVisibilityCommand : BaseDataCommand
    {
        private readonly Window windowToSearchOn;

        public ToggleVisibilityCommand(Window windowToSearchOn)
        {
            this.windowToSearchOn = windowToSearchOn;
        }

        public override bool CanExecute(object parameter)
        {
            return parameter is string && windowToSearchOn != null;
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }
            string searchString = parameter as string;
            object element = windowToSearchOn.FindName(searchString);
            IEnumerable<StackPanel> panels = FindElementsOfType<StackPanel>(windowToSearchOn).Where(currentPanel => {
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
