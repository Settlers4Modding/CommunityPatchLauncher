using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Tasks.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;
using CommunityPatchLauncherFramework.TaskPipeline.Pipeline;
using System.Windows;

namespace CommunityPatchLauncher.Commands.ApplicationWindow
{
    /// <summary>
    /// This command will update the application
    /// </summary>
    internal class UpdateApplicationCommand : BaseCommand
    {
        /// <summary>
        /// The settings manager to use
        /// </summary>
        private readonly SettingManager settingManager;

        /// <summary>
        /// The parent window which called this command
        /// </summary>
        private readonly Window parentWindow;

        /// <summary>
        /// Should we show if the local version is newer
        /// </summary>
        private readonly bool showIfLocalIsNewer;

        /// <summary>
        /// The worker to use
        /// </summary>
        private readonly QueueWorker worker;

        /// <summary>
        /// Create a new instance of this command
        /// </summary>
        /// <param name="settingManager">The settings manager to use</param>
        /// <param name="parentWindow">The parent window</param>
        public UpdateApplicationCommand(SettingManager settingManager, Window parentWindow) : this(settingManager, parentWindow, false)
        {
        }


        /// <summary>
        /// Create a new instance of this command
        /// </summary>
        /// <param name="settingManager">The settings manager to use</param>
        /// <param name="parentWindow">The parent window</param>
        /// <param name="showIfLocalIsNewer">Show if the local version is newer</param>
        public UpdateApplicationCommand(SettingManager settingManager, Window parentWindow, bool showIfLocalIsNewer)
        {
            worker = new QueueWorker(settingManager);
            this.settingManager = settingManager;
            this.parentWindow = parentWindow;
            this.showIfLocalIsNewer = showIfLocalIsNewer;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return settingManager != null && worker != null && parameter is UpdateChannelContainer;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            worker.ExecuteTasks(new UpdateClientFactory(((UpdateChannelContainer)parameter).UpdateBranch, parentWindow, showIfLocalIsNewer));
        }
    }
}
