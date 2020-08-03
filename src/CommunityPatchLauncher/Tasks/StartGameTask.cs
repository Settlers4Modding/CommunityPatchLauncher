using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System.Diagnostics;
using System.IO;

namespace CommunityPatchLauncher.Tasks
{
    internal class StartGameTask : AbstractTask
    {
        /// <summary>
        /// Create a new instance of this task
        /// </summary>
        public StartGameTask()
        {
            abortOnError = true;
        }

        /// <inheritdoc/>
        public override bool Execute(bool previousTaskState)
        {
            string gamePath = settingManager.GetValue<string>("GameFolder");
            string gameFile = gamePath += "S4_Main.exe";
            if (gamePath == null || !File.Exists(gameFile))
            {
                return false;
            }
            Process.Start(gameFile);
            return true;
        }
    }
}
