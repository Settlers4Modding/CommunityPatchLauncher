using System;
using System.Globalization;
using System.Windows.Data;

namespace CommunityPatchLauncher.BindingData.Converter
{
    /// <summary>
    /// This class will convert a string to int
    /// </summary>
    internal class StringToIntConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                return value.ToString();
            }
            return "";
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int returnValue = 0;
            if (value is string stringValue)
            {
                int.TryParse(stringValue, out returnValue);
            }

            return returnValue;
        }
    }
}
