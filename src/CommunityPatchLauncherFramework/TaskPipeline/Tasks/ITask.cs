using CommunityPatchLauncherFramework.Settings.Container;
using CommunityPatchLauncherFramework.Settings.Manager;
using System.Collections.Generic;

namespace CommunityPatchLauncherFramework.TaskPipeline.Tasks
{
    /// <summary>
    /// This interface will define a task to run
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// Settings and data of the previous tasks
        /// </summary>
        HashSet<SettingPair> Settings { get; }

        /// <summary>
        /// The task worker should abort if there was an error
        /// </summary>
        bool AbortOnError { get; }

        /// <summary>
        /// Set the settings manager
        /// </summary>
        /// <param name="settingManager">The settings manager to use</param>
        void Init(SettingManager settingManager, HashSet<SettingPair> taskSettings);

        /// <summary>
        /// Execute this task
        /// </summary>
        /// <param name="previousTaskState">The state of the task before</param>
        /// <returns>True if the task was successful completed</returns>
        bool Execute(bool previousTaskState);
    }
}
