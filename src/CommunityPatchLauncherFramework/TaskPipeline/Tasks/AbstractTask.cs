using CommunityPatchLauncherFramework.Settings.Container;
using CommunityPatchLauncherFramework.Settings.Manager;
using System.Collections.Generic;
using System.Linq;

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

        /// <inheritdoc/>
        public HashSet<SettingPair> Settings => settings;

        /// <summary>
        /// private access to the settings of the previous tasks
        /// </summary>
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

        /// <summary>
        /// Get the setting pair
        /// </summary>
        /// <param name="key">They key to search for</param>
        /// <returns>The setting pair or null</returns>
        public SettingPair GetSetting(string key)
        {
            IReadOnlyList<SettingPair> matchingSettings = settings.Where((obj) => obj.Key == key).ToList();
            if (matchingSettings.Count == 0)
            {
                return default;
            }
            return matchingSettings.First();
        }

        /// <summary>
        /// Get the setting of a given type
        /// </summary>
        /// <typeparam name="T">The type of setting to get</typeparam>
        /// <param name="key">The key to search for</param>
        /// <returns>The setting value as given type</returns>
        public T GetSetting<T>(string key)
        {
            SettingPair pair = GetSetting(key);
            if (pair == null)
            {
                return default;
            }

            return pair.GetValue<T>();
        }

        /// <inheritdoc/>
        public abstract bool Execute(bool previousTaskState);
    }
}
