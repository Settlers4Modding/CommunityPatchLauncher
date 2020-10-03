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


        public UpdatePopupTask(Window parentWindow)
        {
            popupCommand = new OpenCustomPopupWindowCommand(parentWindow, FontAwesome.WPF.FontAwesomeIcon.Question, "Do you want to upgrade?", new YesNoDialog());
        }

        public override bool Execute(bool previousTaskState)
        {
            bool returnState = true;
            Version remoteVersion = GetSetting<Version>("RemoteVersion");
            Version localVersion = GetSetting<Version>("LocalVersion");

            if (localVersion == null || remoteVersion == null)
            {
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
