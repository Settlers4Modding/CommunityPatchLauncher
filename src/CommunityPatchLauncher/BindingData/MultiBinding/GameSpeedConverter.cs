using CommunityPatchLauncher.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace CommunityPatchLauncher.BindingData.MultiBinding
{
    /// <summary>
    /// This class is used for radio buttons game speeds
    /// </summary>
    class GameSpeedConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() == parameter.ToString();
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SpeedModes mode = SpeedModes.Unknown;
            bool realValue = (bool)value;
            if (realValue && parameter is string parameterAsString)
            {
                Enum.TryParse(parameterAsString, out mode);
            }
            return mode;
        }
    }
}
