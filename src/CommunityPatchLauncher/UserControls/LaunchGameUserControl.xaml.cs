using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Enums;
using CommunityPatchLauncher.ViewModels;
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
    /// Interaction logic for LaunchGameUserControl.xaml
    /// </summary>
    public partial class LaunchGameUserControl : UserControl, IDisposable
    {
        public LaunchGameUserControl()
        {
            InitializeComponent();
            DataContext = new LaunchGameModelView();
        }

        public void Dispose()
        {
            if (DataContext is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        public void SetPatch(AvailablePatches availablePatch)
        {
            if (DataContext is LaunchGameModelView launchGameModel)
            {
                Patch patch = new Patch(availablePatch);
                launchGameModel.SetPatch(patch);
            }
        }
    }


}
