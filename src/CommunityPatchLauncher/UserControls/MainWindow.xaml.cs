using CommunityPatchLauncher.ViewModels;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;

using CommunityPatchLauncher.ViewModels;
using System.IO.Compression;
using System.Threading.Tasks;
using System.IO;
using CommunityPatchLauncherFramework.Settings.Manager;
using Microsoft.Win32;
using System.Windows.Controls;

namespace CommunityPatchLauncher.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// The setting manager to use
        /// </summary>
        protected SettingManager settingManager;


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
            Task upgradeTask = UpgradetoSettlersUnited();
            if (sender is Button button)
            {
                button.IsEnabled = false;
                upgradeTask.ContinueWith(_ => Dispatcher.Invoke(() => button.IsEnabled = true));
            }
        }
        private async Task UpgradetoSettlersUnited()
        {
            await ZipInstallerAsync();
            string URI = "https://files.settlers-united.com/Settlers-United.exe";
            var filename = System.Environment.GetEnvironmentVariable("TEMP") + "\\SettlersUnitedSetup.exe";
            DownloadFileAsync(URI, filename, Properties.Resources.Setters_United_Beta, true);
        }
        private void DownloadFileAsync(string URI, string File, string Name, bool SettlersUnited = false)
        {
            DownlaodPanel.Visibility = Visibility.Visible;
            DownlaodLabel.Content = Properties.Resources.Download_United_Update + Environment.NewLine + Name;

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
            var filename = System.Environment.GetEnvironmentVariable("TEMP") + "\\SettlersUnitedSetup.exe";

            var startInfo = new ProcessStartInfo
            {
                FileName = filename,
                Verb = "runas"
            };
            try
            {
                Process.Start(startInfo);
                Environment.Exit(0);
            }
            catch (Exception)
            {
                DownlaodLabel.Content = Properties.Resources.Download_United_Aborted;
            }
            finally
            {
                if (sender is Button button)
                {
                    button.IsEnabled = true;
                }
            }
        }

        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            DownlaodPanel.Visibility = Visibility.Visible;
            ProgressBar.Value = e.ProgressPercentage;
        }
        #endregion

        #region ZIPworker

        public async Task ZipInstallerAsync()
        {
            string zipFilePath = @"assets\HistoryEdition.zip";

            string InstallPath = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Ubisoft\Launcher\Installs\11785", "InstallDir", null);

            using (ZipArchive archive = await Task.Run(() => ZipFile.OpenRead(zipFilePath)))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    string completeFileName = Path.Combine(InstallPath, entry.FullName);
                    string directory = Path.GetDirectoryName(completeFileName);

                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    try
                    {
                        if (entry.FullName.EndsWith(@".map", StringComparison.OrdinalIgnoreCase))
                        {
                            if (!File.Exists(InstallPath + completeFileName))
                            {
                                if (!entry.FullName.EndsWith(@"/", StringComparison.OrdinalIgnoreCase))
                                {
                                    await Task.Run(() => entry.ExtractToFile(completeFileName, true));
                                }
                            }
                            else if (!File.Equals(InstallPath + completeFileName, entry))
                            {

                                if (!entry.FullName.EndsWith(@"/", StringComparison.OrdinalIgnoreCase))
                                {
                                    await Task.Run(() => entry.ExtractToFile(completeFileName, true));
                                }
                            }
                        }
                        else
                        {
                            if (!entry.FullName.EndsWith(@"/", StringComparison.OrdinalIgnoreCase))
                            {
                                await Task.Run(() =>
                                {
                                    try
                                    {
                                        entry.ExtractToFile(completeFileName, true);
                                    }
                                    catch
                                    {
                                        MessageBox.Show("Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        Environment.Exit(0);
                                    }
                                });
                            }
                        }

                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        #endregion
    }
}
