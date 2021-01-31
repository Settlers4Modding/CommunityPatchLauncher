namespace CommunityPatchLauncherFramework.Documentation.Strategy
{
    /// <summary>
    /// Abstract class for document connector
    /// </summary>
    public abstract class BaseDocumentConnectorStrategy : IDocumentConnectorStrategy
    {
        /// <summary>
        /// The fallback language to use
        /// </summary>
        protected string fallbackLanguage;

        /// <inheritdoc/>
        public virtual string ReadDocument(string basePath, string language, string document)
        {
            basePath = CorrectBasePath(basePath);
            return ReadDocument(basePath, language, document, true);
        }

        /// <inheritdoc/>
        public void SetFallbackLanguage(string fallbackLanguage)
        {
            this.fallbackLanguage = fallbackLanguage;
        }

        /// <summary>
        /// Convert the base path to the correct one
        /// </summary>
        /// <param name="basePath">The base path to convert</param>
        /// <returns>The converted base path</returns>
        protected abstract string CorrectBasePath(string basePath);

        /// <summary>
        /// Read the document for real
        /// </summary>
        /// <param name="basePath">THe base path</param>
        /// <param name="language">The language to use</param>
        /// <param name="document">The document to read</param>
        /// <param name="initialCall">Is this called for the first time</param>
        /// <returns>The read string</returns>
        protected abstract string ReadDocument(string basePath, string language, string document, bool initialCall);


    }
}
