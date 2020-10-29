using CommunityPatchLauncher.ViewModels;
using System.Windows.Controls;

namespace CommunityPatchLauncher.UserControls
{
    /// <summary>
    /// Interaction logic for ProgressBarDialog.xaml
    /// </summary>
    public partial class ProgressBarDialog : UserControl
    {
        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public ProgressBarDialog()
        {
            InitializeComponent();
            DataContext = new ProgressBarDialogViewModel();
        }
    }
}
