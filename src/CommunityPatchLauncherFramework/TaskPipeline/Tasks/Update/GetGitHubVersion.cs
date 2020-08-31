using System;
using System.Collections.Generic;
using System.Text;

namespace CommunityPatchLauncherFramework.TaskPipeline.Tasks.Update
{
    public class GetGitHubVersion : AbstractTask
    {
        public override bool Execute(bool previousTaskState)
        {
            return true;
        }
    }
}
