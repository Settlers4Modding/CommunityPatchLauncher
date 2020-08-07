using CommunityPatchLauncher.BindingData;
using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Enums;
using CommunityPatchLauncherFramework.Settings.Manager;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    class LaunchGameViewModel : BaseViewModel
    {
        private SettingManager settingManager;

        public ICommand LaunchGameCommand { get; private set; }
        public IReadOnlyList<Patch> AllPatches { get; }
        public Patch Patch { get; set; }
        public int PatchIndex { get; set; }
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
        private SpeedModes speed;

        public LaunchGameViewModel()
        {
            Patches patches = new Patches();
            AllPatches = patches.AvailablePatch;
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

        private void SetLastSpeedMode()
        {
            speed = SpeedModes.Normal;
            string speedModeToUse = settingManager.GetValue<string>("Speed");
            Enum.TryParse(speedModeToUse, out speed);
        }

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
