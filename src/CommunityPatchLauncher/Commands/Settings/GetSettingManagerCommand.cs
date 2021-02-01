using CommunityPatchLauncherFramework.Settings.Factories;
using System;

namespace CommunityPatchLauncher.Commands.Settings
{
    /// <summary>
    /// Get the settings manager
    /// </summary>
    [Obsolete]
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
