using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;

namespace CommunityPatchLauncher.Commands
{
    public class CloseWindowCommand : BaseCommand
    {
        private readonly Window windowToClose;

        public CloseWindowCommand(Window windowToClose)
        {
            this.windowToClose = windowToClose;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            windowToClose.Close();
        }
    }
}
