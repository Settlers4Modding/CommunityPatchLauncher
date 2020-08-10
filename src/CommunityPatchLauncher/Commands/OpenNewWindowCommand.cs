using System.Windows;

namespace CommunityPatchLauncher.Commands
{
    class OpenNewWindowCommand : BaseCommand
    {
        private readonly Window windowToOpen;

        public OpenNewWindowCommand(Window windowToOpen)
        {
            this.windowToOpen = windowToOpen;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            windowToOpen.ShowDialog();
        }
    }
}
