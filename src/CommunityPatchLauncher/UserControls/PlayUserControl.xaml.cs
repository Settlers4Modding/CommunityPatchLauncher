using CommunityPatchLauncher.BindingData;
using CommunityPatchLauncher.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CommunityPatchLauncher.UserControls
{
    /// <summary>
    /// Interaction logic for PlayUserControl.xaml
    /// </summary>
    public partial class PlayUserControl : UserControl
    {
        public ICommand LaunchGameCommand { get; }
        public Patches AllPatches { get; }

        public PlayUserControl()
        {
            InitializeComponent();

            DataContext = this;
            AllPatches = new Patches();

            LaunchGameCommand = new LaunchGameCommand();
        }
    }
}
