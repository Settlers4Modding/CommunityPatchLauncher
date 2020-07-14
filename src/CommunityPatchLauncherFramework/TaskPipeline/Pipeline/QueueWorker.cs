using CommunityPatchLauncherFramework.Settings.Manager;
using CommunityPatchLauncherFramework.TaskPipeline.Container;
using CommunityPatchLauncherFramework.TaskPipeline.EventData;
using CommunityPatchLauncherFramework.TaskPipeline.Factory;
using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommunityPatchLauncherFramework.TaskPipeline.Pipeline
{
    /// <summary>
    /// This class will allow you to run multi tasks in a row
    /// </summary>
    public class QueueWorker
    {
        /// <summary>
        /// Object used to check if any list is locked
        /// </summary>
        private object listLock;

        /// <summary>
        /// Event if the progress has changed
        /// </summary>
        public event EventHandler<TaskProgressChanged> ProgressChanged;

        /// <summary>
        /// The setting manager to use
        /// </summary>
        private readonly SettingManager settingManager;

        /// <summary>
        /// Next id to use for a workset
        /// </summary>
        private uint nextSetId;

        /// <summary>
        /// All the active work sets,
        /// </summary>
        private List<QueueWorkSet> activeSet;

        /// <summary>
        /// Pool with ready to use work sets
        /// </summary>
        private Queue<QueueWorkSet> workSetPool;

        /// <summary>
        /// Create a new instance of this task planner
        /// </summary>
        /// <param name="settingManager"></param>
        public QueueWorker(SettingManager settingManager)
        {
            nextSetId = 0;
            activeSet = new List<QueueWorkSet>();
            workSetPool = new Queue<QueueWorkSet>();
            listLock = new object();

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
            QueueWorkSet queueWorkSet = null;
            lock (listLock)
            {
                if (workSetPool.Count != 0)
                {
                    queueWorkSet = workSetPool.Dequeue();
                }
                queueWorkSet = queueWorkSet ?? new QueueWorkSet(settingManager, nextSetId++);
                queueWorkSet.Init();
                queueWorkSet.TaskComplete += QueueWorkSet_TaskComplete;
                queueWorkSet.ProgressChanged += QueueWorkSet_ProgressChanged;
                activeSet.Add(queueWorkSet);
            }

            queueWorkSet.ExecuteTask(tasks);
            

            return true;
        }

        /// <summary>
        /// This method will forward the change event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event data</param>
        private void QueueWorkSet_ProgressChanged(object sender, TaskProgressChanged e)
        {
            EventHandler<TaskProgressChanged> handler = ProgressChanged;
            handler?.Invoke(sender, e);
        }

        /// <summary>
        /// This method will forward the task complete event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event data</param>
        private void QueueWorkSet_TaskComplete(object sender, WorkerSetTaskDone e)
        {
            if (sender is QueueWorkSet workSet)
            {
                lock (listLock)
                {
                    activeSet.RemoveAll((obj) => obj.Id == workSet.Id);
                    workSetPool.Enqueue(workSet);
                    workSet.ProgressChanged -= QueueWorkSet_ProgressChanged;
                    workSet.TaskComplete -= QueueWorkSet_TaskComplete;

                    QueueWorkSet_ProgressChanged(e.senderTask, new TaskProgressChanged(e.TotalWorkload, e.TotalWorkload));
                }
            }
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
