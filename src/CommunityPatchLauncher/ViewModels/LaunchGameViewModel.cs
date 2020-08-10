using CommunityPatchLauncher.BindingData;
using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Enums;
using CommunityPatchLauncherFramework.Settings.Manager;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    /// <summary>
    /// The view model for the launch game window
    /// </summary>
    class LaunchGameViewModel : BaseViewModel
    {
        /// <summary>
        /// The settings manager to use
        /// </summary>
        private SettingManager settingManager;

        /// <summary>
        /// The command used to launch the game
        /// </summary>
        public ICommand LaunchGameCommand { get; private set; }

        /// <summary>
        /// All the patches you can select
        /// </summary>
        public IReadOnlyList<Patch> AllPatches { get; }

        /// <summary>
        /// The current seleted patch
        /// </summary>
        public Patch Patch { get; set; }

        /// <summary>
        /// The currently selected index
        /// </summary>
        public int PatchIndex { get; set; }

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
        /// Create a new instance of this class
        /// </summary>
        public LaunchGameViewModel()
        {
            Patches patches = new Patches();
            AllPatches = patches.GetPatches();
            Speed = SpeedModes.Testing;

            IDataCommand dataCommand = new GetSettingManagerCommand();
            dataCommand.Executed += (sender, data) =>
            {
                settingManager = data.GetData<SettingManager>();
                SetLastPatch();
                SetLastSpeedMode();
                LaunchGameCommand = new LaunchGameCommand(settingManager);
            };
            dataCommand.Execute(null);
        }

        /// <summary>
        /// This method will set the last selected patch
        /// </summary>
        private void SetLastPatch()
        {
            string patchToUse = settingManager.GetValue<string>("Patch");
            AvailablePatches realPatch;
            if (Enum.TryParse(patchToUse, out realPatch))
            {
                Patch = new Patch(realPatch);
                for (int i = 0; i < AllPatches.Count; i++)
                {
                    if (AllPatches[i].RealPatch == Patch.RealPatch)
                    {
                        PatchIndex = i;
                        RaisePropertyChanged("PatchIndex");
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// This method will set the last used speed mode
        /// </summary>
        private void SetLastSpeedMode()
        {
            speed = SpeedModes.Normal;
            string speedModeToUse = settingManager.GetValue<string>("Speed");
            Enum.TryParse(speedModeToUse, out speed);
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            if (settingManager != null)
            {
                settingManager.AddValue("Speed", Speed.ToString());
                settingManager.AddValue("Patch", Patch.RealPatch.ToString());
                settingManager.SaveSettings();
            }
        }
    }
}
