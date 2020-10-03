using CommunityPatchLauncher.Commands.ApplicationWindow;
using CommunityPatchLauncher.UserControls.SpecialTypes;
using System.Windows;

namespace CommunityPatchLauncher.ViewModels
{
    internal class InfoPopupViewModel : BaseViewModel, IPopupContent
    {
        public string DialogText { get; private set; }

        public void Init(Window currentWindow, object parameter)
        {
            CloseWindowCommand = new CloseWindowCommand(currentWindow);
            DialogText = parameter.ToString();
        }
    }
}
