
namespace CommunityPatchLauncher.BindingData.Container
{
    /// <summary>
    /// A new language item
    /// </summary>
    public class LanguageItem
    {
        /// <summary>
        /// The display name of the language
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The iso code of the language
        /// </summary>
        public string IsoCode { get; }

        /// <summary>
        /// Create a new language element
        /// </summary>
        /// <param name="name">The display name of the language</param>
        /// <param name="isoCode">The iso code of the language</param>
        public LanguageItem(string name, string isoCode)
        {
            Name = name;
            IsoCode = isoCode;
        }


    }
}
