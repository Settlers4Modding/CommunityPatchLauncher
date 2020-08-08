using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Commands.DataContainer;
using CommunityPatchLauncher.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CommunityPatchLauncher.BindingData.MultiBinding
{
    public class LaunchGameConverter : IMultiValueConverter
    {
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

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
