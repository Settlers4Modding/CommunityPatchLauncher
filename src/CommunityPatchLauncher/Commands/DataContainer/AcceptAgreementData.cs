using CommunityPatchLauncher.BindingData.Container;

namespace CommunityPatchLauncher.Commands.DataContainer
{
    internal class AcceptAgreementData
    {
        public bool Agreement { get; }
        public bool FolderSet { get; }
        public string GameFolder { get; }
        public LanguageItem Language { get; }

        public AcceptAgreementData(bool agreement, bool folderSet, string gameFolder, LanguageItem language)
        {
            Agreement = agreement;
            FolderSet = folderSet;
            GameFolder = gameFolder;
            Language = language;
        }
    }
}
