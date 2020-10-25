using Octokit;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CommunityPatchLauncherFramework.Update
{
    /// <summary>
    /// This class represents a release artifact
    /// </summary>
    public class ArtifactRelease
    {
        /// <summary>
        /// The author of the artifact
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// The summary for this artifact
        /// </summary>
        public string Summary { get; }

        /// <summary>
        /// The description of the artifac
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The tag of the artifact
        /// </summary>
        public string Tag { get; }

        /// <summary>
        /// The version of the artifact
        /// </summary>
        public Version Version { get; }

        /// <summary>
        /// The build artifacts contained in the release
        /// </summary>
        public List<Artifact> Artifacts { get; }

        /// <summary>
        /// Create a new instance of this
        /// </summary>
        /// <param name="release">The release to use</param>
        /// <param name="versionRegex">The regex to use to get the version</param>
        public ArtifactRelease(Release release, Regex versionRegex)
        {
            Artifacts = new List<Artifact>();
            Author = release.Author.Login;
            Summary = release.Name;
            Description = release.Body;
            Tag = release.TagName;
            Version = GetVersion(versionRegex);
            CreateArtifacts(release.Assets);
        }

        /// <summary>
        /// Get the version from the tag
        /// </summary>
        /// <param name="versionRegex">The regex to use to get the version</param>
        /// <returns></returns>
        private Version GetVersion(Regex versionRegex)
        {
            MatchCollection matches = versionRegex.Matches(Tag);
            if (matches.Count == 0 || matches[0].Groups.Count != 2)
            {
                return null;
            }
            string version = matches[0].Groups[1].Value;
            int intVersion;
            if (int.TryParse(version, out intVersion))
            {
                version += ".0.0";
            }
            return new Version(version);
        }

        /// <summary>
        /// Create all the artifacts
        /// </summary>
        /// <param name="releaseAssets">A list with all OctoKit artifacts</param>
        private void CreateArtifacts(IReadOnlyList<ReleaseAsset> releaseAssets)
        {
            foreach (ReleaseAsset asset in releaseAssets)
            {
                Artifacts.Add(new Artifact(asset));
            }
        }
    }
}
