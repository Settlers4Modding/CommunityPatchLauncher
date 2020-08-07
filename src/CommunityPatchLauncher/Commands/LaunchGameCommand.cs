using CommunityPatchLauncher.CommandDataContainer;
using CommunityPatchLauncher.Settings.Factories;
using CommunityPatchLauncher.Tasks.Factories;
using CommunityPatchLauncherFramework.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;
using CommunityPatchLauncherFramework.TaskPipeline.Factory;
using System;
using System.Windows.Input;

namespace CommunityPatchLauncher.Commands
{
    internal class LaunchGameCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly SettingManager manager;

        public LaunchGameCommand()
        {
            ISettingFactory settingFactory = new WpfPropertySettingManagerFactory();
            manager = settingFactory.GetSettingsManager();
        }

        public bool CanExecute(object parameter)
        {
            return parameter is LaunchGameData;
        }

        public void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            if (parameter is LaunchGameData gameData)
            {
                ITaskFactory taskFactory = new LaunchGameFactory(manager.GetValue<string>("VersionInformation"), gameData.Patch, gameData.Speed);
            }


        }
    }
}
