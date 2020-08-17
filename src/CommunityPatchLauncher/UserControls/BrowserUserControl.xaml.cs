using CommunityPatchLauncher.ViewModels;
using System.Windows.Controls;

namespace CommunityPatchLauncher.UserControls
{
    /// <summary>
    /// Interaction logic for BrowserUserControl.xaml
    /// </summary>
    public partial class BrowserUserControl : UserControl
    {
        public BrowserUserControl(string fileToOpen)
        {
            InitializeComponent();
            DataContext = new BrowserModelView(fileToOpen);
        }
    }
}
