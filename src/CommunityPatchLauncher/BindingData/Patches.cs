﻿using CommunityPatchLauncher.BindingData.Container;
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
        /// <summary>
        /// Get all the patches
        /// </summary>
        /// <returns>Returns a list with all the available patches</returns>
        public IReadOnlyList<Patch> GetPatches()
        {
            List<Patch> availablePatch = new List<Patch>();
            foreach (AvailablePatches currentPatch in Enum.GetValues(typeof(AvailablePatches)))
            {
                if (currentPatch == AvailablePatches.CommunityPatchDLC)
                {
                    continue;
                }
                availablePatch.Add(new Patch(currentPatch));
            }
            return availablePatch;
        }
    }
}
