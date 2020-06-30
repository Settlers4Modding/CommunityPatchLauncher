using CommunityPatchLauncherFramework.Documentation.Strategy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommunityPatchLauncherFramework.Documentation.Manager
{
    public class DocumentManager
    {
        private readonly IDocumentConvertStrategy documentConvertStrategy;
        private readonly IDocumentConnectorStrategy connectorStrategy;
        private readonly string basePath;

        public DocumentManager(
            string basePath,
            string fallbackLanguage,
            IDocumentConvertStrategy documentConvertStrategy,
            IDocumentConnectorStrategy connectorStrategy)
        {
            this.basePath = basePath.Last() == '\\' ? basePath : basePath + "\\";
            this.documentConvertStrategy = documentConvertStrategy;
            this.connectorStrategy = connectorStrategy;
            this.connectorStrategy.SetFallbackLanguage(fallbackLanguage);
        }

        public string ReadDocument(string language, string document)
        {
            return connectorStrategy?.ReadDocument(basePath, language, document);
        }

        public string ReadConvertedDocument(string language, string document)
        {
            return ReadConvertedDocument(language, document, documentConvertStrategy);
        }

        public string ReadConvertedDocument(string language, string document, IDocumentConvertStrategy convertStrategy)
        {
            return convertStrategy?.GetConverted(ReadDocument(language, document));
        }
    }
}
