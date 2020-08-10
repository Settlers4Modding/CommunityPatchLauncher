using System.Windows;

namespace CommunityPatchLauncher.Commands
{
    /// <summary>
    /// Close the whole application
    /// </summary>
    class CloseApplicationCommand : BaseCommand
    {
        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
