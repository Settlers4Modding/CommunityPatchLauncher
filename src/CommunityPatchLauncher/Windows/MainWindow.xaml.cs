using CommunityPatchLauncher.ViewModels;
using System.Windows;

namespace CommunityPatchLauncher.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Create a new main window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowModel(this);
        }
    }
}
