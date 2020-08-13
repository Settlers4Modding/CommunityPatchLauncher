using CommunityPatchLauncher.ViewModels.SpecialViews;

namespace CommunityPatchLauncher.Commands.ApplicationWindow
{
    internal class ReloadObjectCommand : BaseCommand
    {
        private readonly IViewModelReloadable reloadable;

        public ReloadObjectCommand(IViewModelReloadable reloadable)
        {
            this.reloadable = reloadable;
        }

        public override bool CanExecute(object parameter)
        {
            return reloadable != null;
        }

        public override void Execute(object parameter)
        {
            reloadable.Reload();
        }
    }
}
