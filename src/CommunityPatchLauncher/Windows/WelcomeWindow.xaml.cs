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
            this.MouseDown += WelcomeWindow_MouseDown;
        }

        private void WelcomeWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        protected void SetDefaultWindowStyle()
        {
            try
            {
                this.Style = this.Resources["WindowStyle"] as System.Windows.Style;
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
