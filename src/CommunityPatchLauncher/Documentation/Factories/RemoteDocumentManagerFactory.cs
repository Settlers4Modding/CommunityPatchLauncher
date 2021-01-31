using CommunityPatchLauncherFramework.Documentation.Strategy;

namespace CommunityPatchLauncher.Documentation.Factories
{
    /// <summary>
    /// This class will create a remote document instance
    /// </summary>
    public class RemoteDocumentManagerFactory : LocalDocumentManagerFactory
    {
        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public RemoteDocumentManagerFactory()
        {
            assemblyPath = Properties.Settings.Default.MarkdownOnlineBasePath;
        }

        /// <inheritdoc/>
        protected override IDocumentConnectorStrategy GetConvertStrategy()
        {
            return new RemoteDocumentConnectorStrategy(60);
        }
    }
}
