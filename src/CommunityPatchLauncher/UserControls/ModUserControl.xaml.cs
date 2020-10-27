using CommunityPatchLauncher.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CommunityPatchLauncher.UserControls
{
    /// <summary>
    /// Interaction logic for ModUserControl.xaml
    /// </summary>
    public partial class ModUserControl : UserControl
    {
        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="parentWindow"></param>
        public ModUserControl(Window parentWindow)
        {
            InitializeComponent();
            DataContext = new ModModelView(parentWindow);
        }
    }
}
