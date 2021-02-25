using CommunityPatchLauncherFramework.Settings.Container;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CommunityPatchLauncherFramework.Settings.Writer
{
    /// <summary>
    /// Write settings file in the xml format
    /// </summary>
    public class XmlSettingWriter : ISettingWriter
    {

        /// <inheritdoc/>
        public bool WriteSettings(HashSet<SettingPair> settings, string connectionString)
        {
            FileInfo fileInfo = new FileInfo(connectionString);
            if (!CreateFolderIfNeeded(fileInfo.DirectoryName))
            {
                return false;
            }

            SerialaizeableSettingFile saveableSettings = new SerialaizeableSettingFile(settings);
            XmlSerializer serializer = new XmlSerializer(typeof(SerialaizeableSettingFile));
            using (TextWriter writer = new StreamWriter(connectionString))
            {
                try
                {
                    serializer.Serialize(writer, saveableSettings);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Create the settings folder if needed
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private bool CreateFolderIfNeeded(string connectionString)
        {
            if (!Directory.Exists(connectionString))
            {
                try
                {
                    Directory.CreateDirectory(connectionString);
                }
                catch (Exception)
                {
                    /// Maybe we need some error handling later on
                    return false;
                }
            }
            return true;
        }
    }
}
