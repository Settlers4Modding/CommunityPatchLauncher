using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System;
using System.Net;

namespace CommunityPatchLauncher.Tasks
{
    public class DownloadFileTask : ProgressAbstractTask
    {
        protected Uri url;

        private int lastStep;

        public DownloadFileTask()
        {
            lastStep = -1;
            totalWorkload = 100;
        }

        public DownloadFileTask(string url) : this(new Uri(url))
        {
        }

        public DownloadFileTask(Uri url) : this()
        {
            this.url = url;
        }

        protected virtual bool DownloadFile(string targetFile)
        {
            return DownloadFile(url, targetFile);
        }
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

        public override bool Execute(bool previousTaskState)
        {
            DownloadFile(url, @"C:\Users\Xanat\AppData\Local\Temp\testload.txt");
            TaskDone();

            return true;
        }

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
