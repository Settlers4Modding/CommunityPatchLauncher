using CommunityPatchLauncherFramework.Settings.Container;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPatchLauncher.Tasks
{
    public class DownloadCommunityPatchTask : DownloadFileTask
    {
        private readonly string communityPatchType;

        public DownloadCommunityPatchTask(string communityPatchType)
        {
            this.communityPatchType = communityPatchType;
        }

        public override bool Execute(bool previousTaskState)
        {
            string patchVersion = communityPatchType + "/" + "Version";
            SettingPair version = Settings.Where((obj) => obj.Key == patchVersion).First();
            if (version == null)
            {
                return false;
            }
            Version remoteVersion = new Version(version.GetValue<string>());
            string localPatchVersion = settingManager.GetValue<string>(patchVersion) ?? "0.0.0.0";
            Version localVersion = new Version(localPatchVersion);

            if ( localVersion > remoteVersion)
            {
                settings.Add(new SettingPair("UpToDate", true));
                return true;
            }
            settings.Add(new SettingPair("UpToDate", false));

            SettingPair remoteUrl = Settings.Where((obj) => obj.Key == communityPatchType + "/" + "URI").First();
            if (remoteUrl == null)
            {
                return false;
            }
            string downloadPath = remoteUrl.GetValue<string>();
            if (downloadPath == null)
            {
                return false;
            }
            string extension = "." + downloadPath.Split('.').Last();

            url = new Uri(downloadPath);
            string targetFileName = Path.GetTempPath() + Path.GetRandomFileName() + extension;
            settings.Add(new SettingPair("PatchInstaller", targetFileName));
            DownloadFile(targetFileName);
            return true;
        }
    }
}
