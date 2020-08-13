using System;
using System.Globalization;
using System.Windows.Data;

namespace CommunityPatchLauncher.BindingData.Converter
{
    /// <summary>
    /// This converter inverts a bool value
    /// </summary>
    internal class InvertBoolConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }
            return false;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
