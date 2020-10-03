using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Commands.ApplicationWindow;
using CommunityPatchLauncher.Enums;
using CommunityPatchLauncher.UserControls;
using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System;
using System.Windows;

namespace CommunityPatchLauncher.Tasks.Update
{
    class UpdatePopupTask : AbstractTask
    {
        private readonly IDataCommand popupCommand;
        private readonly IDataCommand infoPopup;
        private readonly IDataCommand warningPopup;
        private readonly bool showLocalIsNewer;

        public UpdatePopupTask(Window parentWindow, bool showLocalIsNewer)
        {
            popupCommand = new OpenCustomPopupWindowCommand(parentWindow, FontAwesome.WPF.FontAwesomeIcon.Question, Properties.Resources.Dialog_UpgradeTitle, new YesNoDialog());
            infoPopup = new OpenCustomPopupWindowCommand(parentWindow, FontAwesome.WPF.FontAwesomeIcon.Warning, Properties.Resources.Dialog_UpdateInformationTitle, new InfoPopup());
            warningPopup = new OpenCustomPopupWindowCommand(parentWindow, FontAwesome.WPF.FontAwesomeIcon.Warning, Properties.Resources.Dialog_UpdateProblemTitle, new InfoPopup());
            this.showLocalIsNewer = showLocalIsNewer;
        }

        public override bool Execute(bool previousTaskState)
        {
            bool returnState = true;
            Version remoteVersion = GetSetting<Version>("RemoteVersion");
            Version localVersion = GetSetting<Version>("LocalVersion");

            if (localVersion == null || remoteVersion == null)
            {
                warningPopup.Execute(Properties.Resources.Message_VersionUpdateWarning);
                return false;
            }
            if (popupCommand != null)
            {
                popupCommand.Executed += (sender, content) =>
                {
                    object data = content.GetData();
                    if (data is YesNoEnum dialog)
                    {
                        returnState = dialog == YesNoEnum.Yes;
                    }
                };
            }

            if (showLocalIsNewer && localVersion >= remoteVersion)
            {
                infoPopup.Execute(Properties.Resources.Message_LocalVersionIsNewer);
                return false;
            }
            if (localVersion < remoteVersion)
            {
                string text = Properties.Resources.Message_Update;
                text = text.Replace("{remoteVersion}", remoteVersion.ToString());
                text = text.Replace("{localVersion}", localVersion.ToString());
                popupCommand?.Execute(text);
            }

            return returnState;
        }
    }
}
