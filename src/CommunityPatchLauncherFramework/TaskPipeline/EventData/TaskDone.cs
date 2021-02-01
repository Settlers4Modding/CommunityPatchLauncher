namespace CommunityPatchLauncherFramework.TaskPipeline.EventData
{
    /// <summary>
    /// This class will show the total progress done
    /// </summary>
    public class TaskDone
    {
        /// <summary>
        /// Total workload of this task
        /// </summary>
        public int TotalWorkload { get; }

        /// <summary>
        /// Create a new instance of this event dataset
        /// </summary>
        /// <param name="totalWorkload"></param>
        public TaskDone(int totalWorkload)
        {
            TotalWorkload = totalWorkload;
        }
    }
}
