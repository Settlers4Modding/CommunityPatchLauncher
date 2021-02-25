using CommunityPatchLauncher.Commands.ApplicationWindow;
using CommunityPatchLauncher.UserControls.SpecialTypes;
using FontAwesome.WPF;
using System.Windows;
using System.Windows.Media;

namespace CommunityPatchLauncher.ViewModels
{
    /// <summary>
    /// Simple popup info view model
    /// </summary>
    internal class InfoPopupViewModel : BaseViewModel, IPopupContent
    {
        /// <summary>
        /// The text to show
        /// </summary>
        public string DialogText
        {
            get => dialogText;
            private set
            {
                dialogText = value;
                RaisePropertyChanged("DialogText");
            }
        }

        /// <summary>
        /// Private text to show
        /// </summary>
        private string dialogText;

        /// <summary>
        /// The icon to show
        /// </summary>
        public FontAwesomeIcon BoxIcon
        {
            get => boxIcon;
            private set
            {
                boxIcon = value;
                RaisePropertyChanged("BoxIcon");
            }
        }

        /// <summary>
        /// Private accessor for icon to show
        /// </summary>
        private FontAwesomeIcon boxIcon;

        /// <summary>
        /// The icon color to use
        /// </summary>
        public Brush IconBrush
        {
            get => iconBrush;
            private set
            {
                iconBrush = value;
                RaisePropertyChanged("IconBrush");
            }
        }
        /// <summary>
        /// Private accessor for icon color
        /// </summary>
        private Brush iconBrush;

        /// <inheritdoc/>
        public void Init(Window currentWindow, FontAwesomeIcon icon, object parameter)
        {
            BoxIcon = icon;
            switch (BoxIcon)
            {
                case FontAwesomeIcon.Warning:
                    IconBrush = Brushes.Yellow;
                    break;
                case FontAwesomeIcon.Exclamation:
                case FontAwesomeIcon.ExclamationCircle:
                case FontAwesomeIcon.Times:
                case FontAwesomeIcon.TimesCircle:
                case FontAwesomeIcon.TimesCircleOutline:
                    IconBrush = Brushes.Red;
                    break;
                default:
                    IconBrush = Brushes.White;
                    break;
            }

            CloseWindowCommand = new CloseWindowCommand(currentWindow);
            DialogText = parameter.ToString();
        }
    }
}
