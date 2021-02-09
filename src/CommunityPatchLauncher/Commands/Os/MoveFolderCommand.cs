using CommunityPatchLauncher.Commands.Condition;
using System.IO;

namespace CommunityPatchLauncher.Commands.Os
{
    class MoveFolderCommand : BaseCommand
    {
        /// <summary>
        /// Folder before it got changed
        /// </summary>
        private string sourceFolder;

        /// <summary>
        /// The condition to meet for the source folder
        /// </summary>
        private ICondition sourceCondition;

        /// <summary>
        /// The condition to meet for the target folder
        /// </summary>
        private ICondition targetCondition;

        /// <summary>
        /// Moves a Folder to a Folder
        /// </summary>
        /// <param name="sourceFolder">The source folder</param>
        public MoveFolderCommand(string sourceFolder) : this(sourceFolder, null, null)
        {
        }

        /// <summary>
        /// Moves a Folder to a Folder
        /// </summary>
        /// <param name="sourceFolder">The source folder</param>
        public MoveFolderCommand(string sourceFolder, ICondition sourceCondition) : this(sourceFolder, sourceCondition, null)
        {
        }

        /// <summary>
        /// Moves a Folder to a Folder
        /// </summary>
        /// <param name="sourceFolder">The source</param>
        /// <param name="sourceCondition">The source condition to meet</param>
        /// <param name="targetCondition">The target condition to meet</param>
        public MoveFolderCommand(string sourceFolder, ICondition sourceCondition, ICondition targetCondition)
        {
            this.sourceFolder = sourceFolder;
            this.sourceCondition = sourceCondition;
            this.targetCondition = targetCondition;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            if (parameter is string targetFolder)
            {
                return Directory.Exists(sourceFolder) && Directory.Exists(targetFolder);
            }
            return false;
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
                if (targetCondition != null && targetCondition.ConditionFailed(targetFolder))
                {
                    return;
                }
                Directory.CreateDirectory(targetFolder);

                string[] files = Directory.GetFiles(sourceFolder);
                foreach (string file in files)
                {
                    if (!File.Exists(file))
                    {
                        continue;
                    }

                    if (sourceCondition != null && sourceCondition.ConditionFailed(file))
                    {
                        continue;
                    }
                    FileInfo fileInfo = new FileInfo(file);
                    string targetFile = Path.Combine(targetFolder, fileInfo.Name);

                    File.Copy(file, targetFile);
                    if (File.Exists(targetFile))
                    {
                        File.Delete(file);
                    }
                }

                if (Directory.Exists(sourceFolder) && Directory.GetFiles(sourceFolder).Length == 0 && Directory.GetDirectories(sourceFolder).Length == 0)
                {
                    Directory.Delete(sourceFolder);
                }

                sourceFolder = targetFolder;
            }
        }
    }
}
