using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CommunityPatchLauncher.Commands
{
    internal class MaximizeWindowCommand : BaseCommand
    {
        private readonly Window window;

        public MaximizeWindowCommand(Window window)
        {
            this.window = window;
        }

        public override bool CanExecute(object parameter)
        {
            return window.WindowState != WindowState.Minimized;
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }
            WindowState newState = WindowState.Maximized;
            if (window.WindowState == newState)
            {
                newState = WindowState.Normal;
            }

            window.WindowState = newState;
        }
    }
}
