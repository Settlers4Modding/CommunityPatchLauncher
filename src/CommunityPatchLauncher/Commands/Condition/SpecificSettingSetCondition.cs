using CommunityPatchLauncherFramework.Settings.Container;
using CommunityPatchLauncherFramework.Settings.Manager;

namespace CommunityPatchLauncher.Commands.Condition
{
    internal class SpecificSettingSetCondition : BaseCondition
    {
        /// <summary>
        /// The settings manager to use
        /// </summary>
        private readonly SettingManager settingManager;

        /// <summary>
        /// The setting to check
        /// </summary>
        private readonly string settingToCheck;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="settingManager">The setting manager to use</param>
        /// <param name="settingToCheck">The setting to check</param>
        public SpecificSettingSetCondition(SettingManager settingManager, string settingToCheck)
        {
            this.settingManager = settingManager;
            this.settingToCheck = settingToCheck;
        }
        
        /// <inheritdoc/>
        public override bool ConditionFullfilled(object parameter)
        {
            SettingPair pair = settingManager.GetValue(settingToCheck);
            if (pair == null)
            {
                return false;
            }

            if (pair.ValueType == typeof(bool))
            {
                return pair.GetValue<bool>();
            }

            return false;
        }
    }
}
