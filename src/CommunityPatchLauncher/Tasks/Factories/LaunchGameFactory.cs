using CommunityPatchLauncher.Enums;
using CommunityPatchLauncherFramework.TaskPipeline.Factory;
using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System;
using System.Collections.Generic;

namespace CommunityPatchLauncher.Tasks.Factories
{
    /// <summary>
    /// Create the task list for launching the game
    /// </summary>
    internal class LaunchGameFactory : ITaskFactory
    {
        /// <summary>
        /// The url to get the version information from
        /// </summary>
        private readonly string versionInformationUrl;

        /// <summary>
        /// The speed mode to use
        /// </summary>
        private readonly SpeedModes speedMode;

        /// <summary>
        /// The name of the patch file to get
        /// </summary>
        private readonly AvailablePatches patchName;

        /// <summary>
        /// Create a new factory 
        /// </summary>
        /// <param name="versionInformationUrl">The url to get the version information from</param>
        /// <param name="patch">The name of the patch file to get</param>
        /// <param name="speedMode">The speed mode to use</param>
        public LaunchGameFactory(
            string versionInformationUrl,
            AvailablePatches patch,
            SpeedModes speedMode
            )
        {
            this.versionInformationUrl = versionInformationUrl;
            patchName = patch;
            this.speedMode = speedMode;
        }

        /// <summary>
        /// Create a new factory 
        /// </summary>
        /// <param name="versionInformationUrl">The url to get the version information from</param>
        /// <param name="patch">The name of the patch file to get</param>
        /// <param name="speedMode">The speed mode to use</param>
        public LaunchGameFactory(
            string versionInformationUrl,
            string patch,
            SpeedModes speedMode
            )
            : this(versionInformationUrl, AvailablePatches.HistoryEdition, speedMode)
        {
            Enum.TryParse(patch, out patchName);
        }

        /// <inheritdoc/>
        public List<ITask> GetTasks()
        {
            List<ITask> returnList = new List<ITask>();
            returnList.Add(new DownloadVersionInformation(versionInformationUrl));
            returnList.Add(new DownloadCommunityPatchTask(patchName));
            returnList.Add(new UnzipCommunityPatchTask());
            returnList.Add(new CopyFileToGameFolder());
            returnList.Add(new PatchGameSpeedTask(speedMode));
            returnList.Add(new StartGameTask());
            return returnList;
        }
    }
}
