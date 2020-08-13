﻿using CommunityPatchLauncher.Windows;
using FontAwesome.WPF;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CommunityPatchLauncher.Commands
{
    /// <summary>
    /// This command will open a custom popup window
    /// </summary>
    internal class OpenCustomPopupWindowCommand : BaseCommand
    {
        private readonly bool readyToShow;
        private readonly Window parentWindow;
        private readonly FontAwesomeIcon fontAwesomeIcon;
        private readonly string title;
        private readonly UserControl controlToOpen;
        private readonly object parameter;

        public OpenCustomPopupWindowCommand(Window parentWindow, UserControl controlToOpen) : this(parentWindow, FontAwesomeIcon.Info, string.Empty, controlToOpen, null)
        {
        }

        public OpenCustomPopupWindowCommand(Window parentWindow, string title, UserControl controlToOpen) : this(parentWindow, FontAwesomeIcon.Info, title, controlToOpen, null)
        {
        }

        public OpenCustomPopupWindowCommand(
            Window parentWindow,
            FontAwesomeIcon fontAwesomeIcon,
            string title,
            UserControl controlToOpen
            ) : this(parentWindow, fontAwesomeIcon, title, controlToOpen, null)
        {
        }

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

        public override bool CanExecute(object parameter)
        {
            return readyToShow;
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
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
            
            
            return;
        }
    }
}
