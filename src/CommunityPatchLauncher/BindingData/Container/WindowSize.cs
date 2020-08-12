using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPatchLauncher.BindingData.Container
{
    public class WindowSize
    {
        public string DisplayName { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public WindowSize(int width, int height) : this("", width, height)
        {
            DisplayName = width.ToString() + " x " + height.ToString();
        }

        public WindowSize(string displayName, int width, int height)
        {
            DisplayName = displayName;
            Width = width;
            Height = height;
        }
    }
}
