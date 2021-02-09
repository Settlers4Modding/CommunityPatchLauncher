using CommunityPatchLauncher.Commands.DataContainer;
using CommunityPatchLauncherFramework.Settings.Manager;
using System.Windows;

namespace CommunityPatchLauncher.Commands.Settings
{
    /// <summary>
    /// This command will accept the aggreement
    /// </summary>
    internal class AcceptAgreementCommand : BaseCommand
    {
        /// <summary>
        /// Setting manager to use
        /// </summary>
        private readonly SettingManager settingManager;

        /// <summary>
        /// The current window which should be close
        /// </summary>
        private readonly Window currentWindow;

        /// <summary>
        /// The new window to open
        /// </summary>
        private readonly Window windowToOpen;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="settingManager">The settings manager to use for saving settings to</param>
        /// <param name="currentWindow">The current window to close</param>
        /// <param name="windowToOpen">The new window to open</param>
        public AcceptAgreementCommand(SettingManager settingManager, Window currentWindow, Window windowToOpen)
        {
            this.settingManager = settingManager;
            this.currentWindow = currentWindow;
            this.windowToOpen = windowToOpen;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            if (parameter is AcceptAgreementData agreementData)
            {
                bool returnValue = agreementData.FolderSet;
                returnValue &= agreementData.Agreement;
                return returnValue;
            }

            return false;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }
            AcceptAgreementData agreementData = parameter as AcceptAgreementData;
            settingManager.Reload();
            settingManager.AddValue("AgreementAccepted", agreementData.Agreement);
            settingManager.AddValue("GameFolder", agreementData.GameFolder);
            settingManager.AddValue("Language", agreementData.Language.IsoCode);
            settingManager.AddValue("UpdateOnStartup", agreementData.UpdateOnStartup);
            settingManager.AddValue("AgreementChecksum", agreementData.Checksum);
            if (settingManager.GetValue<string>("DownloadFolder") == null)
            {
                string downloadFolder = settingManager.SettingFolderPath + "\\Downloads";
                downloadFolder = downloadFolder.Replace("\\", "/");
                settingManager.AddValue("DownloadFolder", downloadFolder);
            }
            settingManager.SaveSettings();

            currentWindow?.Close();
            windowToOpen?.ShowDialog();
        }
    }
}
