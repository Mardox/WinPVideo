using System;
using System.Windows;
using System.Windows.Input;


using AppStudio.Data.YouTube;using AppStudio.Services;

using Microsoft.Phone.Shell;

namespace AppStudio.Data
{
    public class TrailersViewModel : ViewModelBase<YouTubeSchema>
    {
        override protected string CacheKey
        {
            get { return "TrailersDataSource"; }
        }

        override protected IDataSource<YouTubeSchema> CreateDataSource()
        {
            return new TrailersDataSource(); // YouTubeDataSource
        }

        override public bool IsGoToSourceVisible
        {
            get { return ViewType == ViewTypes.Detail; }
        }
        
        override public void GoToSource()
        {
            base.GoToSource("{ExternalUrl}");
        }

        override public bool IsRefreshVisible
        {
            get { return ViewType == ViewTypes.List; }
        }

        override protected void NavigateToSelectedItem()
        {
            var currentItem = GetCurrentItem();
            if (currentItem != null)
            {
                PhoneApplicationService.Current.State["videoObject"] = GetVideo(currentItem);
                NavigationServices.NavigateToPage("YouTubeViewer");
            }
        }

        private YTHelper GetVideo(YouTubeSchema item)
        {
            return new YTHelper { Title = "Detail", Summary = item.Summary, ExternalUrl = item.ExternalUrl, EmbedHtmlFragment = item.EmbedHtmlFragment };
        }
    }
}
