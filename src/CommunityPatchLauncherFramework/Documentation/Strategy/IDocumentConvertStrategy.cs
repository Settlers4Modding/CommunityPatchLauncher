namespace CommunityPatchLauncherFramework.Documentation.Strategy
{
    /// <summary>
    /// Interface to define strategy to convert document format
    /// </summary>
    public interface IDocumentConvertStrategy
    {
        /// <summary>
        /// Convert the string to the convert variant
        /// </summary>
        /// <param name="rawData">The raw data string</param>
        /// <returns>The converted string</returns>
        string GetConverted(string rawData);
    }
}
