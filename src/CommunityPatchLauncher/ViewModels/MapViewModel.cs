using System.Windows;
using CommunityPatchLauncher.UserControls;
using CommunityPatchLauncher.Commands.Os;
using System.Windows.Input;
using System.Windows.Controls;
using CommunityPatchLauncherFramework.Documentation.Factory;
using CommunityPatchLauncher.Documentation.Factories;
using CommunityPatchLauncherFramework.Documentation.Manager;
using CommunityPatchLauncher.Documentation.Strategy;

namespace CommunityPatchLauncher.ViewModels
{
    /// <summary>
    /// View model for the map view user control
    /// </summary>
    class MapViewModel : BaseViewModel
    {
        /// <summary>
        /// Opens the MP Map Folder
        /// </summary>
        public ICommand OpenMulitplayerMapFolder { get; private set; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="parent">The parent user control</param>
        public MapViewModel(UserControl parent)
        {
             DependencyObject mapinfoDisplay = (DependencyObject)parent.FindName("WB_MapInfo");
            if (mapinfoDisplay is BrowserUserControl browserControl)
            {
                if (browserControl.DataContext is BrowserModelView modelView)
                {
                    modelView.ShowLoading(false);
                    modelView.ChangeDocument("MapInfo.md");
                }
            }

            OpenMulitplayerMapFolder = new OpenFolderCommand(settingManager.GetValue<string>("GameFolder") + "/Map/User/");
        }
    }
}
