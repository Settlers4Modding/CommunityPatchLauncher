using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CommunityPatchLauncher.Commands
{
    internal class OpenControlToPanel : BaseCommand
    {

        protected readonly DockPanel panelToUse;
        protected readonly UserControl userControl;

        public OpenControlToPanel(DockPanel panelToUse, UserControl userControl)
        {
            this.panelToUse = panelToUse;
            this.userControl = userControl;
        }


        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            foreach (UIElement child in panelToUse.Children)
            {
                if (child is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
            panelToUse.Children.Clear();
            panelToUse.Children.Add(userControl);
        }
    }
}
