using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System;

namespace CommunityPatchLauncher.Tasks.Update
{
    /// <summary>
    /// Write the last patched version to the settings
    /// </summary>
    class WriteCurrentVersionToSettings : AbstractTask
    {
        /// <inheritdoc/>
        public override bool Execute(bool previousTaskState)
        {
            Version remoteVersion = GetSetting<Version>("RemoteVersion");
            settingManager.AddValue("LauncherVersion", remoteVersion.ToString());
            settingManager.SaveSettings();
            return true;
        }
    }
}
