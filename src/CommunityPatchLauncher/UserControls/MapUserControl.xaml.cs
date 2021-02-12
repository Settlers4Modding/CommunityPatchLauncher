using CommunityPatchLauncher.ViewModels;
using System.Windows.Controls;

namespace CommunityPatchLauncher.UserControls
{
    /// <summary>
    /// Interactionlogic for MapUserControl.xaml
    /// </summary>
    public partial class MapUserControl : UserControl
    {
        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public MapUserControl()
        {
            InitializeComponent();
            DataContext = new MapViewModel(this);
        }
    }
}
