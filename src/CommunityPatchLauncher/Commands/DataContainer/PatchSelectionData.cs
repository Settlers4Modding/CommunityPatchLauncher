using CommunityPatchLauncher.Enums;
using System.Windows.Controls.Primitives;

namespace CommunityPatchLauncher.Commands.DataContainer
{
    /// <summary>
    /// This class will contain all the data needed for the patch selection cmmand
    /// </summary>
    internal class PatchSelectionData
    {
        /// <summary>
        /// The currently clicked button
        /// </summary>
        public ToggleButton Button { get; }

        /// <summary>
        /// The currently selected patch
        /// </summary>
        public AvailablePatches Patch { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="button">The current button</param>
        /// <param name="Patch">The currently selected patch</param>
        public PatchSelectionData(ToggleButton button, AvailablePatches Patch)
        {
            Button = button;
            this.Patch = Patch;
        }

    }
}
