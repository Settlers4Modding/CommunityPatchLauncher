using CommunityPatchLauncher.Commands.DataContainer;
using CommunityPatchLauncherFramework.Settings.Manager;
using System.Windows;

namespace CommunityPatchLauncher.Commands
{
    internal class AcceptAgreementCommand : BaseCommand
    {
        private readonly SettingManager settingManager;
        private readonly Window currentWindow;
        private readonly Window windowToOpen;

        public AcceptAgreementCommand(SettingManager settingManager, Window currentWindow, Window windowToOpen)
        {
            this.settingManager = settingManager;
            this.currentWindow = currentWindow;
            this.windowToOpen = windowToOpen;
        }

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

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }
            AcceptAgreementData agreementData = parameter as AcceptAgreementData;
            settingManager.AddValue("AgreementAccepted", agreementData.Agreement);
            settingManager.AddValue("GameFolder", agreementData.GameFolder);
            settingManager.AddValue("Language", agreementData.Language.IsoCode);
            settingManager.SaveSettings();

            currentWindow?.Close();
            windowToOpen?.ShowDialog();
        }
    }
}
