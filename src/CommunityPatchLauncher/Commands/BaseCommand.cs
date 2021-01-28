using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CommunityPatchLauncher.Commands
{
    public abstract class BaseCommand : ICommand
    {
        /// <summary>
        /// Did the can execute value change
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Raise a event that the can execute did change
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// This method will find all the elements of a given type
        /// </summary>
        /// <typeparam name="T">The type of elemnts to find</typeparam>
        /// <param name="dependencyObject">The object to search elements in</param>
        /// <returns></returns>
        protected virtual IEnumerable<T> FindElementsOfType<T>(DependencyObject dependencyObject) where T : DependencyObject
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

        /// <summary>
        /// Find all elements of given type with the given tag
        /// </summary>
        /// <typeparam name="T">The type of element to find</typeparam>
        /// <param name="dependencyObject">The dependency object to search in</param>
        /// <param name="tag">The tag to search for</param>
        /// <returns></returns>
        protected virtual IEnumerable<T> FindElementsOfType<T>(DependencyObject dependencyObject, string tag) where T : DependencyObject
        {
            return FindElementsOfType<T>(dependencyObject).Where(currentObject =>
            {
                if (currentObject is FrameworkElement element)
                {
                    return element.Tag != null && element.Tag.ToString() == tag;
                }
                return false;
            });

        }

        /// <inheritdoc/>
        public abstract bool CanExecute(object parameter);

        /// <inheritdoc/>
        public abstract void Execute(object parameter);
    }
}
