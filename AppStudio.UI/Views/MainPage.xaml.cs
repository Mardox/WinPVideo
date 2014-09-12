#define DEBUG_AGENT
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

using Microsoft.Phone.Controls;
using Microsoft.Phone.Net.NetworkInformation;

using AppStudio.Services;
using Microsoft.Phone.Scheduler;
using System.IO.IsolatedStorage;
using System.Collections.Generic;

namespace AppStudio
{
    public partial class MainPage
    {
        //for backgroundtask
        PeriodicTask periodicTask;
        string periodicTaskName = "PeriodicAgent";
        ResourceIntensiveTask resourceIntensiveTask;
        string resourceIntensiveTaskName = "ResourceIntensiveAgent";
        public bool agentsAreEnabled = true;

        #region Data for the MainPage
        string[] categoryType = {
                                    "Search",
                                    "Playlist"
                                };

        string[] categoryName = {
                                    "Trailers",
                                    "Music",
                                    "Java",
                                    "iPhone 6",
                                    "Google I/O"
                                };

        string[] queryName = {
                                 "Movie Trailers 2014",
                                 "Music 2014",
                                 "PLDAA5DE54FB5215EC", //Playlist ID
                                 "iphone 6",
                                 "PLOU2XLYxmsIJQe6T9CKafiDm7p_LCCx6F" //Playlist ID
                             };


        #endregion

        public MainPage()
        {
            InitializeComponent();
            MainViewModels = new MainViewModels();

            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            try
            {
                if ((bool)settings["toastNotifications"])
                {
                    StartPeriodicAgent();
                }
                else
                {
                    RemoveAgent(periodicTaskName);
                }
            }
            catch(KeyNotFoundException ex)
            {
                StartPeriodicAgent();
            }

            
        }

        void SaveSettings()
        {
            string[] finalQuery = {
                                      categoryType[0] + queryName[0],
                                      categoryType[0] + queryName[1],
                                      categoryType[1] + queryName[2],
                                      categoryType[0] + queryName[3],
                                      categoryType[1] + queryName[4],
                                  };

            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            // SearchInput is a TextBox defined in XAML.
            for (int i = 0; i < 5; i++)
            {
                if (!settings.Contains("Item" + (i + 1) + "Data"))
                {
                    settings.Add("Item" + (i+1) + "Data", finalQuery[i]);
                }
                else
                {
                    settings["Item" + (i + 1) + "Data"] = finalQuery[i];
                }
            }
            settings.Save();

            //Titles for items
            Item1.Header = categoryName[0];
            Item2.Header = categoryName[1];
            Item3.Header = categoryName[2];
            Item4.Header = categoryName[3];
            Item5.Header = categoryName[4];
        }

        public MainViewModels MainViewModels { get; private set; }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            // Main page must be always the top entry
            NavigationService.RemoveBackEntry();

            MainViewModels.SetViewType(ViewTypes.List);

            DataContext = MainViewModels;
            MainViewModels.UpdateAppBar();

            SaveSettings();

            await MainViewModels.LoadData(NetworkInterface.GetIsNetworkAvailable());

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            SpeechServices.Stop();
            base.OnNavigatedFrom(e);
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var panorama = sender as Panorama;
            if (panorama != null)
            {
                var item = panorama.SelectedItem as PanoramaItem;
                if (item != null)
                {
                    MainViewModels.SelectedItem = item.Content as ViewModelBase;
                }
            }
            SpeechServices.Stop();
        }

        private void StartPeriodicAgent()
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            // Note the time when the app is opened
            if (!settings.Contains("timeLastUsed"))
            {
                settings.Add("timeLastUsed", DateTime.Now.ToString());
            }
            else
            {
                settings["timeLastUsed"] = DateTime.Now.ToString();
            }

            // Variable for tracking enabled status of background agents for this app.
            agentsAreEnabled = true;

            // Obtain a reference to the period task, if one exists
            periodicTask = ScheduledActionService.Find(periodicTaskName) as PeriodicTask;

            // If the task already exists and background agents are enabled for the
            // application, you must remove the task and then add it again to update 
            // the schedule
            if (periodicTask != null)
            {
                RemoveAgent(periodicTaskName);
            }

            periodicTask = new PeriodicTask(periodicTaskName);

            // The description is required for periodic agents. This is the string that the user
            // will see in the background services Settings page on the device.
            periodicTask.Description = "This demonstrates a periodic task.";

            // Place the call to Add in a try block in case the user has disabled agents.
            try
            {
                ScheduledActionService.Add(periodicTask);

                // If debugging is enabled, use LaunchForTest to launch the agent in one minute.
#if(DEBUG_AGENT)
                ScheduledActionService.LaunchForTest(periodicTaskName, TimeSpan.FromSeconds(10));
#endif
            }
            catch (InvalidOperationException exception)
            {
                if (exception.Message.Contains("BNS Error: The action is disabled"))
                {
                    MessageBox.Show("Background agents for this application have been disabled by the user.");
                    agentsAreEnabled = false;
                }

                if (exception.Message.Contains("BNS Error: The maximum number of ScheduledActions of this type have already been added."))
                {
                    // No user action required. The system prompts the user when the hard limit of periodic tasks has been reached.

                }
            }
            catch (SchedulerServiceException)
            {
                // No user action required.
            }
        }

        private void StartResourceIntensiveAgent()
        {
            // Variable for tracking enabled status of background agents for this app.
            agentsAreEnabled = true;

            resourceIntensiveTask = ScheduledActionService.Find(resourceIntensiveTaskName) as ResourceIntensiveTask;

            // If the task already exists and background agents are enabled for the
            // application, you must remove the task and then add it again to update 
            // the schedule.
            if (resourceIntensiveTask != null)
            {
                RemoveAgent(resourceIntensiveTaskName);
            }

            resourceIntensiveTask = new ResourceIntensiveTask(resourceIntensiveTaskName);

            // The description is required for periodic agents. This is the string that the user
            // will see in the background services Settings page on the device.
            resourceIntensiveTask.Description = "This demonstrates a resource-intensive task.";

            // Place the call to Add in a try block in case the user has disabled agents.
            try
            {
                ScheduledActionService.Add(resourceIntensiveTask);

                // If debugging is enabled, use LaunchForTest to launch the agent in one minute.
#if(DEBUG_AGENT)
                ScheduledActionService.LaunchForTest(resourceIntensiveTaskName, TimeSpan.FromSeconds(10));
#endif
            }
            catch (InvalidOperationException exception)
            {
                if (exception.Message.Contains("BNS Error: The action is disabled"))
                {
                    MessageBox.Show("Background agents for this application have been disabled by the user.");
                    agentsAreEnabled = false;

                }
            }
            catch (SchedulerServiceException)
            {
                // No user action required.
            }


        }
        private void RemoveAgent(string name)
        {
            try
            {
                ScheduledActionService.Remove(name);
            }
            catch (Exception)
            {
            }
        }
    }
}
