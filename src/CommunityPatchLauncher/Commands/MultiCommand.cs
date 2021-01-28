using System.Collections.Generic;
using System.Windows.Input;

namespace CommunityPatchLauncher.Commands
{
    /// <summary>
    /// This command will allow you to performe multiple commands at once
    /// </summary>
    public class MultiCommand : BaseCommand
    {
        /// <summary>
        /// The commands to execute
        /// </summary>
        private readonly List<ICommand> commands;

        /// <summary>
        /// Create a new instance of this command
        /// </summary>
        /// <param name="commands"></param>
        public MultiCommand(List<ICommand> commands)
        {
            this.commands = commands;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            bool canExecute = true;
            foreach (ICommand command in commands)
            {
                canExecute &= command != null && command.CanExecute(parameter);
            }
            return canExecute;
        }

        /// <inheritdoc/>
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
