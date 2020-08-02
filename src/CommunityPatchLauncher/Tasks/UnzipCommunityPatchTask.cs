using CommunityPatchLauncherFramework.Settings.Container;
using CommunityPatchLauncherFramework.Settings.Manager;
using CommunityPatchLauncherFramework.TaskPipeline.EventData;
using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace CommunityPatchLauncher.Tasks
{
    /// <summary>
    /// This class will unzip the community patch
    /// </summary>
    public class UnzipCommunityPatchTask : IProgressTask
    {
        /// <summary>
        /// The total workload which should be done
        /// </summary>
        private readonly int totalWorkload;

        /// <summary>
        /// The settings manager to use
        /// </summary>
        private SettingManager settingsManager;

        /// <summary>
        /// The internal settings of this task
        /// </summary>
        public HashSet<SettingPair> Settings { get; private set; }

        /// <summary>
        /// Should this class abort on error
        /// </summary>
        public bool AbortOnError { get; private set; }

        /// <summary>
        /// Did the progress change
        /// </summary>
        public event EventHandler<TaskProgressChanged> ProgressChanged;

        /// <summary>
        /// Is the task already completed
        /// </summary>
        public event EventHandler<TaskDone> TaskComplete;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public UnzipCommunityPatchTask()
        {
            AbortOnError = true;
            totalWorkload = 100;
        }

        /// <inheritdoc>
        public void Init(SettingManager settingManager, HashSet<SettingPair> taskSettings)
        {
            this.settingsManager = settingManager;
            Settings = taskSettings;
        }

        /// <inheritdoc>
        public bool Execute(bool previousTaskState)
        {
            SettingPair upToDate = Settings.Where((obj) => obj.Key == "UpToDate").First();
            if (upToDate == null)
            {
                TaskDone();
                return false;
            }
            if (upToDate.GetValue<bool>())
            {
                TaskDone();
                return true;
            }
            SettingPair filePath = Settings.Where((obj) => obj.Key == "PatchInstaller").First();

            if (filePath == null)
            {
                TaskDone();
                return true;
            }
            string zipFilePath = filePath.GetValue<string>();
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
                    int currentProgress = (int)((currentFile / (float)filesToUnzip) * 100);
                    EventHandler<TaskProgressChanged> handler = ProgressChanged;
                    FileInfo info = new FileInfo(unzipFolder + entry.FullName);
                    if (!Directory.Exists(info.DirectoryName))
                    {
                        Directory.CreateDirectory(info.DirectoryName);
                    }
                    try
                    {
                        if (info.DirectoryName + "\\" == info.FullName)
                        {
                            handler?.Invoke(this, new TaskProgressChanged(totalWorkload, currentProgress));
                            continue;
                        }
                        entry.ExtractToFile(info.FullName, true);
                    }
                    catch (Exception ex)
                    {
                        TaskDone();
                        return false;
                    }

                    handler?.Invoke(this, new TaskProgressChanged(totalWorkload, currentProgress));
                }
            }

            Settings.Add(new SettingPair("extractPath", unzipFolder));
            TaskDone();
            return true;
        }

        /// <summary>
        /// The task is completed
        /// </summary>
        private void TaskDone()
        {
            EventHandler<TaskDone> handler = TaskComplete;
            handler?.Invoke(this, new TaskDone(totalWorkload));
        }

        /// <inheritdoc>
        public int GetStepCount()
        {
            return totalWorkload;
        }
    }
}
