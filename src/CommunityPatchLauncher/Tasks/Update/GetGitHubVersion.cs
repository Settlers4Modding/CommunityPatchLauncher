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
    /// <summary>
    /// Get the version from GitHub
    /// </summary>
    public class GetGitHubVersion : AbstractTask
    {
        /// <summary>
        /// Name of the repository owner
        /// </summary>
        private readonly string repositoryOwner;

        /// <summary>
        /// Filter to use to get the version
        /// </summary>
        private readonly Regex filter;

        /// <summary>
        /// The warning popup to use
        /// </summary>
        private readonly IDataCommand warningPopup;

        /// <summary>
        /// The name of the repository
        /// </summary>
        public string RepositoryName { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="repositoryOwner">The name of the repository owner</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <param name="filter">Regex filter to use</param>
        /// <param name="parentWindow">The parent window used for</param>
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

        /// <inheritdoc/>
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
                warningPopup?.Execute(Properties.Resources.Dialog_GetRemoteNoBuildsFound);
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
