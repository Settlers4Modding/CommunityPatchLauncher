using CommunityPatchLauncherFramework.Documentation.Factory;
using CommunityPatchLauncherFramework.Documentation.Manager;
using CommunityPatchLauncherFramework.Documentation.Strategy;
using System.IO;
using System.Reflection;

namespace CommunityPatchLauncher.Factories
{
    /// <summary>
    /// Create a document manager ready to use
    /// </summary>
    public class LocalDocumentManagerFactory : IDocumentManagerFactory
    {
        /// <summary>
        /// The directory this assembly is in
        /// </summary>
        private readonly string assemblyPath;

        /// <summary>
        /// Create a new instance of this factory
        /// </summary>
        public LocalDocumentManagerFactory()
        {
            FileInfo fileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
            assemblyPath = fileInfo.DirectoryName + "\\docs";
        }

        /// <inheritdoc/>
        public DocumentManager GetDocumentManager(string fallbackLanguage, IDocumentConvertStrategy convertStrategy)
        {
            return new DocumentManager(
                assemblyPath,
                fallbackLanguage,
                convertStrategy,
                new LocalDocumentConnectorStrategy());
        }
    }
}
