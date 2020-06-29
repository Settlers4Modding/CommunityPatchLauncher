using CommunityPatchLauncherFramework.Documentation.Strategy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommunityPatchLauncherFramework.Documentation.Manager
{
    public class DocumentManager
    {
        private readonly string fallbackLanguage;
        private readonly IDocumentConvertStrategy documentConvertStrategy;

        private readonly string folder;

        public DocumentManager(string folder, string fallbackLanguage, IDocumentConvertStrategy documentConvertStrategy)
        {
            this.folder = folder.Last() == '\\' ? folder : folder + "\\";
            this.fallbackLanguage = fallbackLanguage;
            this.documentConvertStrategy = documentConvertStrategy;
        }

        public string ReadDocument(string language, string document)
        {
            string path = folder + language;
            if (!Directory.Exists(path))
            {
                path = folder + fallbackLanguage;
            }
            path += "\\" + document; ;
            if (!File.Exists(path))
            {
                return string.Empty;
            }

            return File.ReadAllText(path);
        }

        public string ReadConvertedDocument(string language, string document)
        {
            return documentConvertStrategy?.GetConverted(ReadDocument(language, document));
        }
    }
}
