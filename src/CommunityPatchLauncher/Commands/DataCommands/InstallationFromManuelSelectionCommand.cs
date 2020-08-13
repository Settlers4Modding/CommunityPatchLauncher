using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Windows.Forms;

namespace CommunityPatchLauncher.Commands.DataCommands
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
			CommonOpenFileDialog dialog = new CommonOpenFileDialog();
			dialog.IsFolderPicker = true;

			CommonFileDialogResult result = dialog.ShowDialog();
			if (result == CommonFileDialogResult.Ok) {
				string filePath = dialog.FileName + "\\";
				data = File.Exists(filePath + "S4_Main.exe") ? filePath : "";
				ExecutionDone();
			}
		}
    }
}
