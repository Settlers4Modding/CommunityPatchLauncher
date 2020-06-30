using Markdig;

namespace CommunityPatchLauncherFramework.Documentation.Strategy
{
    /// <summary>
    /// This class will convert the loaded markdown textes to html
    /// </summary>
    public class MarkdownHtmlConvertStrategy : IDocumentConvertStrategy
    {
        /// <inheritdoc/>
        public string GetConverted(string rawData)
        {
            string html = "<head><meta http-equiv='Content-Type' content='text/html;charset=UTF-8'></head><body>";
            html += Markdown.ToHtml(rawData);
            html += "</body>";
            return html;
        }
    }
}
