using Octokit;
using System;

namespace CommunityPatchLauncherFramework.Update
{
    /// <summary>
    /// A build artifact
    /// </summary>
    public class Artifact
    {
        /// <summary>
        /// The name of the build artifact
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The download url for this specific artifact
        /// </summary>
        public string DownloadUrl { get; }

        /// <summary>
        /// The download uri for this specific artifact
        /// </summary>
        public Uri DownloadUri { get; }

        /// <summary>
        /// Create a new instance of this artifact from a Octokit artifact
        /// </summary>
        /// <param name="releaseAsset">The release artifact to create this from</param>
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
