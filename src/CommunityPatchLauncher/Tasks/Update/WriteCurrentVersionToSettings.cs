using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPatchLauncher.Tasks.Update
{
    class WriteCurrentVersionToSettings : AbstractTask
    {
        public override bool Execute(bool previousTaskState)
        {
            Version remoteVersion = GetSetting<Version>("RemoteVersion");
            settingManager.AddValue("LauncherVersion", remoteVersion.ToString());
            settingManager.SaveSettings();
            return true;
        }
    }
}
