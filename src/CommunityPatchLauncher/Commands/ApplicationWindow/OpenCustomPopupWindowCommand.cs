using CommunityPatchLauncher.UserControls.SpecialTypes;
using CommunityPatchLauncher.Windows;
using FontAwesome.WPF;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CommunityPatchLauncher.Commands.ApplicationWindow
{
    /// <summary>
    /// This command will open a custom popup window
    /// </summary>
    internal class OpenCustomPopupWindowCommand : BaseDataCommand
    {
        /// <summary>
        /// Is the popup ready to show
        /// </summary>
        private readonly bool readyToShow;

        /// <summary>
        /// The parent window to use
        /// </summary>
        private readonly Window parentWindow;

        /// <summary>
        /// The icon to use
        /// </summary>
        private readonly FontAwesomeIcon fontAwesomeIcon;

        /// <summary>
        /// The title to use
        /// </summary>
        private readonly string title;

        /// <summary>
        /// The control to include
        /// </summary>
        private readonly UserControl controlToOpen;

        /// <summary>
        /// The parameter for the window
        /// </summary>
        private readonly object parameter;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="parentWindow">The parent window</param>
        /// <param name="controlToOpen">The control to open</param>
        public OpenCustomPopupWindowCommand(
            Window parentWindow,
            UserControl controlToOpen
            ) : this(parentWindow, FontAwesomeIcon.Info, string.Empty, controlToOpen, null)
        {
        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="parentWindow">The parent window</param>
        /// <param name="title">The title to use</param>
        /// <param name="controlToOpen">The control to open</param>
        public OpenCustomPopupWindowCommand(
            Window parentWindow,
            string title,
            UserControl
            controlToOpen
            ) : this(parentWindow, FontAwesomeIcon.Info, title, controlToOpen, null)
        {
        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="parentWindow">The parent window</param>
        /// <param name="fontAwesomeIcon">The icon to use for the title bar</param>
        /// <param name="title">The title to use</param>
        /// <param name="controlToOpen">The control to open</param>
        public OpenCustomPopupWindowCommand(
            Window parentWindow,
            FontAwesomeIcon fontAwesomeIcon,
            string title,
            UserControl controlToOpen
            ) : this(parentWindow, fontAwesomeIcon, title, controlToOpen, null)
        {
        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="parentWindow">The parent window</param>
        /// <param name="fontAwesomeIcon">The icon to use for the title bar</param>
        /// <param name="title">The title to use</param>
        /// <param name="controlToOpen">The control to open</param>
        /// <param name="parameter">The parameter to send to the popup window</param>
        public OpenCustomPopupWindowCommand(
            Window parentWindow,
            FontAwesomeIcon fontAwesomeIcon,
            string title,
            UserControl controlToOpen,
            object parameter
            )
        {
            if (parentWindow == null || controlToOpen == null)
            {
                return;
            }
            readyToShow = true;
            this.parentWindow = parentWindow;
            this.fontAwesomeIcon = fontAwesomeIcon;
            this.title = title;
            this.controlToOpen = controlToOpen;
            this.parameter = parameter;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return readyToShow;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            data = null;
            if (!CanExecute(parameter))
            {
                ExecutionDone();
                return;
            }

            object realParameter = this.parameter ?? parameter;

            if (parameter != null && this.parameter != null)
            {
                List<object> parameterList = new List<object>();
                parameterList.Add(parameter);
                parameterList.Add(this.parameter);
                realParameter = parameterList;
            }

            Window windowToOpen = new PopupWindow(controlToOpen, title, fontAwesomeIcon, realParameter);
            if (parentWindow.ShowActivated)
            {
                windowToOpen.Owner = Window.GetWindow(parentWindow);
                windowToOpen.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            windowToOpen?.ShowDialog();

            if (controlToOpen.DataContext is IPopupReturnDataContent returnDataWindow)
            {
                data = returnDataWindow.getReturnData();
            }

            ExecutionDone();
            return;
        }
    }
}
