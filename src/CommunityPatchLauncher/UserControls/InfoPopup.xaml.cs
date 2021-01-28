using CommunityPatchLauncher.ViewModels;
using System.Windows.Controls;

namespace CommunityPatchLauncher.UserControls
{
    /// <summary>
    /// Interaction logic for InfoPopup.xaml
    /// </summary>
    public partial class InfoPopup : UserControl
    {
        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public InfoPopup()
        {
            InitializeComponent();
            DataContext = new InfoPopupViewModel();
        }
    }
}
