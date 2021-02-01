﻿using CommunityPatchLauncherFramework.Update;
using System;
using System.IO;
using System.Net;

namespace CommunityPatchLauncher.Tasks.Update
{
    /// <summary>
    /// This class will download the launcher update if needed
    /// </summary>
    internal class DownloadLauncherUpdate : DownloadFileTask
    {
        /// <inheritdoc/>
        public override bool Execute(bool previousTaskState)
        {
            Version localVersion = GetSetting<Version>("LocalVersion");
            Version remoteVersion = GetSetting<Version>("RemoteVersion");
            string downloadTarget = Path.GetTempPath() + "SIVLauncherUpdate.zip";
            ArtifactRelease latestArtifact = GetSetting<ArtifactRelease>("LatestArtifact");
            if (latestArtifact == null || latestArtifact.Artifacts.Count == 0 || localVersion >= remoteVersion)
            {
                return false;
            }
            if (File.Exists(downloadTarget))
            {
                File.Delete(downloadTarget);
            }
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(latestArtifact.Artifacts[0].DownloadUri, downloadTarget);
            }
            AddSetting("LauncherUpdate", downloadTarget);
            return true;
        }

    }
}
