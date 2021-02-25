using CommunityPatchLauncher.Commands.Condition;
using CommunityPatchLauncher.Enums;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;

namespace CommunityPatchLauncher.Commands.Os
{
    /// <summary>
    /// Command to select a folder and save it into the data field
    /// </summary>
    internal class SelectFolderCommand : BaseDataCommand
    {
        /// <summary>
        /// The condition to meet
        /// </summary>
        private readonly ICondition condition;

        /// <summary>
        /// Create a new instance for this class
        /// </summary>
        public SelectFolderCommand() : this(null)
        {
        }

        /// <summary>
        /// Create a new instance for this class
        /// </summary>
        /// <param name="condition">The condition to meet</param>
        public SelectFolderCommand(ICondition condition)
        {
            this.condition = condition;
        }

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
                if (condition != null && condition.ConditionFailed(data))
                {
                    ThrowError(ErrorSeverityEnum.Error, condition.ErrorMessage);
                    return;
                }
                ExecutionDone();
            }
        }
    }
}
