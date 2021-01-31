using System;
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
        /// The last loaded file
        /// </summary>
        private string lastFile;

        /// <summary>
        /// Seconds until we request the file again
        /// </summary>
        private readonly int secondsUntilRequest;

        /// <summary>
        /// The last time the file got requested
        /// </summary>
        private DateTime lastTimeRequested;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public RemoteDocumentConnectorStrategy(int secondsUntilRequest)
        {
            this.secondsUntilRequest = secondsUntilRequest;
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

            if (lastFile != path)
            {
                lastFile = path;
            }
            double diffInSeconds = (lastTimeRequested - DateTime.Now).TotalSeconds;

            if (lastTimeRequested.Year != 1 && diffInSeconds < 60 && diffInSeconds != 0)
            {
                returnData = LoadCachedFile(localFile);
                if (returnData != string.Empty)
                {
                    return returnData;
                }
            }

            WebRequest request = WebRequest.Create(path);
            request.Timeout = 5000;

            WebResponse response;
            try
            {
                response = request.GetResponse();
            }
            catch (Exception ex)
            {
                if (initialCall && language != fallbackLanguage)
                {
                    return ReadDocument(basePath, fallbackLanguage, document, false);
                }
                returnData = LoadCachedFile(localFile);
                returnData = returnData == string.Empty ? returnData : "# This data is loaded from cached!" + returnData;
                return returnData;
            }

            using (WebClient client = new WebClient())
            {
                try
                {
                    returnData = client.DownloadString(response.ResponseUri);
                    lastTimeRequested = DateTime.Now;
                }
                catch (WebException ex)
                {
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
