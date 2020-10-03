using CommunityPatchLauncher.Commands.ApplicationWindow;
using CommunityPatchLauncher.UserControls.SpecialTypes;
using FontAwesome.WPF;
using System.Windows;
using System.Windows.Media;

namespace CommunityPatchLauncher.ViewModels
{
    internal class InfoPopupViewModel : BaseViewModel, IPopupContent
    {
        public string DialogText { get; private set; }

        public FontAwesomeIcon BoxIcon
        {
            get => boxIcon;
            private set
            {
                boxIcon = value;
                RaisePropertyChanged("BoxIcon");
            }
        }
        private FontAwesomeIcon boxIcon;

        public Brush IconBrush
        {
            get => iconBrush;
            private set
            {
                iconBrush = value;
                RaisePropertyChanged("IconColor");
            }
        }
        private Brush iconBrush;

        public InfoPopupViewModel()
        {

        }

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
