using Markdig;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommunityPatchLauncherFramework.Documentation.Strategy
{
    public class MarkdownConvertStrategy : IDocumentConvertStrategy
    {
        public string GetConverted(string rawData)
        {
            return Markdown.ToHtml(rawData);
        }
    }
}
