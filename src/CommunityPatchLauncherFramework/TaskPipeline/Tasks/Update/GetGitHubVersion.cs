using CommunityPatchLauncherFramework.Update;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommunityPatchLauncherFramework.TaskPipeline.Tasks.Update
{
    public class GetGitHubVersion : AbstractTask
    {
        private readonly string repositoryOwner;
        private readonly Regex filter;

        public GetGitHubVersion(
            string repositoryOwner,
            string repositoryName,
            Regex filter
            )
        {
            this.repositoryOwner = repositoryOwner;
            RepositoryName = repositoryName;
            this.filter = filter;
        }

        public GetGitHubVersion(
            string repositoryOwner,
            string repositoryName,
            string filter
            )
        {
            this.repositoryOwner = repositoryOwner;
            this.RepositoryName = repositoryName;
            try
            {
                this.filter = new Regex(filter);
            }
            catch (Exception)
            {
                //Everything is okay the execution will just fail
            }

            return;
        }

        public string RepositoryName { get; }

        public override bool Execute(bool previousTaskState)
        {
            GitHubClient client = new GitHubClient(new ProductHeaderValue(repositoryOwner));
            Task<IReadOnlyList<Release>> releaseTask = client.Repository.Release.GetAll(repositoryOwner, RepositoryName);
            releaseTask.Wait(4000);
            if (!releaseTask.IsCompleted && filter != null)
            {
                return false;
            }
            IReadOnlyList<Release> releases = releaseTask.Result;
            releases = releases.Where(obj =>
            {
                return filter.IsMatch(obj.TagName);
            }).ToList();

            if (releases.Count == 0)
            {
                return false;
            }

            List<ArtifactRelease> artifactReleases = new List<ArtifactRelease>();
            foreach(Release release in releases)
            {
                artifactReleases.Add(new ArtifactRelease(release, filter));
            }
            artifactReleases.Sort((release1, release2) => release2.Version.CompareTo(release1.Version));
            ArtifactRelease newestArtifact = artifactReleases.First();

            AddSetting("RemoteVersion", newestArtifact.Version);
            AddSetting("LatestArtifact", newestArtifact);

            return true;
        }
    }
}
