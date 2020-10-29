using CommunityPatchLauncher.Tasks.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;
using CommunityPatchLauncherFramework.TaskPipeline.Factory;
using CommunityPatchLauncherFramework.TaskPipeline.Pipeline;
using System.Threading.Tasks;

namespace CommunityPatchLauncher.Commands.TaskCommands
{
    /// <summary>
    /// This command will allow you to download all the patches
    /// </summary>
    internal class DownloadAllPatchesCommand : BaseProgressCommand
    {
        /// <summary>
        /// The worker to use to do the tasks
        /// </summary>
        private readonly QueueWorker worker;

        /// <summary>
        /// Create a new instance of this command
        /// </summary>
        /// <param name="settingManager">The setting manager to use</param>
        public DownloadAllPatchesCommand(SettingManager settingManager)
        {
            worker = new QueueWorker(settingManager);
            worker.ProgressChanged += (sender, data) =>
            {
                TriggerProgressChanged(data);
            };
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return worker != null;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            ITaskFactory taskFactory = new DownloadOfflineFilesFactory(Properties.Settings.Default.VersionInformation);
            Task<bool> startTask = worker.AsyncExecuteTasks(taskFactory);
            startTask.ContinueWith((result) =>
            {
                ExecutionDone();
            });
        }
    }
}
