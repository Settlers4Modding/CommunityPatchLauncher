using CommunityPatchLauncherFramework.TaskPipeline.EventData;
using System;

namespace CommunityPatchLauncher.Commands
{
    /// <summary>
    /// This base class will tell you if there was any progress change
    /// </summary>
    abstract class BaseProgressCommand : BaseDataCommand, IProgressCommand
    {
        /// <inheritdoc/>
        public event EventHandler<TaskProgressChanged> ProgressChanged;

        /// <summary>
        /// This method will trigger the progress changed event
        /// </summary>
        /// <param name="currentWorkload">The current workload</param>
        /// <param name="totalWorkload">The total workload</param>
        protected void TriggerProgressChanged(int currentWorkload, int totalWorkload)
        {
            TriggerProgressChanged(new TaskProgressChanged(totalWorkload, currentWorkload));
        }

        /// <summary>
        /// This method will trigger the progress changed event
        /// </summary>
        /// <param name="progressChange">The progress changed data set</param>
        protected void TriggerProgressChanged(TaskProgressChanged progressChange)
        {
            EventHandler<TaskProgressChanged> handler = ProgressChanged;
            handler?.Invoke(this, progressChange);
        }
    }
}
