using CommunityPatchLauncher.UserControls;
using CommunityPatchLauncher.ViewModels;

namespace CommunityPatchLauncher.Commands.ApplicationWindow
{
    /// <summary>
    /// This class will change the content of the browser
    /// </summary>
    class ChangeBrowserContentCommand : BaseCommand
    {
        /// <summary>
        /// The browser view model to use
        /// </summary>
        private readonly BrowserModelView browserModel;

        /// <summary>
        /// The fallback target file to use
        /// </summary>
        private readonly string targetFile;

        /// <summary>
        /// Change the browser content
        /// </summary>
        /// <param name="browserModel">The browser model view</param>
        public ChangeBrowserContentCommand(BrowserModelView browserModel) : this(browserModel, string.Empty)
        {
        }

        /// <summary>
        /// Change the browser content
        /// </summary>
        /// <param name="browser">The browser</param>
        public ChangeBrowserContentCommand(BrowserUserControl browser) : this(browser, string.Empty)
        {
        }

        /// <summary>
        /// Change the browser content
        /// </summary>
        /// <param name="browser">The browser</param>
        /// <param name="targetFile">The fallback target file</param>
        public ChangeBrowserContentCommand(BrowserUserControl browser, string targetFile)
        {
            if (browser.DataContext is BrowserModelView modelView)
            {
                this.browserModel = modelView;
                this.targetFile = targetFile;
            }
        }

        /// <summary>
        /// Change the browser content
        /// </summary>
        /// <param name="browserModel">The browser model view</param>
        /// <param name="targetFile">The fallback target file</param>
        public ChangeBrowserContentCommand(BrowserModelView browserModel, string targetFile)
        {
            this.browserModel = browserModel;
            this.targetFile = targetFile;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return browserModel != null;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }
            string target = targetFile;
            if (parameter is string document)
            {
                target = document;
            }
            browserModel.ChangeDocument(target);
        }
    }
}
