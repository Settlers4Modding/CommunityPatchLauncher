using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Commands.DataContainer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace CommunityPatchLauncher.BindingData.Converter
{
    internal class ResizeWindowConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            ResizeWindowData returnData = null;
            if (values.Length != 4)
            {
                return returnData;
            }
            if (values[0] is bool custom && custom)
            {
                if (values[1] is string width && values[2] is string height)
                {
                    int realWidth;
                    int realHeight;
                    if (int.TryParse(width, out realWidth) && int.TryParse(height, out realHeight))
                    {
                        returnData = new ResizeWindowData(realWidth, realHeight, true);
                    }
                }
                return returnData;
            }
            if (values[3] is WindowSize windowSize)
            {
                returnData = new ResizeWindowData(windowSize);
            }
            return returnData;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
