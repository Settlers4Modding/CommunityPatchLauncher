using System.Diagnostics;
using System.IO;

namespace CommunityPatchLauncher.Commands.Os
{
    /// <summary>
    /// This command will open a given folder
    /// </summary>
    internal class OpenFolderCommand : BaseCommand
    {
        private readonly string folderToOpen;

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
