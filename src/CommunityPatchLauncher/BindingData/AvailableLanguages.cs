using CommunityPatchLauncher.BindingData.Container;
using System.Collections.Generic;

namespace CommunityPatchLauncher.BindingData
{
    /// <summary>
    /// This class will get you all the available languages
    /// </summary>
    public class AvailableLanguages
    {
        /// <summary>
        /// return all the available languages
        /// </summary>
        /// <returns>A list with all the languages</returns>
        public IReadOnlyList<LanguageItem> GetAvailableLanguages()
        {
            List<LanguageItem> languageItems = new List<LanguageItem>()
            {
                new LanguageItem("German", "de-DE"),
                new LanguageItem("English", "en-EN")
            };
            return languageItems;
        }
    }
}
