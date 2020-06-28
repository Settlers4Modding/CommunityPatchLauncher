using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace CommunityPatchLauncher.Commands
{
    /// <summary>
    /// This command will restart the application
    /// </summary>
    internal class RestartApplicationCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        /// <inheritdoc/>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <inheritdoc/>
        public void Execute(object parameter)
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
