using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Commands.DataContainer;
using CommunityPatchLauncher.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace CommunityPatchLauncher.BindingData.MultiBinding
{
    /// <summary>
    /// This class will convert different values into a class usable by the launch game command
    /// </summary>
    public class LaunchGameConverter : IMultiValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            object returnObject = null;
            if (values.Length == 2)
            {
                if (values[0] is SpeedModes mode && values[1] is Patch patch)
                {
                    returnObject = new LaunchGameData(patch.RealPatch, mode);
                }
            }
            return returnObject;
        }

        /// <inheritdoc/>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
