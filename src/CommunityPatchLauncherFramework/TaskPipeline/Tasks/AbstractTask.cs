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
        protected bool abortOnError;

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
        /// Add new settings to the setting pair
        /// </summary>
        /// <param name="key">They key of the setting to add</param>
        /// <param name="value">The value of the setting to add</param>
        public void AddSetting(string key, object value)
        {
            settings?.Add(new SettingPair(key, value));
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

        /// <summary>
        /// This method will add settings to the internal setting library
        /// </summary>
        /// <typeparam name="T">The type of object to add</typeparam>
        /// <param name="key">The key of the setting to add</param>
        /// <param name="data">The data to add</param>
        /// <returns>True if adding was successful</returns>
        public bool AddSetting<T>(string key, T data)
        {
            return AddSetting<T>(key, data, false);
        }

        /// <summary>
        /// This method will add settings to the internal setting library
        /// </summary>
        /// <typeparam name="T">The type of object to add</typeparam>
        /// <param name="key">The key of the setting to add</param>
        /// <param name="data">The data to add</param>
        /// <param name="overrideEntry">Should we override already existing entries</param>
        /// <returns>True if adding was successful</returns>
        public bool AddSetting<T>(string key, T data, bool overrideEntry)
        {
            bool entryExisting = settings?.Where((obj) => obj.Key == key).ToList().Count > 0;
            if (!entryExisting)
            {
                settings?.Add(new SettingPair(key, data));
                return true;
            }
            if (overrideEntry)
            {
                int removed = settings.RemoveWhere((obj) => obj.Key == key);
                return removed > 0 ? AddSetting<T>(key, data) : false;
            }
            return false;
        }

        /// <inheritdoc/>
        public abstract bool Execute(bool previousTaskState);
    }
}
