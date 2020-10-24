using CommunityPatchLauncherFramework.TaskPipeline.EventData;
using System;

namespace CommunityPatchLauncher.Commands
{
    /// <summary>
    /// This interface will extend the data interface with a progress event
    /// </summary>
    internal interface IProgressCommand : IDataCommand
    {
        event EventHandler<TaskProgressChanged> progressChanged;
    }
}
