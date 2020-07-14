using CommunityPatchLauncherFramework.Settings.Container;
using CommunityPatchLauncherFramework.Settings.Manager;
using CommunityPatchLauncherFramework.TaskPipeline.EventData;
using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using SevenZip.Compression.LZMA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPatchLauncher.Tasks
{
    public class UnzipCommunityPatchTask : IProgressTask
    {
        private readonly int workload;
        private SettingManager settingsManager;

        public HashSet<SettingPair> Settings => settings;
        private HashSet<SettingPair> settings;

        public bool AbortOnError => throw new NotImplementedException();

        public event EventHandler<TaskProgressChanged> ProgressChanged;
        public event EventHandler<TaskDone> TaskComplete;

        public UnzipCommunityPatchTask()
        {
            workload = 2;
        }

        public void Init(SettingManager settingManager, HashSet<SettingPair> taskSettings)
        {
            this.settingsManager = settingManager;
            this.settings = taskSettings;
        }

        public bool Execute(bool previousTaskState)
        {
            SettingPair upToDate = Settings.Where((obj) => obj.Key == "UpToDate").First();
            if (upToDate == null)
            {
                return false;
            }
            if (upToDate.GetValue<bool>())
            {
                return true;
            }
            SevenZip.Compression.LZMA.Encoder encoder = new SevenZip.Compression.LZMA.Encoder();
            SettingPair filePath = Settings.Where((obj) => obj.Key == "PatchInstaller").First();
            if (filePath == null)
            {
                return true;
            }
            return true;
        }

        public int GetStepCount()
        {
            return workload;
        }
    }
}
