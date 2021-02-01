using CommunityPatchLauncher.Commands.DataContainer;
using CommunityPatchLauncherFramework.Settings.Manager;

namespace CommunityPatchLauncher.Commands.Settings
{
    /// <summary>
    /// This command will save the window size
    /// </summary>
    internal class SaveWindowSizeCommand : BaseCommand
    {
        /// <summary>
        /// The setting manager to use for saving
        /// </summary>
        private readonly SettingManager settingManager;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="settingManager">Setting manager to use for saving</param>
        public SaveWindowSizeCommand(SettingManager settingManager)
        {
            this.settingManager = settingManager;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return settingManager != null && parameter is ResizeWindowData;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }
            if (parameter is ResizeWindowData resizeWindowData)
            {
                settingManager.AddValue("Width", resizeWindowData.Width);
                settingManager.AddValue("Height", resizeWindowData.Height);
                settingManager.AddValue("CustomWindowSize", resizeWindowData.Custom);
                settingManager.SaveSettings();
            }

        }
    }
}
