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
            string applicationPath = Assembly.GetExecutingAssembly().Location;
            FileInfo applicationInfo = new FileInfo(applicationPath);
            string cssPath = "file:///";
            cssPath += applicationInfo.DirectoryName + "\\";
            cssPath = cssPath.Replace("\\", "/");
            cssPath += "Styles/DefaultBrowserStyle.css";
            string html = "<head><meta http-equiv='Content-Type' content='text/html;charset=UTF-8'>";
            html += "<link rel='stylesheet' type='text/css' href='" + cssPath + "'>";
            html += "</head><body>";
            html += Markdown.ToHtml(rawData);
            html += "</body>";
            return html;
        }
    }
}
