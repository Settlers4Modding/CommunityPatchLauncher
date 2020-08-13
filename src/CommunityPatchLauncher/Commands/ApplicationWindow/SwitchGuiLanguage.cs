using CommunityPatchLauncherFramework.Settings.Manager;
using System;
using System.Globalization;
using System.Threading;

namespace CommunityPatchLauncher.Commands.ApplicationWindow
{
    /// <summary>
    /// This command will switch the GUI language
    /// </summary>
    internal class SwitchGuiLanguage : BaseCommand
    {
        /// <summary>
        /// The setting manager to use
        /// </summary>
        private readonly SettingManager settingManager;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="settingManager">The setting manager to use</param>
        public SwitchGuiLanguage(SettingManager settingManager)
        {
            this.settingManager = settingManager;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return settingManager != null;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }
            string language = settingManager.GetValue<string>("Language");
            if (language == null)
            {
                return;
            }
            try
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            }
            catch (Exception)
            {
            }
        }
    }
}
