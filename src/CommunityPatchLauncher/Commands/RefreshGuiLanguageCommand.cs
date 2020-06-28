using System;
using System.Windows;
using System.Windows.Input;

namespace CommunityPatchLauncher.Commands
{
    /// <summary>
    /// This command will refresh the window gui
    /// </summary>
    internal class RefreshGuiLanguageCommand : ICommand
    {
        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged;

        /// <inheritdoc/>
        public bool CanExecute(object parameter)
        {
            return parameter != null && parameter is Window;
        }

        /// <inheritdoc/>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                Window window = parameter as Window;
                Window newWindow = Activator.CreateInstance(window.GetType()) as Window;
                newWindow.Show();
                window.Close();
            }
        }
    }
}
