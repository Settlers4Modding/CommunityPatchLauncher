namespace CommunityPatchLauncher.BindingData.Container
{
    /// <summary>
    /// This class represents window sizes
    /// </summary>
    public class WindowSize
    {
        /// <summary>
        /// The name to show for this size
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// The width of this size
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// The height of this size
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="width">The widtg to use</param>
        /// <param name="height">The heigt to use</param>
        public WindowSize(int width, int height) : this("", width, height)
        {
            DisplayName = width.ToString() + " x " + height.ToString();
        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="displayName">The displayname</param>
        /// <param name="width">The widtg to use</param>
        /// <param name="height">The heigt to use</param>
        public WindowSize(string displayName, int width, int height)
        {
            DisplayName = displayName;
            Width = width;
            Height = height;
        }
    }
}
