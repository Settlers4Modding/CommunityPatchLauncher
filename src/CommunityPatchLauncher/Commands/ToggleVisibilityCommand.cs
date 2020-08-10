using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CommunityPatchLauncher.Commands
{
    public class ToggleVisibilityCommand : BaseDataCommand
    {
        public override bool CanExecute(object parameter)
        {
            return parameter is Visibility;
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            Visibility visibility;
            Enum.TryParse(parameter.ToString(), out visibility);
            data = Visibility.Collapsed;
            if (visibility == Visibility.Collapsed || visibility == Visibility.Hidden)
            {
                data = Visibility.Visible;
            }
            
            ExecutionDone();
        }
    }
}
