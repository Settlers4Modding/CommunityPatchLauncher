﻿using System.Windows;

namespace CommunityPatchLauncher.Commands.ApplicationWindow
{
    /// <summary>
    /// Open new window command
    /// </summary>
    class OpenNewWindowCommand : BaseCommand
    {
        /// <summary>
        /// The new window to open
        /// </summary>
        private readonly Window windowToOpen;

        /// <summary>
        /// Create a new instance of this command
        /// </summary>
        /// <param name="windowToOpen">The new window to open</param>
        public OpenNewWindowCommand(Window windowToOpen)
        {
            this.windowToOpen = windowToOpen;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            windowToOpen.ShowDialog();
        }
    }
}
