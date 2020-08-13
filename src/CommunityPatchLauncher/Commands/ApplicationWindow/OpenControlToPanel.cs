using CommunityPatchLauncher.ViewModels.SpecialViews;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CommunityPatchLauncher.Commands.ApplicationWindow
{
    /// <summary>
    /// This command will open a given command to a specific control panel
    /// </summary>
    internal class OpenControlToPanel : BaseCommand
    {
        /// <summary>
        /// The panel to add the control to
        /// </summary>
        protected readonly DockPanel panelToUse;

        /// <summary>
        /// The user control to add
        /// </summary>
        protected readonly UserControl userControl;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="panelToUse">The panel to add the control to</param>
        /// <param name="userControl">The control to add</param>
        public OpenControlToPanel(DockPanel panelToUse, UserControl userControl)
        {
            this.panelToUse = panelToUse;
            this.userControl = userControl;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            foreach (UIElement child in panelToUse.Children)
            {
                if (child is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
            List<UIElement> elementsToRemove = new List<UIElement>();
            foreach (UIElement uIElement in panelToUse.Children)
            {
                elementsToRemove.Add(uIElement);
            }
            foreach (UIElement elementToRemove in elementsToRemove)
            {
                panelToUse.Children.Remove(elementToRemove);
            }

            if (userControl.DataContext is IViewModelReloadable reloadable)
            {
                reloadable.Reload();
            }
            panelToUse.Children.Add(userControl);
        }
    }
}
