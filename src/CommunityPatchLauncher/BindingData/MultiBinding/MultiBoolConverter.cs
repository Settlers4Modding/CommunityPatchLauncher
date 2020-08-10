using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace CommunityPatchLauncher.BindingData.MultiBinding
{
    /// <summary>
    /// This class will check multiple bools and return false if one of them is false
    /// </summary>
    internal class MultiBoolConverter : IMultiValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool[] boolValues = Array.ConvertAll(values, item => (bool)item);
            return boolValues.All(currentValue => currentValue);
        }

        /// <inheritdoc/>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
