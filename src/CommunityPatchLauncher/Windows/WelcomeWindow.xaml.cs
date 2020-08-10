using CommunityPatchLauncher.ViewModels;
using System.Windows;

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
        }
    }
}
