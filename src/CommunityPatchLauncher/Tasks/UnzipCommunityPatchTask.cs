using CommunityPatchLauncherFramework.Settings.Container;
using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System;
using System.IO;
using System.IO.Compression;

namespace CommunityPatchLauncher.Tasks
{
    /// <summary>
    /// This class will unzip the community patch
    /// </summary>
    internal class UnzipCommunityPatchTask : ProgressAbstractTask
    {
        /// <inheritdoc>
        public override bool Execute(bool previousTaskState)
        {
            string filePath = GetSetting<string>("PatchInstaller");
            if (filePath == null)
            {
                TaskDone();
                return false;
            }
            string zipFilePath = filePath;
            if (!File.Exists(zipFilePath))
            {
                TaskDone();
                return false;
            }
            string unzipFolder = Path.GetTempPath() + Guid.NewGuid() + "\\";

            using (ZipArchive archive = new ZipArchive(new FileStream(zipFilePath, FileMode.Open), ZipArchiveMode.Read))
            {
                int filesToUnzip = archive.Entries.Count;
                int currentFile = 0;
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    currentFile++;
                    int currentProgress = (int)(currentFile / (float)filesToUnzip * 100);
                    FileInfo info = new FileInfo(unzipFolder + entry.FullName);
                    if (!Directory.Exists(info.DirectoryName))
                    {
                        Directory.CreateDirectory(info.DirectoryName);
                    }
                    try
                    {
                        if (info.DirectoryName + "\\" == info.FullName)
                        {
                            ProgressHasChanged(currentProgress);
                            continue;
                        }
                        entry.ExtractToFile(info.FullName, true);
                    }
                    catch (Exception ex)
                    {
                        TaskDone();
                        return false;
                    }

                    ProgressHasChanged(currentProgress);
                }
            }

            Settings.Add(new SettingPair("ExtractPath", unzipFolder));
            TaskDone();
            return true;
        }
    }
}
