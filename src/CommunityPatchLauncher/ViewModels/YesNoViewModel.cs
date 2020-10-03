using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Commands.ApplicationWindow;
using CommunityPatchLauncher.Commands.DataCommands;
using CommunityPatchLauncher.Enums;
using CommunityPatchLauncher.UserControls.SpecialTypes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    internal class YesNoViewModel : BaseViewModel, IPopupReturnDataContent
    {
        private YesNoEnum dialogResult;
        public YesNoEnum YesDialog;
        public YesNoEnum NoDialog;


        public ICommand YesCommand
        {
            get => yesCommand;
            private set
            {
                yesCommand = value;
                RaisePropertyChanged("YesCommand");
            }
        }
        private ICommand yesCommand;

        public ICommand NoCommand
        {
            get => noCommand;
            private set
            {
                noCommand = value;
                RaisePropertyChanged("NoCommand");
            }
        }
        private ICommand noCommand;

        public string DialogText { get; private set; }

        public YesNoViewModel()
        {
            DialogText = string.Empty;
            dialogResult = YesNoEnum.No;
            YesDialog = YesNoEnum.Yes;
            NoDialog = YesNoEnum.No;
        }

        public void Init(Window currentWindow, object parameter)
        {
            this.currentWindow = currentWindow;

            CloseWindowCommand = new CloseWindowCommand(currentWindow);
            IDataCommand yesNoCommand = new SetYesNoEnumCommand();
            yesNoCommand.Executed += (sender, content) =>
            {
                object data =  content.GetData();
                if (data is YesNoEnum yesNoData)
                {
                    dialogResult = yesNoData;
                }
            };
            YesCommand = new MultiCommand(new List<ICommand>{
                yesNoCommand,
                CloseWindowCommand
            });

            NoCommand = new MultiCommand(new List<ICommand>{
                yesNoCommand,
                CloseWindowCommand
            });

            DialogText = parameter?.ToString();
        }

        public T getReturnData<T>()
        {
            Type dataType = typeof(T);
            return dataType == typeof(YesNoEnum) ? (T)Convert.ChangeType(dialogResult, dataType) : default;
        }

        public object getReturnData()
        {
            return dialogResult;
        }
    }
}
