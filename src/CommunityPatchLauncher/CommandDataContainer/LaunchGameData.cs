using CommunityPatchLauncher.Enums;

namespace CommunityPatchLauncher.CommandDataContainer
{
    internal class LaunchGameData
    {
        public AvailablePatches Patch { get; }
        public SpeedModes Speed { get; }

        public LaunchGameData(AvailablePatches patch, SpeedModes speed)
        {
            Patch = patch;
            Speed = speed;
        }
    }
}
