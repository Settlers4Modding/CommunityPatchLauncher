using System.Windows;

namespace CommunityPatchLauncher.Commands
{
    internal class MinimizeWindowCommand : BaseCommand
    {
        private readonly Window window;

        public MinimizeWindowCommand(Window window)
        {
            this.window = window;
        }

        public override bool CanExecute(object parameter)
        {
            return window.WindowState != WindowState.Minimized;
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            window.WindowState = WindowState.Minimized;
        }
    }
}
