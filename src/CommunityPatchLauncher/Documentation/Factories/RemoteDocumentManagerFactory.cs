using CommunityPatchLauncherFramework.Documentation.Strategy;
using System;

namespace CommunityPatchLauncher.Documentation.Factories
{
    /// <summary>
    /// This class will create a remote document instance
    /// </summary>
    public class RemoteDocumentManagerFactory : LocalDocumentManagerFactory
    {
        /// <summary>
        /// The timespan beween request times
        /// </summary>
        private readonly TimeSpan requestTimeSpan;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public RemoteDocumentManagerFactory() : this(new TimeSpan(1, 0, 0))
        {
        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public RemoteDocumentManagerFactory(TimeSpan timeSpan) : this(timeSpan, Properties.Settings.Default.MarkdownOnlineBasePath)
        {
            requestTimeSpan = timeSpan;
        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public RemoteDocumentManagerFactory(TimeSpan timeSpan, string basePath) : base(basePath)
        {
            requestTimeSpan = timeSpan;
        }

        /// <inheritdoc/>
        protected override IDocumentConnectorStrategy GetConvertStrategy()
        {
            return new RemoteDocumentConnectorStrategy(requestTimeSpan, Properties.Settings.Default.MarkdownOnlineBasePath);
        }
    }
}
