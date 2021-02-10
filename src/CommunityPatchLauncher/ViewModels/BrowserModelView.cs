using CommunityPatchLauncher.Commands.ApplicationWindow;
using CommunityPatchLauncher.Documentation.Factories;
using CommunityPatchLauncherFramework.Documentation.Factory;
using CommunityPatchLauncherFramework.Documentation.Manager;
using CommunityPatchLauncherFramework.Documentation.Strategy;
using System.Threading.Tasks;
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
        private DocumentManager documentManager;

        /// <summary>
        /// The name of the document to show
        /// </summary>
        private string documentToShow;

        /// <summary>
        /// Fallback manager to use
        /// </summary>
        private DocumentManager fallbackManager;

        /// <summary>
        /// Show loading placeholder
        /// </summary>
        private bool showLoading;

        /// <summary>
        /// Create a new instance of this view model
        /// </summary>
        /// <param name="documentToShow"></param>
        public BrowserModelView(string documentToShow, UserControl control, IDocumentManagerFactory factoryToUse)
        {
            showLoading = true;
            ChangeDocumentProdiver(factoryToUse);
            this.documentToShow = documentToShow == string.Empty ? "NotReadable.md" : documentToShow;

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
                    if (url.ToLower().StartsWith("http"))
                    {
                        eventArgs.Cancel = true;
                        ICommand openLink = new OpenLinkCommand(url);
                        openLink.Execute(null);
                    }
                    if (url.ToLower().StartsWith("about") && url.ToLower().EndsWith(".md"))
                    {
                        string currentLanguage = settingManager.GetValue<string>("Language");
                        string target = url.Replace("about:", "");
                        LoadNewContent(currentLanguage, target);
                    }
                };
            }
        }

        /// <summary>
        /// Show loading replacement
        /// </summary>
        /// <param name="newValue"></param>
        public void ShowLoading(bool newValue)
        {
            showLoading = newValue;
        }

        /// <summary>
        /// Change the document provider
        /// </summary>
        /// <param name="factoryToUse">The factory to use</param>
        public void ChangeDocumentProdiver(IDocumentManagerFactory factoryToUse)
        {
            ChangeDocumentProdiver(factoryToUse.GetDocumentManager(
                Properties.Settings.Default.FallbackLanguage,
                new MarkdownHtmlConvertStrategy()
                ));
        }

        /// <summary>
        /// Change the document provider
        /// </summary>
        /// <param name="manager">The manager to use</param>
        public void ChangeDocumentProdiver(DocumentManager manager)
        {
            documentManager = manager;
        }

        /// <summary>
        /// Change the document to render
        /// </summary>
        /// <param name="newDocument">The new document to use</param>
        public void ChangeDocument(string newDocument)
        {
            documentToShow = newDocument;
            Reload();
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
            LoadNewContent(currentLanguage, documentToShow);
        }

        /// <summary>
        /// Load the new content in the browser
        /// </summary>
        /// <param name="currentLanguage">The current language</param>
        /// <param name="target">The current target</param>
        private void LoadNewContent(string currentLanguage, string target)
        {
            Task<string> contentData = documentManager?.ReadConvertedDocumentAsync(currentLanguage, target);
            contentData.ContinueWith((data) =>
            {
                ChangeBrowserData(data, currentLanguage);
            });

            if (showLoading)
            {
                BrowserContent = GetFallbackManager()?.ReadConvertedDocument(currentLanguage, "Loading.md");
            }
        }

        /// <summary>
        /// Change the browser data
        /// </summary>
        /// <param name="data">The data to use</param>
        /// <param name="currentLanguage">The current language</param>
        private void ChangeBrowserData(Task<string> data, string currentLanguage)
        {
            BrowserContent = data.Result == string.Empty ?
            GetFallbackManager().ReadConvertedDocument(
                currentLanguage,
                Properties.Settings.Default.NotReadableFile
            ) :
            data.Result;
        }

        /// <summary>
        /// Get the fallback managers
        /// </summary>
        /// <returns>A fallback document manager</returns>
        private DocumentManager GetFallbackManager()
        {
            if (fallbackManager != null)
            {
                return fallbackManager;
            }

            IDocumentManagerFactory factory = new LocalDocumentManagerFactory();
            fallbackManager = factory.GetDocumentManager(
                Properties.Settings.Default.FallbackLanguage,
                new MarkdownHtmlConvertStrategy()
                );
            return GetFallbackManager();
        }
    }
}
