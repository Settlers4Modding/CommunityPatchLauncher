using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.UserControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    internal class MainWindowModel : BaseViewModel
    {
        public ICommand CloseApplication { get; private set; }
        public ICommand MaximizeWindow { get; private set; }
        public ICommand MinimizeWindow { get; private set; }

        public ICommand OpenHomeCommand { get; private set; }

        public IDataCommand ChangeGroupVisiblity { get; private set; }

        public Visibility HomeGroupVisible {
            get => homeGroupVisible;
            private set
            {
                homeGroupVisible = value;
                RaisePropertyChanged("HomeGroupVisible");
            }
        }
        private Visibility homeGroupVisible;

        private readonly DockPanel contentDock;


        public MainWindowModel(Window window) : base(window)
        {
            CloseApplication = new CloseApplicationCommand();
            MinimizeWindow = new MinimizeWindowCommand(currentWindow);
            MaximizeWindow = new MaximizeWindowCommand(currentWindow);

            object dockArea = window.FindName("DP_ContentDock");
            if (dockArea is DockPanel panel)
            {
                contentDock = panel;

                OpenHomeCommand = new OpenControlToPanel(contentDock, new PlayUserControl());
            }

            ChangeGroupVisiblity = new ToggleVisibilityCommand();
            ChangeGroupVisiblity.Executed += (sender, data) =>
            {
                HomeGroupVisible = data.GetData<Visibility>();
            };
            HomeGroupVisible = Visibility.Visible;
        }


    }
}
