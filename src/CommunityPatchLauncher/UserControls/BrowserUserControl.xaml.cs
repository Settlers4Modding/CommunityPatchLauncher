using CommunityPatchLauncher.ViewModels;
using System.Windows.Controls;

namespace CommunityPatchLauncher.UserControls
{
    /// <summary>
    /// Interaction logic for BrowserUserControl.xaml
    /// </summary>
    public partial class BrowserUserControl : UserControl
    {
        /// <summary>
        /// A user control containing a full size webbrowser to display content
        /// </summary>
        /// <param name="fileToOpen"></param>
        public BrowserUserControl(string fileToOpen)
        {
            InitializeComponent();
            DataContext = new BrowserModelView(fileToOpen);
        }
    }
}
