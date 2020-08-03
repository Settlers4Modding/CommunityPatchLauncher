using CommunityPatchLauncherFramework.Settings.Container;
using System;
using System.IO;
using System.Linq;

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
            string targetFileName = getDownloadPath() + "\\" + communityPatchType + extension;

            settings.Add(new SettingPair("PatchInstaller", targetFileName));
            settingManager.AddValue(patchVersion, remoteVersion.ToString());
            if (localVersion < remoteVersion)
            {
                if (File.Exists(targetFileName))
                {
                    File.Delete(targetFileName);
                }
                DownloadFile(targetFileName);
            }
            DownloadDone();
            settingManager.SaveSettings();
            return true;
        }

        private string getDownloadPath()
        {
            string downloadFolder = settingManager.GetValue<string>("downloadFolder");
            if (downloadFolder == null || downloadFolder == "" || !Directory.Exists(downloadFolder))
            {
                FileInfo file = new FileInfo(settingManager.SettingFilePath);
                downloadFolder = file.DirectoryName;
                downloadFolder += "\\Downloads";
                if (!Directory.Exists(downloadFolder))
                {
                    Directory.CreateDirectory(downloadFolder);
                }
                settingManager.AddValue("downloadFolder", downloadFolder);
            }

            return downloadFolder;
        }
    }
}
