﻿using System;
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
        /// The notice to use if cached
        /// </summary>
        private readonly string cacheNotice;

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
        public RemoteDocumentConnectorStrategy(TimeSpan timeSpanUntilRepeat, string cacheNotice)
        {
            this.timeSpanUntilRepeat = timeSpanUntilRepeat;
            this.cacheNotice = cacheNotice;
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
            path += Path.DirectorySeparatorChar + document;
            localFile += Path.DirectorySeparatorChar + document;

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
                    if (initialCall)
                    {
                        if (language != fallbackLanguage)
                        {
                            returnData = ReadDocument(basePath, fallbackLanguage, document, false);
                        }
                        returnData = returnData == string.Empty ? LoadCachedFile(localFile, true) : returnData;
                        if (returnData == string.Empty)
                        {
                            string localFallbackFile = cacheFolder + fallbackLanguage + "/" + document;
                            returnData = LoadCachedFile(localFallbackFile, true);
                        }
                        return returnData;
                    }
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
        /// <param name="filePath">The filename to load</param>
        /// <returns>The from cache loaded file data</returns>
        private string LoadCachedFile(string filePath)
        {
            return LoadCachedFile(filePath, false);
        }

        /// <summary>
        /// Load the cached file
        /// </summary>
        /// <param name="filePath">The filename to load</param>
        /// <param name="addNotice">Add a notice that it was loaded from cache</param>
        /// <returns>The from cache loaded file data</returns>
        private string LoadCachedFile(string filePath, bool addNotice)
        {
            string returnData = string.Empty;
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    returnData = reader.ReadToEnd();
                }
            }

            returnData = addNotice && returnData != string.Empty ? "# " + cacheNotice + returnData : returnData;
            return returnData;
        }
    }
}
