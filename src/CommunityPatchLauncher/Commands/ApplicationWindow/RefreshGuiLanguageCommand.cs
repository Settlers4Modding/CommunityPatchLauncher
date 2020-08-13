using System;
using System.Windows;

namespace CommunityPatchLauncher.Commands.ApplicationWindow
{
    /// <summary>
    /// This command will refresh the window gui
    /// </summary>
    internal class RefreshGuiLanguageCommand : BaseCommand
    {
        private readonly Window windowToRefresh;

        public RefreshGuiLanguageCommand(Window windowToRefresh)
        {
            this.windowToRefresh = windowToRefresh;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return windowToRefresh != null && windowToRefresh is Window;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                Window window = windowToRefresh;
                Window newWindow = Activator.CreateInstance(window.GetType()) as Window;
                newWindow.Left = window.Left;
                newWindow.Top = window.Top;
                newWindow.Show();
                window.Close();
            }
        }
    }
}
