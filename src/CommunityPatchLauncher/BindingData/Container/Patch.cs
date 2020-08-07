using CommunityPatchLauncher.Enums;
using System.Collections.Generic;

namespace CommunityPatchLauncher.BindingData.Container
{
    public class Patch
    {
        public string Name { get; }
        public AvailablePatches RealPatch { get; }

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
