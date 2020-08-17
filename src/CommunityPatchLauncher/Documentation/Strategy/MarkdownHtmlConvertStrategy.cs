using Markdig;
using System.IO;
using System.Reflection;
using System.Windows;

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
            return GetHtml(rawData);
        }

        protected virtual string GetCssLink()
        {
            string applicationPath = Assembly.GetExecutingAssembly().Location;
            FileInfo applicationInfo = new FileInfo(applicationPath);

            string cssPath = "file:///";
            cssPath += applicationInfo.DirectoryName + "\\";
            cssPath = cssPath.Replace("\\", "/");
            cssPath += "Styles/DefaultBrowserStyle.css";

            return "<link rel='stylesheet' type='text/css' href='" + cssPath + "'>";
        }

        protected virtual string GetHtml(string rawData)
        {
            string html = "<head><meta http-equiv='Content-Type' content='text/html;charset=UTF-8'>";
            html += "<meta http-equiv='X - UA - Compatible' content='IE = edge'>";
            html += GetCssLink();
            html += "</head><body>";
            MarkdownPipeline pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            html += Markdown.ToHtml(rawData, pipeline);
            html += "</body>";
            return html;
        }
    }
}
