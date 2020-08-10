using CommunityPatchLauncher.Commands;
using System.Windows;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    internal class MainWindowModel : BaseViewModel
    {
        public ICommand CloseApplication { get; private set; }
        public ICommand MaximizeWindow { get; private set; }
        public ICommand MinimizeWindow { get; private set; }


        public MainWindowModel(Window window) : base(window)
        {
            CloseApplication = new CloseApplicationCommand();
            MinimizeWindow = new MinimizeWindowCommand(currentWindow);
            MaximizeWindow = new MaximizeWindowCommand(currentWindow);
        }


    }
}
