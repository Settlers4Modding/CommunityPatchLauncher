using CommunityPatchLauncher.BindingData.Container;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CommunityPatchLauncher.BindingData
{
    /// <summary>
    /// This class will get you all the possible window sizes
    /// </summary>
    public class WindowsSizeService
    {
        /// <summary>
        /// Get all the window sizes from the resource file
        /// </summary>
        /// <returns>A list with all the window sizes</returns>
        public IReadOnlyList<WindowSize> GetWindowSizes()
        {
            List<WindowSize> windowSizes = new List<WindowSize>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("CommunityPatchLauncher.Assets.Resources.PredefinedWindowSizes.txt"))
            {
                if (stream == null)
                {
                    return default;   
                }
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (!line.Contains(";"))
                        {
                            continue;
                        }
                        string[] widthAndHeight = line.Split(';');
                        if (widthAndHeight.Length != 2)
                        {
                            continue;
                        }
                        int width;
                        int height;
                        if (!int.TryParse(widthAndHeight[0], out width) || !int.TryParse(widthAndHeight[1], out height))
                        {
                            continue;
                        }

                        windowSizes.Add(new WindowSize(width, height));
                    }
                }

            }
            return windowSizes;
        }
    }
}
