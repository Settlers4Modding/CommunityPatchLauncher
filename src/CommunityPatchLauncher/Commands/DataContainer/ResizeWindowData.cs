using CommunityPatchLauncher.BindingData.Container;

namespace CommunityPatchLauncher.Commands.DataContainer
{
    /// <summary>
    /// This class represents a resize window data set
    /// </summary>
    internal class ResizeWindowData
    {
        /// <summary>
        /// The witdh to resize to
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// The height to resize to
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Are those are custom values
        /// </summary>
        public bool Custom { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="windowSize">The window size to use</param>
        public ResizeWindowData(WindowSize windowSize) : this(windowSize.Width, windowSize.Height, false)
        {
        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="width">The width to use</param>
        /// <param name="height">The heigt to use</param>
        /// <param name="custom">Is this a custom ration</param>
        public ResizeWindowData(int width, int height, bool custom)
        {
            Width = width;
            Height = height;
            Custom = custom;
        }
    }
}
