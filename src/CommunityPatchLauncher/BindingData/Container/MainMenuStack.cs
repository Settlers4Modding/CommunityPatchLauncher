using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPatchLauncher.BindingData.Container
{
    public class MainMenuStack
    {
        public string DisplayName { get; }
        public string ImagePath { get; }

        public MainMenuStack(string displayName, string imagePath)
        {
            ImagePath = imagePath;
            DisplayName = displayName;
        }
    }
}
