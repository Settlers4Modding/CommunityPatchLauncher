using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Commands.TaskCommands;
using CommunityPatchLauncher.Enums;
using CommunityPatchLauncher.Factories;
using CommunityPatchLauncher.ViewModels.SpecialViews;
using CommunityPatchLauncherFramework.Documentation.Factory;
using CommunityPatchLauncherFramework.Documentation.Manager;
using CommunityPatchLauncherFramework.Documentation.Strategy;
using CommunityPatchLauncherFramework.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;
using System;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    /// <summary>
    /// View model to use for the launch game user control
    /// </summary>
    internal class LaunchGameModelView : BaseViewModel
    {
        /// <summary>
        /// The command used to launch the game
        /// </summary>
        public ICommand LaunchGameCommand { get; private set; }

        /// <summary>
        /// The patch to use for this launch button
        /// </summary>
        public Patch PatchToUse
        {
            get => patchToUse;
            private set
            {
                patchToUse = value;
                RaisePropertyChanged("PatchToUse");
            }
        }
        /// <summary>
        /// private acces to the patch to use
        /// </summary>
        private Patch patchToUse;

        /// <summary>
        /// The current speed mode
        /// </summary>
        public SpeedModes Speed
        {
            get
            {
                return speed;
            }
            set
            {
                if (value == SpeedModes.Unknown)
                {
                    return;
                }
                speed = value;
                RaisePropertyChanged("Speed");
            }
        }
        /// <summary>
        /// Private current speed mode
        /// </summary>
        private SpeedModes speed;

        /// <summary>
        /// The settings manager to use for saving
        /// </summary>
        private readonly SettingManager settingManager;

        /// <summary>
        /// The current path to save the seting to
        /// </summary>
        private string currentSpeedPath;

        /// <summary>
        /// Content of the changelog
        /// </summary>
        public string ChangelogContent { 
            get => changelogContent; 
            private set 
            {
                changelogContent = value;
                RaisePropertyChanged("ChangelogContent");
            }
        }
        /// <summary>
        /// Private content of the change log
        /// </summary>
        private string changelogContent;

        /// <summary>
        /// Create a new instance of this view model
        /// </summary>
        public LaunchGameModelView()
        {
            ISettingFactory factory = new XmlSettingFactory();
            settingManager = factory.GetSettingsManager();
            LaunchGameCommand = new LaunchGameCommand(settingManager);
        }

        /// <summary>
        /// Set the patch for this view model
        /// </summary>
        /// <param name="availablePatch">The patch to use</param>
        public void SetPatch(Patch availablePatch)
        {
            PatchToUse = availablePatch;
            currentSpeedPath = patchToUse.RealPatch.ToString() + "/Speed";
            string speedString = settingManager.GetValue<string>(currentSpeedPath);
            SpeedModes newMode = SpeedModes.Normal;
            if (speedString != null)
            {
                Enum.TryParse(speedString, out newMode);
            }
            Speed = newMode;

            IDocumentManagerFactory managerFactory = new LocalDocumentManagerFactory();
            DocumentManager documentManager = managerFactory.GetDocumentManager("en-EN", new MarkdownHtmlConvertStrategy());
            ChangelogContent = documentManager.ReadConvertedDocument("en-EN", "Placeholder.md");
        }

        /// <summary>
        /// Save the data to the setting manager
        /// </summary>
        private void SaveData()
        {
            settingManager.AddValue(currentSpeedPath, speed.ToString());
            settingManager.SaveSettings();
        }
         
        public override void Reload()
        {
            settingManager.Reload();
        }
    }
}
