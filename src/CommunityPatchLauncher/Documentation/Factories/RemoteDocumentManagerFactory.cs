using CommunityPatchLauncherFramework.Documentation.Strategy;

namespace CommunityPatchLauncher.Documentation.Factories
{
    public class RemoteDocumentManagerFactory : LocalDocumentManagerFactory
    {
        public RemoteDocumentManagerFactory()
        {
            assemblyPath = Properties.Settings.Default.MarkdownOnlineBasePath;
        }

        protected override IDocumentConnectorStrategy GetConvertStrategy()
        {
            return new RemoteDocumentConnectorStrategy(60);
        }
    }
}
