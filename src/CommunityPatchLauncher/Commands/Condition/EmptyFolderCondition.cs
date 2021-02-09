using System.IO;

namespace CommunityPatchLauncher.Commands.Condition
{
    /// <summary>
    /// Check if a folder is empty
    /// </summary>
    internal class EmptyFolderCondition : BaseCondition
    {
        /// <inheritdoc/>
        public override bool ConditionFullfilled(object parameter)
        {
            errorMessage = Properties.Resources.Default_FolderIsNotEmpty;
            if (parameter is string folder)
            {
                if (Directory.Exists(folder) && Directory.GetFiles(folder).Length <= 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
