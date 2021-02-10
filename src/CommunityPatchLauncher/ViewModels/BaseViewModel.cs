using CommunityPatchLauncher.Commands.ApplicationWindow;
using CommunityPatchLauncher.DataContainers;
using CommunityPatchLauncher.UserControls;
using CommunityPatchLauncher.ViewModels.SpecialViews;
using CommunityPatchLauncherFramework.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;
using FontAwesome.WPF;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    /// <summary>
    /// Basic view model which defines some events
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged, IDisposable, IViewModelReloadable
    {
        /// <summary>
        /// Refresh the gui
        /// </summary>
        public ICommand RefreshGuiCommand { get; protected set; }

        /// <summary>
        /// The command used to close the window
        /// </summary>
        public ICommand CloseWindowCommand
        {
            get => closeWindowCommand;
            protected set
            {
                closeWindowCommand = value;
                RaisePropertyChanged("CloseWindowCommand");
            }
        }
        /// <summary>
        /// The command used to close the window
        /// </summary>
        private ICommand closeWindowCommand;

        /// <summary>
        /// The command used to maximize the window
        /// </summary>
        public ICommand MaximizeWindowCommand
        {
            get => maximizeWindowCommand;
            protected set
            {
                maximizeWindowCommand = value;
                RaisePropertyChanged("MaximizeWindowCommand");
            }
        }

        /// <summary>
        /// The command used to maximize the window
        /// </summary>
        private ICommand maximizeWindowCommand;

        /// <summary>
        /// The command used to minimize the window
        /// </summary>
        public ICommand MinimizeWindowCommand
        {
            get => minimizeWindowCommand;
            protected set
            {
                minimizeWindowCommand = value;
                RaisePropertyChanged("MaximizeWindowCommand");
            }
        }

        /// <summary>
        /// The command used to minimize the window
        /// </summary>
        private ICommand minimizeWindowCommand;

        /// <summary>
        /// The command used to change the window size
        /// </summary>
        public ICommand ChangeWindowSizeCommand
        {
            get => changeWindowSizeCommand;
            protected set
            {
                changeWindowSizeCommand = value;
                RaisePropertyChanged("MaximizeWindowCommand");
            }
        }

        /// <summary>
        /// The command used to change the window size
        /// </summary>
        private ICommand changeWindowSizeCommand;

        /// <summary>
        /// The title of the window
        /// </summary>
        public string WindowTitle { get; protected set; }

        /// <summary>
        /// Is the window icon visible
        /// </summary>
        public bool IconVisible
        {
            get => iconVisible;
            protected set
            {
                iconVisible = value;
                RaisePropertyChanged("IconVisible");
            }
        }

        /// <summary>
        /// Is the window icon visible
        /// </summary>
        private bool iconVisible;

        /// <summary>
        /// Property has changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The current window for this view
        /// </summary>
        protected Window currentWindow;

        /// <summary>
        /// The setting manager to use
        /// </summary>
        protected SettingManager settingManager;

        /// <summary>
        /// The last position of the mouse
        /// </summary>
        private Position lastMousePosition;

        /// <summary>
        /// This will block getting the next position once
        /// </summary>
        protected TimeSpan blockPositionTime;

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
        public BaseViewModel(Window window) : this(window, false)
        {
        }


        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="window">The window which uses this model</param>
        public BaseViewModel(Window window, bool addTrigger)
        {
            blockPositionTime = new TimeSpan(0);
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
                AddWindowResizeableCommand();
                RefreshGuiCommand = new RefreshGuiLanguageCommand(currentWindow);
                if (addTrigger)
                {
                    currentWindow.MouseDown += CurrentWindow_MouseDown;
                    currentWindow.MouseUp += CurrentWindow_MouseUp;
                    currentWindow.MouseMove += CurrentWindow_MouseMove;
                }

                SetDefaultWindowStyle();
                SwitchGuiLanguage();
                WindowTitle = currentWindow.Title;
            }

            CloseWindowCommand = new CloseWindowCommand(currentWindow);

            MinimizeWindowCommand = new MinimizeWindowCommand(currentWindow);
            MaximizeWindowCommand = new MaximizeWindowCommand(currentWindow);
            IconVisible = true;
        }

        /// <summary>
        /// Switch the language of the gui
        /// </summary>
        /// <param name="specificIsoCode">The specific iso code to use</param>
        protected void SwitchGuiLanguage(string specificIsoCode)
        {
            settingManager.AddValue("Language", specificIsoCode);
            SwitchGuiLanguage(true);
        }

        /// <summary>
        /// Switch language of the gui
        /// </summary>
        /// <param name="forceRefresh">Should we force refresh the gui</param>
        protected void SwitchGuiLanguage(bool forceRefresh)
        {
            if (currentWindow == null || settingManager == null)
            {
                return;
            }
            string language = settingManager.GetValue<string>("Language");
            if (language != null)
            {
                try
                {
                    CultureInfo info = new CultureInfo(language);
                    Thread.CurrentThread.CurrentUICulture = info;
                    if (forceRefresh)
                    {
                        RefreshGuiCommand?.Execute(null);
                    }
                }
                catch (Exception)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Switch the gui language without force refresh
        /// </summary>
        protected void SwitchGuiLanguage()
        {
            SwitchGuiLanguage(false);
        }

        /// <summary>
        /// Add the command to resize the window
        /// </summary>
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
            DateTime currentTime = DateTime.Now;
            DateTime blockedUntil = currentTime - blockPositionTime;
            if (blockedUntil < currentTime)
            {
                blockPositionTime = new TimeSpan(0);
                return;
            }
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (currentWindow.WindowState == WindowState.Maximized)
                {
                    lastMousePosition = new Position(Mouse.GetPosition(currentWindow));
                }
                currentWindow.DragMove();
            }
        }

        /// <summary>
        /// Mouse up event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void CurrentWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            lastMousePosition = null;
        }

        /// <summary>
        /// Event if the mouse is moved on the form
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event data</param>
        private void CurrentWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (currentWindow.WindowState == WindowState.Maximized && lastMousePosition != null)
                {
                    Position currentPosition = new Position(e.GetPosition(currentWindow));
                    if (currentPosition.DistanceTo(lastMousePosition) > Properties.Settings.Default.WindowMoveDistance)
                    {
                        lastMousePosition = null;
                        currentWindow.WindowState = WindowState.Normal;
                        currentWindow.DragMove();
                    }
                }
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

        /// <inheritdoc/>
        public virtual void Reload()
        {
            settingManager.Reload();
        }
    }
}
