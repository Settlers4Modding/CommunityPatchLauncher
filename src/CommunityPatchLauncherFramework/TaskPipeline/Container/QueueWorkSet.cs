using CommunityPatchLauncherFramework.Settings.Manager;
using CommunityPatchLauncherFramework.TaskPipeline.EventData;
using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityPatchLauncherFramework.TaskPipeline.Container
{
    internal class QueueWorkSet
    {
        /// <summary>
        /// This event will tell us if there is a progress change
        /// </summary>
       public  event EventHandler<TaskProgressChanged> ProgressChanged;

        /// <summary>
        /// This event will be fired if the task is complete
        /// </summary>
        public event EventHandler<WorkerSetTaskDone> TaskComplete;

        public uint Id { get; }

        int totalWorkLoad;
        private readonly SettingManager settingManager;

        public QueueWorkSet(SettingManager settingManager, uint id)
        {
            Id = id;
            this.settingManager = settingManager;
        }

        public void Init()
        {
            totalWorkLoad = 0;
        }

        public bool ExecuteTask(List<ITask> tasks)
        {
            bool lastTaskState = true;
            totalWorkLoad = getCompleteWorkLoad(tasks);
            foreach (ITask task in tasks)
            {
                task.Init(settingManager);
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
            }

            return lastTaskState;
        }

        private void ProgressTask_TaskComplete(object sender, TaskDone e)
        {
            EventHandler<WorkerSetTaskDone> handler = TaskComplete;
            handler?.Invoke(this, new WorkerSetTaskDone(e, sender));
        }

        private void ProgressTask_ProgressChanged(object sender, TaskProgressChanged e)
        {
            EventHandler<TaskProgressChanged> handler = ProgressChanged;
            handler?.Invoke(sender, e);
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
