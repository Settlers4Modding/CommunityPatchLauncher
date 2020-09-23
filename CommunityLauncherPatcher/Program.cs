using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading;

namespace CommunityLauncherPatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 5)
            {
                Console.WriteLine("Seems like some parameters are wrong upgrade did not work, please restart the launcher");
                Console.WriteLine("Press any key to close this window");
                Console.ReadLine();
                return;
            }

            string caller = args[0];
            string callerId = args[1];
            string sourceFile = args[2];
            string targetFolder = args[3];
            string fileToIgnore = args[4];

            Console.WriteLine("Caller: " + caller);
            Console.WriteLine("callerId: " + callerId);
            Console.WriteLine("sourceFile: " + sourceFile);
            Console.WriteLine("targetFolder: " + targetFolder);
            Console.WriteLine("fileToIgnore: " + fileToIgnore);
            int processId = -1;
            int.TryParse(callerId, out processId);
            bool applicationClosed = false;

            if (!File.Exists(sourceFile) || !File.Exists(caller))
            {
                Console.WriteLine("Launcher (caller) or patch file not available, please restart the launcher!");
                Console.WriteLine("Press any key to close this window");
                Console.ReadLine();
            }

            try
            {
                Process mainWindow = Process.GetProcessById(processId);
                Console.WriteLine("Waiting for max 10 seconds for caller to close");
                mainWindow?.WaitForExit(10000);
            }
            catch (Exception)
            {
                Console.WriteLine("Application already stopped");
                applicationClosed = true;
            }
            DirectoryInfo directoryInfo = null;
            if (Directory.Exists(targetFolder))
            {
                directoryInfo = new DirectoryInfo(targetFolder);
            }
            Console.WriteLine("Waiting 2 seconds to be sure");
            Thread.Sleep(2000);
            Console.WriteLine("Ready to patch");
            PatchLauncher(sourceFile, fileToIgnore, directoryInfo);
            Console.WriteLine("Restarting patcher:" + caller);
            Process.Start(caller);
            Console.WriteLine("Done closing in 10 seconds");
            Thread.Sleep(10000);
        }

        private static void PatchLauncher(string zipArchive, string fileToIgnore, DirectoryInfo targetFolder)
        {
            using (ZipArchive archive = ZipFile.OpenRead(zipArchive))
            {
                foreach(ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.Name == fileToIgnore)
                    {
                        Console.WriteLine("Ignore file " + fileToIgnore);
                        continue;
                    }
                    string targetFile = targetFolder.FullName + "\\" + entry.FullName;
                    Console.WriteLine("Extracts file " + entry.FullName + " to " + targetFile);
                    try
                    {
                        if (entry.Name == string.Empty)
                        {
                            Console.WriteLine("Folder " + entry.FullName + " ignoring!");
                            continue;
                        }
                        entry.ExtractToFile(targetFile, true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Something went wrong, aborting");
                        Console.WriteLine(ex.Message);
                        break;
                    }
                }
            }
        }
    }
}
