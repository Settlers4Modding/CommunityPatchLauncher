using CommunityPatchLauncherFramework.Settings.Manager;
using CommunityPatchLauncherFramework.Settings.Reader;
using CommunityPatchLauncherFramework.Settings.Writer;
using System;
using System.IO;

namespace CommunityPatchLauncherFramework.Settings.Factories
{
    /// <summary>
    /// Create a xml settings factory
    /// </summary>
    public class XmlSettingFactory : ISettingFactory
    {
        /// <summary>
        /// The reader to use
        /// </summary>
        private ISettingReader readerToUse;

        /// <summary>
        /// The writer to use
        /// </summary>
        private ISettingWriter writerToUse;

        /// <inheritdoc/>
        public SettingManager GetSettingsManager()
        {
            string settingFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            settingFile += Path.DirectorySeparatorChar;
            settingFile += "SIVCommunityPatchLauncher" + Path.DirectorySeparatorChar;
            settingFile += "settings.xml";
            return GetSettingsManager(settingFile);
        }

        /// <inheritdoc/>
        public SettingManager GetSettingsManager(string settingsConnectionString)
        {
            readerToUse = readerToUse ?? new XmlSettingReader();
            writerToUse = writerToUse ?? new XmlSettingWriter();
            return new SettingManager(readerToUse, writerToUse, settingsConnectionString);
        }
    }
}
