using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System;

namespace CommunityPatchLauncher.Tasks.Update
{
    /// <summary>
    /// This class will get the develop version from the local settings
    /// </summary>
    internal class GetLocalVersionFromSettings : AbstractTask
    {
        /// <inheritdoc/>
        public override bool Execute(bool previousTaskState)
        {
            string launcherVersionString = settingManager.GetValue<string>("LauncherVersion");
            Version launcherVersion = launcherVersionString == null ? new Version(0, 0, 0) : new Version(launcherVersionString);
            AddSetting("LocalVersion", launcherVersion);
            AddSetting("IsDevelopment", true);
            return true;
        }
    }
}
