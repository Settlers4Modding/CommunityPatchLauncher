using Octokit;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CommunityPatchLauncherFramework.Update
{
    public class ArtifactRelease
    {
        public string Author { get;  }
        public string Summary { get; }
        public string Description { get; }
        public string Tag { get; }
        public Version Version { get; }
        public List<Artifact> Artifacts { get; }

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

        private void CreateArtifacts(IReadOnlyList<ReleaseAsset> releaseAssets)
        {
            foreach (ReleaseAsset asset in releaseAssets)
            {
                Artifacts.Add(new Artifact(asset));
            }
        }
    }
}
