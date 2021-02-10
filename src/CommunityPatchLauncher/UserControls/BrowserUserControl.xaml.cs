using CommunityPatchLauncher.Documentation.Factories;
using CommunityPatchLauncher.ViewModels;
using CommunityPatchLauncherFramework.Documentation.Factory;
using System.Windows.Controls;

namespace CommunityPatchLauncher.UserControls
{
    /// <summary>
    /// Interaction logic for BrowserUserControl.xaml
    /// </summary>
    public partial class BrowserUserControl : UserControl
    {
        /// <summary>
        /// Empty constructor to embedd the browser
        /// </summary>
        public BrowserUserControl() : this(string.Empty)
        {
        }

        /// <summary>
        /// A user control containing a full size webbrowser to display content
        /// </summary>
        /// <param name="fileToOpen"></param>
        public BrowserUserControl(string fileToOpen) : this(fileToOpen, new LocalDocumentManagerFactory())
        {
        }

        /// <summary>
        /// A user control containing a full size webbrowser to display content
        /// </summary>
        /// <param name="fileToOpen"></param>
        public BrowserUserControl(string fileToOpen, IDocumentManagerFactory managerFactory)
        {
            InitializeComponent();
            DataContext = new BrowserModelView(fileToOpen, this, managerFactory);
        }
    }
}
