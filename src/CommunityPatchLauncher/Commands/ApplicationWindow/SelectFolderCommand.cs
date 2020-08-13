using System.IO;
using System.Windows.Forms;

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
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (parameter != null && parameter is string stringParameter)
            {
                if (Directory.Exists(stringParameter))
                {
                    folderBrowserDialog.SelectedPath = stringParameter;
                }
            }
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filePath = folderBrowserDialog.SelectedPath + "\\";
                data = Directory.Exists(filePath) ? filePath : string.Empty;
                ExecutionDone();
            }
        }
    }
}
