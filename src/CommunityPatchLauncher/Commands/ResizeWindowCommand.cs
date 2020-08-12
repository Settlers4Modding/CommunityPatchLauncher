using CommunityPatchLauncher.Commands.DataContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CommunityPatchLauncher.Commands
{
    internal class ResizeWindowCommand : BaseCommand
    {
        private readonly Window windowToResize;

        public ResizeWindowCommand(Window windowToResize)
        {
            this.windowToResize = windowToResize;
        }

        public override bool CanExecute(object parameter)
        {
            return windowToResize != null && parameter is ResizeWindowData;
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            if (parameter is ResizeWindowData resizeWindowData)
            {
                windowToResize.Width = resizeWindowData.Width;
                windowToResize.Height = resizeWindowData.Height;
            }
        }
    }
}
