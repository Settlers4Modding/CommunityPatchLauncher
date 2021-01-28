using System.IO;

namespace CommunityPatchLauncherFramework.Documentation.Strategy
{
    /// <summary>
    /// This connector will read the document from the local disc
    /// </summary>
    public class LocalDocumentConnectorStrategy : IDocumentConnectorStrategy
    {
        /// <summary>
        /// The fallback language to use
        /// </summary>
        private string fallbackLanguage;

        /// <inheritdoc/>
        public void SetFallbackLanguage(string fallbackLanguage)
        {
            this.fallbackLanguage = fallbackLanguage;
        }

        /// <inheritdoc/>
        public string ReadDocument(string basePath, string language, string document)
        {
            return ReadDocument(basePath, language, document, true);
        }

        protected string ReadDocument(string basePath, string language, string document, bool initialCall)
        {
            string path = basePath + language;
            if (!Directory.Exists(path))
            {
                path = basePath + fallbackLanguage;
            }
            path += "\\" + document; ;
            if (!File.Exists(path))
            {
                if (initialCall)
                {
                    return ReadDocument(basePath, fallbackLanguage, document, false);
                }
                return string.Empty;
            }

            return File.ReadAllText(path);
        }
    }
}
