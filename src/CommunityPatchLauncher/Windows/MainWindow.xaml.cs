using CommunityPatchLauncher.ViewModels;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;

namespace CommunityPatchLauncher.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Create a new main window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowModel(this);
        }



        #region Downloader
        private void UpgradetoSettlersUnited(object sender, RoutedEventArgs e)
        {
            UpgradetoSettlersUnited();

        }
        private void UpgradetoSettlersUnited()
        {
           
            string URI = "https://files.settlers-united.com/Settlers-United.exe";
            DownloadFileAsync(URI, "SettlersUnitedSetup.exe", "Upgrade auf Settlers United (Beta)" , true);
        }
        private void DownloadFileAsync(string URI, string File, string Name, bool SettlersUnited = false)
        {
            DownlaodPanel.Visibility = Visibility.Visible;
            DownlaodLabel.Content = "Herunterladen des Updates..." + "\n" + Name;

            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadProgressChanged += DownloadProgressChanged;
                    wc.DownloadFileCompleted += DownloadFileEventCompletedUnited;
                    wc.DownloadFileAsync(new Uri(URI), File);
                }
            }
            catch (Exception)
            {
            }

           
        }


        private void DownloadFileEventCompletedUnited(object sender, AsyncCompletedEventArgs e)
        {
            //ToDo Install first HE!!! @Leonards05 | Pumpline#5578
            var startInfo = new ProcessStartInfo
            {
                FileName = "SettlersUnitedSetup.exe",

                Verb = "runas"
            };
            Process.Start(startInfo);
            Environment.Exit(0);
        }

        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            DownlaodPanel.Visibility = Visibility.Visible;
            ProgressBar.Value = e.ProgressPercentage;
        }
        #endregion
    }
}
