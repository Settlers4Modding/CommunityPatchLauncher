using CommunityPatchLauncher.Settings.Reader;
using System;
using System.IO;
using System.Net;

namespace CommunityPatchLauncher.Tasks
{
    /// <summary>
    /// Download and parse the version information
    /// </summary>
    internal class DownloadVersionInformation : DownloadFileTask
    {
        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="url">The url to download the information from</param>
        public DownloadVersionInformation(string url) : base(url)
        {
        }

        /// <inheritdoc/>
        public override bool Execute(bool previousTaskState)
        {
            MemoryStream dataStream = DownloadToMemory();
            if (dataStream == null)
            {
                return false;
            }
            string content = string.Empty;
            using (StreamReader reader = new StreamReader(dataStream))
            {
                content = reader.ReadToEnd();
            }
            dataStream = null;
            IniStringReader iniReader = new IniStringReader();
            settings = iniReader.LoadSettings(content);
            TaskDone();
            return true;
        }

        /// <summary>
        /// Download the file into the memory
        /// </summary>
        /// <returns>The memorystream with the downloaded data</returns>
        private MemoryStream DownloadToMemory()
        {
            MemoryStream stream = null;
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.DownloadProgressChanged += Client_DownloadProgressChanged;
                    byte[] data = client.DownloadDataTaskAsync(url).GetAwaiter().GetResult();
                    stream = new MemoryStream(data);
                }
                catch (Exception)
                {
                    return stream;
                }
            }
            return stream;
        }
    }
}
