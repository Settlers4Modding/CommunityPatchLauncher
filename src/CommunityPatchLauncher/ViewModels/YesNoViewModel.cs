using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Commands.ApplicationWindow;
using CommunityPatchLauncher.Commands.DataCommands;
using CommunityPatchLauncher.Enums;
using CommunityPatchLauncher.UserControls.SpecialTypes;
using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    /// <summary>
    /// Simple yes no dialog content
    /// </summary>
    internal class YesNoViewModel : BaseViewModel, IPopupReturnDataContent
    {
        /// <summary>
        /// The result to return
        /// </summary>
        private YesNoEnum dialogResult;

        /// <summary>
        /// The option to use in yes case
        /// </summary>
        public YesNoEnum YesDialog;

        /// <summary>
        /// The option to use in no case
        /// </summary>
        public YesNoEnum NoDialog;

        /// <summary>
        /// The command to use for the yes button
        /// </summary>
        public ICommand YesCommand
        {
            get => yesCommand;
            private set
            {
                yesCommand = value;
                RaisePropertyChanged("YesCommand");
            }
        }
        /// <summary>
        /// Private accessor for the yes command
        /// </summary>
        private ICommand yesCommand;

        /// <summary>
        /// The command to use for the no button
        /// </summary>
        public ICommand NoCommand
        {
            get => noCommand;
            private set
            {
                noCommand = value;
                RaisePropertyChanged("NoCommand");
            }
        }
        /// <summary>
        /// Private accessor for the no button
        /// </summary>
        private ICommand noCommand;

        /// <summary>
        /// The text to show
        /// </summary>
        public string DialogText { get; private set; }

        /// <summary>
        /// Create a new instance of this model
        /// </summary>
        public YesNoViewModel()
        {
            DialogText = string.Empty;
            dialogResult = YesNoEnum.No;
            YesDialog = YesNoEnum.Yes;
            NoDialog = YesNoEnum.No;
        }

        /// <inheritdoc/>
        public void Init(Window currentWindow, FontAwesomeIcon icon, object parameter)
        {
            this.currentWindow = currentWindow;

            CloseWindowCommand = new CloseWindowCommand(currentWindow);
            IDataCommand yesNoCommand = new SetYesNoEnumCommand();
            yesNoCommand.Executed += (sender, content) =>
            {
                object data = content.GetData();
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

        /// <inheritdoc/>
        public T getReturnData<T>()
        {
            Type dataType = typeof(T);
            return dataType == typeof(YesNoEnum) ? (T)Convert.ChangeType(dialogResult, dataType) : default;
        }

        /// <inheritdoc/>
        public object getReturnData()
        {
            return dialogResult;
        }
    }
}
