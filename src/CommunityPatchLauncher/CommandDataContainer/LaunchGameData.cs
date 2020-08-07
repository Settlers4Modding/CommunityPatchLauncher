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
        string VersionInformationUrl { get; }
        AvailablePatches Patch { get; }
        SpeedModes Speed { get; }

        public LaunchGameData()
        {

        }
    }
}
