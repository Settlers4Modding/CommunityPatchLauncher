using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Container;
using CommunityPatchLauncherFramework.Settings.Factories;
using CommunityPatchLauncherFramework.Settings.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CommunityPatchLauncher.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SettingManager settingManager;

        /// <summary>
        /// Create a new main window
        /// </summary>
        public MainWindow()
        {
            IDataCommand settingManagerCommand = new GetSettingManagerCommand();
            settingManagerCommand.Executed += SettingManagerCommand_Executed;
            settingManagerCommand.Execute(null);
            InitializeComponent();
        }

        private void SettingManagerCommand_Executed(object sender, EventArguments.DataCommandEventArg e)
        {
            settingManager = e.GetData<SettingManager>();
            if (settingManager == null)
            {
                return;
            }
            SettingPair language = settingManager.GetValue("language");
            if (language == null)
            {
                return;
            }
            
            ICommand changeLanguage = new SwitchGuiLanguage();
            changeLanguage.Execute(language.GetValue<string>());
        }
    }
}
