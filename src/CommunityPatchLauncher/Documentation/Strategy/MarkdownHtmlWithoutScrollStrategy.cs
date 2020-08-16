using CommunityPatchLauncherFramework.Documentation.Strategy;
using Markdig;

namespace CommunityPatchLauncher.Documentation.Strategy
{
    internal class MarkdownHtmlWithoutScrollStrategy : MarkdownHtmlConvertStrategy
    {
        protected override string GetHtml(string rawData)
        {
            string html = "<head><meta http-equiv='Content-Type' content='text/html;charset=UTF-8'>";
            html += "<meta http-equiv='X - UA - Compatible' content='IE = edge'>";
            html += GetCssLink();
            html += "</head><body scroll='no'>";
            html += Markdown.ToHtml(rawData);
            html += "</body>";
            return html;
        }
    }
}
