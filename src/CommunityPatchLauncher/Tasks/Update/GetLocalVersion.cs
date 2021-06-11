using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System;
using System.IO;
using System.Reflection;

namespace CommunityPatchLauncher.Tasks.Update
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
            versionString = versionString.Length > 0 && versionString[0] == 'v' ? versionString.Substring(1) : versionString;
            Version realVersion = new Version(0, 0, 0);
            try
            {
                realVersion = new Version(versionString);
            }
            catch (Exception)
            {
                //Loading the version went badly wrong, whups
            }
            
            AddSetting("LocalVersion", realVersion);
            return true;
        }
    }
}
