using CommunityPatchLauncherFramework.TaskPipeline.Factory;
using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System.Collections.Generic;

namespace CommunityPatchLauncher.Tasks.Factories
{
    /// <summary>
    /// This task queue will download all the patches
    /// </summary>
    internal class DownloadOfflineFilesFactory : ITaskFactory
    {
        /// <summary>
        /// The url to get the version information from
        /// </summary>
        private readonly string versionInformationUrl;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="versionInformationUrl">The url to get the version information from</param>
        public DownloadOfflineFilesFactory(string versionInformationUrl)
        {
            this.versionInformationUrl = versionInformationUrl;
        }

        /// <inheritdoc/>
        public List<ITask> GetTasks()
        {
            List<ITask> returnList = new List<ITask>();
            returnList.Add(new DownloadVersionInformation(versionInformationUrl));
            returnList.Add(new DownloadAllPatches());
            return returnList;
        }
    }
}
