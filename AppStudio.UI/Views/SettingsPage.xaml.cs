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

namespace AppStudio.Views
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        private IsolatedStorageSettings settings;
        public SettingsPage()
        {
            InitializeComponent();
            settings = IsolatedStorageSettings.ApplicationSettings;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Retrieving values");
                toastNotifications.IsChecked = (bool)settings["toastNotifications"];
            }
            catch (KeyNotFoundException ex)
            {
                System.Diagnostics.Debug.WriteLine("First Time using the app");
                settings.Add("toastNotifications", true);
                settings.Save();

            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Exiting, so save now");
            settings["toastNotifications"] = toastNotifications.IsChecked;
        }
    }
}