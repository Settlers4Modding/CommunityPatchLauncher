using CommunityPatchLauncherFramework.Documentation.Manager;
using CommunityPatchLauncherFramework.Documentation.Strategy;

namespace CommunityPatchLauncherFramework.Documentation.Factory
{
    /// <summary>
    /// A factory tempalte for a document manager
    /// </summary>
    public interface IDocumentManagerFactory
    {
        /// <summary>
        /// Get a ready to use instanc of a document manager
        /// </summary>
        /// <param name="fallbackLanguage">The fallback language to use</param>
        /// <param name="convertStrategy">The convert strategy to use</param>
        /// <returns>A useable document manager</returns>
        DocumentManager GetDocumentManager(string fallbackLanguage, IDocumentConvertStrategy convertStrategy);
    }
}
