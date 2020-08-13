using CommunityPatchLauncher.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;
using Microsoft.Win32;
using System.IO;

namespace CommunityPatchLauncher.Commands.DataCommands
{
    /// <summary>
    /// This class will find the installation folder from the registry
    /// </summary>
    internal class InstallationFromRegistryCommand : BaseDataCommand
    {
        /// <summary>
        /// The setting manager to use to read the version reg key from
        /// </summary>
        private readonly SettingManager manager;

        public InstallationFromRegistryCommand()
        {
            ISettingFactory settingFactory = new WpfPropertySettingManagerFactory();
            manager = settingFactory.GetSettingsManager();
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            string registryKeyPath = manager?.GetValue<string>("GamePathRegKeyPath");
            string registryKeyName = manager?.GetValue<string>("GamePathRegKeyName");
            if (registryKeyPath == null || registryKeyName == null)
            {
                data = string.Empty;
                ExecutionDone();
                return;
            }
            string installPath = Registry.GetValue(registryKeyPath, registryKeyName, "").ToString();
            data = File.Exists(installPath + "S4_Main.exe") ? installPath : "";
            ExecutionDone();
        }
    }
}
