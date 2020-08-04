using System.IO;
using System.Windows.Forms;

namespace CommunityPatchLauncher.Commands
{
    /// <summary>
    /// Get the installation directory from manual selection
    /// </summary>
    internal class InstallationFromManuelSelectionCommand : BaseDataCommand
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
                data = File.Exists(filePath + "S4_Main.exe") ? filePath : "";
                ExecutionDone();
            }
        }
    }
}
