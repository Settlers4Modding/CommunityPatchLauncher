using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Enums;
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

        public LaunchGameModelView()
        {
            ISettingFactory factory = new XmlSettingFactory();
            SettingManager settingManager = factory.GetSettingsManager();
            LaunchGameCommand = new LaunchGameCommand(settingManager);
        }

        public void SetPatch(Patch availablePatch)
        {
            PatchToUse = availablePatch;
        }
    }
}
