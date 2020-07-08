using CommunityPatchLauncherFramework.TaskPipeline.EventData;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommunityPatchLauncherFramework.TaskPipeline.Tasks
{
    public interface IProgressTask : ITask
    {
        /// <summary>
        /// This event will tell us if there is a progress change
        /// </summary>
        event EventHandler<TaskProgressChanged> ProgressChanged;

        /// <summary>
        /// This event will be fired if the task is complete
        /// </summary>
        event EventHandler<TaskDone> TaskComplete;

        /// <summary>
        /// Get the step count for this task
        /// </summary>
        /// <returns>The steps needed for this task to complete</returns>
        int GetStepCount();
    }
}
