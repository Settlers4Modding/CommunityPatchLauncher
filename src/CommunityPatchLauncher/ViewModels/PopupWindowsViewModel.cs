using CommunityPatchLauncher.UserControls.SpecialTypes;
using FontAwesome.WPF;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CommunityPatchLauncher.ViewModels
{
    /// <summary>
    /// This class is the view model for the popup 
    /// </summary>
    internal class PopupWindowsViewModel : BaseViewModel
    {
        /// <summary>
        /// The icon to use for the title bar
        /// </summary>
        public FontAwesomeIcon TitleBarIcon
        {
            get => titleBarIcon;
            private set
            {
                titleBarIcon = value;
                RaisePropertyChanged("TitleBarIcon");
            }
        }
        /// <summary>
        /// The icon to use for the title bar
        /// </summary>
        private FontAwesomeIcon titleBarIcon;

        /// <summary>
        /// The dock panel to use for docking content
        /// </summary>
        private readonly DockPanel dockPanelToFill;

        /// <summary>
        /// The content to use
        /// </summary>
        public UserControl Content { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="window">The window this popup was opened in</param>
        /// <param name="content">The content for the popup window</param>
        /// <param name="parameter">The parameter for the popup window</param>
        public PopupWindowsViewModel(Window window, UserControl content, object parameter) : this(window, content, FontAwesomeIcon.None, parameter)
        {
        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="window">The window this popup was opened in</param>
        /// <param name="content">The content for the popup window</param>
        /// <param name="fontAwesomeIcon">The icon to use for the title bar</param>
        /// <param name="parameter">The parameter for the popup window</param>
        public PopupWindowsViewModel(Window window, UserControl content, FontAwesomeIcon fontAwesomeIcon, object parameter) : base(window)
        {
            TitleBarIcon = fontAwesomeIcon;
            Content = content;
            object controlDock = currentWindow.FindName("DP_ControlDock");
            currentWindow.SizeToContent = SizeToContent.WidthAndHeight;

            if (content.DataContext is IPopupContent popupContent)
            {
                popupContent.Init(currentWindow, fontAwesomeIcon, parameter);
            }

            if (controlDock is DockPanel dockPanel)
            {
                dockPanelToFill = dockPanel;
                dockPanelToFill.Children.Add(Content);
            }
        }

        /// <inheritdoc/>
        protected override void AddWindowResizeableCommand()
        {
        }

        /// <summary>
        /// Things to do if the window is disposing
        /// </summary>
        public override void Dispose()
        {
            dockPanelToFill.Children.Remove(Content);
            if (Content is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
