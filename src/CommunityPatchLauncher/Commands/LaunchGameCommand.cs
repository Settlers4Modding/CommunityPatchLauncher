using CommunityPatchLauncher.Commands.DataContainer;
using CommunityPatchLauncher.Settings.Factories;
using CommunityPatchLauncher.Tasks.Factories;
using CommunityPatchLauncherFramework.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;
using CommunityPatchLauncherFramework.TaskPipeline.Factory;
using CommunityPatchLauncherFramework.TaskPipeline.Pipeline;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommunityPatchLauncher.Commands
{
    internal class LaunchGameCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly SettingManager manager;
        private readonly SettingManager settingManager;

        public LaunchGameCommand(SettingManager manager)
        {
            ISettingFactory settingFactory = new WpfPropertySettingManagerFactory();
            this.manager = settingFactory.GetSettingsManager();
            settingManager = manager;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is LaunchGameData gameData)
            {
                ITaskFactory taskFactory = new LaunchGameFactory(manager.GetValue<string>("VersionInformation"), gameData.Patch, gameData.Speed);
                QueueWorker worker = new QueueWorker(settingManager);
                Task<bool> startTask = worker.AsyncExecuteTasks(taskFactory);
            }


        }
    }
}
