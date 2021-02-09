using System.IO;

namespace CommunityPatchLauncher.Commands.Condition
{
    /// <summary>
    /// This condition will check if the folder is the settlers 4 folder
    /// </summary>
    internal class SettlerFolderCondition : BaseCondition
    {
        /// <inheritdoc/>
        public override bool ConditionFullfilled(object parameter)
        {
            errorMessage = Properties.Resources.Default_NotTheSettlerFolder;
            if (parameter is string filePath)
            {
                return File.Exists(filePath + "S4_Main.exe");
            }

            return false;
        }
    }
}
