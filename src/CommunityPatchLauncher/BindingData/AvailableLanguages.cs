using CommunityPatchLauncher.BindingData.Container;
using System.Collections.Generic;

namespace CommunityPatchLauncher.BindingData
{
    public class AvailableLanguages
    {
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
