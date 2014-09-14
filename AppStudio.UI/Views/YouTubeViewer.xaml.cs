using System.ComponentModel;
using System.Windows.Navigation;
using AppStudio.Data.YouTube;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using AppStudio.Data;
using System.IO.IsolatedStorage;
using AppStudio.Services;
using GoogleAds;
using Microsoft.Phone.Tasks;
using AppStudio.Resources;

namespace AppStudio
{
    public partial class YouTubeViewer : PhoneApplicationPage
    {
        private InterstitialAd interstitialAd;
        public YTViewerViewModel YouTubeModel { get; set; }
        public YouTubeViewer()
        {
            InitializeComponent();
            YouTubeModel = new YTViewerViewModel();
            DataContext = YouTubeModel;

            interstitialAd = new InterstitialAd(AppResources.AdMobDetailInterstitial);
            AdRequest adRequest = new AdRequest();

            interstitialAd.ReceivedAd += OnAdReceived;
            interstitialAd.DismissingOverlay += OnAdDismissed;
            interstitialAd.LoadAd(adRequest);

        }


        private void OnAdReceived(object sender, AdEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Ad received successfully");

        }

        private void OnAdDismissed(object sender, AdEventArgs e)
        {
            NavigationService.GoBack();
        }



        

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (PhoneApplicationService.Current.State.ContainsKey("videoObject"))
            {
                this.YouTubeModel.SelectedItem = PhoneApplicationService.Current.State["videoObject"] as YTHelper;
            }
            else if (IsolatedStorageSettings.ApplicationSettings.Contains("videoItemSummary"))
            {
                string videoItemSummary = IsolatedStorageSettings.ApplicationSettings["videoItemSummary"] as string;
                string videoItemExternalUrl = IsolatedStorageSettings.ApplicationSettings["videoItemExternalUrl"] as string;
                string videoItemEmbedHtmlFragment = IsolatedStorageSettings.ApplicationSettings["videoItemEmbedHtmlFragment"] as string;
                YTHelper videoItem = new YTHelper { Title = "Detail", Summary = videoItemSummary, ExternalUrl = videoItemExternalUrl, EmbedHtmlFragment = videoItemEmbedHtmlFragment };
                this.YouTubeModel.SelectedItem = videoItem;
            }
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            this.YouTubeModel.SelectedItem = null;
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (PhoneApplicationService.Current.State.ContainsKey("videoObject"))
            {
                try
                {
                    int count = 1;
                    IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                    if (!settings.Contains("videoViewerBackCount"))
                    {
                        settings.Add("videoViewerBackCount", 1);
                    }
                    else
                    {
                        count = (int) IsolatedStorageSettings.ApplicationSettings["videoViewerBackCount"];
                        settings["videoViewerBackCount"] = count + 1;
                    }
                    settings.Save();
                    if (count % 3 == 0)
                    {
                        interstitialAd.ShowAd();
                    }
                }
                catch (Exception ex)
                {
                    NavigationService.GoBack();
                }
                
            }
            else
            {
                NavigationServices.NavigateToPage("MainPage");
                interstitialAd.ShowAd();
            }
            base.OnBackKeyPress(e);
        }

        private void ShareClick(object sender, EventArgs e)
        {
            ShareLinkTask shareLinkTask = new ShareLinkTask();
            shareLinkTask.Title = YouTubeModel.SelectedItem.Title + AppResources.CustomShareMessage + " " + AppResources.ApplicationTitle;
            shareLinkTask.LinkUri = new Uri(YouTubeModel.SelectedItem.ExternalUrl, UriKind.Absolute);
            //shareLinkTask.Message = AppResources.CustomShareMessage + AppResources.ApplicationTitle;

            shareLinkTask.Show();
        }
    }
}
