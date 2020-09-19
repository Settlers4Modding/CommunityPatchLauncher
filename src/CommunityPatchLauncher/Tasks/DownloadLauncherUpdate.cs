using CommunityPatchLauncherFramework.Update;
using System;
using System.IO;
using System.Net;

namespace CommunityPatchLauncher.Tasks
{
    internal class DownloadLauncherUpdate : DownloadFileTask
    {
        public override bool Execute(bool previousTaskState)
        {
            Version localVersion = GetSetting<Version>("LocalVersion");
            Version remoteVersion = GetSetting<Version>("RemoteVersion");
            string downloadTarget = Path.GetTempPath() + "SIVLauncherUpdate.zip";
            ArtifactRelease latestArtifact = GetSetting<ArtifactRelease>("LatestArtifact");
            if (latestArtifact == null || latestArtifact.Artifacts.Count == 0)
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
            AddSetting<string>("LauncherUpdate", downloadTarget);
            return true;
        }

    }
}
