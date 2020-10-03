using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;

namespace CommunityPatchLauncher.Tasks.Update
{
    /// <summary>
    /// Patch the launcher
    /// </summary>
    internal class PatchLauncher : AbstractTask
    {
        /// <summary>
        /// The name of the launcher app
        /// </summary>
        private readonly string launcherAppName;

        /// <summary>
        /// The path to the application to patch
        /// </summary>
        private readonly string applicationPath;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="launcherAppName">The name of the launcher app</param>
        public PatchLauncher(string launcherAppName)
        {
            this.launcherAppName = launcherAppName;
            
            FileInfo fileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
            applicationPath = fileInfo.DirectoryName;
        }

        /// <inheritdoc/>
        public override bool Execute(bool previousTaskState)
        {

            //AddSetting<string>("LauncherUpdate", @"C:\Users\Xanat\AppData\Local\Temp\SIVLauncherUpdate.zip");
            if (!previousTaskState)
            {
                return previousTaskState;
            }
            string launcherUpdate = GetSetting<string>("LauncherUpdate");
            if (!File.Exists(launcherUpdate))
            {
                return false;
            }
            string destinationFile = Path.GetTempPath() + launcherAppName;
            if (File.Exists(destinationFile))
            {
                File.Delete(destinationFile);
            }
            using (ZipArchive archive = ZipFile.OpenRead(launcherUpdate))
            {
                ZipArchiveEntry launcherUpdater = archive.Entries.Where(entry => entry.Name == launcherAppName).FirstOrDefault();
                if (launcherUpdater == null)
                {
                    return false;
                }
                launcherUpdater.ExtractToFile(destinationFile);
            }

            string gameFolder = settingManager?.GetValue<string>("GameFolder");
            if (gameFolder == null)
            {
                return false;
            }
            
            if (File.Exists(destinationFile))
            {
                string arguments = "\"" + Assembly.GetExecutingAssembly().Location + "\" ";
                arguments += Process.GetCurrentProcess().Id + " ";
                arguments += "\"" + launcherUpdate + "\" ";
                arguments += "\"" + applicationPath + "\" ";
                arguments += "\"" + launcherAppName + "\"";
                Process.Start(destinationFile, arguments);
            }

            return true;
        }
    }
}
