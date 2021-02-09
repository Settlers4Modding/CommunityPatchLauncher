using System;
using System.Windows;

namespace CommunityPatchLauncher.Commands.ApplicationWindow
{
    /// <summary>
    /// This command will maximize a window
    /// </summary>
    internal class MaximizeWindowCommand : BaseCommand
    {
        /// <summary>
        /// The window to work on
        /// </summary>
        private readonly Window window;

        /// <summary>
        /// Last time we changed the state
        /// </summary>
        private DateTime lastStateChange;

        /// <summary>
        /// The cooldown in milliseconds
        /// </summary>
        private int cooldown;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="window">The window to work on</param>
        public MaximizeWindowCommand(Window window) : this(window, 500)
        {
        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="window">The window to work on</param>
        public MaximizeWindowCommand(Window window, int cooldownInMilliseconds)
        {
            this.window = window;
            lastStateChange = DateTime.Now;
            cooldown = cooldownInMilliseconds;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return window.WindowState != WindowState.Minimized;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }
            TimeSpan timeSpan = DateTime.Now - lastStateChange;
            if (timeSpan.TotalMilliseconds < cooldown)
            {
                return;
            }
            lastStateChange = DateTime.Now;
            WindowState newState = WindowState.Maximized;
            if (window.WindowState == newState)
            {
                newState = WindowState.Normal;
            }

            window.WindowState = newState;
        }
    }
}
