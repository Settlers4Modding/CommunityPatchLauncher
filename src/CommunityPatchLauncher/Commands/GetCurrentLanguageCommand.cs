using CommunityPatchLauncherFramework.Settings.Container;
using CommunityPatchLauncherFramework.Settings.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPatchLauncher.Commands
{
    /// <summary>
    /// This command will return you the current language of the application
    /// </summary>
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
                SettingPair language = manager.GetValue("language");
                if (language != null)
                {
                    data = language.GetValue<string>();
                }
            }
            ExecutionDone();
        }
    }
}
