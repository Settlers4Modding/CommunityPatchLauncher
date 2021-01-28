using CommunityPatchLauncher.Enums;

namespace CommunityPatchLauncher.BindingData.Container
{
    /// <summary>
    /// Container class for update channels
    /// </summary>
    public class UpdateChannelContainer
    {
        /// <summary>
        /// The update branch to use
        /// </summary>
        public UpdateBranchEnum UpdateBranch { get; private set; }

        /// <summary>
        /// The display name to use
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// The update channel container
        /// </summary>
        /// <param name="updateBranch">The update branch to use</param>
        public UpdateChannelContainer(UpdateBranchEnum updateBranch)
        {
            UpdateBranch = updateBranch;
            DisplayName = Properties.Resources.ResourceManager.GetString(updateBranch.ToString());
            DisplayName = DisplayName ?? updateBranch.ToString();
        }
    }
}
