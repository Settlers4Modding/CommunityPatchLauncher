using CommunityPatchLauncher.ViewModels;
using System.Windows.Controls;

namespace CommunityPatchLauncher.UserControls
{
    /// <summary>
    /// Interaction logic for PlayUserControl.xaml
    /// </summary>
    public partial class PlayUserControl : UserControl
    {
        public PlayUserControl()
        {
            InitializeComponent();

            DataContext = new LaunchGameViewModel();
        }
    }
}
