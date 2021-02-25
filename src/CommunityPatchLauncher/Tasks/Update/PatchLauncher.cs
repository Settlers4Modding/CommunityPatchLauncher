using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;

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

            bool hasWriteAccess = true;
            try
            {
                DirectorySecurity security = Directory.GetAccessControl(applicationPath);
                string testFile = applicationPath + "\\writeAccessTest.txt";
                File.Create(testFile);
                if (File.Exists(testFile))
                {
                    File.Delete(testFile);
                }
            }
            catch (Exception)
            {
                hasWriteAccess = false;
            }

            if (File.Exists(destinationFile))
            {
                ProcessStartInfo info = new ProcessStartInfo(destinationFile);
                
                string arguments = "\"" + Assembly.GetExecutingAssembly().Location + "\" ";
                arguments += Process.GetCurrentProcess().Id + " ";
                arguments += "\"" + launcherUpdate + "\" ";
                arguments += "\"" + applicationPath + "\" ";
                arguments += "\"" + launcherAppName + "\"";
                info.Arguments = arguments;
                if (!hasWriteAccess)
                {
                    info.Verb = "runas";
                }
                try
                {
                    Process.Start(info);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
