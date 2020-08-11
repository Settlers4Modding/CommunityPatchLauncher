using CommunityPatchLauncher.Enums;
using CommunityPatchLauncher.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CommunityPatchLauncher.Commands
{
    internal class OpenLaunchUserControlToPanel : OpenControlToPanel
    {
        public OpenLaunchUserControlToPanel(DockPanel panelToUse, UserControl userControl) : base(panelToUse, userControl)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return userControl is LaunchGameUserControl;
        }

        public override void Execute(object parameter)
        {
            base.Execute(parameter);
            if (userControl is LaunchGameUserControl launchGameUserControl && parameter is AvailablePatches patch)
            {
                launchGameUserControl.SetPatch(patch);
            }
        }
    }
}
