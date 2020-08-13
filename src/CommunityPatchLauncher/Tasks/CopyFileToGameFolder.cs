using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System;
using System.Collections.Generic;
using System.IO;

namespace CommunityPatchLauncher.Tasks
{
    /// <summary>
    /// This class will copy the game files to the game folder
    /// </summary>
    internal class CopyFileToGameFolder : ProgressAbstractTask
    {
        /// <inheritdoc/>
        public override bool Execute(bool previousTaskState)
        {
            string extractPath = GetSetting<string>("ExtractPath");
            string targetFolder = settingManager.GetValue<string>("GameFolder");
            if (targetFolder == null || extractPath == null || !File.Exists(extractPath + "S4_Main.exe"))
            {
                TaskDone();
                return false;
            }
            IReadOnlyList<string> filesToMove = GetFilesInFolder(extractPath);
            int currentFileCount = 0;
            foreach (string currentFile in filesToMove)
            {
                currentFileCount++;
                int currentProgress = (int)(currentFileCount / (float)filesToMove.Count * 100);
                FileInfo fileInfo = new FileInfo(currentFile);
                string realTarget = targetFolder;
                if (extractPath != fileInfo.DirectoryName + "\\")
                {
                    realTarget += fileInfo.DirectoryName.Replace(extractPath, "") + "/";
                    realTarget.Replace("\\", "/");
                }
                realTarget += fileInfo.Name;

                try
                {
                    File.Copy(currentFile, realTarget, true);
                }
                catch (Exception)
                {
                    Directory.Delete(extractPath, true);
                    return false;
                }
                ProgressHasChanged(currentProgress);
            }

            Directory.Delete(extractPath, true);
            TaskDone();
            return true;
        }

        /// <summary>
        /// Get all the files in a given folder
        /// </summary>
        /// <param name="folder">The folder to get all the files from</param>
        /// <returns>A list with all the files in the folder</returns>
        private IReadOnlyList<string> GetFilesInFolder(string folder)
        {
            List<string> returnList = new List<string>();
            if (!Directory.Exists(folder))
            {
                return returnList;
            }
            foreach (string file in Directory.GetFiles(folder))
            {
                returnList.Add(file);
            }
            foreach (string subFolder in Directory.GetDirectories(folder))
            {
                returnList.AddRange(GetFilesInFolder(subFolder));
            }
            return returnList;
        }
    }
}
