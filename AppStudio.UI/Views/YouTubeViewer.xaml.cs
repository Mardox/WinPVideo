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

namespace AppStudio
{
    public partial class YouTubeViewer : PhoneApplicationPage
    {
        private InterstitialAd interstitialAd;
        public YouTubeViewer()
        {
            InitializeComponent();
            YouTubeModel = new YTViewerViewModel();
            DataContext = YouTubeModel;


            interstitialAd = new InterstitialAd("ca-app-pub-3230884902788293/6718136398");
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



        public YTViewerViewModel YouTubeModel { get; set; }

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
                NavigationService.GoBack();
            }
            else
            {
                NavigationServices.NavigateToPage("MainPage");
            }
            
            
            interstitialAd.ShowAd();
            base.OnBackKeyPress(e);
        }
    }
}
