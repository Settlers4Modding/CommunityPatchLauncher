using CommunityPatchLauncherFramework.Settings.Container;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CommunityPatchLauncher.Tasks
{
    /// <summary>
    /// This class will download all the files used for offline usage
    /// </summary>
    internal class DownloadAllPatches : DownloadCommunityPatchTask
    {
        /// <summary>
        /// The total workload
        /// </summary>
        private int realTotalWorkload;

        /// <summary>
        /// The current file which is getting downloaded
        /// </summary>
        private int currentFile;

        /// <summary>
        /// Download all the patches from the server
        /// </summary>
        public DownloadAllPatches()
        {
            currentFile = 0;
        }

        /// <inheritdoc/>
        public override bool Execute(bool previousTaskState)
        {
            IReadOnlyList<string> patches = GetPatchList();
            realTotalWorkload = patches.Count * 100;
            foreach (string patch in patches)
            {
                currentFile++;
                Version remoteVersion = GetRemoteVersion(patch);
                if (remoteVersion == null)
                {
                    return false;
                }
                Version localVersion = GetLocalVersion(patch);

                string downloadPath = GetDownloadString(patch);
                string extension = "." + downloadPath.Split('.').Last();

                url = new Uri(downloadPath);
                string targetFileName = GetDownloadPath() + "\\" + patch + extension;
                settings.Add(new SettingPair("PatchInstaller", targetFileName));
                settingManager.AddValue(GetPatchVersion(patch), remoteVersion.ToString());

                if (!File.Exists(targetFileName) || localVersion < remoteVersion)
                {
                    if (File.Exists(targetFileName))
                    {
                        File.Delete(targetFileName);
                    }
                    DownloadFile(targetFileName);
                }
            }
            settingManager.SaveSettings();
            TaskDone();
            return true;
        }

        /// <summary>
        /// Get all the patches to download
        /// </summary>
        /// <returns></returns>
        private IReadOnlyList<string> GetPatchList()
        {
            List<string> returnList = new List<string>();
            foreach (SettingPair pair in Settings)
            {
                string[] currentSplit = pair.Key.Split('/');
                string currentPatch = currentSplit.Length > 1 ? currentSplit[0] : "";
                if (!returnList.Contains(currentPatch) && currentPatch != "")
                {
                    if (currentPatch.ToLower() == "launcher")
                    {
                        continue;
                    }
                    returnList.Add(currentPatch);
                }
            }
            return returnList;
        }

        /// <inheritdoc/>
        protected override void ProgressHasChanged(int currentProgress)
        {
            int maxValue = currentFile * 100;
            int baseValue = maxValue - 100;
            int realProgress = baseValue + currentProgress;
            realProgress = realProgress > maxValue ? maxValue : realProgress;
            realProgress = (int)(realProgress / (float)realTotalWorkload * 100);
            base.ProgressHasChanged(realProgress);
        }
    }
}
