using CommunityPatchLauncherFramework.TaskPipeline.Tasks;
using System.IO;
using SharpCompress.Archives.SevenZip;
using System.Linq;
using SharpCompress.Common;
using SharpCompress.Archives;
using SharpCompress.Archives.Zip;

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
	}
}
