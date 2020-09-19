using Octokit;
using System;

namespace CommunityPatchLauncherFramework.Update
{
    public class Artifact
    {
        public string Name { get; }
        public string DownloadUrl { get; }
        public Uri DownloadUri { get;  }

        public Artifact(ReleaseAsset releaseAsset)
        {
            Name = releaseAsset.Name;
            DownloadUrl = releaseAsset.BrowserDownloadUrl;
            try
            {
                DownloadUri = new Uri(DownloadUrl);
            }
            catch (Exception)
            {
            }
        }
    }
}
