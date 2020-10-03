using CommunityPatchLauncher.Commands.ApplicationWindow;
using CommunityPatchLauncher.Enums;
using CommunityPatchLauncher.Settings.Factories;
using CommunityPatchLauncher.Tasks.Update;
using CommunityPatchLauncherFramework.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;
using CommunityPatchLauncherFramework.TaskPipeline.Factory;
using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System.Collections.Generic;
using System.Windows;

namespace CommunityPatchLauncher.Tasks.Factories
{
    /// <summary>
    /// This class will create all the tasks required to update the client
    /// </summary>
    public class UpdateClientFactory : ITaskFactory
    {
        /// <summary>
        /// The setting manager to use
        /// </summary>
        private readonly SettingManager manager;

        /// <summary>
        /// The branch to check
        /// </summary>
        private readonly UpdateBranchEnum updateBranch;

        /// <summary>
        /// The parent window the factory was called in
        /// </summary>
        private readonly Window parentWindow;

        /// <summary>
        /// Show if the local version is newer
        /// </summary>
        private readonly bool showIfLocalIsNewer;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="updateBranch">The update branch to use</param>
        /// <param name="parentWindow">The parent window which was calling the factory</param>
        public UpdateClientFactory(UpdateBranchEnum updateBranch, Window parentWindow) : this(updateBranch, parentWindow, false)
        {
        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="updateBranch">The update branch to use</param>
        /// <param name="parentWindow">The parent window which was calling the factory</param>
        /// <param name="showIfLocalIsNewer">Show if local version is newer</param>
        public UpdateClientFactory(UpdateBranchEnum updateBranch, Window parentWindow, bool showIfLocalIsNewer)
        {
            ISettingFactory factory = new WpfPropertySettingManagerFactory();
            manager = factory.GetSettingsManager();
            this.updateBranch = updateBranch;
            this.parentWindow = parentWindow;
            this.showIfLocalIsNewer = showIfLocalIsNewer;
        }

        /// <summary>
        /// Create the task list ready to use
        /// </summary>
        /// <returns>A list with all the tasks</returns>
        public List<ITask> GetTasks()
        {
            List<ITask> tasks = new List<ITask>();
            string settingToLoad = "ReleaseFilter";
            ITask localVersionTask = new GetLocalVersion();
            if (updateBranch == UpdateBranchEnum.Develop)
            {
                settingToLoad = "DevelopFilter";
                localVersionTask = new GetLocalVersionFromSettings();
            }

            string repositoryOwner = manager.GetValue<string>("ReporistoryOwner");
            string repositoryName = manager.GetValue<string>("RepositoryName");
            string releaseFilter = manager.GetValue<string>(settingToLoad);
            string patchLauncher = manager.GetValue<string>("PatchAppName");

            tasks.Add(localVersionTask);
            tasks.Add(new GetGitHubVersion(repositoryOwner, repositoryName, releaseFilter, parentWindow));
            tasks.Add(new UpdatePopupTask(parentWindow, showIfLocalIsNewer));
            tasks.Add(new DownloadLauncherUpdate());
            if (updateBranch == UpdateBranchEnum.Develop)
            {
                tasks.Add(new WriteCurrentVersionToSettings());
            }
            tasks.Add(new PatchLauncher(patchLauncher));
            tasks.Add(new CloseApplicationTask(new CloseApplicationCommand()));
            return tasks;
        }
    }
}
