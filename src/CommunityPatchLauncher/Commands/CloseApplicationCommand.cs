using System.Windows;

namespace CommunityPatchLauncher.Commands
{
    class CloseApplicationCommand : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
