using System.Windows;

namespace CommunityPatchLauncher.Commands
{
    /// <summary>
    /// Close a specific window
    /// </summary>
    public class CloseWindowCommand : BaseCommand
    {
        /// <summary>
        /// The window to close
        /// </summary>
        private readonly Window windowToClose;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="windowToClose">The window to close</param>
        public CloseWindowCommand(Window windowToClose)
        {
            this.windowToClose = windowToClose;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            windowToClose.Close();
        }
    }
}
