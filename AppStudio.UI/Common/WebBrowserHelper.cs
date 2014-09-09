using System;
using System.Windows;
using Microsoft.Phone.Controls;

namespace AppStudio
{
    public class WebBrowserHelper
    {
        public static readonly DependencyProperty HtmlProperty =
            DependencyProperty.RegisterAttached("Html",
            typeof(string),
            typeof(WebBrowserHelper),
            new PropertyMetadata(null, OnHtmlChanged));

        public static string GetHtml(DependencyObject obj)
        {
            return (string)obj.GetValue(HtmlProperty);
        }

        public static void SetHtml(DependencyObject obj, string value)
        {
            obj.SetValue(HtmlProperty, value);
        }

        private static void OnHtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser webBrowser = d as WebBrowser;
            if (webBrowser != null)
            {
                if (e.NewValue == null)
                {
                    webBrowser.Navigate(new Uri("about:blank"));
                }
                else
                {
                    webBrowser.NavigateToString(string.Format("<!DOCTYPE html><html><head><style>html, body, iframe {{ margin:0; padding:0; height:100%; }} iframe {{ display:block; width:100%; border:none; }}</style><meta name='viewport' content='width=device-width,height=270,user-scalable=no' /></head><body bgcolor='black'><iframe frameborder='0' src='{0}'/></body></html>", e.NewValue));
                }
            }
        }
    }
}
