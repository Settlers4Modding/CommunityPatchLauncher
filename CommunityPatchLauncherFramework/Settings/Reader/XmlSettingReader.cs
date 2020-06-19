using CommunityPatchLauncherFramework.Settings.Container;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CommunityPatchLauncherFramework.Settings.Reader
{
    /// <summary>
    /// Read settings files in xml format
    /// </summary>
    public class XmlSettingReader : ISettingReader
    {
        /// <inheritdoc/>
        public HashSet<SettingPair> GetAllSettings(string connectionString)
        {
            if (!File.Exists(connectionString))
            {
                return default;
            }

            SerialaizeableSettingFile data = null;
            XmlSerializer serializer = new XmlSerializer(typeof(SerialaizeableSettingFile));
            using (TextReader reader = new StreamReader(connectionString))
            {
                try
                {
                    data = (SerialaizeableSettingFile)serializer.Deserialize(reader);

                }
                catch (Exception)
                {
                    ///Loading did fail!
                    return default;
                }


            }
            return data == null ? default : data.GetSettingPairs();
        }

        /// <inheritdoc/>
        public SettingPair LoadSetting(string key, string connectionString)
        {
            throw new NotImplementedException();
        }
    }
}
