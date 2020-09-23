using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System.Windows.Input;

namespace CommunityPatchLauncher.Tasks
{
    class CloseApplicationTask : AbstractTask
    {
        private readonly ICommand closeApplicationCommand;

        public CloseApplicationTask(ICommand closeApplicationCommand)
        {
            this.closeApplicationCommand = closeApplicationCommand;
        }

        public override bool Execute(bool previousTaskState)
        {
            if (closeApplicationCommand == null)
            {
                return false;
            }
            closeApplicationCommand.Execute(null);
            return true;
        }
    }
}
