using CommunityPatchLauncher.Commands;
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
        public ICommand CloseApplication { get; private set; }
        public ICommand MaximizeWindow { get; private set; }
        public ICommand MinimizeWindow { get; private set; }


        /// <summary>
        /// Property has changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The current window for this view
        /// </summary>
        protected Window currentWindow;

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
            currentWindow = window;

            CloseApplication = new CloseApplicationCommand();
            MinimizeWindow = new MinimizeWindowCommand(currentWindow);
            MaximizeWindow = new MaximizeWindowCommand(currentWindow);
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
