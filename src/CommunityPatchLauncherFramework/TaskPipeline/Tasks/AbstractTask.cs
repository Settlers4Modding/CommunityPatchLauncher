using CommunityPatchLauncherFramework.Settings.Container;
using CommunityPatchLauncherFramework.Settings.Manager;
using System.Collections.Generic;

namespace CommunityPatchLauncherFramework.TaskPipeline.Tasks
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

        public HashSet<SettingPair> Settings => settings;
        protected HashSet<SettingPair> settings;

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
        public virtual void Init(SettingManager settingManager, HashSet<SettingPair> settings)
        {
            this.settingManager = settingManager;
            this.settings = settings;
        }

        /// <inheritdoc/>
        public abstract bool Execute(bool previousTaskState);
    }
}
