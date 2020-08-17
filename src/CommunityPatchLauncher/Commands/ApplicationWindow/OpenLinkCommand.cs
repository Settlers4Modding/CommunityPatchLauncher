using System;
using System.Diagnostics;
namespace CommunityPatchLauncher.Commands.ApplicationWindow
{
    /// <summary>
    /// This command will open a url in the browser
    /// </summary>
    internal class OpenLinkCommand : BaseCommand
    {
        /// <summary>
        /// Url to open in default browser
        /// </summary>
        private readonly Uri urlToOpen;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="urlToOpen">The url to open</param>
        public OpenLinkCommand(string urlToOpen) : this(new Uri(urlToOpen))
        {
        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="urlToOpen">The url to open</param>
        public OpenLinkCommand(Uri urlToOpen)
        {
            this.urlToOpen = urlToOpen;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return urlToOpen != null;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }
            Process.Start(urlToOpen.ToString());
        }
    }
}
