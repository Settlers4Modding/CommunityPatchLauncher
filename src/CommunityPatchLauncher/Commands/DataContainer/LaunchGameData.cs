using CommunityPatchLauncher.Enums;

namespace CommunityPatchLauncher.Commands.DataContainer
{
    /// <summary>
    /// Data needed to launch the game
    /// </summary>
    internal class LaunchGameData
    {
        /// <summary>
        /// The patch to use for launching
        /// </summary>
        public AvailablePatches Patch { get; }

        /// <summary>
        /// The speed mode to use for launching
        /// </summary>
        public SpeedModes Speed { get; }

        /// <summary>
        /// Create a new instance of this data container
        /// </summary>
        /// <param name="patch">The patch to use for launching</param>
        /// <param name="speed">The speed to use for launching</param>
        public LaunchGameData(AvailablePatches patch, SpeedModes speed)
        {
            Patch = patch;
            Speed = speed;
        }
    }
}
