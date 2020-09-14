using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System.IO;
using SharpCompress.Archives.SevenZip;
using System.Linq;
using SharpCompress.Common;
using SharpCompress.Archives;
using SharpCompress.Archives.Zip;
using CommunityPatchLauncherFramework.Settings.Factories;
using CommunityPatchLauncher.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;

namespace CommunityPatchLauncher.Tasks
{
	/// <summary>
	/// This class will unzip the community patch from its .7z file
	/// and unzip the zip file inside it and extract all contents to the game folder under "GameFolder"
	/// </summary>
	internal class UnzipAndExtractCommunityPatchTask : ProgressAbstractTask
	{
		/// <inheritdoc>
		public override bool Execute(bool previousTaskState) {
			string filePath = GetSetting<string>("PatchInstaller");
			if (filePath == null) {
				TaskDone();
				return false;
			}
			string zipFilePath = filePath;
			if (!File.Exists(zipFilePath)) {
				TaskDone();
				return false;
			}

			string targetFolder = settingManager.GetValue<string>("GameFolder");

			using (MemoryStream innerArchive = new MemoryStream()) {
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
				using (var outerArchive = SevenZipArchive.Open(filePath)) {
					outerArchive.Entries.First().WriteTo(innerArchive);
				}

				using (var extractedGame = ZipArchive.Open(innerArchive)) {
					DoPreInstallSteps(extractedGame);
					foreach (var entry in extractedGame.Entries.Where(entry => !entry.IsDirectory)) {
						entry.WriteToDirectory(targetFolder, new ExtractionOptions() {
							ExtractFullPath = true,
							Overwrite = true
						});
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
						string gameFolder = settingManager.GetValue<string>("GameFolder");
						string fullPath = gameFolder + line;
						if (File.Exists(fullPath))
                        {
							File.Delete(fullPath);
                        }
					}
				}
			}
		}
	}
}
