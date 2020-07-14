using CommunityPatchLauncher.Settings.Reader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPatchLauncher.Tasks
{
    public class DownloadVersionInformation : DownloadFileTask
    {
        public DownloadVersionInformation(string url) : base(url)
        {
        }

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
            DownloadDone();
            return true;
        }

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
