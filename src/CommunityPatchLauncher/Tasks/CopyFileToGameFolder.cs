using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System;
using System.Collections.Generic;
using System.IO;

namespace CommunityPatchLauncher.Tasks
{
    internal class CopyFileToGameFolder : ProgressAbstractTask
    {
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
            foreach (string currentFile in filesToMove)
            {
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
                catch (Exception ex)
                {
                    Directory.Delete(extractPath, true);
                    return false;
                }
            }

            Directory.Delete(extractPath, true);
            return true;
        }

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
