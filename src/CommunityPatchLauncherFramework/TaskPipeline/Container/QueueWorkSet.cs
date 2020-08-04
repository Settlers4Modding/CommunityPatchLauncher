using CommunityPatchLauncherFramework.Settings.Container;
using CommunityPatchLauncherFramework.Settings.Manager;
using CommunityPatchLauncherFramework.TaskPipeline.EventData;
using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommunityPatchLauncherFramework.TaskPipeline.Container
{
    internal class QueueWorkSet
    {
        /// <summary>
        /// This event will tell us if there is a progress change
        /// </summary>
        public event EventHandler<TaskProgressChanged> ProgressChanged;

        /// <summary>
        /// This event will be fired if the task is complete
        /// </summary>
        public event EventHandler<WorkerSetTaskDone> TaskComplete;

        /// <summary>
        /// The unique id of this work set
        /// </summary>
        public uint Id { get; }

        /// <summary>
        /// The total work load for this set
        /// </summary>
        int totalWorkLoad;

        /// <summary>
        /// The work already done from executed tasks
        /// </summary>
        int alreadyDoneWorkload;

        /// <summary>
        /// The settings manager to add to the tasks
        /// </summary>
        private readonly SettingManager settingManager;

        /// <summary>
        /// The settings for the tasks to pass on
        /// </summary>
        private HashSet<SettingPair> taskSettings;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="settingManager">The settings manager to pass on to every task</param>
        /// <param name="id">The id to use for this work set</param>
        public QueueWorkSet(SettingManager settingManager, uint id)
        {
            Id = id;
            this.settingManager = settingManager;
            taskSettings = new HashSet<SettingPair>();
        }

        /// <summary>
        /// Reset this work set to the start state
        /// </summary>
        public void Init()
        {
            totalWorkLoad = 0;
            alreadyDoneWorkload = 0;
            taskSettings.Clear();
        }

        /// <summary>
        /// Execute all the tasks in the list
        /// </summary>
        /// <param name="tasks">All the tasks to execute</param>
        /// <returns>The result of the task execution</returns>
        public bool ExecuteTask(List<ITask> tasks)
        {
            bool lastTaskState = true;
            totalWorkLoad = getCompleteWorkLoad(tasks);
            foreach (ITask task in tasks)
            {
                task.Init(settingManager, taskSettings);
                if (task is IProgressTask progressTask)
                {
                    progressTask.ProgressChanged += ProgressTask_ProgressChanged; ;
                    progressTask.TaskComplete += ProgressTask_TaskComplete;
                }
                lastTaskState = task.Execute(lastTaskState);
                if (lastTaskState == false && task.AbortOnError)
                {
                    break;
                }
                taskSettings = task.Settings;
            }

            return lastTaskState;
        }

        /// <summary>
        /// Event if a task was complete
        /// </summary>
        /// <param name="sender">The task wich completed there work</param>
        /// <param name="e">The task done dataset from the task</param>
        private void ProgressTask_TaskComplete(object sender, TaskDone e)
        {
            EventHandler<WorkerSetTaskDone> handler = TaskComplete;
            alreadyDoneWorkload += e.TotalWorkload;
            if (alreadyDoneWorkload == totalWorkLoad)
            {
                handler?.Invoke(this, new WorkerSetTaskDone(totalWorkLoad, sender));
            }
            ProgressTask_ProgressChanged(sender, new TaskProgressChanged(totalWorkLoad, 0));
            
        }

        /// <summary>
        /// A task did progress in there execution
        /// </summary>
        /// <param name="sender">The task which did progress</param>
        /// <param name="e">The progress which have been done</param>
        private void ProgressTask_ProgressChanged(object sender, TaskProgressChanged e)
        {
            EventHandler<TaskProgressChanged> handler = ProgressChanged;
            handler?.Invoke(sender, new TaskProgressChanged(totalWorkLoad, alreadyDoneWorkload + e.CurrentWorkload));
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
    }
}
