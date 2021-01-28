using CommunityPatchLauncher.Enums;
using System.Collections.Generic;

namespace CommunityPatchLauncher.BindingData.Container
{
    /// <summary>
    /// A new patch you can use for downloading
    /// </summary>
    public class Patch
    {
        /// <summary>
        /// The display name of the patch
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The enum value for the patch
        /// </summary>
        public AvailablePatches RealPatch { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="realPatch">The real patch to use</param>
        public Patch(AvailablePatches realPatch)
        {

            char lastChar = ' ';
            int currentPosition = 0;
            List<int> spacePositions = new List<int>();
            string name = realPatch.ToString();
            foreach (char currentChar in name)
            {
                if (lastChar != ' ')
                {
                    if (char.IsLower(lastChar) && char.IsUpper(currentChar))
                    {
                        spacePositions.Add(currentPosition);
                    }
                }
                lastChar = currentChar;
                currentPosition++;
            }
            currentPosition = 0;
            foreach (int position in spacePositions)
            {
                int realPosition = position + currentPosition;
                name = name.Insert(realPosition, " ");
                currentPosition++;
            }
            Name = name;
            RealPatch = realPatch;
        }
    }
}
