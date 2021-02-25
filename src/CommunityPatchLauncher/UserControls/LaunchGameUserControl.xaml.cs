using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Enums;
using CommunityPatchLauncher.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CommunityPatchLauncher.UserControls
{
    /// <summary>
    /// Interaction logic for LaunchGameUserControl.xaml
    /// </summary>
    public partial class LaunchGameUserControl : UserControl, IDisposable
    {
        public LaunchGameUserControl(Window mainWindow)
        {
            InitializeComponent();
            DataContext = new LaunchGameModelView(this, mainWindow);
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
