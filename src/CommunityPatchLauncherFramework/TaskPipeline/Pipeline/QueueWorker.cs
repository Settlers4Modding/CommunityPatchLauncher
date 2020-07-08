using CommunityPatchLauncherFramework.Settings.Manager;
using CommunityPatchLauncherFramework.TaskPipeline.Factory;
using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityPatchLauncherFramework.TaskPipeline.Pipeline
{
    /// <summary>
    /// This class will allow you to run multi tasks in a row
    /// </summary>
    public class QueueWorker
    {
        /// <summary>
        /// The setting manager to use
        /// </summary>
        private readonly SettingManager settingManager;

        /// <summary>
        /// Create a new instance of this task planner
        /// </summary>
        /// <param name="settingManager"></param>
        public QueueWorker(SettingManager settingManager)
        {
            this.settingManager = settingManager;
        }

        /// <summary>
        /// This will create the tasks in the factory and run them
        /// </summary>
        /// <param name="factory">The factory to use</param>
        /// <returns>True if the task execution was successful</returns>
        public bool ExecuteTasks(ITaskFactory factory)
        {
            return ExecuteTasks(factory.GetTasks());
        }

        /// <summary>
        /// This will execute all the tasks in the list
        /// </summary>
        /// <param name="tasks">The tasks to execute</param>
        /// <returns>True if the task execution was successful</returns>
        public bool ExecuteTasks(List<ITask> tasks)
        {
            bool lastTaskState = true;
            foreach (ITask task in tasks)
            {
                task.Init(settingManager);
                lastTaskState = task.Execute(lastTaskState);
                if (lastTaskState == false && task.AbortOnError)
                {
                    break;
                }
            }

            return lastTaskState;
        }

        /// <summary>
        /// Get the complete work load of the tasks to do
        /// </summary>
        /// <param name="tasks">All the tasks to checks</param>
        /// <returns>The total workload to do</returns>
        private int getCompleteWorkLoad(List<ITask> tasks)
        {
            List<ITask> progressTasks = tasks.FindAll((task) => {
                return task is IProgressTask;
            });

            return getCompleteWorkLoad(progressTasks.Cast<IProgressTask>().ToList());
        }

        /// <summary>
        /// Get the complete work load of the tasks to do
        /// </summary>
        /// <param name="tasks">All the tasks to checks</param>
        /// <returns>The total workload to do</returns>
        private int getCompleteWorkLoad(List<IProgressTask> tasks)
        {
            int returnValue = 0;
            foreach (IProgressTask task in tasks)
            {
                returnValue += task.GetStepCount();
            }
            return returnValue;
        }


        /// <summary>
        /// This will create the tasks in the factory and run them async
        /// </summary>
        /// <param name="factory">The factory to use</param>
        /// <returns>True if the task execution was successful</returns>
        public async Task<bool> AsyncExecuteTasks(ITaskFactory factory)
        {
            return await AsyncExecuteTasks(factory.GetTasks());
        }

        /// <summary>
        /// This will execute all the tasks async in the list
        /// </summary>
        /// <param name="tasks">The tasks to execute</param>
        /// <returns>True if the task execution was successful</returns>
        public async Task<bool> AsyncExecuteTasks(List<ITask> tasks)
        {
            Task<bool> runner = new Task<bool>(() => ExecuteTasks(tasks));
            runner.Start();
            return await runner;
        }
    }
}
