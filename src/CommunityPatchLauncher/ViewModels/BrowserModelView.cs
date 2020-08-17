using CommunityPatchLauncher.Documentation.Factories;
using CommunityPatchLauncherFramework.Documentation.Factory;
using CommunityPatchLauncherFramework.Documentation.Manager;
using CommunityPatchLauncherFramework.Documentation.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CommunityPatchLauncher.ViewModels
{
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

        private string browserContent;

        /// <summary>
        /// The document manager to use
        /// </summary>
        private readonly DocumentManager documentManager;

        /// <summary>
        /// The name of the document to show
        /// </summary>
        private readonly string documentToShow;

        public BrowserModelView(string documentToShow)
        {
            IDocumentManagerFactory factory = new LocalDocumentManagerFactory();
            documentManager = factory.GetDocumentManager("en-EN", new MarkdownHtmlConvertStrategy());
            this.documentToShow = documentToShow;
        }

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
