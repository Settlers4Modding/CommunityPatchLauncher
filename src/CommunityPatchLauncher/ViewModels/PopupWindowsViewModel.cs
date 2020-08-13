using CommunityPatchLauncher.BindingData;
using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.UserControls.SpecialTypes;
using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    /// <summary>
    /// This class is the view model for the popup 
    /// </summary>
    internal class PopupWindowsViewModel : BaseViewModel
    {
        public FontAwesomeIcon TitleBarIcon
        {
            get => titleBarIcon;
            private set
            {
                titleBarIcon = value;
                RaisePropertyChanged("TitleBarIcon");
            }
        }
        private FontAwesomeIcon titleBarIcon;

        private readonly DockPanel dockPanelToFill;
        public UserControl Content { get; }

        public PopupWindowsViewModel(Window window, UserControl content, object parameter) : this(window, content, FontAwesomeIcon.None, parameter)
        {
        }

        public PopupWindowsViewModel(Window window, UserControl content, FontAwesomeIcon fontAwesomeIcon, object parameter) : base(window)
        {
            TitleBarIcon = fontAwesomeIcon;
            Content = content;
            object controlDock = currentWindow.FindName("DP_ControlDock");
            currentWindow.SizeToContent = SizeToContent.WidthAndHeight;

            if (content.DataContext is IPopupContent popupContent)
            {
                popupContent.Init(currentWindow, parameter);
            }

            if (controlDock is DockPanel dockPanel)
            {
                dockPanelToFill = dockPanel;
                dockPanelToFill.Children.Add(Content);
            }
        }

        protected override void AddWindowResizeableCommand()
        {
        }

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
