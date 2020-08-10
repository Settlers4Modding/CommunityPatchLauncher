using System;
using System.Windows.Input;

namespace CommunityPatchLauncher.Commands
{
    public abstract class BaseCommand : ICommand
    {
        /// <summary>
        /// Did the can execute value change
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Raise a event that the can execute did change
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        /// <inheritdoc/>
        public abstract bool CanExecute(object parameter);

        /// <inheritdoc/>
        public abstract void Execute(object parameter);
    }
}
