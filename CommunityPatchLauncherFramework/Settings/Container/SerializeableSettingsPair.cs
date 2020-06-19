using System;

namespace CommunityPatchLauncherFramework.Settings.Container
{
    /// <summary>
    /// This class will allow you to serialize a setting pair
    /// </summary>
    public class SerializeableSettingsPair
    {
        /// <summary>
        /// The key of the setting pair
        /// </summary>
        public string Key;

        /// <summary>
        /// The value of the setting pair
        /// </summary>
        public string Value;

        /// <summary>
        /// Get the type of the value
        /// </summary>
        public string ValueType;
        
        /// <summary>
        /// Create an empty instance of this class
        /// </summary>
        public SerializeableSettingsPair()
        {
        }

        /// <summary>
        /// Create a instance of this class from a given setting pair
        /// </summary>
        /// <param name="settingPair"></param>
        public SerializeableSettingsPair(SettingPair settingPair)
        {
            Key = settingPair.Key;
            Value = settingPair.Value.ToString();
            ValueType = settingPair.ValueType.ToString();
        }

        /// <summary>
        /// Return a setting pair instead of this class instance
        /// </summary>
        /// <returns>A ready to use setting pair</returns>
        public SettingPair GetSettingPair()
        {
            Type type = Type.GetType(ValueType);
            object data = Convert.ChangeType(Value, type);
            return new SettingPair(Key, data, type);
        }
    }
}
