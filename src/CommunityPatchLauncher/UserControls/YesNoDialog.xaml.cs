using CommunityPatchLauncher.ViewModels;
using System.Windows.Controls;

namespace CommunityPatchLauncher.UserControls
{
    /// <summary>
    /// Interaction logic for YesNoDialog.xaml
    /// </summary>
    public partial class YesNoDialog : UserControl
    {
        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public YesNoDialog()
        {
            InitializeComponent();
            DataContext = new YesNoViewModel();
        }
    }
}
