using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;

namespace CommunityPatchLauncher.Commands.ApplicationWindow
{
    /// <summary>
    /// Command to select a folder and save it into the data field
    /// </summary>
    internal class SelectFolderCommand : BaseDataCommand
    {
        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;

            if (parameter != null && parameter is string stringParameter)
            {
                if (Directory.Exists(stringParameter))
                {
                    dialog.DefaultDirectory = stringParameter;
                }
            }

            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                string filePath = dialog.FileName + "\\";
                data = Directory.Exists(filePath) ? filePath : string.Empty;
                ExecutionDone();
            }
        }
    }
}
