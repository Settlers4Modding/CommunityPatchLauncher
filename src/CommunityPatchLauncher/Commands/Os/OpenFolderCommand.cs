using System.Diagnostics;
using System.IO;

namespace CommunityPatchLauncher.Commands.Os
{
    /// <summary>
    /// This command will open a given folder
    /// </summary>
    internal class OpenFolderCommand : BaseCommand
    {
        /// <summary>
        /// The folder to open
        /// </summary>
        private readonly string folderToOpen;

        /// <summary>
        /// Create a new instance of this command
        /// </summary>
        /// <param name="folderToOpen"></param>
        public OpenFolderCommand(string folderToOpen)
        {
            this.folderToOpen = folderToOpen;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return Directory.Exists(GetRealFolder(parameter));
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            Process.Start(GetRealFolder(parameter));
        }

        /// <summary>
        /// Get the real folder which should be opend
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private string GetRealFolder(object parameter)
        {
            string folderToCheck = folderToOpen;
            if (parameter != null && parameter is string realFolder)
            {
                folderToCheck = realFolder;
            }
            return folderToCheck;
        }
    }
}
