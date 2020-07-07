using CommunityPatchLauncherFramework.Settings.Manager;

namespace CommunityPatchLauncherFramework.TaskPipeline.Task
{
    /// <summary>
    /// This abstract class will help you to create tasks on your own
    /// </summary>
    public abstract class AbstractTask : ITask
    {
        /// <inheritdoc/>
        public bool AbortOnError => abortOnError;

        /// <summary>
        /// The private readonly abort error bool
        /// </summary>
        protected readonly bool abortOnError;

        /// <summary>
        /// The current setting manager
        /// </summary>
        protected SettingManager settingManager;

        /// <summary>
        /// Create a new abstract class for the task interface
        /// </summary>
        public AbstractTask()
        {
            abortOnError = true;
        }

        /// <inheritdoc/>
        public virtual void Init(SettingManager settingManager)
        {
            
        }

        /// <inheritdoc/>
        public abstract bool Execute(bool previousTaskState);
    }
}
