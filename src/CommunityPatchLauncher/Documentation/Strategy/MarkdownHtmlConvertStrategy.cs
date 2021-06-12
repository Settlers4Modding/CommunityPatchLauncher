using Markdig;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CommunityPatchLauncherFramework.Documentation.Strategy
{
    /// <summary>
    /// This class will convert the loaded markdown textes to html
    /// </summary>
    public class MarkdownHtmlConvertStrategy : IDocumentConvertStrategy
    {
        protected const string IMAGE_REGEX = "<img.*src=\\\"([\\w\\.\\/]+)\\\".*\\/>";//<img.*(src=\\\\\"[\\w\\.\\/] +\\\").*\\/>

        /// <inheritdoc/>
        public string GetConverted(string rawData)
        {
            string html = GetHtml(rawData);
            return DoPostProcessingSteps(html);
        }

        /// <summary>
        /// Get the link to the css file
        /// </summary>
        /// <returns>The string to the css file</returns>
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

        /// <summary>
        /// Do some post processing steps for the html data
        /// </summary>
        /// <param name="html">The current html</param>
        /// <returns>The string with all the changes</returns>
        protected virtual string DoPostProcessingSteps(string html)
        {
            return ReplaceLocalImagePaths(html);
        }

        /// <summary>
        /// Replace the local image paths with static ones
        /// </summary>
        /// <param name="html">The html to work on</param>
        /// <returns>The changed img paths</returns>
        protected virtual string ReplaceLocalImagePaths(string html)
        {
            Regex regex = new Regex(IMAGE_REGEX);
            MatchCollection matches =  regex.Matches(html);
            string baseFolder = Assembly.GetExecutingAssembly().Location;
            FileInfo baseFolderInfo = new FileInfo(baseFolder);
            string baseImagePath = Path.Combine(baseFolderInfo.DirectoryName, "Assets");
            foreach (Match match in matches)
            {
                if (match.Groups.Count == 2)
                {
                    Group group = match.Groups[1];
                    string currentPath = group.Value;
                    if (currentPath.ToLower().StartsWith("http"))
                    {
                        continue;
                    }
                    string newPath = Path.Combine(baseImagePath, currentPath);
                    string baseReplace = match.Groups[0].Value;
                    string realReplace = baseReplace.Replace(currentPath, newPath);
                    html = html.Replace(baseReplace, realReplace);
                }
            }

            return html;
        }

        protected virtual string GetHtml(string rawData)
        {
            if (rawData == string.Empty)
            {
                return rawData;
            }
            string html = "<head><meta http-equiv='Content-Type' content='text/html;charset=UTF-8'>";
            html += "<meta http-equiv='X - UA - Compatible' content='IE = edge'>";
            html += GetCssLink();
            html += "</head><body oncontextmenu='return false;'>";
            MarkdownPipeline pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            html += Markdown.ToHtml(rawData, pipeline);
            html += "</body>";
            return html;
        }
    }
}
