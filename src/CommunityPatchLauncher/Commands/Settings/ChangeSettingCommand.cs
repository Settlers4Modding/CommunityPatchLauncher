using CommunityPatchLauncherFramework.Settings.Container;
using CommunityPatchLauncherFramework.Settings.Manager;
using System;

namespace CommunityPatchLauncher.Commands.Settings
{
    /// <summary>
    /// This command will change a single setting
    /// </summary>
    internal class ChangeSettingCommand : BaseCommand
    {
        /// <summary>
        /// The setting manager to use
        /// </summary>
        private readonly SettingManager settingManager;

        /// <summary>
        /// The setting name to change
        /// </summary>
        private readonly string settingName;

        /// <summary>
        /// Force reload setting before saving
        /// </summary>
        private readonly bool forceReload;

        /// <summary>
        /// The datatype of the setting
        /// </summary>
        private readonly Type settingType;

        /// <summary>
        /// Create a new instance of this command
        /// </summary>
        /// <param name="settingManager">The settings manager to use</param>
        /// <param name="settingName">The setting name to change</param>
        /// <param name="forceReload">Force reload before saving</param>
        public ChangeSettingCommand(
            SettingManager settingManager,
            string settingName,
            bool forceReload
            )
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

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return settingType != null && parameter != null && parameter.GetType() == settingType;
        }

        /// <inheritdoc/>
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
