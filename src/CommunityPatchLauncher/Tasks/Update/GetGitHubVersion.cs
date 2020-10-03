using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Commands.ApplicationWindow;
using CommunityPatchLauncher.UserControls;
using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using CommunityPatchLauncherFramework.Update;
using FontAwesome.WPF;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace CommunityPatchLauncher.Tasks.Update
{
    public class GetGitHubVersion : AbstractTask
    {
        private readonly string repositoryOwner;
        private readonly Regex filter;

        private readonly IDataCommand warningPopup;

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

        public string RepositoryName { get; }

        public GetGitHubVersion(
            string repositoryOwner,
            string repositoryName,
            string filter,
            Window parentWindow
            )
        {
            this.repositoryOwner = repositoryOwner;
            RepositoryName = repositoryName;
            try
            {
                this.filter = new Regex(filter);
            }
            catch (Exception)
            {
                //Everything is okay the execution will just fail
            }

            warningPopup = new OpenCustomPopupWindowCommand(parentWindow, FontAwesomeIcon.Exclamation, Properties.Resources.Dialog_UpdateProblemTitle, new InfoPopup());
        }

        public override bool Execute(bool previousTaskState)
        {
            GitHubClient client = new GitHubClient(new ProductHeaderValue(repositoryOwner));
            Task<IReadOnlyList<Release>> releaseTask;
            try
            {
                releaseTask = client.Repository.Release.GetAll(repositoryOwner, RepositoryName);
                releaseTask.Wait(4000);
            }
            catch (Exception ex)
            {
                warningPopup?.Execute(Properties.Resources.Dialog_GetRemoteVersionError.Replace("{exception}", ex.Message));
                return false;
            }

            if (!releaseTask.IsCompleted && filter != null)
            {
                warningPopup?.Execute(Properties.Resources.Dialog_GetRemoteTimeout);
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
            foreach (Release release in releases)
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
