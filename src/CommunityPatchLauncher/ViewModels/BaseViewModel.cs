using System;
using System.ComponentModel;
using System.Windows;

namespace CommunityPatchLauncher.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected Window currentWindow;

        public BaseViewModel() : this(null)
        {
        }

        public BaseViewModel(Window window)
        {
            currentWindow = window;
        }

        internal void RaisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public virtual void Dispose()
        {
        }
    }
}
