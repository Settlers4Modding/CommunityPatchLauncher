using CommunityPatchLauncher.Documentation.Factories;
using CommunityPatchLauncherFramework.Documentation.Factory;
using CommunityPatchLauncherFramework.Documentation.Manager;
using CommunityPatchLauncherFramework.Documentation.Strategy;

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
        public BrowserModelView(string documentToShow)
        {
            IDocumentManagerFactory factory = new LocalDocumentManagerFactory();
            documentManager = factory.GetDocumentManager("en-EN", new MarkdownHtmlConvertStrategy());
            this.documentToShow = documentToShow;
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
