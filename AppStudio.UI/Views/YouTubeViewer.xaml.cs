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

namespace AppStudio
{
    public partial class YouTubeViewer : PhoneApplicationPage
    {
        public YouTubeViewer()
        {
            InitializeComponent();
            YouTubeModel = new YTViewerViewModel();
            DataContext = YouTubeModel;
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
            base.OnBackKeyPress(e);
        }
    }
}
