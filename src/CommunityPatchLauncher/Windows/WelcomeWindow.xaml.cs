using CommunityPatchLauncher.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace CommunityPatchLauncher.Windows
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        /// <summary>
        /// welcome window for the application
        /// </summary>
        public WelcomeWindow()
        {
            InitializeComponent();
            DataContext = new WelcomeViewModel(this);
            SetDefaultWindowStyle();
            MouseDown += WelcomeWindow_MouseDown;
        }

        private void WelcomeWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (WindowState == WindowState.Maximized)
                {
                    WindowState = WindowState.Normal;
                }
                DragMove();
            }
        }

        protected void SetDefaultWindowStyle()
        {
            try
            {
                Style = Resources["WindowStyle"] as Style;
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
