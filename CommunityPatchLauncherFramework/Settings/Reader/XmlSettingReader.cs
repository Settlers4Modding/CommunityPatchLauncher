using CommunityPatchLauncherFramework.Settings.Container;
using System;
using System.Collections.Generic;

namespace CommunityPatchLauncherFramework.Settings.Reader
{
    /// <summary>
    /// Read settings files in xml format
    /// </summary>
    public class XmlSettingReader : ISettingReader
    {
        /// <inheritdoc/>
        public HashSet<SettingPair> GetAllSettings()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public SettingPair LoadSetting(string key, string connectionString)
        {
            throw new NotImplementedException();
        }
    }
}
