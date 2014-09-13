using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Net.NetworkInformation;
using System.Diagnostics;
using AppStudio.Resources;
using GoogleAds;

namespace AppStudio.Views
{
    public partial class SearchPage : PhoneApplicationPage
    {
        public MainViewModels MainViewModels { get; private set; }
        AdView bannerAd;
        public SearchPage()
        {
            InitializeComponent();
            Loaded += SearchPage_Loaded;
            LoadBannerAd();
            bannerAd.FailedToReceiveAd += searchAdUnit_FailedToReceiveAd;
        }

        private void LoadBannerAd()
        {
            bannerAd = new AdView
            {
                Format = AdFormats.Banner,
                AdUnitID = AppResources.AdMobBanner,
            };
            AdRequest adRequest = new AdRequest();
            // Assumes we've defined a Grid that has a name
            // directive of ContentPanel.
            LayoutRoot.Children.Add(bannerAd);
            bannerAd.VerticalAlignment = VerticalAlignment.Top;
            bannerAd.LoadAd(adRequest);
        }

        void searchAdUnit_FailedToReceiveAd(object sender, GoogleAds.AdErrorEventArgs e)
        {
            bannerAd.Visibility = Visibility.Collapsed;
        }

        void SearchPage_Loaded(object sender, RoutedEventArgs e)
        {
            SearchInput.Focus();
            if (IsolatedStorageSettings.ApplicationSettings.Contains("searchData"))
            {
                callLoadData();
            }
            else
            {
                ProgressBar.Visibility = Visibility.Collapsed;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                // SearchInput is a TextBox defined in XAML.
                if (!settings.Contains("searchData"))
                {
                    settings.Add("searchData", SearchInput.Text);
                }
                else
                {
                    settings["searchData"] = SearchInput.Text;
                }
                settings.Save();
                callLoadData();
            }
            else
            {
                bannerAd.Visibility = Visibility.Collapsed;
                MessageBox.Show("Check Network connection", "Sorry", MessageBoxButton.OK);
            }
            
        }

        private async void callLoadData()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                MainViewModels = new MainViewModels();
                MainViewModels.SetViewType(ViewTypes.List);
                DataContext = MainViewModels;
                MainViewModels.UpdateAppBar();
                await MainViewModels.LoadSearchData(NetworkInterface.GetIsNetworkAvailable());
            }
            else
            {
                bannerAd.Visibility = Visibility.Collapsed;
            }
        }
    }
}