using CommunityPatchLauncher.Commands.DataContainer;
using CommunityPatchLauncher.Settings.Factories;
using CommunityPatchLauncher.Tasks.Factories;
using CommunityPatchLauncherFramework.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;
using CommunityPatchLauncherFramework.TaskPipeline.Factory;
using CommunityPatchLauncherFramework.TaskPipeline.Pipeline;
using System.Threading.Tasks;

namespace CommunityPatchLauncher.Commands.TaskCommands
{
    /// <summary>
    /// This command will launch the game
    /// </summary>
    internal class LaunchGameCommand : BaseDataCommand
    {
        /// <summary>
        /// The setting manager to use to read the version information from
        /// </summary>
        private readonly SettingManager manager;

        /// <summary>
        /// The setting manager to use for launching
        /// </summary>
        private readonly SettingManager settingManager;

        /// <summary>
        /// Create a new instance of this command
        /// </summary>
        /// <param name="manager">The setting manager to use</param>
        public LaunchGameCommand(SettingManager manager)
        {
            ISettingFactory settingFactory = new WpfPropertySettingManagerFactory();
            this.manager = settingFactory.GetSettingsManager();
            settingManager = manager;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return parameter is LaunchGameData;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (parameter is LaunchGameData gameData)
            {
                ITaskFactory taskFactory = new LaunchGameFactory(
                    manager.GetValue<string>("VersionInformation"),
                    gameData.Patch,
                    gameData.Speed
                    );
                QueueWorker worker = new QueueWorker(settingManager);
                Task<bool> startTask = worker.AsyncExecuteTasks(taskFactory);
            }
        }
    }
}
