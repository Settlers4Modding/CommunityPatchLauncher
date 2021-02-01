using CommunityPatchLauncher.ViewModels;
using System.Windows.Controls;

namespace CommunityPatchLauncher.UserControls
{
    /// <summary>
    /// Interaction logic for ResizeWindowUserControl.xaml
    /// </summary>
    public partial class ResizeWindowUserControl : UserControl
    {
        public ResizeWindowUserControl()
        {
            InitializeComponent();
            DataContext = new ResizeWindowViewModel();
        }
    }
}
