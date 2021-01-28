using CommunityPatchLauncher.Commands.ApplicationWindow;
using CommunityPatchLauncher.Documentation.Factories;
using CommunityPatchLauncherFramework.Documentation.Factory;
using CommunityPatchLauncherFramework.Documentation.Manager;
using CommunityPatchLauncherFramework.Documentation.Strategy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    /// <summary>
    /// This class is the view model for the browser control
    /// </summary>
    internal class BrowserModelView : BaseViewModel
    {
        /// <summary>
        /// The current content of the browser
        /// </summary>
        public string BrowserContent
        {
            get => browserContent;
            private set
            {
                browserContent = value;
                RaisePropertyChanged("BrowserContent");
            }
        }

        /// <summary>
        /// Private access to ther browser content
        /// </summary>
        private string browserContent;

        /// <summary>
        /// The document manager to use
        /// </summary>
        private readonly DocumentManager documentManager;

        /// <summary>
        /// The name of the document to show
        /// </summary>
        private readonly string documentToShow;

        /// <summary>
        /// Create a new instance of this view model
        /// </summary>
        /// <param name="documentToShow"></param>
        public BrowserModelView(string documentToShow, UserControl control)
        {
            IDocumentManagerFactory factory = new LocalDocumentManagerFactory();
            documentManager = factory.GetDocumentManager("en-EN", new MarkdownHtmlConvertStrategy());
            this.documentToShow = documentToShow;

            DependencyObject browserObject = (DependencyObject)control.FindName("WB_browser");
            if (browserObject is WebBrowser browser)
            {
                browser.PreviewKeyDown += (sender, eventArgs) =>
                {
                    eventArgs.Handled = eventArgs.Key == Key.F5;
                };
                browser.Navigating += (sender, eventArgs) =>
                {
                    if (eventArgs.Uri == null)
                    {
                        return;
                    }
                    string url = eventArgs.Uri.ToString();
                    url = url.ToLower();
                    if (url.StartsWith("http"))
                    {
                        eventArgs.Cancel = true;
                        ICommand openLink = new OpenLinkCommand(url);
                        openLink.Execute(null);
                    }
                };
            }
        }

        /// <inheritdoc/>
        public override void Reload()
        {
            base.Reload();
            string currentLanguage = settingManager.GetValue<string>("Language");
            if (currentLanguage == null)
            {
                return;
            }
            BrowserContent = documentManager?.ReadConvertedDocument(currentLanguage, documentToShow);
        }
    }
}
