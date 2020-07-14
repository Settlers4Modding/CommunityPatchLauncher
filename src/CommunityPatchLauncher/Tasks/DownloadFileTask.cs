using CommunityPatchLauncherFramework.TaskPipeline.EventData;
using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPatchLauncher.Tasks
{
    public class DownloadFileTask : AbstractTask, IProgressTask
    {
        protected Uri url;
        private readonly int totalWorkload;

        private int lastStep;

        public event EventHandler<TaskProgressChanged> ProgressChanged;
        public event EventHandler<TaskDone> TaskComplete;

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
            DownloadDone();

            return true;
        }

        protected void DownloadDone()
        {
            EventHandler<TaskDone> handler = TaskComplete;
            handler?.Invoke(this, new TaskDone(totalWorkload));
        }

        protected void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            float percent = ((float)e.BytesReceived / (float)e.TotalBytesToReceive) * 100f;
            int writeablePercentage = (int)percent;
            if (writeablePercentage == lastStep)
            {
                return;
            }
            EventHandler<TaskProgressChanged> handler = ProgressChanged;
            handler?.Invoke(this, new TaskProgressChanged(totalWorkload, writeablePercentage));
            lastStep = writeablePercentage;
        }

        public int GetStepCount()
        {
            return totalWorkload;
        }
    }
}
