using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPatchLauncher.Commands.ApplicationWindow
{
    internal class OpenLinkCommand : BaseCommand
    {
        /// <summary>
        /// Url to open in default browser
        /// </summary>
        private readonly Uri urlToOpen;

        public OpenLinkCommand(string urlToOpen) : this(new Uri(urlToOpen))
        {
        }

        public OpenLinkCommand(Uri urlToOpen)
        {
            this.urlToOpen = urlToOpen;
        }

        public override bool CanExecute(object parameter)
        {
            return urlToOpen != null;
        }

        public override void Execute(object parameter)
        {
            Process.Start(urlToOpen.ToString());
        }
    }
}
