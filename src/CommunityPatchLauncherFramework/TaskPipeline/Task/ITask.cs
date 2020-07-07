using CommunityPatchLauncherFramework.Settings.Manager;

namespace CommunityPatchLauncherFramework.TaskPipeline.Task
{
    /// <summary>
    /// This interface will define a task to run
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// The task worker should abort if there was an error
        /// </summary>
        bool AbortOnError { get; }

        /// <summary>
        /// Set the settings manager
        /// </summary>
        /// <param name="settingManager">The settings manager to use</param>
        void Init(SettingManager settingManager);

        /// <summary>
        /// Execute this task
        /// </summary>
        /// <param name="previousTaskState">The state of the task before</param>
        /// <returns>True if the task was successful completed</returns>
        bool Execute(bool previousTaskState);
    }
}
