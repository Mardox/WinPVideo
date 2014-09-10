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
        private string fromToast;
        public SearchPage()
        {
            InitializeComponent();
            Loaded += SearchPage_Loaded;
        }

        void SearchPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("searchData"))
            {
                callLoadData();
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
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

        private async void callLoadData()
        {
            MainViewModels = new MainViewModels();
            MainViewModels.SetViewType(ViewTypes.List);
            DataContext = MainViewModels;
            MainViewModels.UpdateAppBar();
            await MainViewModels.LoadSearchData(NetworkInterface.GetIsNetworkAvailable());
        }
    }
}