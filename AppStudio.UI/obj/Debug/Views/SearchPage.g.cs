﻿#pragma checksum "C:\Users\Kalyan\Source\Repos\WindowsPhoneVideoApp\AppStudio.UI\Views\SearchPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9900F2A85C7C420717E7DF4A437C174E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using GoogleAds;
using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace AppStudio.Views {
    
    
    public partial class SearchPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal GoogleAds.AdView searchAdUnit;
        
        internal System.Windows.Controls.TextBox SearchInput;
        
        internal System.Windows.Controls.Button SearchButton;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Microsoft.Phone.Controls.Panorama Container;
        
        internal Microsoft.Phone.Controls.PanoramaItem SearchItem;
        
        internal System.Windows.Controls.ProgressBar ProgressBar;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/AppStudio;component/Views/SearchPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.searchAdUnit = ((GoogleAds.AdView)(this.FindName("searchAdUnit")));
            this.SearchInput = ((System.Windows.Controls.TextBox)(this.FindName("SearchInput")));
            this.SearchButton = ((System.Windows.Controls.Button)(this.FindName("SearchButton")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.Container = ((Microsoft.Phone.Controls.Panorama)(this.FindName("Container")));
            this.SearchItem = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("SearchItem")));
            this.ProgressBar = ((System.Windows.Controls.ProgressBar)(this.FindName("ProgressBar")));
        }
    }
}

