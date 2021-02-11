using System.Windows;
using CommunityPatchLauncher.UserControls;
using CommunityPatchLauncher.Commands.Os;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    class MapViewModel : BaseViewModel
    {
        /// <summary>
        /// Opens the MP Map Folder
        /// </summary>
        public ICommand OpenMulitplayerMapFolder { get; private set; }

        public MapViewModel (Window window)
        {
             DependencyObject mapinfoDisplay = (DependencyObject) window.FindName("WB_MapInfo");
            if (mapinfoDisplay is BrowserUserControl browserControl)
            {
                if (browserControl.DataContext is BrowserModelView modelView)
                {
                    modelView.ChangeDocument("MapInfo.md");
                }
            }

            OpenMulitplayerMapFolder = new OpenFolderCommand(settingManager.GetValue<string>("GameFolder") + "/Map/User/");
        }
    }
}
