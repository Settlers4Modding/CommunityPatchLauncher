using CommunityPatchLauncher.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPatchLauncher.CommandDataContainer
{
    internal class LaunchGameData
    {
        public string VersionInformationUrl { get; }
        public AvailablePatches Patch { get; }
        public SpeedModes Speed { get; }

        public LaunchGameData()
        {

        }
    }
}
