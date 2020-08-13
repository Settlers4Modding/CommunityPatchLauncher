using CommunityPatchLauncher.ViewModels.SpecialViews;

namespace CommunityPatchLauncher.Commands.ApplicationWindow
{
    /// <summary>
    /// Reload a given object
    /// </summary>
    internal class ReloadObjectCommand : BaseCommand
    {
        /// <summary>
        /// The object to reload
        /// </summary>
        private readonly IViewModelReloadable reloadable;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="reloadable">The object to reload</param>
        public ReloadObjectCommand(IViewModelReloadable reloadable)
        {
            this.reloadable = reloadable;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return reloadable != null;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }
            reloadable.Reload();
        }
    }
}
