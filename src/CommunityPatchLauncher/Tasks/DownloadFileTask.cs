using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System;
using System.Net;

namespace CommunityPatchLauncher.Tasks
{
    /// <summary>
    /// This class will provide some defaule file download
    /// </summary>
    internal class DownloadFileTask : ProgressAbstractTask
    {
        /// <summary>
        /// The url to download from
        /// </summary>
        protected Uri url;

        /// <summary>
        /// The last step executed
        /// </summary>
        private int lastStep;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public DownloadFileTask()
        {
            lastStep = -1;
            totalWorkload = 100;
        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="url">The url to download from</param>
        public DownloadFileTask(string url) : this(new Uri(url))
        {
        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="url">The url to download from</param>
        public DownloadFileTask(Uri url) : this()
        {
            this.url = url;
        }

        /// <summary>
        /// Download the file
        /// </summary>
        /// <param name="targetFile">The file to save the bytes into</param>
        /// <returns>True if download was successful</returns>
        protected virtual bool DownloadFile(string targetFile)
        {
            return DownloadFile(url, targetFile);
        }

        /// <summary>
        /// Download a given file
        /// </summary>
        /// <param name="sourceFile">The file url to download</param>
        /// <param name="targetFile">The file to save the download into</param>
        /// <returns>True if the download was successful</returns>
        protected virtual bool DownloadFile(Uri sourceFile, string targetFile)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadProgressChanged += Client_DownloadProgressChanged;
                    client.DownloadFileTaskAsync(url, targetFile).GetAwaiter().GetResult();
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public override bool Execute(bool previousTaskState)
        {
            DownloadFile(url, @"C:\Users\Xanat\AppData\Local\Temp\testload.txt");
            TaskDone();

            return true;
        }

        /// <summary>
        /// The download progress did change
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event data</param>
        protected void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            float percent = ((float)e.BytesReceived / (float)e.TotalBytesToReceive) * 100f;
            int writeablePercentage = (int)percent;
            if (writeablePercentage == lastStep)
            {
                return;
            }
            ProgressHasChanged(writeablePercentage);
            lastStep = writeablePercentage;
        }
    }
}
