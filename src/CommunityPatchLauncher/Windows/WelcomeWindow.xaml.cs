using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Factories;
using CommunityPatchLauncher.ViewModels;
using CommunityPatchLauncherFramework.Documentation.Factory;
using CommunityPatchLauncherFramework.Documentation.Manager;
using CommunityPatchLauncherFramework.Documentation.Strategy;
using CommunityPatchLauncherFramework.Settings.Container;
using CommunityPatchLauncherFramework.Settings.Manager;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CommunityPatchLauncher.Windows
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        /// <summary>
        /// welcome window for the application
        /// </summary>
        public WelcomeWindow()
        {
            InitializeComponent();
            DataContext = new WelcomeViewModel(this);
            //B_Ok.Click += B_Ok_Click;
            //FillAgreement();
        }

        /// <summary>
        /// This method will fill the agreement website with the proper data
        /// </summary>
        private void FillAgreement()
        {
            IDocumentManagerFactory factory = new LocalDocumentManagerFactory();
            DocumentManager documentManager = factory.GetDocumentManager("en-EN", new MarkdownHtmlConvertStrategy());
            ComboBoxItem item = CB_LanguageSelector.SelectedItem as ComboBoxItem;
            WB_Agreement.NavigateToString(documentManager.ReadConvertedDocument(item.Tag.ToString(), "Agreement.md"));
        }

        /// <summary>
        /// If the okay button is clicked
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        //private void B_Ok_Click(object sender, RoutedEventArgs e)
        //{
            //ComboBoxItem selectedLanguage = CB_LanguageSelector.SelectedItem as ComboBoxItem;
            //settingManager.AddValue("Language", selectedLanguage.Tag.ToString());
            //settingManager.AddValue("WelcomeShown", CB_Agree.IsChecked);
            //settingManager.SaveSettings();
            //ShowIfNeeded();
        //}

        /// <summary>
        /// Hide this form if it was already shown and agreed
        /// </summary>
        //private void ShowIfNeeded()
        //{
            //if (settingManager == null)
            //{
                //return;
            //}
            //SettingPair showWelcome = settingManager.GetValue("WelcomeShown");
            //bool welcomeShown = showWelcome != null && showWelcome.GetValue<bool>();
            //if (welcomeShown)
            //{
                //Window mainWindow = new MainWindow();
                //mainWindow.Show();
                //this.Close();
            //}
        //}
    }
}
