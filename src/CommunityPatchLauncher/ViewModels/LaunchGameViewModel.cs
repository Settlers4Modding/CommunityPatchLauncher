using CommunityPatchLauncher.BindingData;
using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Enums;
using CommunityPatchLauncherFramework.Settings.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    class LaunchGameViewModel : BaseViewModel
    {
        public ICommand LaunchGameCommand { get; private set; }
        public Patches AllPatches { get; }
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
            AllPatches = new Patches();
            Speed = SpeedModes.Testing;

            IDataCommand dataCommand = new GetSettingManagerCommand();
            dataCommand.Executed += (sender, data) =>
            {
                LaunchGameCommand = new LaunchGameCommand(data.GetData<SettingManager>());
            };
            dataCommand.Execute(null);
            
        }

    }
}
