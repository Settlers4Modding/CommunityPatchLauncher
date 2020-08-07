using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace CommunityPatchLauncher.BindingData.MultiBinding
{
    internal class MultiBoolConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool[] boolValues = Array.ConvertAll(values, item => (bool)item);
            return boolValues.All(currentValue => currentValue);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
