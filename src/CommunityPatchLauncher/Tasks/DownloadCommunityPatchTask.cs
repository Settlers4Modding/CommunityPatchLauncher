using CommunityPatchLauncher.Enums;
using CommunityPatchLauncherFramework.Settings.Container;
using System;
using System.IO;
using System.Linq;

namespace CommunityPatchLauncher.Tasks
{
    /// <summary>
    /// Download the community patch
    /// </summary>
    internal class DownloadCommunityPatchTask : DownloadFileTask
    {
        /// <summary>
        /// The type of patch to download
        /// </summary>
        private readonly string communityPatchType;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="patch">The community patch type to download</param>
        public DownloadCommunityPatchTask(AvailablePatches patch) : this(patch.ToString())
        {
        }

        /// <summary>
        /// Create a new instance of this clss
        /// </summary>
        /// <param name="communityPatchType">The community patch type to download</param>
        public DownloadCommunityPatchTask(string communityPatchType)
        {
            this.communityPatchType = communityPatchType;
        }

        /// <inheritdoc/>
        public override bool Execute(bool previousTaskState)
        {
            string patchVersion = communityPatchType + "/" + "Version";
            string version = GetSetting<string>(patchVersion);
            if (version == null)
            {
                return false;
            }
            Version remoteVersion = new Version(version);
            string localPatchVersion = settingManager.GetValue<string>(patchVersion) ?? "0.0.0.0";
            Version localVersion = new Version(localPatchVersion);

            string downloadPath = GetSetting<string>(communityPatchType + "/" + "URI");
            if (downloadPath == null)
            {
                return false;
            }
            string extension = "." + downloadPath.Split('.').Last();

            url = new Uri(downloadPath);
            string targetFileName = getDownloadPath() + "\\" + communityPatchType + extension;

            settings.Add(new SettingPair("PatchInstaller", targetFileName));
            settingManager.AddValue(patchVersion, remoteVersion.ToString());
            if (!File.Exists(targetFileName) || localVersion < remoteVersion)
            {
                if (File.Exists(targetFileName))
                {
                    File.Delete(targetFileName);
                }
                DownloadFile(targetFileName);
            }
            TaskDone();
            settingManager.SaveSettings();
            return true;
        }

        /// <summary>
        /// Get the path to download the file into
        /// </summary>
        /// <returns>The download path for new installations</returns>
        private string getDownloadPath()
        {
            string downloadFolder = settingManager.GetValue<string>("DownloadFolder");
            if (downloadFolder == null || downloadFolder == "" || !Directory.Exists(downloadFolder))
            {
                FileInfo file = new FileInfo(settingManager.SettingFilePath);
                downloadFolder = file.DirectoryName;
                downloadFolder += "\\Downloads";
                if (!Directory.Exists(downloadFolder))
                {
                    Directory.CreateDirectory(downloadFolder);
                }
                settingManager.AddValue("DownloadFolder", downloadFolder);
            }

            return downloadFolder;
        }
    }
}
