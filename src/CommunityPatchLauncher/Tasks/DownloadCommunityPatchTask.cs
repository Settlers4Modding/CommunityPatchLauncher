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
        /// Create a new instance of this class and reset it to the vanilla version
        /// </summary>
        public DownloadCommunityPatchTask() : this(AvailablePatches.HistoryEdition)
        {

        }

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
            abortOnError = false;
        }

        /// <inheritdoc/>
        public override bool Execute(bool previousTaskState)
        {
            Version remoteVersion = GetRemoteVersion(communityPatchType);
            Version localVersion = GetLocalVersion(communityPatchType);

            string downloadPath = GetDownloadString(communityPatchType);
            string fileEnd = downloadPath == string.Empty ? "7z" : downloadPath.Split('.').Last();
            string extension = "." + fileEnd;

            string targetFileName = GetDownloadPath() + "\\" + communityPatchType + extension;

            settings.Add(new SettingPair("PatchInstaller", targetFileName));
            if (remoteVersion == null)
            {
                return false;
            }
            url = new Uri(downloadPath);
            settingManager.AddValue(GetPatchVersion(communityPatchType), remoteVersion.ToString());
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
        /// Get the patch version string for searching
        /// </summary>
        /// <param name="communityPatch">The name of the patch to get the uri from</param>
        /// <returns>The string to search for in the settings</returns>
        protected string GetPatchVersion(string communityPatch)
        {
            return communityPatch + "/" + "Version";
        }

        /// <summary>
        /// Get the local version for a given patch
        /// </summary>
        /// <param name="communityPatch">The name of the patch to get the uri from</param>
        /// <returns>The local version</returns>
        protected Version GetLocalVersion(string communityPatch)
        {
            string localPatchVersion = settingManager.GetValue<string>(GetPatchVersion(communityPatch)) ?? "0.0.0.0";
            return new Version(localPatchVersion);
        }

        /// <summary>
        /// Get the version of the remote patch
        /// </summary>
        /// <param name="communityPatch">The name of the patch to get the uri from</param>
        /// <returns>The remote version</returns>
        protected Version GetRemoteVersion(string communityPatch)
        {
            string version = GetSetting<string>(GetPatchVersion(communityPatch));
            if (version == null)
            {
                return null;
            }
            if (!version.Contains("."))
            {
                int outValue = 0;
                if (int.TryParse(version, out outValue))
                {
                    version += ".0";
                }
            }
            return new Version(version);
        }

        /// <summary>
        /// Get the string where to download
        /// </summary>
        /// <param name="communityPatch">The name of the patch to get the uri from</param>
        /// <returns>The string to download the file from</returns>
        protected string GetDownloadString(string communityPatch)
        {
            string downloadPath = GetSetting<string>(communityPatchType + "/" + "URI");
            return downloadPath ?? string.Empty;
        }

        /// <summary>
        /// Get the path to download the file into
        /// </summary>
        /// <returns>The download path for new installations</returns>
        protected string GetDownloadPath()
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
