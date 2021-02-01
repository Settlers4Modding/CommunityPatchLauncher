using CommunityPatchLauncher.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CommunityPatchLauncher.UserControls
{
    /// <summary>
    /// Interaction logic for SettingsUserControl.xaml
    /// </summary>
    public partial class SettingsUserControl : UserControl
    {
        public SettingsUserControl(Window window)
        {
            InitializeComponent();
            DataContext = new SettingsViewModel(window);
        }
    }
}
