using CommunityPatchLauncherFramework.Documentation.Strategy;
using System.Threading.Tasks;

namespace CommunityPatchLauncherFramework.Documentation.Manager
{
    /// <summary>
    /// This class will allow you to load documents via a given connector
    /// </summary>
    public class DocumentManager
    {
        /// <summary>
        /// The strategy used to convert the document into another format
        /// </summary>
        private readonly IDocumentConvertStrategy documentConvertStrategy;

        /// <summary>
        /// The connector to use to load data from
        /// </summary>
        private readonly IDocumentConnectorStrategy connectorStrategy;

        /// <summary>
        /// The base path of the document storage
        /// </summary>
        private readonly string basePath;

        /// <summary>
        /// Create a new instance of this manager class
        /// </summary>
        /// <param name="basePath">The base path of the document storage</param>
        /// <param name="fallbackLanguage">The fallback language to use</param>
        /// <param name="documentConvertStrategy">The strategy to use to convert to document into another format</param>
        /// <param name="connectorStrategy">The connector to use for loading the document</param>
        public DocumentManager(
            string basePath,
            string fallbackLanguage,
            IDocumentConvertStrategy documentConvertStrategy,
            IDocumentConnectorStrategy connectorStrategy)
        {
            this.basePath = basePath;
            this.documentConvertStrategy = documentConvertStrategy;
            this.connectorStrategy = connectorStrategy;
            this.connectorStrategy.SetFallbackLanguage(fallbackLanguage);
        }

        /// <summary>
        /// Read the whole document
        /// </summary>
        /// <param name="language">The language to use for the document to load</param>
        /// <param name="document">The name of the document to load</param>
        /// <returns>The whole document as a string</returns>
        public string ReadDocument(string language, string document)
        {
            return connectorStrategy?.ReadDocument(basePath, language, document);
        }

        /// <summary>
        /// Read the whole document async
        /// </summary>
        /// <param name="language">The language to use for the document to load</param>
        /// <param name="document">The name of the document to load</param>
        /// <returns>The whole document as a string</returns>
        public async Task<string> ReadDocumentAsync(string language, string document)
        {
            Task<string> returnTask = Task.Run(() =>
            {
                return connectorStrategy.ReadDocument(basePath, language, document);
            });

            return await returnTask;
        }

        /// <summary>
        /// Read the whole document and convert it. This will use the default startegy for conversion
        /// </summary>
        /// <param name="language">The language to use for the document to load</param>
        /// <param name="document">The name of the document to load</param>
        /// <returns>The whole document converted with the convert startegy</returns>
        public string ReadConvertedDocument(string language, string document)
        {
            return ReadConvertedDocument(language, document, documentConvertStrategy);
        }

        /// <summary>
        /// Read the whole document async and convert it. This will use the default startegy for conversion
        /// </summary>
        /// <param name="language">The language to use for the document to load</param>
        /// <param name="document">The name of the document to load</param>
        /// <returns>The whole document converted with the convert startegy</returns>
        public async Task<string> ReadConvertedDocumentAsync(string language, string document)
        {
            Task<string> returnTask = Task.Run(() =>
            {
                return ReadConvertedDocument(language, document, documentConvertStrategy);
            });

            return await returnTask;
        }

        /// <summary>
        /// Read the whole document and convert it
        /// </summary>
        /// <param name="language">The language to use for the document to load</param>
        /// <param name="document">The name of the document to load</param>
        /// <param name="convertStrategy">The convert strategy to use</param>
        /// <returns>The whole document converted with the convert startegy</returns>
        public string ReadConvertedDocument(
            string language,
            string document,
            IDocumentConvertStrategy convertStrategy
            )
        {
            return convertStrategy?.GetConverted(ReadDocument(language, document));
        }

        /// <summary>
        /// Read the whole async document and convert it
        /// </summary>
        /// <param name="language">The language to use for the document to load</param>
        /// <param name="document">The name of the document to load</param>
        /// <param name="convertStrategy">The convert strategy to use</param>
        /// <returns>The whole document converted with the convert startegy</returns>
        public async Task<string> ReadConvertedDocumentAsync(
            string language,
            string document,
            IDocumentConvertStrategy convertStrategy
            )
        {
            Task<string> returnTask = Task.Run(() =>
            {
                return convertStrategy?.GetConverted(ReadDocument(language, document));
            });
            return await returnTask;
        }
    }
}
