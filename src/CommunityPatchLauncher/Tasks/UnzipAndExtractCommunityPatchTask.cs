using CommunityPatchLauncher.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;
using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using SharpCompress.Archives;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CommunityPatchLauncher.Tasks
{
    /// <summary>
    /// This class will unzip the community patch from its .7z file
    /// and unzip the zip file inside it and extract all contents to the game folder under "GameFolder"
    /// </summary>
    internal class UnzipAndExtractCommunityPatchTask : ProgressAbstractTask
    {
        /// <inheritdoc>
        public override bool Execute(bool previousTaskState)
        {
            string filePath = GetSetting<string>("PatchInstaller");
            if (filePath == null)
            {
                TaskDone();
                return false;
            }
            string zipFilePath = filePath;
            if (!File.Exists(zipFilePath))
            {
                TaskDone();
                return false;
            }

            string targetFolder = settingManager.GetValue<string>("GameFolder");

            using (MemoryStream innerArchive = new MemoryStream())
            {
                /*
					* Patch zip structure:
					* Patch.7z:
					* -Patch.zip:
					* --S4_Main.exe
					* --binkw32.dll
					* --...
					* 
					* innerArchive: Patch.zip
					* outerArchive: Patch.7z
					*/

                //Extract the folder zip inside the 7Zip to a stream.
                using (var outerArchive = SevenZipArchive.Open(filePath))
                {
                    outerArchive.Entries.First().WriteTo(innerArchive);
                }

                using (var extractedGame = ZipArchive.Open(innerArchive))
                {
                    DoPreInstallSteps(extractedGame);
                    int currentProgress = 0;
                    foreach (var entry in extractedGame.Entries.Where(entry => !entry.IsDirectory))
                    {
                        if (entry.Key == "PreInstall.txt")
                        {
                            continue;
                        }
                        entry.WriteToDirectory(targetFolder, new ExtractionOptions()
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        });
                        currentProgress = (int)(0 / extractedGame.Entries.Count * 100);
                        ProgressHasChanged(currentProgress);
                    }
                }
            }

            TaskDone();
            return true;
        }

        /// <summary>
        /// Get a specific entry from the archive
        /// </summary>
        /// <param name="entryName">The entry name to get</param>
        /// <param name="archive">The archive to search in</param>
        /// <returns>The entry or null</returns>
        private ZipArchiveEntry GetSpecificEntry(string entryName, ZipArchive archive)
        {
            var entryToReturn = archive.Entries.Where(entry =>
            {
                return entry.IsDirectory == false && entry.Key == entryName;
            }).FirstOrDefault();
            return entryToReturn;
        }

        /// <summary>
        /// Do the pre installation steps
        /// </summary>
        /// <param name="zipArchive"></param>
        private void DoPreInstallSteps(ZipArchive zipArchive)
        {
            ISettingFactory settingFactory = new WpfPropertySettingManagerFactory();
            SettingManager applicationSettings = settingFactory.GetSettingsManager();
            string preInstallFileName = applicationSettings.GetValue<string>("PreInstall");
            ZipArchiveEntry preInstall = GetSpecificEntry(preInstallFileName, zipArchive);
            if (preInstall != null)
            {
                using (StreamReader fileReader = new StreamReader(preInstall.OpenEntryStream()))
                {
                    string line = string.Empty;
                    while ((line = fileReader.ReadLine()) != null)
                    {
                        //TODO: IMPORTANT: Check if this is enough protection so that nobody can escape the game folder!
                        line = line.Replace("../", "");
                        line = line.Replace("..\\", "");
                        string gameFolder = settingManager.GetValue<string>("GameFolder");
                        string fullPath = gameFolder + line;
                        if (File.Exists(fullPath))
                        {
                            File.Delete(fullPath);
                            continue;
                        }
                        DeleteFolder(fullPath);
                    }
                }
            }
        }

        /// <summary>
        /// Delete a complete folder
        /// </summary>
        /// <param name="rootPath">The root path to the folder</param>
        private void DeleteFolder(string rootPath)
        {
            if (!Directory.Exists(rootPath))
            {
                return;
            }

            foreach(string directory in Directory.GetDirectories(rootPath))
            {
                DeleteFolder(directory);
            }

            foreach(string file in Directory.GetFiles(rootPath))
            {
                File.Delete(file);
            }
            Directory.Delete(rootPath);
        }
    }
}
