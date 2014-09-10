using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Net.NetworkInformation;
using MyToolkit.Multimedia;
using System.ComponentModel;

namespace AppStudio.Views
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        public DetailsPage()
        {
            InitializeComponent();
        }
        // When page is navigated to set data context to selected item in list 
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                MessageBox.Show("An error has occurred! Please verify your internet connection.");
                NavigationService.GoBack();
            }
            else
            {
                string VideoId = "";
                if (NavigationContext.QueryString.TryGetValue("VideoId", out VideoId))
                {
                    //VideoId = VideoId.Replace("=", "");
                    var url = await YouTube.GetVideoUriAsync(VideoId, YouTubeQuality.Quality360P);
                    //player.Source = url.Uri;
                }
            }
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (YouTube.CancelPlay()) // used to abort current youtube download
                e.Cancel = true;
            else
            {
                // your code here
            }
            base.OnBackKeyPress(e);
        }

    }
}