using FontAwesome.WPF;
using System.Windows;

namespace CommunityPatchLauncher.UserControls.SpecialTypes
{
    interface IPopupContent
    {
        void Init(Window currentWindow, FontAwesomeIcon icon, object parameter);
    }
}
