using System.Windows;

namespace CommunityPatchLauncher.Commands.ApplicationWindow
{
    /// <summary>
    /// This command will maximize a window
    /// </summary>
    internal class MaximizeWindowCommand : BaseCommand
    {
        /// <summary>
        /// The window to work on
        /// </summary>
        private readonly Window window;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="window">The window to work on</param>
        public MaximizeWindowCommand(Window window)
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
            WindowState newState = WindowState.Maximized;
            if (window.WindowState == newState)
            {
                newState = WindowState.Normal;
            }

            window.WindowState = newState;
        }
    }
}
