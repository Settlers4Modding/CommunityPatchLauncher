using CommunityPatchLauncher.Commands.Condition;
using System.Windows;

namespace CommunityPatchLauncher.Commands.ApplicationWindow
{
    /// <summary>
    /// This command will minimize a window
    /// </summary>
    internal class MinimizeWindowCommand : BaseCommand
    {
        /// <summary>
        /// The window to work on
        /// </summary>
        private readonly Window window;

        /// <summary>
        /// The condition to meet
        /// </summary>
        private readonly ICondition condition;

        /// <summary>
        /// The callback to use for thread independet action
        /// </summary>
        /// <param name="window">The window to performe the action on</param>
        /// <param name="newState">The new state to set</param>
        private delegate void SetWindowStateCallback(Window window, WindowState newState);

        /// <summary>
        /// Get the state of a window
        /// </summary>
        /// <param name="window">The window to work on</param>
        /// <returns>The current state</returns>
        private delegate WindowState GetWindowStateCallback(Window window);

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="window">The window to work on</param>
        public MinimizeWindowCommand(Window window) : this(window, null)
        {
        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="window">The window to work on</param>
        public MinimizeWindowCommand(Window window, ICondition condition)
        {
            this.window = window;
            this.condition = condition;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            object data = window.Dispatcher.Invoke(
                new GetWindowStateCallback(GetWindowState),
                new object[] { window }
                );
            if (data is WindowState currentState)
            {
                return currentState != WindowState.Minimized;
            }

            return false;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }
            if (condition != null && condition.ConditionFailed(parameter))
            {
                return;
            }

            window.Dispatcher.Invoke(
                    new SetWindowStateCallback(SetWindowState),
                    new object[] { window, WindowState.Minimized }
                    );
        }

        /// <summary>
        /// Callback method to use for thread independet action
        /// </summary>
        /// <param name="window">The window to performe the action on</param>
        /// <param name="newState">The new state to set</param>
        private void SetWindowState(Window window, WindowState newState)
        {
            window.WindowState = newState;
        }


        /// <summary>
        /// Get the state of a window
        /// </summary>
        /// <param name="window">The window to work on</param>
        /// <returns>The current state</returns>
        private WindowState GetWindowState(Window window)
        {
            return window.WindowState;
        }
    }
}
