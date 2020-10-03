using CommunityPatchLauncher.BindingData;
using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Commands.ApplicationWindow;
using CommunityPatchLauncher.Commands.Settings;
using CommunityPatchLauncher.UserControls.SpecialTypes;
using FontAwesome.WPF;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    /// <summary>
    /// This class is the resize window view model
    /// </summary>
    internal class ResizeWindowViewModel : BaseViewModel, IPopupContent
    {

        /// <summary>
        /// The command used to resize the window
        /// </summary>
        public ICommand ResizeWindowCommand
        {
            get => resizeWindowCommand;
            private set
            {
                resizeWindowCommand = value;
                RaisePropertyChanged("ResizeWindowCommand");
            }
        }
        /// <summary>
        /// The private command used to resize the window
        /// </summary>
        private ICommand resizeWindowCommand;

        /// <summary>
        /// All the available sizes
        /// </summary>
        public IReadOnlyList<WindowSize> AvailableSizes { get; private set; }

        /// <summary>
        /// Is the user using custom values
        /// </summary>
        public bool CustomValues
        {
            get => customValues;
            set
            {
                customValues = value;
                RaisePropertyChanged("CustomValues");
            }
        }
        /// <summary>
        /// Private is the user using custom values
        /// </summary>
        private bool customValues;

        /// <summary>
        /// The selected window size
        /// </summary>
        public WindowSize SelectedSize
        {
            get => selectedSize; set
            {
                selectedSize = value;
                RaisePropertyChanged("SelectedSize");
            }
        }
        /// <summary>
        /// The private selected size
        /// </summary>
        private WindowSize selectedSize;

        /// <summary>
        /// The selected index
        /// </summary>
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                RaisePropertyChanged("SelectedIndex");
            }
        }
        /// <summary>
        /// The private selected index
        /// </summary>
        private int selectedIndex;

        /// <summary>
        /// The parameter of this popup content
        /// </summary>
        public object Parameter { get; private set; }

        /// <summary>
        /// The custome width entered by the user
        /// </summary>
        public int CustomWidth { get; set; }

        /// <summary>
        /// The custom height entered by the user
        /// </summary>
        public int CustomHeight { get; set; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public ResizeWindowViewModel()
        {
            WindowsSizeService windowsSizeService = new WindowsSizeService();
            AvailableSizes = windowsSizeService.GetWindowSizes();
        }

        /// <inheritdoc/>
        protected override void AddWindowResizeableCommand()
        {
            
        }

        /// <summary>
        /// Find a specific size by given values
        /// </summary>
        /// <param name="width">The width to search</param>
        /// <param name="height">The height to search</param>
        /// <returns></returns>
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

        /// <inheritdoc/>
        public void Init(Window currentWindow, FontAwesomeIcon icon, object parameter)
        {
            CloseWindowCommand = new CloseWindowCommand(currentWindow);
            if (parameter is Window windowToResize)
            {
                ResizeWindowCommand = new MultiCommand(new List<ICommand>()
                {
                    new ResizeWindowCommand(windowToResize),
                    new SaveWindowSizeCommand(settingManager),
                    CloseWindowCommand
                });
            }
            SetTextBoxes();
        }

        /// <summary>
        /// Set the content for the text boxes
        /// </summary>
        private void SetTextBoxes()
        {
            int currentWidth = settingManager.GetValue<int>("Width");
            int currentHeight = settingManager.GetValue<int>("Height");
            bool customValues = settingManager.GetValue<bool>("CustomWindowSize");

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
                if (!customValues)
                {
                    currentHeight = selectedSize.Height;
                    currentWidth = selectedSize.Width;
                }
            }

            CustomHeight = currentHeight;
            CustomWidth = currentWidth;

            RaisePropertyChanged("CustomWidth");
            RaisePropertyChanged("CustomHeight");
        }
    }
}
