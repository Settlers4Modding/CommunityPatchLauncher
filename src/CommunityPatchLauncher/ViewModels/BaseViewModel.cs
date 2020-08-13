using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.UserControls;
using CommunityPatchLauncherFramework.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;
using FontAwesome.WPF;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    /// <summary>
    /// Basic view model which defines some events
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged, IDisposable
    {
        public ICommand CloseWindowCommand
        { 
            get => closeWindowCommand; 
            protected set
            {
                closeWindowCommand = value;
                RaisePropertyChanged("CloseWindowCommand");
            }
        }
        private ICommand closeWindowCommand;

        public ICommand MaximizeWindowCommand 
        { 
            get => maximizeWindowCommand;
            protected set
            {
                maximizeWindowCommand = value;
                RaisePropertyChanged("MaximizeWindowCommand");
            }
        }
        private ICommand maximizeWindowCommand;

        public ICommand MinimizeWindowCommand
        {
            get => minimizeWindowCommand;
            protected set
            {
                minimizeWindowCommand = value;
                RaisePropertyChanged("MaximizeWindowCommand");
            }
        }
        private ICommand minimizeWindowCommand;

        public ICommand ChangeWindowSizeCommand
        {
            get => changeWindowSizeCommand;
            protected set
            {
                changeWindowSizeCommand = value;
                RaisePropertyChanged("MaximizeWindowCommand");
            }
        }
        private ICommand changeWindowSizeCommand;

        public string WindowTitle { get; protected set; }

        public bool IconVisible { 
            get => iconVisible;  
            protected set
            {
                iconVisible = value;
                RaisePropertyChanged("IconVisible");
            }
        }
        private bool iconVisible;


        /// <summary>
        /// Property has changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The current window for this view
        /// </summary>
        protected Window currentWindow;

        protected SettingManager settingManager;

        /// <summary>
        /// Create a new instance of this class without a attached window
        /// </summary>
        public BaseViewModel() : this(null)
        {
        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="window">The window which uses this model</param>
        public BaseViewModel(Window window)
        {
            ISettingFactory settingFactory = new XmlSettingFactory();
            settingManager = settingFactory.GetSettingsManager();

            currentWindow = window;
            if (currentWindow != null)
            {
                int currentWidth = settingManager.GetValue<int>("Width");
                int currentHeight = settingManager.GetValue<int>("Height");
                if (currentWidth != 0 && currentHeight != 0)
                {
                    currentWindow.Width = currentWidth;
                    currentWindow.Height = currentHeight;
                }
            }

            CloseWindowCommand = new CloseWindowCommand(currentWindow);
            AddWindowResizeableCommand();
            MinimizeWindowCommand = new MinimizeWindowCommand(currentWindow);
            MaximizeWindowCommand = new MaximizeWindowCommand(currentWindow);
            IconVisible = true;

            if (currentWindow != null)
            {
                currentWindow.MouseDown += CurrentWindow_MouseDown;
                SetDefaultWindowStyle();
                WindowTitle = currentWindow.Title;
            }
        }

        protected virtual void AddWindowResizeableCommand()
        {
            ChangeWindowSizeCommand = new OpenCustomPopupWindowCommand(
                currentWindow,
                FontAwesomeIcon.ArrowsAlt,
                Properties.Resources.ResizeWindow_Title,
                new ResizeWindowUserControl(),
                currentWindow
                );
        }

        /// <summary>
        /// Drag event for this window
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void CurrentWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (currentWindow.WindowState == WindowState.Maximized)
                {
                    currentWindow.WindowState = WindowState.Normal;
                }
                currentWindow.DragMove();
            }
        }

        /// <summary>
        /// Set the default window style
        /// </summary>
        protected void SetDefaultWindowStyle()
        {
            try
            {
                currentWindow.Style = currentWindow.Resources["WindowStyle"] as Style;
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// A property has changed
        /// </summary>
        /// <param name="prop"></param>
        internal void RaisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        /// <summary>
        /// Dispose this view
        /// </summary>
        public virtual void Dispose()
        {
        }
    }
}
