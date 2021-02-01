﻿using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Commands.ApplicationWindow;
using CommunityPatchLauncher.Commands.TaskCommands;
using CommunityPatchLauncher.Documentation.Factories;
using CommunityPatchLauncher.Documentation.Strategy;
using CommunityPatchLauncher.Enums;
using CommunityPatchLauncherFramework.Documentation.Factory;
using CommunityPatchLauncherFramework.Documentation.Manager;
using CommunityPatchLauncherFramework.Documentation.Strategy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CommunityPatchLauncher.ViewModels
{
    /// <summary>
    /// View model to use for the launch game user control
    /// </summary>
    internal class LaunchGameModelView : BaseViewModel
    {
        /// <summary>
        /// The command used to launch the game
        /// </summary>
        public ICommand LaunchGameCommand { get; private set; }

        /// <summary>
        /// The patch to use for this launch button
        /// </summary>
        public Patch PatchToUse
        {
            get => patchToUse;
            private set
            {
                patchToUse = value;
                RaisePropertyChanged("PatchToUse");
            }
        }
        /// <summary>
        /// private acces to the patch to use
        /// </summary>
        private Patch patchToUse;

        /// <summary>
        /// The current speed mode
        /// </summary>
        public SpeedModes Speed
        {
            get
            {
                return speed;
            }
            set
            {
                if (value == SpeedModes.Unknown)
                {
                    return;
                }
                speed = value;
                RaisePropertyChanged("Speed");
            }
        }
        /// <summary>
        /// Private current speed mode
        /// </summary>
        private SpeedModes speed;

        /// <summary>
        /// The current path to save the seting to
        /// </summary>
        private string currentSpeedPath;

        /// <summary>
        /// Content of the changelog
        /// </summary>
        public string ChangelogContent
        {
            get => changelogContent;
            private set
            {
                changelogContent = value;
                RaisePropertyChanged("ChangelogContent");
            }
        }
        /// <summary>
        /// Private content of the change log
        /// </summary>
        private string changelogContent;

        /// <summary>
        /// This is the the patch description
        /// </summary>
        public string PatchDescription
        {
            get => patchDescription;
            private set
            {
                patchDescription = value;
                RaisePropertyChanged("PatchDescription");
            }
        }
        /// <summary>
        /// This is the private parch description
        /// </summary>
        private string patchDescription;

        /// <summary>
        /// The progress value for the bar
        /// </summary>
        public int ProgressValue
        {
            get => progressValue;
            set
            {
                progressValue = value;
                RaisePropertyChanged("ProgressValue");
            }
        }

        /// <summary>
        /// Private accessor for progress value
        /// </summary>
        private int progressValue;

        private UserControl parent;


        /// <summary>
        /// The local manager factory to use
        /// </summary>
        private readonly IDocumentManagerFactory localDocumentManagerFactory;

        /// <summary>
        /// The remote manager factory to use
        /// </summary>
        private readonly DocumentManager remoteDocumentManager;

        /// <summary>
        /// Create a new instance of this view model
        /// </summary>
        public LaunchGameModelView(UserControl parent)
        {
            this.parent = parent;
            IProgressCommand launchGameCommand = new LaunchGameCommand(settingManager);
            ICommand toggleCommand = new ToggleVisiblityCommand(parent, "PB_DownloadState");
            launchGameCommand.ProgressChanged += (sender, data) =>
            {
                float percent = (float)data.CurrentWorkload / (float)data.TotalWorkload;
                ProgressValue = (int)(percent * 100);
            };
            launchGameCommand.Executed += (sender, data) =>
            {
                toggleCommand.Execute(string.Empty);
            };
            LaunchGameCommand = new MultiCommand(new List<ICommand>() {
                toggleCommand,
                launchGameCommand,
            });

            IEnumerable<WebBrowser> browserObjects = FindVisualChildren<WebBrowser>((DependencyObject)parent.Content);
            foreach (WebBrowser browser in browserObjects)
            {
                browser.PreviewKeyDown += (sender, eventArgs) =>
                {
                    eventArgs.Handled = eventArgs.Key == Key.F5;
                };
                browser.Navigating += (sender, eventArgs) =>
                {
                    if (eventArgs.Uri == null)
                    {
                        return;
                    }
                    string url = eventArgs.Uri.ToString();
                    url = url.ToLower();
                    if (url.StartsWith("http"))
                    {
                        eventArgs.Cancel = true;
                        ICommand openLink = new OpenLinkCommand(url);
                        openLink.Execute(null);
                    }
                };
            }

            localDocumentManagerFactory = new LocalDocumentManagerFactory();
            IDocumentManagerFactory remoteDocumentManagerFactory = new RemoteDocumentManagerFactory();
            remoteDocumentManager = remoteDocumentManagerFactory.GetDocumentManager(
                "en-EN",
                new MarkdownHtmlConvertStrategy()
                );
        }

        /// <summary>
        /// Set the patch for this view model
        /// </summary>
        /// <param name="availablePatch">The patch to use</param>
        public void SetPatch(Patch availablePatch)
        {
            if (PatchToUse != null && availablePatch != null && availablePatch.RealPatch == PatchToUse.RealPatch)
            {
                return;
            }
            LoadChangelog(availablePatch);
            PatchToUse = availablePatch;
            currentSpeedPath = patchToUse.RealPatch.ToString() + "/Speed";
            string speedString = settingManager.GetValue<string>(currentSpeedPath);
            SpeedModes newMode = SpeedModes.Normal;
            if (speedString != null)
            {
                Enum.TryParse(speedString, out newMode);
            }
            Speed = newMode;

            DocumentManager scrollLessDocumentManager = localDocumentManagerFactory.GetDocumentManager(
                "en-EN",
                new MarkdownHtmlWithoutScrollStrategy()
            );
            string language = settingManager.GetValue<string>("Language");
            if (language != null)
            {
                PatchDescription = scrollLessDocumentManager.ReadConvertedDocument(
                language,
                patchToUse.RealPatch.ToString() + ".md"
            );
            }
        }

        /// <summary>
        /// Load the changelog file
        /// </summary>
        /// <param name="availablePatch">Load the changelog for the following patch</param>
        private void LoadChangelog(Patch availablePatch)
        {
            string language = settingManager.GetValue<string>("Language");

            DocumentManager localDocumentManager = localDocumentManagerFactory.GetDocumentManager(
                "en-EN",
                new MarkdownHtmlConvertStrategy()
                );
            ChangelogContent = localDocumentManager.ReadConvertedDocument(
                language,
                Properties.Settings.Default.FileLoading
                );

            string fileName = availablePatch.RealPatch + Properties.Settings.Default.PatchChangelogFileName;
            Task<string> fileContent = remoteDocumentManager.ReadConvertedDocumentAsync(language, fileName);
            fileContent.ContinueWith((data) =>
            {
                ChangelogContent = data.Result == string.Empty ? 
                                    localDocumentManager.ReadConvertedDocument(
                                        language,
                                        Properties.Settings.Default.NotReadableFile
                                        ) : 
                                    data.Result;
            });
        }

        /// <summary>
        /// Find all elements which are child of the depobj with a specific type
        /// </summary>
        /// <typeparam name="T">The type to search for</typeparam>
        /// <param name="depObj">The base object to crawl on</param>
        /// <returns>A list with all elements of type T</returns>
        private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        /// <summary>
        /// Save the data to the setting manager
        /// </summary>
        private void SaveData()
        {
            settingManager.AddValue(currentSpeedPath, speed.ToString());
            settingManager.SaveSettings();
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            base.Dispose();
            SaveData();
        }

        /// <inheritdoc/>
        public override void Reload()
        {
            settingManager.Reload();
        }
    }
}
