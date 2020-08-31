using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System;
using System.IO;
using System.Reflection;

namespace CommunityPatchLauncher.Update
{
    /// <summary>
    /// Get the local version of this installation
    /// </summary>
    public class GetLocalVersion : AbstractTask
    {
        /// <inheritdoc/>
        public override bool Execute(bool previousTaskState)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string versionString = "0.0.0";
            using (Stream stream = assembly.GetManifestResourceStream("CommunityPatchLauncher.Version.txt"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    versionString = reader.ReadLine();
                }
            }
            Version realVersion = new Version(versionString);
            AddSetting("LocalVersion", realVersion);
            return true;
        }
    }
}
