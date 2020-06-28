using CommunityPatchLauncherFramework.Settings.Factories;

namespace CommunityPatchLauncher.Commands
{
    /// <summary>
    /// Get the settings manager
    /// </summary>
    internal class GetSettingManagerCommand : BaseDataCommand
    {
        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            ISettingFactory factory = new XmlSettingFactory();
            data = factory.GetSettingsManager();
            ExecutionDone();
        }
    }
}
