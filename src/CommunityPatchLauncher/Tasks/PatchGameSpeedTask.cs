using CommunityPatchLauncher.Enums;
using CommunityPatchLauncher.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;
using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System;
using System.IO;

namespace CommunityPatchLauncher.Tasks
{
    /// <summary>
    /// This task will patch the game speed
    /// </summary>
    internal class PatchGameSpeedTask : AbstractTask
    {
        private readonly SpeedModes speedMode;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="speedMode">The speed mode to use</param>
        public PatchGameSpeedTask(SpeedModes speedMode)
        {
            abortOnError = true;
            this.speedMode = speedMode;
        }

        /// <inheritdoc/>
        public override bool Execute(bool previousTaskState)
        {
            string targetFolder = settingManager.GetValue<string>("GameFolder");
            string gameFile = targetFolder + "S4_Main.exe";
            if (targetFolder == null || !File.Exists(gameFile))
            {
                return false;
            }

            ISettingFactory settingFactory = new WpfPropertySettingManagerFactory();
            SettingManager staticSettings = settingFactory.GetSettingsManager();
            Byte speedByte = staticSettings.GetValue<Byte>(speedMode.ToString());
            long speedPosition = staticSettings.GetValue<long>("PatchPosition");

            using (Stream writer = new FileStream((gameFile), FileMode.Open, FileAccess.ReadWrite))
            {
                writer.Position = speedPosition;
                writer.WriteByte(speedByte);
            }

            return true;
        }
    }
}
