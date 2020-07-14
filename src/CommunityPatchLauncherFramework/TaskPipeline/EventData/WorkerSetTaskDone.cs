using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace CommunityPatchLauncherFramework.TaskPipeline.EventData
{
    public class WorkerSetTaskDone : TaskDone
    {
        public object senderTask { get; }

        public WorkerSetTaskDone(int totalWorkload, object sender) : base(totalWorkload)
        {
            senderTask = sender;
        }

        public WorkerSetTaskDone(TaskDone taskDoneData, object sender) : this (taskDoneData.TotalWorkload, sender)
        {
        }
    }
}
