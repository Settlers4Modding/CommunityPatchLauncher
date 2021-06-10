using CommunityPatchLauncher.BindingData.Container;
using CommunityPatchLauncher.Commands;
using CommunityPatchLauncher.Commands.ApplicationWindow;
using CommunityPatchLauncher.Commands.Condition;
using CommunityPatchLauncher.Commands.TaskCommands;
using CommunityPatchLauncher.Documentation.Factories;
using CommunityPatchLauncher.Documentation.Strategy;
using CommunityPatchLauncher.Enums;
using CommunityPatchLauncher.UserControls;
using CommunityPatchLauncherFramework.Documentation.Factory;
using CommunityPatchLauncherFramework.Documentation.Manager;
using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Parent user control
        /// </summary>
        private readonly UserControl parent;

        /// <summary>
        /// Create a new instance of this view model
        /// </summary>
        public LaunchGameModelView(UserControl parent, Window mainWindow) : base(mainWindow)
        {
            this.parent = parent;
            IProgressCommand launchGameCommand = new LaunchGameCommand(settingManager);
            ICommand toggleCommand = new ToggleVisiblityCommand(parent, "PB_DownloadState");
            ICommand minimizeCommand = new MinimizeWindowCommand(
                                            mainWindow,
                                            new SpecificSettingSetCondition(
                                                            settingManager,
                                                            "minimizeOnGameStart"
                                                            )
                                            );

            BrowserModelView changelogModelView = GetChangelogView();
            changelogModelView.ChangeDocumentProdiver(new RemoteDocumentManagerFactory(new TimeSpan(0, 60, 0)));

            BrowserModelView patchInfoModelView = GetInfoView();
            IDocumentManagerFactory factory = new LocalDocumentManagerFactory();
            DocumentManager patchInfoManager = factory.GetDocumentManager("en-EN", new MarkdownHtmlWithoutScrollStrategy());
            patchInfoModelView.ShowLoading(false);
            patchInfoModelView.ChangeDocumentProdiver(patchInfoManager);

            launchGameCommand.ProgressChanged += (sender, data) =>
            {
                float percent = (float)data.CurrentWorkload / (float)data.TotalWorkload;
                ProgressValue = (int)(percent * 100);
            };
            launchGameCommand.Executed += (sender, data) =>
            {
                toggleCommand.Execute(string.Empty);
                minimizeCommand.Execute(null);
            };
            LaunchGameCommand = new MultiCommand(new List<ICommand>() {
                toggleCommand,
                launchGameCommand,
            });
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
            //settingManager.GetValue<string>("");
            currentSpeedPath = patchToUse.RealPatch.ToString() + "/Speed";
            string speedString = settingManager.GetValue<string>(currentSpeedPath);
            SpeedModes newMode = SpeedModes.Normal;
            if (speedString != null)
            {
                Enum.TryParse(speedString, out newMode);
            }
            Speed = newMode;
            LoadPatchInformation(availablePatch);
        }

        /// <summary>
        /// Load the changelog file
        /// </summary>
        /// <param name="availablePatch">Load the changelog for the following patch</param>
        private void LoadChangelog(Patch availablePatch)
        {
            BrowserModelView modelView = GetChangelogView();
            string fileName = availablePatch.RealPatch + Properties.Settings.Default.PatchChangelogFileName;
            modelView.ChangeDocument(fileName);
        }

        /// <summary>
        /// Load the patch information
        /// </summary>
        /// <param name="availablePatch">Current patch</param>
        private void LoadPatchInformation(Patch availablePatch)
        {
            BrowserModelView modelView = GetInfoView();
            string fileName = availablePatch.RealPatch.ToString() + ".md";
            modelView.ChangeDocument(fileName);
        }

        /// <summary>
        /// Get the changelog view
        /// </summary>
        /// <returns>The browser view</returns>
        private BrowserModelView GetChangelogView()
        {
            return GetBrowserViewByName("Changelog");
        }

        /// <summary>
        /// Get the info view
        /// </summary>
        /// <returns>The browser view</returns>
        private BrowserModelView GetInfoView()
        {
            return GetBrowserViewByName("PatchInformation");
        }

        /// <summary>
        /// Get the browser view by name
        /// </summary>
        /// <param name="name">The name of the browser to return</param>
        /// <returns>The browser view</returns>
        private BrowserModelView GetBrowserViewByName(string name)
        {
            object infoBrowser = parent.FindName(name);
            if (infoBrowser is BrowserUserControl browserContent)
            {
                if (browserContent.DataContext is BrowserModelView modelView)
                {
                    return modelView;
                }
            }
            return null;
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
