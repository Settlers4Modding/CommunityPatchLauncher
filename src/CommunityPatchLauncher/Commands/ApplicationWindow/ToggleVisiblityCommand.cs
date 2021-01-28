using System.Windows;
using System.Windows.Controls;

namespace CommunityPatchLauncher.Commands.ApplicationWindow
{
    /// <summary>
    /// This class will toggle the visiblity of a element
    /// </summary>
    internal class ToggleVisiblityCommand : BaseCommand
    {
        /// <summary>
        /// The control to search on
        /// </summary>
        private readonly ContentControl controlToSeachOn;

        /// <summary>
        /// If a fixed element is defined
        /// </summary>
        private readonly UIElement toggleObject;

        /// <summary>
        /// This delegate will allow to update the element thread safe
        /// </summary>
        /// <param name="element">The element to change</param>
        /// <param name="newVisiblity">The new visiblity to use</param>
        private delegate void UpdateVisiblityCallback(UIElement element, Visibility newVisiblity);

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="controlElement">The control element to search on</param>
        public ToggleVisiblityCommand(ContentControl controlElement)
        {
            controlToSeachOn = controlElement;
        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="controlElement">The control element to search on</param>
        /// <param name="fixedElementName">The name of the object to search on</param>
        public ToggleVisiblityCommand(ContentControl controlElement, string fixedElementName) : this(controlElement)
        {
            toggleObject = GetObjectFromWindow(fixedElementName);
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return controlToSeachOn != null && (parameter is string || toggleObject != null);
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }
            UIElement element = toggleObject ?? GetObjectFromWindow(parameter.ToString());
            if (element == null)
            {
                return;
            }
            Visibility targetVisiblity = ElementIsVisible(element) ? Visibility.Collapsed : Visibility.Visible;
            element.Dispatcher.Invoke(
                new UpdateVisiblityCallback(this.UpdateVisibility),
                new object[] { element, targetVisiblity });

        }

        /// <summary>
        /// Callback method to thread safe update the element
        /// </summary>
        /// <param name="element">The element to update</param>
        /// <param name="targetVisibility">The new visiblity</param>
        private void UpdateVisibility(UIElement element, Visibility targetVisibility)
        {
            element.Visibility = targetVisibility;
        }

        /// <summary>
        /// Is this element visible
        /// </summary>
        /// <param name="element">The element to check</param>
        /// <returns>True if the element is visible</returns>
        private bool ElementIsVisible(UIElement element)
        {
            return element.Visibility == Visibility.Visible;
        }

        /// <summary>
        /// Get the object from the window to search on
        /// </summary>
        /// <param name="objectName">The name of the object to get</param>
        /// <returns>The ui element to update or null if not found</returns>
        private UIElement GetObjectFromWindow(string objectName)
        {
            UIElement returnObject = null;
            if (controlToSeachOn == null)
            {
                return returnObject;
            }
            object element = controlToSeachOn.FindName(objectName);
            if (element is UIElement dependencyObject)
            {
                returnObject = dependencyObject;
            }

            return returnObject;
        }
    }
}
