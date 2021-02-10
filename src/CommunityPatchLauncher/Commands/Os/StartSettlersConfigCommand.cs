using System.Collections.Generic;
using System.IO;
using CommunityPatchLauncherFramework.Settings.Manager;

namespace CommunityPatchLauncher.Commands.Os
{
    /// <summary>
    /// starts the settlers 4 he configuration file
    /// </summary>
    class StartSettlersConfigCommand : StartProgramCommand
    {
        /// <summary>
        /// The settings manager to use
        /// </summary>
        private SettingManager settingManager;

        public StartSettlersConfigCommand(SettingManager settingManager)
        {
            string gameFolder = settingManager.GetValue<string>("GameFolder");
            string configPath = gameFolder + Properties.Settings.Default.ConfigBaseFolder + "Settlers4Config.exe";

            this.settingManager = settingManager;
            programPath = configPath;
        }
    }
}
