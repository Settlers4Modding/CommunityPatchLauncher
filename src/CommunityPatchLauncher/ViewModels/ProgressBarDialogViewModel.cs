using CommunityPatchLauncher.Commands.ApplicationWindow;
using CommunityPatchLauncher.UserControls.SpecialTypes;
using FontAwesome.WPF;
using System.Windows;

namespace CommunityPatchLauncher.ViewModels
{
    class ProgressBarDialogViewModel : BaseViewModel, IPopupContent
    {
        /// <summary>
        /// Public access to the progress cvalue
        /// </summary>
        public int ProgressValue
        {
            get => progressValue;
            set
            {
                progressValue = value;
                RaisePropertyChanged("ProgressValue");
            }
        }

        /// <summary>
        /// Private accessor for the progress value
        /// </summary>
        private int progressValue;

        /// <summary>
        /// Update the progress bar
        /// </summary>
        /// <param name="currentProgress">Current progress done</param>
        /// <param name="maxProgress">Max progress which need to be done</param>
        public void UpdateProgress(int currentProgress, int maxProgress)
        {
            float value = (float)(currentProgress) / (float)maxProgress;
            ProgressValue = (int)(value * 100);
            if (currentProgress == maxProgress)
            {
                CloseWindowCommand.Execute(null);
            }
        }

        /// <inheritdoc/>
        public void Init(Window currentWindow, FontAwesomeIcon icon, object parameter)
        {
            CloseWindowCommand = new CloseWindowCommand(currentWindow);
        }
    }
}
