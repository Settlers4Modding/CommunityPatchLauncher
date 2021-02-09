using CommunityPatchLauncher.Enums;
using System;
using System.Collections.Generic;
using System.IO;

namespace CommunityPatchLauncher.Commands.Condition
{
    /// <summary>
    /// This class will check if the patch file is valid
    /// </summary>
    internal class IsAvailablePatchCondition : BaseCondition
    {
        /// <summary>
        /// The allowed extension
        /// </summary>
        private readonly string allowedExtension;

        /// <summary>
        /// A list with all allowed patch names
        /// </summary>
        private readonly List<string> patches;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public IsAvailablePatchCondition()
        {
            allowedExtension = ".7z";
            patches = new List<string>();
            foreach (AvailablePatches currentPatch in Enum.GetValues(typeof(AvailablePatches)))
            {
                patches.Add(currentPatch.ToString());
            }
        }

        /// <inheritdoc/>
        public override bool ConditionFullfilled(object parameter)
        {
            if (parameter is string sourceFile && File.Exists(sourceFile))
            {
                FileInfo info = new FileInfo(sourceFile);
                string sourceName = info.Name;
                string extension = info.Extension;
                sourceName = sourceName.Replace(extension, "");
                extension = extension.ToLower();

                if (!patches.Contains(sourceName) || extension != allowedExtension)
                {
                    return false;
                }

                return true;
            }
            return false;
        }
    }
}
