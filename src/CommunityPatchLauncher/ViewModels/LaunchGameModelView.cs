using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Enums;
using CommunityPatchLauncher.Factories;
using CommunityPatchLauncherFramework.Documentation.Factory;
using CommunityPatchLauncherFramework.Documentation.Manager;
using CommunityPatchLauncherFramework.Documentation.Strategy;
using CommunityPatchLauncherFramework.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    internal class LaunchGameModelView : BaseViewModel
    {
        /// <summary>
        /// The command used to launch the game
        /// </summary>
        public ICommand LaunchGameCommand { get; private set; }

        public Patch PatchToUse
        {
            get => patchToUse;
            private set
            {
                patchToUse = value;
                RaisePropertyChanged("PatchToUse");
            }
        }
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

        private readonly SettingManager settingManager;

        private string currentSpeedPath;

        public string ChangelogContent { 
            get => changelogContent; 
            private set 
            {
                changelogContent = value;
                RaisePropertyChanged("ChangelogContent");
            }
        }
        private string changelogContent;

        public LaunchGameModelView()
        {
            ISettingFactory factory = new XmlSettingFactory();
            settingManager = factory.GetSettingsManager();
            LaunchGameCommand = new LaunchGameCommand(settingManager);
        }

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

        private void SaveData()
        {
            settingManager.AddValue(currentSpeedPath, speed.ToString());
            settingManager.SaveSettings();
        }

        public override void Dispose()
        {
            SaveData();
        }
    }
}
