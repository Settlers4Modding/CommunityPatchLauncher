using System.Diagnostics;
using System.IO;

namespace CommunityPatchLauncher.Commands.Os
{
    class MoveFolderCommand : BaseCommand
    {
        /// <summary>
        /// Later target folder
        /// </summary>
        private string targetFolder;

        /// <summary>
        /// Folder before it got changed
        /// </summary>
        private string lastFolder;

        /// <summary>
        /// Target folder has to be Empty?
        /// Yes: true, No: False
        /// </summary>
        private bool hasToEmpty;

        /// <summary>
        /// Moves a Folder to a Folder
        /// </summary>
        /// <param name="moveFrom"></param>
        /// <param name="hasToEmpty"></param>
        public MoveFolderCommand(string moveFrom, bool hasToEmpty)
        {

            this.lastFolder = moveFrom;
            this.hasToEmpty = hasToEmpty;

        }

        /// <summary>
        /// Check if the Folder is empty,
        /// returns true when folder exists and there are no Files given,
        /// false when one is not true
        /// </summary>
        /// <param name="targetFolder"></param>
        /// <returns></returns>
        private bool IsEmpty(string targetFolder)
        {
            if (Directory.Exists(targetFolder) && Directory.GetFiles(targetFolder).Length <= 0)
            {
                return true;
            }
            return false;
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
                if (parameter != null && parameter is string targetFolder)
            {
                    if (!Directory.Exists(targetFolder)) Directory.CreateDirectory(targetFolder);
                if (hasToEmpty == true)
                {
                    if (IsEmpty(targetFolder))
                    {
                        Directory.Move(lastFolder, targetFolder);
                    }
                }
                else
                {
                    Directory.Move(lastFolder, targetFolder);
                }
            }
        
            //Process.Start(GetRealFolder(parameter));
        }
        private string GetRealFolder(object parameter)
        {
            string folderToCheck = targetFolder;
            if (parameter != null && parameter is string realFolder)
            {
                folderToCheck = realFolder;
            }
            return folderToCheck;
        }

    }
}
