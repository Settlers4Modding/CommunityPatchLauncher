using Microsoft.Win32;
using System.IO;

namespace CommunityPatchLauncher.Commands
{
    /// <summary>
    /// This class will find the installation folder from the registry
    /// </summary>
    internal class InstallationFromRegistryCommand : BaseDataCommand
    {
        /// <summary>
        /// Path of the registry key to read
        /// </summary>
        private const string REGISTER_KEY = @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Ubisoft\Launcher\Installs\11785";

        /// <summary>
        /// Name of the registry key to read
        /// </summary>
        private const string KEY_NAME = "InstallDir";

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            string installPath = Registry.GetValue(REGISTER_KEY, KEY_NAME, "").ToString();
            data = File.Exists(installPath + "S4_Main.exe") ? installPath : "";
            ExecutionDone();
        }
    }
}
