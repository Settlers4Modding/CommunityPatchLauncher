using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace CommunityPatchLauncherFramework.Documentation.Strategy
{
    /// <summary>
    /// Get files from a remote source
    /// </summary>
    public class RemoteDocumentConnectorStrategy : BaseDocumentConnectorStrategy
    {
        /// <summary>
        /// The cache folder to use
        /// </summary>
        private string cacheFolder;

        /// <summary>
        /// Timespan until loading again from source
        /// </summary>
        private readonly TimeSpan timeSpanUntilRepeat;

        /// <summary>
        /// Dictionary with the last request times
        /// </summary>
        private readonly Dictionary<string, DateTime> latestRequestDictionary;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public RemoteDocumentConnectorStrategy(TimeSpan timeSpanUntilRepeat)
        {
            this.timeSpanUntilRepeat = timeSpanUntilRepeat;
            latestRequestDictionary = new Dictionary<string, DateTime>();
            cacheFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            cacheFolder += "\\SIVCommunityPatchLauncher\\Cache\\";
            if (!Directory.Exists(cacheFolder))
            {
                Directory.CreateDirectory(cacheFolder);
            }
        }

        /// <inheritdoc/>
        protected override string CorrectBasePath(string basePath)
        {
            return basePath.Last() == '/' ? basePath : basePath + "/";
        }

        /// <inheritdoc/>
        protected override string ReadDocument(string basePath, string language, string document, bool initialCall)
        {
            string path = basePath + language;
            string localFile = cacheFolder + language;
            if (!Directory.Exists(localFile))
            {
                Directory.CreateDirectory(localFile);
            }
            string returnData = string.Empty;
            path += "/" + document;
            localFile += "\\" + document;

            if (!latestRequestDictionary.ContainsKey(path))
            {
                latestRequestDictionary.Add(path, new DateTime());
            }

            DateTime lastTimeRequested = latestRequestDictionary[path];

            DateTime repeatTime = DateTime.Now - timeSpanUntilRepeat;

            if (lastTimeRequested.Year != 1 && lastTimeRequested > repeatTime)
            {
                returnData = LoadCachedFile(localFile);
                if (returnData != string.Empty)
                {
                    return returnData;
                }
            }
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    returnData = client.DownloadString(path);
                    latestRequestDictionary[path] = DateTime.Now;
                }
                catch (WebException ex)
                {
                    if (initialCall && language != fallbackLanguage)
                    {
                        return ReadDocument(basePath, fallbackLanguage, document, false);
                    }
                    returnData = LoadCachedFile(localFile);
                    returnData = returnData == string.Empty ? returnData : "# This data is loaded from cached!" + returnData;
                    return returnData;
                }
                using (StreamWriter writer = new StreamWriter(localFile))
                {
                    writer.Write(returnData);
                }
            }
            return returnData;
        }


        /// <summary>
        /// Load the cached file
        /// </summary>
        /// <param name="fileName">the filename to load</param>
        /// <returns>The from cache loaded file data</returns>
        private string LoadCachedFile(string fileName)
        {
            string returnData = "";
            if (File.Exists(fileName))
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    returnData = reader.ReadToEnd();
                }
            }

            return returnData;
        }
    }
}
