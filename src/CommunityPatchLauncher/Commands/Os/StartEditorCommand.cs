using CommunityPatchLauncherFramework.Settings.Manager;
using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace CommunityPatchLauncher.Commands.Os
{
    /// <summary>
    /// Start the game map editor
    /// </summary>
    internal class StartEditorCommand : StartProgramCommand
    {
        /// <summary>
        /// The pre file path base
        /// </summary>
        private string preFilePathBase;

        /// <summary>
        /// The settings manager to use
        /// </summary>
        private SettingManager settingManager;

        /// <summary>
        /// Fallback code
        /// </summary>
        private List<string> allowedCodes;

        /// <summary>
        /// Start the editor command
        /// </summary>
        /// <param name="settingManager"></param>
        public StartEditorCommand(SettingManager settingManager)
        {
            allowedCodes = new List<string>(){
                "De",
                "En"
            };
            string gameFolder = settingManager.GetValue<string>("GameFolder");
            string editorPlusPath = gameFolder + Properties.Settings.Default.EditorBaseFolder + "S4EditorPlus.exe";
            string editorPlusPathDownload = gameFolder + Properties.Settings.Default.EditorBaseFolder + "S4Editor_203.exe";
            string editorPath = gameFolder + Properties.Settings.Default.EditorBaseFolder + "S4Editor.exe";
            string editorPreBase = gameFolder + Properties.Settings.Default.EditorBaseFolder + "RunEditor";

            this.settingManager = settingManager;
            programPath = File.Exists(editorPlusPath) ? editorPlusPath : File.Exists(editorPlusPathDownload) ? editorPlusPathDownload : editorPath;
        }

        /// <summary>
        /// Get the converted language code
        /// </summary>
        /// <param name="baseCode"></param>
        /// <returns></returns>
        private string GetConvertedCode(string baseCode)
        {
            string[] code = baseCode.Split('-');
            string realCode = code.Length == 2 ? code[0] : baseCode;
            char[] chars = realCode.ToCharArray();
            chars[0] = char.ToUpper(chars[0]);
            realCode = new string(chars);

            return realCode;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            string gameFolder = settingManager.GetValue<string>("GameFolder");
            string editorPlusPath = gameFolder + Properties.Settings.Default.EditorBaseFolder + "S4EditorPlus.exe";
            string editorPlusPathDownload = gameFolder + Properties.Settings.Default.EditorBaseFolder + "S4Editor_203.exe";
            string editorPath = gameFolder + Properties.Settings.Default.EditorBaseFolder + "S4Editor.exe";
            string editorPreBase = gameFolder + Properties.Settings.Default.EditorBaseFolder + "RunEditor";

            programPath = File.Exists(editorPlusPath) ? editorPlusPath : File.Exists(editorPlusPathDownload) ? editorPlusPathDownload : editorPath;
            preFilePathBase = editorPreBase;

            string language = settingManager.GetValue<string>("Language");
            string realCode = GetConvertedCode(language);
            realCode = allowedCodes.Contains(realCode) ? realCode : Properties.Settings.Default.FallbackLanguage;
            string realBasePath = preFilePathBase + realCode + ".bat";

            if (!RegistryKeyFilled())
            {
                base.Execute(realBasePath);
                return;
            }
            base.Execute(parameter);
        }

        /// <summary>
        /// Check if the regestry key for the editor exists
        /// </summary>
        /// <returns>True if the key exist</returns>
        private bool RegistryKeyFilled()
        {
            object setting = Registry.GetValue(Properties.Settings.Default.EditorRegKey, "UserMapDir", "");
            string editorSettings = setting?.ToString();
            return !string.IsNullOrEmpty(editorSettings);
        }
    }
}
