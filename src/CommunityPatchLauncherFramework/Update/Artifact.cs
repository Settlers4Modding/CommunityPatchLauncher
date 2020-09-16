using Octokit;

namespace CommunityPatchLauncherFramework.Update
{
    public class Artifact
    {
        public string Name { get; }
        public string DownloadUrl { get; }

        public Artifact(ReleaseAsset releaseAsset)
        {
            Name = releaseAsset.Name;
            DownloadUrl = releaseAsset.BrowserDownloadUrl;
        }
    }
}
