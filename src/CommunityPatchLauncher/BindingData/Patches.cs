using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Enums;
using System;
using System.Collections.Generic;

namespace CommunityPatchLauncher.BindingData
{
    /// <summary>
    /// This class will return you a list with all the available patches
    /// </summary>
    public class Patches
    {
        public IReadOnlyList<Patch> AvailablePatch => availablePatch;
        private readonly List<Patch> availablePatch;

        public Patches()
        {
            availablePatch = new List<Patch>();
            foreach (AvailablePatches currentPatch in Enum.GetValues(typeof(AvailablePatches)))
            {
                availablePatch.Add(new Patch(currentPatch));
            }
        }
    }
}
