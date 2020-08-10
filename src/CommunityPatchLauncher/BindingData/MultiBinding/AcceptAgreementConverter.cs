using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Commands.DataContainer;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace CommunityPatchLauncher.BindingData.MultiBinding
{
    /// <summary>
    /// This class will convert the values from the welcome screen into a class instance
    /// </summary>
    class AcceptAgreementConverter : IMultiValueConverter
    {
        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
