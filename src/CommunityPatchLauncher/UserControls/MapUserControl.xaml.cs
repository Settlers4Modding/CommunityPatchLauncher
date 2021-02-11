using CommunityPatchLauncher.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CommunityPatchLauncher.UserControls
{
    /// <summary>
    /// Interaktionslogik für MapUserControl.xaml
    /// </summary>
    public partial class MapUserControl : UserControl
    {
        public MapUserControl(Window window)
        {
            InitializeComponent();
            DataContext = new MapViewModel(window);
        }
    }
}
