using CommunityPatchLauncher.BindingData.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPatchLauncher.Commands.DataContainer
{
    internal class ResizeWindowData
    {
        public int Width { get; }
        public int Height { get; }
        public bool Custom { get; }

        public ResizeWindowData(WindowSize windowSize) : this(windowSize.Width, windowSize.Height, false)
        {
        }

        public ResizeWindowData(int width, int height, bool custom)
        {
            Width = width;
            Height = height;
            Custom = custom;
        }
    }
}
