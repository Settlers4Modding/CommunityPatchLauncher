using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Commands.DataContainer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace CommunityPatchLauncher.BindingData.MultiBinding
{
    class AcceptAgreementConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 4)
            {
                return null;
            }
            bool agreement = false;
            bool folderSet = false;
            string gameFolder = string.Empty;
            LanguageItem language = null;

            if (values[0] is bool agreementState)
            {
                agreement = agreementState;
            }
            if (values[1] is bool folderState)
            {
                folderSet = folderState;
            }
            string gameFolderToSet = values[2]?.ToString();
            
            if (Directory.Exists(gameFolderToSet) && File.Exists(gameFolderToSet + "S4_Main.exe"))
            {
                gameFolder = gameFolderToSet;
            }
            
            if (values[3] is LanguageItem languageItem)
            {
                language = languageItem;
            }

            return new AcceptAgreementData(agreement, folderSet, gameFolder, language);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
