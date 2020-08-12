using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.UserControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CommunityPatchLauncher.ViewModels
{
    internal class MainWindowModel : BaseViewModel
    {
        public ICommand LaunchGameCommand { get; private set; }
        public ICommand ComingSoonCommand { get; private set; }

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
            IconVisible = false;
            object dockArea = window.FindName("DP_ContentDock");
            if (dockArea is DockPanel panel)
            {
                contentDock = panel;

                LaunchGameCommand = new OpenControlToPanel(contentDock, new PatchVersionSelectionUserControl(currentWindow));
                ComingSoonCommand = new OpenControlToPanel(contentDock, new ComingSoonControl());
            }

            ChangeGroupVisiblity = new ToggleVisibilityCommand(currentWindow);
            ChangeGroupVisiblity.Executed += (sender, data) =>
            {
                //HomeGroupVisible = data.GetData<Visibility>();
            };
            HomeGroupVisible = Visibility.Visible;
        }


    }
}
