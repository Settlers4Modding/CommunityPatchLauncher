using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System.Windows.Input;

namespace CommunityPatchLauncher.Tasks
{
    /// <summary>
    /// This task will close the application
    /// </summary>
    class CloseApplicationTask : AbstractTask
    {
        /// <summary>
        /// The command to use for closing
        /// </summary>
        private readonly ICommand closeApplicationCommand;

        /// <summary>
        /// Create a new instance of this task
        /// </summary>
        /// <param name="closeApplicationCommand">The command to use</param>
        public CloseApplicationTask(ICommand closeApplicationCommand)
        {
            this.closeApplicationCommand = closeApplicationCommand;
        }

        /// <inheritdoc/>
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
