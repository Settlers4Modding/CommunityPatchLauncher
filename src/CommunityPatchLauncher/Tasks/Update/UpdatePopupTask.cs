using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Commands.ApplicationWindow;
using CommunityPatchLauncher.Enums;
using CommunityPatchLauncher.UserControls;
using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System;
using System.Windows;

namespace CommunityPatchLauncher.Tasks.Update
{
    /// <summary>
    /// This class will show a popup if you should update the app
    /// </summary>
    class UpdatePopupTask : AbstractTask
    {
        /// <summary>
        /// The yes no popup
        /// </summary>
        private readonly IDataCommand yesNoPopup;

        /// <summary>
        /// The info popup to use
        /// </summary>
        private readonly IDataCommand infoPopup;

        /// <summary>
        /// The warning popup to use
        /// </summary>
        private readonly IDataCommand warningPopup;

        /// <summary>
        /// Show the local is newer popup
        /// </summary>
        private readonly bool showLocalIsNewer;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="parentWindow">The parent window to use</param>
        /// <param name="showLocalIsNewer">Show if local is newer</param>
        public UpdatePopupTask(Window parentWindow, bool showLocalIsNewer)
        {
            yesNoPopup = new OpenCustomPopupWindowCommand(parentWindow, FontAwesome.WPF.FontAwesomeIcon.Question, Properties.Resources.Dialog_UpgradeTitle, new YesNoDialog());
            infoPopup = new OpenCustomPopupWindowCommand(parentWindow, FontAwesome.WPF.FontAwesomeIcon.Warning, Properties.Resources.Dialog_UpdateInformationTitle, new InfoPopup());
            warningPopup = new OpenCustomPopupWindowCommand(parentWindow, FontAwesome.WPF.FontAwesomeIcon.Warning, Properties.Resources.Dialog_UpdateProblemTitle, new InfoPopup());
            this.showLocalIsNewer = showLocalIsNewer;
        }

        /// <inheritdoc/>
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
            if (yesNoPopup != null)
            {
                yesNoPopup.Executed += (sender, content) =>
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
                yesNoPopup?.Execute(text);
            }

            return returnState;
        }
    }
}
