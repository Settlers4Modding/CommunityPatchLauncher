using System.Diagnostics;
using System.Windows;

namespace CommunityPatchLauncher.Commands.ApplicationWindow
{
    /// <summary>
    /// This command will restart the application
    /// </summary>
    internal class RestartApplicationCommand : BaseCommand
    {
        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
