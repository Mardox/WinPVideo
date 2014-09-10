#define DEBUG_AGENT
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

using Microsoft.Phone.Controls;
using Microsoft.Phone.Net.NetworkInformation;

using AppStudio.Services;
using Microsoft.Phone.Scheduler;

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

        public MainPage()
        {
            InitializeComponent();
            MainViewModels = new MainViewModels();
            StartPeriodicAgent();
        }

        public MainViewModels MainViewModels { get; private set; }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            // Main page must be always the top entry
            NavigationService.RemoveBackEntry();

            MainViewModels.SetViewType(ViewTypes.List);

            DataContext = MainViewModels;
            MainViewModels.UpdateAppBar();
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
