using CommunityPatchLauncher.Tasks.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;
using CommunityPatchLauncherFramework.TaskPipeline.Pipeline;
using System.Windows;

namespace CommunityPatchLauncher.Commands.ApplicationWindow
{
    internal class UpdateApplicationCommand : BaseCommand
    {
        private readonly SettingManager settingManager;
        private readonly Window parentWindow;
        private readonly QueueWorker worker;

        public UpdateApplicationCommand(SettingManager settingManager, Window parentWindow)
        {
            worker = new QueueWorker(settingManager);
            this.settingManager = settingManager;
            this.parentWindow = parentWindow;
        }

        public override bool CanExecute(object parameter)
        {
            return settingManager != null && worker != null;
        }

        public override void Execute(object parameter)
        {
            worker.ExecuteTasks(new UpdateClientFactory(Enums.UpdateBranchEnum.Develop, parentWindow));
        }
    }
}
