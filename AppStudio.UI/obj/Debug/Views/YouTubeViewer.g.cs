﻿#pragma checksum "C:\Users\HooMan\Desktop\Windows Apps\Test App\AppStudio.UI\Views\YouTubeViewer.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "47C4DCF738C94775201DD73D37543835"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Cimbalino.Phone.Toolkit.Behaviors;
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


namespace AppStudio {
    
    
    public partial class YouTubeViewer : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.VisualStateGroup PageOrientationStates;
        
        internal System.Windows.VisualState Landscape;
        
        internal System.Windows.VisualState LandscapeLeft;
        
        internal System.Windows.VisualState LandscapeRight;
        
        internal System.Windows.VisualState Portrait;
        
        internal System.Windows.VisualState PortraitUp;
        
        internal System.Windows.VisualState PortraitDown;
        
        internal System.Windows.Controls.RowDefinition TopMargin;
        
        internal System.Windows.Controls.RowDefinition Title;
        
        internal System.Windows.Controls.RowDefinition WebBrowserRow;
        
        internal System.Windows.Controls.RowDefinition BottomMargin;
        
        internal Microsoft.Phone.Controls.WebBrowser WebBrowser;
        
        internal System.Windows.Controls.Border HeaderHost;
        
        internal Cimbalino.Phone.Toolkit.Behaviors.ApplicationBarBehavior appBar;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/AppStudio;component/Views/YouTubeViewer.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.PageOrientationStates = ((System.Windows.VisualStateGroup)(this.FindName("PageOrientationStates")));
            this.Landscape = ((System.Windows.VisualState)(this.FindName("Landscape")));
            this.LandscapeLeft = ((System.Windows.VisualState)(this.FindName("LandscapeLeft")));
            this.LandscapeRight = ((System.Windows.VisualState)(this.FindName("LandscapeRight")));
            this.Portrait = ((System.Windows.VisualState)(this.FindName("Portrait")));
            this.PortraitUp = ((System.Windows.VisualState)(this.FindName("PortraitUp")));
            this.PortraitDown = ((System.Windows.VisualState)(this.FindName("PortraitDown")));
            this.TopMargin = ((System.Windows.Controls.RowDefinition)(this.FindName("TopMargin")));
            this.Title = ((System.Windows.Controls.RowDefinition)(this.FindName("Title")));
            this.WebBrowserRow = ((System.Windows.Controls.RowDefinition)(this.FindName("WebBrowserRow")));
            this.BottomMargin = ((System.Windows.Controls.RowDefinition)(this.FindName("BottomMargin")));
            this.WebBrowser = ((Microsoft.Phone.Controls.WebBrowser)(this.FindName("WebBrowser")));
            this.HeaderHost = ((System.Windows.Controls.Border)(this.FindName("HeaderHost")));
            this.appBar = ((Cimbalino.Phone.Toolkit.Behaviors.ApplicationBarBehavior)(this.FindName("appBar")));
        }
    }
}

