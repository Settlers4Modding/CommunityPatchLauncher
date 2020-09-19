using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CommunityPatchLauncher.Tasks.Update
{
    internal class PatchLauncher : AbstractTask
    {
        private readonly string launcherAppName;

        public PatchLauncher(string launcherAppName)
        {
            this.launcherAppName = launcherAppName;
        }

        public override bool Execute(bool previousTaskState)
        {
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

            if (File.Exists(destinationFile))
            {
                string arguments = Assembly.GetExecutingAssembly().Location + " ";
                arguments += Process.GetCurrentProcess().Id + " ";
                arguments += launcherUpdate + " ";
                arguments += "Test";
                Process.Start(destinationFile, arguments);
            }


            return true;

        }
    }
}
