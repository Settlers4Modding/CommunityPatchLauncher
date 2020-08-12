using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommunityPatchLauncher.Commands
{
    public class MultiCommand : BaseCommand
    {
        private readonly List<ICommand> commands;

        public MultiCommand(List<ICommand> commands)
        {
            this.commands = commands;
        }

        public override bool CanExecute(object parameter)
        {
            bool canExecute = true;
            foreach (ICommand command in commands)
            {
                canExecute = command.CanExecute(parameter);
            }
            return canExecute;
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }
            foreach (ICommand command in commands)
            {
                command.Execute(parameter);
            }
        }
    }
}
