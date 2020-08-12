using CommunityPatchLauncher.BindingData;
using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Commands;
using CommunityPatchLauncherFramework.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;
using FontAwesome.WPF;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    internal class PopupWindowsViewModel : BaseViewModel
    {
        private readonly DockPanel dockPanelToFill;
        private readonly UserControl content;

        public IReadOnlyList<WindowSize> AvailableSizes { get; private set; }

        public bool CustomValues
        {
            get => customValues;
            set
            {
                customValues = value;
                RaisePropertyChanged("CustomValues");
            }
        }
        private bool customValues;

        public WindowSize SelectedSize
        {
            get => selectedSize; set
            {
                selectedSize = value;
                RaisePropertyChanged("SelectedSize");
            }
        }
        private WindowSize selectedSize;

        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                RaisePropertyChanged("SelectedIndex");
            }
        }
        private int selectedIndex;

        public FontAwesomeIcon TitleBarIcon {
            get => titleBarIcon;
            private set
            {
                titleBarIcon = value;
                RaisePropertyChanged("TitleBarIcon");
            }
        }
        private FontAwesomeIcon titleBarIcon;
        public object Parameter { get; }

        public ICommand ResizeWindowCommand { get; private set; }

        public int CustomWidth
        {
            get => customWidth;
            set
            {
                customWidth = value;
            }
        }
        private int customWidth;
        public int CustomHeight
        {
            get => customHeight;
            set
            {
                customHeight = value;
            }
        }
        private int customHeight;

        public PopupWindowsViewModel(Window window, UserControl content, object parameter) : this(window, content, FontAwesomeIcon.None, parameter)
        {
        }

        public PopupWindowsViewModel(Window window, UserControl content, FontAwesomeIcon fontAwesomeIcon, object parameter) : base(window)
        {
            int currentWidth = settingManager.GetValue<int>("Width");
            int currentHeight = settingManager.GetValue<int>("Height");
            bool customValues = settingManager.GetValue<bool>("CustomWindowSize");

            if (parameter is Window windowToResize)
            {
                ResizeWindowCommand = new MultiCommand(new List<ICommand>()
                {
                    new ResizeWindowCommand(windowToResize),
                    new SaveWindowSizeCommand(settingManager),
                    CloseWindowCommand
                });
            }

            Parameter = parameter;
            this.content = content;
            TitleBarIcon = fontAwesomeIcon;
            object controlDock = currentWindow.FindName("DP_ControlDock");
            currentWindow.SizeToContent = SizeToContent.WidthAndHeight;
            if (controlDock is DockPanel dockPanel)
            {
                dockPanelToFill = dockPanel;
                dockPanelToFill.Children.Add(this.content);
            }

            AvailableSizes = new List<WindowSize>()
            {
                new WindowSize(800, 480),
                new WindowSize(800, 600),
                new WindowSize(800, 800)
            };

            if (customValues)
            {
                SelectedIndex = 0;
                CustomValues = customValues; 
            }
            

            if (AvailableSizes.Count > 0)
            {
                int selectedIndex = FindSizeByValues(currentWidth, currentHeight);
                SelectedIndex = selectedIndex;
                SelectedSize = AvailableSizes[SelectedIndex];
                currentHeight = selectedSize.Height;
                currentWidth = selectedSize.Width;
            }

            CustomHeight = currentHeight;
            CustomWidth = currentWidth;

            RaisePropertyChanged("CustomWidth");
            RaisePropertyChanged("CustomHeight");
        }

        private int FindSizeByValues(int width, int height)
        {
            int returnValue = 0;
            for (int i = 0; i < AvailableSizes.Count; i++)
            {
                WindowSize currentSize = AvailableSizes[i];
                if (currentSize.Width == width && currentSize.Height == height)
                {
                    returnValue = i;
                    break;
                }
            }

            return returnValue;
        }

        protected override void AddWindowResizeableCommand()
        {
        }

        public override void Dispose()
        {
            dockPanelToFill.Children.Remove(this.content);
        }
    }
}
