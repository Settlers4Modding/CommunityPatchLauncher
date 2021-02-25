using System.IO;
using System.Linq;

namespace CommunityPatchLauncherFramework.Documentation.Strategy
{
    /// <summary>
    /// This connector will read the document from the local disc
    /// </summary>
    public class LocalDocumentConnectorStrategy : BaseDocumentConnectorStrategy
    {
        /// <inheritdoc/>
        protected override string CorrectBasePath(string basePath)
        {
            return basePath.Last() == '\\' ? basePath : basePath + "\\";
        }

        /// <inheritdoc/>
        protected override string ReadDocument(string basePath, string language, string document, bool initialCall)
        {
            string path = basePath + language;
            if (!Directory.Exists(path))
            {
                path = basePath + fallbackLanguage;
            }
            path += "\\" + document; ;
            if (!File.Exists(path))
            {
                if (initialCall && language != fallbackLanguage)
                {
                    return ReadDocument(basePath, fallbackLanguage, document, false);
                }
                return string.Empty;
            }

            return File.ReadAllText(path);
        }
    }
}
