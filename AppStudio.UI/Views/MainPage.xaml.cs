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
using GoogleAds;
using AppStudio.Resources;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.IO;

namespace AppStudio
{
    public partial class MainPage
    {
        public static int index;
        //for backgroundtask
        PeriodicTask periodicTask;
        string periodicTaskName = "PeriodicAgent";
        public bool agentsAreEnabled = true;
        private InterstitialAd interstitialAd;
        private MainPageData data;
        public MainViewModels MainViewModels { get; private set; }

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
                settings.Add("toastNotifications", true);
                StartPeriodicAgent();
            }

            LoadBannerAd();

            interstitialAd = new InterstitialAd(AppResources.AdMobHomeInterstitial);
            AdRequest adRequest = new AdRequest();

            interstitialAd.ReceivedAd += OnAdReceived;
            interstitialAd.DismissingOverlay += OnAdDismissed;
            interstitialAd.LoadAd(adRequest);

            //for DrawCeleb Page
            ReadFile();
            AllNamesLoad();
        }

        public ObservableCollection<AppStudio.MainViewModels.ItemViewModel> AllNames { get; private set; }
        public static IList<string> listnameofceleb = new List<string>();

        public void AllNamesLoad()
        {
            this.AllNames = new ObservableCollection<AppStudio.MainViewModels.ItemViewModel>();
            this.AllNames.Clear();
            for (int i = 0; i < listnameofceleb.Count; i++)
            {
                this.AllNames.Add(new AppStudio.MainViewModels.ItemViewModel() { Title = listnameofceleb[i], LargeImage = "/images/" + i.ToString() + "/" + i.ToString() + "0.png" });
            }
            celebnameslist.ItemsSource = AllNames;
        }

        private void ReadFile()
        {
            listnameofceleb.Clear();
            data = new MainPageData();
            foreach (string item in data.drawItemsList)
            {
                listnameofceleb.Add(item);
            }
        }

        private void LoadBannerAd()
        {
            AdView bannerAd = new AdView
            {
                Format = AdFormats.Banner,
                AdUnitID = AppResources.AdMobBanner,
            };
            AdRequest adRequest = new AdRequest();
            // Assumes we've defined a Grid that has a name
            // directive of ContentPanel.
            LayoutRoot.Children.Add(bannerAd);
            bannerAd.VerticalAlignment = VerticalAlignment.Bottom;
            bannerAd.LoadAd(adRequest);
        }



        private void OnAdReceived(object sender, AdEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Ad received successfully");

        }

        private void OnAdDismissed(object sender, AdEventArgs e)
        {
            Application.Current.Terminate();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            //Do your work here
            try
            {
                interstitialAd.ShowAd();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                var result = MessageBox.Show("Come back soon!", "Are you sure?", MessageBoxButton.OKCancel);
                e.Cancel = result != MessageBoxResult.OK;
            }
            
            base.OnBackKeyPress(e);
        }

        #region Data for the MainPage
        void SaveSettings()
        {
            data = new MainPageData();
            string[] finalQuery = data.returnFinalQuery();
            string[] categoryName = data.returnCategoryName();

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
        #endregion

        

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
            //if (panorama.SelectedItem == drawItem)
            //{
            //    progressBar.Visibility = Visibility.Collapsed;
            //    appBar.IsVisible = false;
            //}
            SpeechServices.Stop();
        }

        private void StartPeriodicAgent()
        {
            //IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            // Note the time when the app is opened
            //if (!settings.Contains("timeLastUsed"))
            //{
            //    settings.Add("timeLastUsed", DateTime.Now.ToString());
            //}
            //else
            //{
            //    settings["timeLastUsed"] = DateTime.Now.ToString();
            //}

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
            periodicTask.Description = "Background task of " + AppResources.ApplicationTitle;

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

        private void celebgrid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            index = celebnameslist.Items.IndexOf((sender as Grid).DataContext);
            NavigationService.Navigate(new Uri("/Views/draw.xaml", UriKind.Relative));
        }

        private void drawItem_Loaded(object sender, RoutedEventArgs e)
        {
            progressBar.Visibility = Visibility.Collapsed;
        }
    }
}
