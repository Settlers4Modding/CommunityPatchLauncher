using CommunityPatchLauncher.Enums;
using System;
using System.Collections.Generic;

namespace CommunityPatchLauncher.BindingData.Container
{
    /// <summary>
    /// This model will make the update channels accessable
    /// </summary>
    internal class UpdateChannelModel
    {
        /// <summary>
        /// The update channels whoch can be used
        /// </summary>
        private readonly List<UpdateChannelContainer> updateChannels;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public UpdateChannelModel()
        {
            updateChannels = new List<UpdateChannelContainer>();
            foreach (UpdateBranchEnum option in Enum.GetValues(typeof(UpdateBranchEnum)))
            {
                updateChannels.Add(new UpdateChannelContainer(option));
            }
        }

        /// <summary>
        /// Get all the update channels
        /// </summary>
        /// <returns>The usable update channels</returns>
        public IReadOnlyList<UpdateChannelContainer> GetUpdateChannels() => updateChannels;
    }
}
