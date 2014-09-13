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

namespace AppStudio.Views
{
    public partial class SearchPage : PhoneApplicationPage
    {
        public MainViewModels MainViewModels { get; private set; }
        public SearchPage()
        {
            InitializeComponent();
            Loaded += SearchPage_Loaded;

            searchAdUnit.FailedToReceiveAd += searchAdUnit_FailedToReceiveAd;
        }

        void searchAdUnit_FailedToReceiveAd(object sender, GoogleAds.AdErrorEventArgs e)
        {
            searchAdUnit.Visibility = Visibility.Collapsed;
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
                searchAdUnit.Visibility = Visibility.Collapsed;
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
                searchAdUnit.Visibility = Visibility.Collapsed;
            }
        }
    }
}