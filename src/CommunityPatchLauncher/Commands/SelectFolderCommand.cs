using System;
using System.IO;
using System.Windows.Forms;

namespace CommunityPatchLauncher.Commands
{
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
