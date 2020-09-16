using CommunityPatchLauncher.Enums;
using CommunityPatchLauncher.Settings.Factories;
using CommunityPatchLauncher.Tasks.Update;
using CommunityPatchLauncherFramework.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;
using CommunityPatchLauncherFramework.TaskPipeline.Factory;
using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using CommunityPatchLauncherFramework.TaskPipeline.Tasks.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPatchLauncher.Tasks.Factories
{
    public class UpdateClientFactory : ITaskFactory
    {
        private readonly SettingManager manager;
        private readonly UpdateBranchEnum updateBranch;

        public UpdateClientFactory(UpdateBranchEnum updateBranch)
        {
            ISettingFactory factory = new WpfPropertySettingManagerFactory();
            manager = factory.GetSettingsManager();
            this.updateBranch = updateBranch;
        }

        public List<ITask> GetTasks()
        {
            List<ITask> tasks = new List<ITask>();
            tasks.Add(new GetLocalVersion());
            string settingToLoad = "ReleaseFilter";
            if (updateBranch == UpdateBranchEnum.Develop)
            {
                settingToLoad = "DevelopFilter";
            }
            string repositoryOwner = manager.GetValue<string>("ReporistoryOwner");
            string repositoryName = manager.GetValue<string>("RepositoryName");
            string releaseFilter = manager.GetValue<string>(settingToLoad);
            tasks.Add(new GetGitHubVersion(repositoryOwner, repositoryName, releaseFilter));
            return tasks;
        }
    }
}
