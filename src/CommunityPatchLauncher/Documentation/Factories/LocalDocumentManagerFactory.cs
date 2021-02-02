using CommunityPatchLauncherFramework.Documentation.Factory;
using CommunityPatchLauncherFramework.Documentation.Manager;
using CommunityPatchLauncherFramework.Documentation.Strategy;
using System.IO;
using System.Reflection;

namespace CommunityPatchLauncher.Documentation.Factories
{
    /// <summary>
    /// Create a document manager ready to use
    /// </summary>
    public class LocalDocumentManagerFactory : IDocumentManagerFactory
    {
        /// <summary>
        /// The directory this assembly is in
        /// </summary>
        protected string basePath;

        /// <summary>
        /// Create a new instance of this factory
        /// </summary>
        public LocalDocumentManagerFactory()
        {
            FileInfo fileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
            basePath = fileInfo.DirectoryName + "\\docs";
        }

        /// <summary>
        /// Create a new instance of this factory
        /// </summary>
        /// <param name="basePath">The base path to use</param>
        public LocalDocumentManagerFactory(string basePath)
        {
            this.basePath = basePath;
        }

        /// <summary>
        /// This method will get you the connector strategy to use
        /// </summary>
        /// <returns>A valid instance of the connector strategy</returns>
        protected virtual IDocumentConnectorStrategy GetConvertStrategy()
        {
            return new LocalDocumentConnectorStrategy();
        }

        /// <inheritdoc/>
        public DocumentManager GetDocumentManager(string fallbackLanguage, IDocumentConvertStrategy convertStrategy)
        {
            return new DocumentManager(
                basePath,
                fallbackLanguage,
                convertStrategy,
                GetConvertStrategy());
        }
    }
}
