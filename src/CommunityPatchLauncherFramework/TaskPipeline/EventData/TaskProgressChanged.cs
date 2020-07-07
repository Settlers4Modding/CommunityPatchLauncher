namespace CommunityPatchLauncherFramework.TaskPipeline.EventData
{
    /// <summary>
    /// This class will show the changed Progress
    /// </summary>
    public class TaskProgressChanged : TaskDone
    {
        /// <summary>
        /// The currently done workload of this task
        /// </summary>
        public int CurrentWorkload { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="totalWorkload">The total workload which should be done</param>
        /// <param name="currentWorkload">The current workload which was done already</param>
        public TaskProgressChanged(int totalWorkload, int currentWorkload) : base(totalWorkload)
        {
            CurrentWorkload = currentWorkload;
        }

    }
}
