namespace CommunityPatchLauncherFramework.TaskPipeline.EventData
{
    public class WorkerSetTaskDone : TaskDone
    {
        /// <summary>
        /// The task which did send the event
        /// </summary>
        public object senderTask { get; }

        /// <summary>
        /// Create a new worker set done container
        /// </summary>
        /// <param name="totalWorkload">The total workload of the work set/param>
        /// <param name="sender">The task which triggered the event</param>
        public WorkerSetTaskDone(int totalWorkload, object sender) : base(totalWorkload)
        {
            senderTask = sender;
        }
    }
}
