using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Enums;
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
        private readonly bool showIfLocalIsNewer;
        private readonly QueueWorker worker;

        public UpdateApplicationCommand(SettingManager settingManager, Window parentWindow) : this(settingManager, parentWindow, false)
        {
        }

        public UpdateApplicationCommand(SettingManager settingManager, Window parentWindow, bool showIfLocalIsNewer)
        {
            worker = new QueueWorker(settingManager);
            this.settingManager = settingManager;
            this.parentWindow = parentWindow;
            this.showIfLocalIsNewer = showIfLocalIsNewer;
        }

        public override bool CanExecute(object parameter)
        {
            return settingManager != null && worker != null && parameter is UpdateChannelContainer;
        }

        public override void Execute(object parameter)
        {
            worker.ExecuteTasks(new UpdateClientFactory(((UpdateChannelContainer)parameter).UpdateBranch, parentWindow, showIfLocalIsNewer));
        }
    }
}
