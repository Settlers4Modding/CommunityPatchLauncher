using CommunityPatchLauncher.Tasks.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;
using CommunityPatchLauncherFramework.TaskPipeline.Pipeline;

namespace CommunityPatchLauncher.Commands.ApplicationWindow
{
    internal class UpdateApplicationCommand : BaseCommand
    {
        private readonly SettingManager settingManager;
        private readonly QueueWorker worker;

        public UpdateApplicationCommand(SettingManager settingManager)
        {
            worker = new QueueWorker(settingManager);
            this.settingManager = settingManager;
        }

        public override bool CanExecute(object parameter)
        {
            return settingManager != null && worker != null;
        }

        public override void Execute(object parameter)
        {
            worker.ExecuteTasks(new UpdateClientFactory(Enums.UpdateBranchEnum.Develop));
        }
    }
}
