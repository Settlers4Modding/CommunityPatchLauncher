using CommunityPatchLauncherFramework.TaskPipeline.EventData;
using System;

namespace CommunityPatchLauncherFramework.TaskPipeline.Tasks
{
    /// <summary>
    /// This class is a template for a progress abstract Task
    /// </summary>
    public abstract class ProgressAbstractTask : AbstractTask, IProgressTask
    {
        /// <summary>
        /// The total workload which should be done
        /// </summary>
        protected int totalWorkload;

        /// <summary>
        /// Was the done signal send already
        /// </summary>
        private bool doneSend;

        /// <summary>
        /// Did the progress change
        /// </summary>
        public event EventHandler<TaskProgressChanged> ProgressChanged;

        /// <summary>
        /// Is the task already completed
        /// </summary>
        public event EventHandler<TaskDone> TaskComplete;

        /// <summary>
        /// Create a new instance of this abstract class
        /// </summary>
        public ProgressAbstractTask()
        {
            totalWorkload = 100;
            doneSend = false;
        }

        /// <inheritdoc>
        public virtual int GetStepCount()
        {
            return totalWorkload;
        }

        /// <summary>
        /// The task is completed
        /// </summary>
        protected virtual void TaskDone()
        {
            if (doneSend)
            {
                return;
            }
            EventHandler<TaskDone> handler = TaskComplete;
            handler?.Invoke(this, new TaskDone(totalWorkload));
            doneSend = true;
        }

        /// <summary>
        /// The progress did change
        /// </summary>
        /// <param name="currentProgress">The current progress</param>
        protected virtual void ProgressHasChanged(int currentProgress)
        {
            currentProgress = currentProgress > totalWorkload ? totalWorkload : currentProgress;
            EventHandler<TaskProgressChanged> handler = ProgressChanged;
            handler?.Invoke(this, new TaskProgressChanged(totalWorkload, currentProgress));
        }
    }
}
