using CommunityPatchLauncherFramework.Settings.Container;
using System.Collections.Generic;

namespace CommunityPatchLauncherFramework.Settings.Writer
{
    /// <summary>
    /// This class will not write anything it's just a valid interface implementation
    /// </summary>
    public class DummySettingWriter : ISettingWriter
    {
        /// <inheritdoc/>
        public bool WriteSettings(HashSet<SettingPair> settings, string connectionString)
        {
            return true;
        }
    }
}
