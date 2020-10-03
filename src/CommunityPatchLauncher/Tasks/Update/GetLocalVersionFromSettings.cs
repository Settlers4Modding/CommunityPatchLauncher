using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System;

namespace CommunityPatchLauncher.Tasks.Update
{
    internal class GetLocalVersionFromSettings : AbstractTask
    {
        public override bool Execute(bool previousTaskState)
        {
            string launcherVersionString = settingManager.GetValue<string>("LauncherVersion");
            Version launcherVersion = launcherVersionString == null ? new Version(0, 0, 0) : new Version(launcherVersionString);
            AddSetting("LocalVersion", launcherVersion);
            return true;
        }
    }
}
