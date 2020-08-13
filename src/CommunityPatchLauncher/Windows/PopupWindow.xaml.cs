using CommunityPatchLauncher.ViewModels;
using FontAwesome.WPF;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CommunityPatchLauncher.Windows
{
    /// <summary>
    /// Interaction logic for PopupWindow.xaml
    /// </summary>
    public partial class PopupWindow : Window
    {
        /// <summary>
        /// Create a new popup window
        /// </summary>
        /// <param name="content">The content of the window</param>
        /// <param name="title">The title of the window</param>
        public PopupWindow(UserControl content, string title) : this(content, title, FontAwesomeIcon.None, null)
        {
        }

        /// <summary>
        /// Create a new popup window
        /// </summary>
        /// <param name="content">The content of the window</param>
        /// <param name="title">The title of the window</param>
        /// <param name="parameter">The parameter to use</param>
        public PopupWindow(UserControl content, string title, object parameter) : this(content, title, FontAwesomeIcon.None, parameter)
        {
        }

        /// <summary>
        /// Create a new popup window
        /// </summary>
        /// <param name="content">The content of the window</param>
        /// <param name="title">The title of the window</param>
        /// <param name="icon">The icon to use in the titlebar</param>
        /// <param name="parameter">The parameter to use</param>
        public PopupWindow(UserControl content, string title, FontAwesomeIcon icon, object parameter)
        {
            InitializeComponent();
            Title = title;
            DataContext = new PopupWindowsViewModel(this, content, icon, parameter);
            this.Closing += PopupWindow_Closing;
        }

        /// <summary>
        /// Closing event of the popup window
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void PopupWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataContext is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
