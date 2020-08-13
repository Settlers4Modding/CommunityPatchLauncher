using System;
using System.Globalization;
using System.Threading;
using System.Windows.Input;

namespace CommunityPatchLauncher.Commands.ApplicationWindow
{
    /// <summary>
    /// This command will switch the GUI language
    /// </summary>
    [Obsolete]
    internal class SwitchGuiLanguage : ICommand
    {
        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged;

        /// <inheritdoc/>
        public bool CanExecute(object parameter)
        {
            return parameter != null && parameter.GetType() == typeof(string);
        }

        /// <inheritdoc/>
        public void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(parameter.ToString());
        }
    }
}
