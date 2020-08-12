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
        public PopupWindow(UserControl content, string title) : this(content, title, FontAwesomeIcon.None, null)
        {
        }

        public PopupWindow(UserControl content, string title, object parameter) : this(content, title, FontAwesomeIcon.None, parameter)
        {
        }

        public PopupWindow(UserControl content, string title, FontAwesomeIcon icon, object parameter)
        {
            InitializeComponent();
            Title = title;
            DataContext = new PopupWindowsViewModel(this, content, icon, parameter);
            this.Closing += PopupWindow_Closing;
        }

        private void PopupWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataContext is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
