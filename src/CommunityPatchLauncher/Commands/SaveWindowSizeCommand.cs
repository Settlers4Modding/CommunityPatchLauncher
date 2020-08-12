using CommunityPatchLauncher.Commands.DataContainer;
using CommunityPatchLauncherFramework.Settings.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommunityPatchLauncher.Commands
{
    internal class SaveWindowSizeCommand : BaseCommand
    {
        private readonly SettingManager settingManager;

        public SaveWindowSizeCommand(SettingManager settingManager)
        {
            this.settingManager = settingManager;
        }

        public override bool CanExecute(object parameter)
        {
            return settingManager != null && parameter is ResizeWindowData;
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }
            if (parameter is ResizeWindowData resizeWindowData)
            {
                settingManager.AddValue("Width", resizeWindowData.Width);
                settingManager.AddValue("Height", resizeWindowData.Height);
                settingManager.AddValue("CustomWindowSize", resizeWindowData.Custom);
                settingManager.SaveSettings();
            }
            
        }
    }
}
