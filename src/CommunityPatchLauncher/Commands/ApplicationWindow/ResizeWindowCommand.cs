using CommunityPatchLauncher.Commands.DataContainer;
using System.Windows;

namespace CommunityPatchLauncher.Commands.ApplicationWindow
{
    /// <summary>
    /// This command will resize a specific window
    /// </summary>
    internal class ResizeWindowCommand : BaseCommand
    {
        /// <summary>
        /// The window to resize
        /// </summary>
        private readonly Window windowToResize;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="windowToResize"></param>
        public ResizeWindowCommand(Window windowToResize)
        {
            this.windowToResize = windowToResize;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return windowToResize != null && parameter is ResizeWindowData;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            if (parameter is ResizeWindowData resizeWindowData)
            {
                windowToResize.Width = resizeWindowData.Width;
                windowToResize.Height = resizeWindowData.Height;
            }
        }
    }
}
