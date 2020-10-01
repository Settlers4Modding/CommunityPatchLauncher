using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Commands.DataCommands;
using CommunityPatchLauncher.Enums;
using CommunityPatchLauncher.UserControls.SpecialTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    internal class YesNoViewModel : BaseViewModel, IPopupContent
    {
        //private 

        private Window currentWindow;

        private YesNoEnum dialogResult;

        public ICommand OkCommand { get; private set; }

        public string DialogText { get; private set; }

        public YesNoViewModel()
        {
            DialogText = string.Empty;

            OkCommand = new MultiCommand(new List<ICommand>{
                new SetYesNoEnumCommand(dialogResult, YesNoEnum.Yes),
                CloseWindowCommand
            });

            CloseWindowCommand = new MultiCommand(new List<ICommand>{
                new SetYesNoEnumCommand(dialogResult),
                CloseWindowCommand
            });
        }

        public void Init(Window currentWindow, object parameter)
        {
            this.currentWindow = currentWindow;
            DialogText = parameter?.ToString();
        }
    }
}
