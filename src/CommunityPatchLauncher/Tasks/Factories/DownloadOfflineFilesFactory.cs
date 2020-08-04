using CommunityPatchLauncherFramework.TaskPipeline.Factory;
using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPatchLauncher.Tasks.Factories
{
    internal class DownloadOfflineFilesFactory : ITaskFactory
    {
        private readonly string versionInformationUrl;

        public DownloadOfflineFilesFactory(string versionInformationUrl)
        {
            this.versionInformationUrl = versionInformationUrl;
        }

        public List<ITask> GetTasks()
        {
            List<ITask> returnList = new List<ITask>();
            returnList.Add(new DownloadVersionInformation(versionInformationUrl));
            returnList.Add(new DownloadAllPatches());
            return returnList;
        }
    }
}
