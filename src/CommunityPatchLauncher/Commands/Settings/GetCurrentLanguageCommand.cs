using CommunityPatchLauncherFramework.Settings.Container;
using CommunityPatchLauncherFramework.Settings.Manager;
using System;

namespace CommunityPatchLauncher.Commands.Settings
{
    /// <summary>
    /// This command will return you the current language of the application
    /// </summary>
    [Obsolete]
    internal class GetCurrentLanguageCommand : BaseDataCommand
    {
        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return parameter is SettingManager;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (parameter is SettingManager manager)
            {
                SettingPair language = manager.GetValue("Language");
                if (language != null)
                {
                    data = language.GetValue<string>();
                }
            }
            ExecutionDone();
        }
    }
}
