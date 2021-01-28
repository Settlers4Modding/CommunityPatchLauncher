using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System.Collections.Generic;

namespace CommunityPatchLauncherFramework.TaskPipeline.Factory
{
    /// <summary>
    /// A task factory to create task queues
    /// </summary>
    public interface ITaskFactory
    {
        /// <summary>
        /// This will return you a list with tasks
        /// </summary>
        /// <returns>A ready to use list with tasks</returns>
        List<ITask> GetTasks();
    }
}
