using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;

namespace CommunityLauncherPatcher
{
    /// <summary>
    /// This small console app will patch the launcher
    /// </summary>
    class Program
    {
        /// <summary>
        /// The explicit files to ignore for deletion
        /// </summary>
        private static HashSet<string> ignoreDeleteFilesStarts;

        /// <summary>
        /// Writer to use for logging
        /// </summary>
        private static StreamWriter writer;

        /// <summary>
        /// The current username
        /// </summary>
        private static string username;

        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args">Argument srequired for patching</param>
        static void Main(string[] args)
        {
            string logFile = Path.GetTempPath();
            logFile = Path.Combine(logFile, DateTime.Now.ToString("yyyyMMdd_HHmmss") + " _launcherUpdate.log");
            writer = new StreamWriter(logFile);
            username = Environment.UserName;

            if (args.Length != 5)
            {
                LogLine("Seems like some parameters are wrong upgrade did not work, please restart the launcher");
                LogLine("Press any key to close this window", false);
                Console.ReadLine();
                return;
            }

            ignoreDeleteFilesStarts = new HashSet<string>();
            ignoreDeleteFilesStarts.Add("unins");

            string caller = args[0];
            string callerId = args[1];
            string sourceFile = args[2];
            string targetFolder = args[3];
            string fileToIgnore = args[4];

            LogLine("Caller: " + caller);
            LogLine("callerId: " + callerId);
            LogLine("sourceFile: " + sourceFile);
            LogLine("targetFolder: " + targetFolder);
            LogLine("fileToIgnore: " + fileToIgnore);
            int processId = -1;
            int.TryParse(callerId, out processId);

            if (!File.Exists(sourceFile) || !File.Exists(caller))
            {
                LogLine("Launcher (caller) or patch file not available, please restart the launcher!");
                LogLine("Press any key to close this window", false);
                writer.Close();
                Console.ReadLine();
            }

            try
            {
                Process mainWindow = Process.GetProcessById(processId);
                LogLine("Waiting for max 10 seconds for caller to close", false);
                mainWindow?.WaitForExit(10000);
            }
            catch (Exception)
            {
                LogLine("Application already stopped", false);
            }
            DirectoryInfo directoryInfo = null;
            if (Directory.Exists(targetFolder))
            {
                directoryInfo = new DirectoryInfo(targetFolder);
            }
            LogLine("Waiting 2 seconds to be sure", false);
            Thread.Sleep(2000);
            LogLine("Ready to patch");
            PatchLauncher(sourceFile, fileToIgnore, directoryInfo);
            LogLine("Restarting launcher: " + caller);
            Process.Start(caller);
            LogLine("Done closing in 10 seconds");
            writer.Close();
            Thread.Sleep(10000);
        }

        /// <summary>
        /// patch the launcher
        /// </summary>
        /// <param name="zipArchive">The zip archive to use</param>
        /// <param name="fileToIgnore">Files to ignore while patching</param>
        /// <param name="targetFolder">The target to unzip the content to</param>
        private static void PatchLauncher(string zipArchive, string fileToIgnore, DirectoryInfo targetFolder)
        {
            List<string> currentFiles = GetInstallFolderFiles(targetFolder.FullName);
            bool extractionCompleted = true;
            using (ZipArchive archive = ZipFile.OpenRead(zipArchive))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.Name == fileToIgnore)
                    {
                        LogLine("Ignore file \"" + fileToIgnore + "\"");
                        continue;
                    }
                    string targetFile = Path.Combine(targetFolder.FullName, entry.FullName);
                    targetFile = Path.GetFullPath(targetFile);
                    FileInfo fileInfo = new FileInfo(targetFile);
                    if (!fileInfo.DirectoryName.StartsWith(targetFolder.FullName))
                    {
                        LogLine("Tried to extract file to wrong directory! Extraction of file canceled!");
                        LogLine("Filename: " + targetFile);
                        Thread.Sleep(1000);
                        continue;
                    }
                    if (!Directory.Exists(fileInfo.DirectoryName))
                    {
                        LogLine("Creating missing directory: " + fileInfo.DirectoryName);
                        Directory.CreateDirectory(fileInfo.DirectoryName);
                    }
                    LogLine("Extracts file \"" + entry.FullName + "\" to \"" + targetFile + "\"");
                    try
                    {
                        if (entry.Name == string.Empty)
                        {
                            LogLine("Folder \"" + entry.FullName + "\" ignoring!");
                            continue;
                        }
                        entry.ExtractToFile(targetFile, true);
                        currentFiles.Remove(targetFile);
                    }
                    catch (Exception ex)
                    {
                        LogLine("Something went wrong, aborting");
                        LogLine(ex.Message);
                        extractionCompleted = false;
                        writer.Close();
                        break;
                    }
                }
                if (extractionCompleted)
                {
                    foreach(string file in currentFiles)
                    {
                        bool ignoreFile = false;

                        FileInfo info = new FileInfo(file);
                        foreach (string start in ignoreDeleteFilesStarts)
                        {
                            if (info.Name.StartsWith(start))
                            {
                                ignoreFile = true;
                                break;
                            }
                        }
                        if (ignoreFile)
                        {
                            continue;
                        }
                        if (info.DirectoryName.StartsWith(targetFolder.FullName) && File.Exists(info.FullName))
                        {
                            LogLine("Delete additional file: " + info.FullName);
                            File.Delete(info.FullName);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get all the files in the installation folder
        /// </summary>
        /// <param name="root">The folder to use as root</param>
        /// <returns>A list with all the files in the installation folder</returns>
        private static List<string> GetInstallFolderFiles(string root)
        {
            List<string> returnFiles = new List<string>();
            if (!Directory.Exists(root))
            {
                return returnFiles;
            }

            returnFiles = Directory.GetFiles(root).ToList();
            foreach(string directory in Directory.GetDirectories(root))
            {
                returnFiles.AddRange(GetInstallFolderFiles(directory));
            }

            return returnFiles;
        }

        /// <summary>
        /// Write the current log line
        /// </summary>
        /// <param name="logline">The line to write</param>
        private static void LogLine(string logline)
        {
            LogLine(logline, true);
        }

        /// <summary>
        /// Write the current line to the log
        /// </summary>
        /// <param name="logline">The logline to write</param>
        /// <param name="writeToFile">Should be written to the file</param>
        private static void LogLine(string logline, bool writeToFile)
        {
            Console.WriteLine(logline);
            if (writeToFile && writer.BaseStream != null)
            {
                logline = logline.Replace(username, "{USERNAME}");
                writer?.WriteLine(logline);
            }
        }
    }
}
