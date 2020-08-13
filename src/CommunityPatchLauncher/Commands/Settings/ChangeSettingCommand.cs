using CommunityPatchLauncherFramework.Settings.Container;
using CommunityPatchLauncherFramework.Settings.Manager;
using System;

namespace CommunityPatchLauncher.Commands.Settings
{
    internal class ChangeSettingCommand : BaseCommand
    {
        private readonly SettingManager settingManager;
        private readonly string settingName;
        private readonly bool forceReload;
        private readonly Type settingType;

        public ChangeSettingCommand(SettingManager settingManager, string settingName, bool forceReload)
        {
            this.settingManager = settingManager;
            this.settingName = settingName;
            this.forceReload = forceReload;
            SettingPair pair = settingManager.GetValue(settingName);
            if (pair == null)
            {
                return;
            }
            settingType = pair.ValueType;
        }

        public override bool CanExecute(object parameter)
        {
            return settingType != null && parameter.GetType() == settingType;
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            object value = Convert.ChangeType(parameter, settingType);
            if (forceReload)
            {
                settingManager.Reload();
            }
            settingManager.AddValue(settingName, value);
        }
    }
}
