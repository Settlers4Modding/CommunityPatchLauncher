using CommunityPatchLauncher.Commands.DataContainer;
using CommunityPatchLauncher.Enums;
using System;
using System.Globalization;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace CommunityPatchLauncher.BindingData.Converter
{
    /// <summary>
    /// This class will convert data to a patch selection data set
    /// </summary>
    class PatchSelectionConverter : IMultiValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is AvailablePatches patch && values[1] is ToggleButton button)
            {
                return new PatchSelectionData(button, patch);
            }
            return null;
        }

        /// <inheritdoc/>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
