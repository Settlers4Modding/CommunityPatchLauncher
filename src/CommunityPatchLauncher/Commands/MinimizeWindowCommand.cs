using System.Windows;

namespace CommunityPatchLauncher.Commands
{
    /// <summary>
    /// This command will minimize a window
    /// </summary>
    internal class MinimizeWindowCommand : BaseCommand
    {
        /// <summary>
        /// The window to work on
        /// </summary>
        private readonly Window window;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="window">The window to work on</param>
        public MinimizeWindowCommand(Window window)
        {
            this.window = window;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return window.WindowState != WindowState.Minimized;
        }

        /// <inheritdoc/>
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
