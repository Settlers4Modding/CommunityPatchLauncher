using CommunityPatchLauncher.BindingData.Container;

namespace CommunityPatchLauncher.Commands.DataContainer
{
    /// <summary>
    /// This class is a data container for the accept agreement command
    /// </summary>
    internal class AcceptAgreementData
    {
        /// <summary>
        /// Was the agreement accepted
        /// </summary>
        public bool Agreement { get; }

        /// <summary>
        /// Is the game folder set and okay
        /// </summary>
        public bool FolderSet { get; }

        /// <summary>
        /// The game folder to use
        /// </summary>
        public string GameFolder { get; }

        /// <summary>
        /// Check for update on launcher startup
        /// </summary>
        public bool UpdateOnStartup { get; }

        /// <summary>
        /// The language to use
        /// </summary>
        public LanguageItem Language { get; }

        /// <summary>
        /// Checksum of the agreement text
        /// </summary>
        public string Checksum { get;  }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="agreement">Was the agreement accepted</param>
        /// <param name="folderSet">Is the game folder which was set okay</param>
        /// <param name="gameFolder">The path to the game folder</param>
        /// <param name="language">The language to use</param>
        /// <param name="checkOnStartup">Check for update on app startup</param>
        /// <param name="checksum">The Checksum generated from the fallback language agreement</param>
        public AcceptAgreementData(
            bool agreement,
            bool folderSet,
            string gameFolder,
            LanguageItem language,
            bool checkOnStartup,
            string checksum
            )
        {
            Agreement = agreement;
            FolderSet = folderSet;
            GameFolder = gameFolder;
            Language = language;
            UpdateOnStartup = checkOnStartup;
            Checksum = checksum;
        }
    }
}
