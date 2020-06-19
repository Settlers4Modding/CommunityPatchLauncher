using CommunityPatchLauncherFramework.Settings.Container;
using System;

namespace CommunityPatchLauncherFramework.Settings.Writer
{
    /// <summary>
    /// Write settings file in the xml format
    /// </summary>
    public class XmlSettingWriter : ISettingWriter
    {
        /// <inheritdoc/>
        public bool WriteSetting(string key, object value, string connectionString)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool WriteSetting(SettingPair settingPair, string connectionString)
        {
            throw new NotImplementedException();
        }
    }
}
