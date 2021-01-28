namespace CommunityPatchLauncherFramework.Documentation.Strategy
{
    /// <summary>
    /// This interface defineds a document connector to read the data from it
    /// </summary>
    public interface IDocumentConnectorStrategy
    {
        /// <summary>
        /// Set the fallback language of this connector
        /// </summary>
        /// <param name="fallbackLanguage"></param>
        void SetFallbackLanguage(string fallbackLanguage);

        /// <summary>
        /// Read the whole document and return it
        /// </summary>
        /// <param name="basePath">The base path to use</param>
        /// <param name="language">The language code to use</param>
        /// <param name="document">The name of the document to load</param>
        /// <returns>The document content as string</returns>
        string ReadDocument(string basePath, string language, string document);
    }
}
