using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPatchLauncher.BindingData
{
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
