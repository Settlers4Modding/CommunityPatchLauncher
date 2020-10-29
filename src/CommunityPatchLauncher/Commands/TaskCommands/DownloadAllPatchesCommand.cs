using CommunityPatchLauncher.Tasks.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;
using CommunityPatchLauncherFramework.TaskPipeline.Factory;
using CommunityPatchLauncherFramework.TaskPipeline.Pipeline;
using System.Threading.Tasks;

namespace CommunityPatchLauncher.Commands.TaskCommands
{
    internal class DownloadAllPatchesCommand : BaseProgressCommand
    {
        private readonly QueueWorker worker;

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
