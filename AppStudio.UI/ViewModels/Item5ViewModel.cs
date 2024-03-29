﻿using AppStudio.Data.YouTube;
using AppStudio.Services;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class Item5ViewModel : ViewModelBase<YouTubeSchema>
    {
        override protected string CacheKey
        {
            get { return "Item5DataSource"; }
        }

        override protected IDataSource<YouTubeSchema> CreateDataSource()
        {
            return new Item5DataSource(); // YouTubeDataSource
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
            return new YTHelper { Title = item.Title, Summary = item.Summary, ExternalUrl = item.ExternalUrl, EmbedHtmlFragment = item.EmbedHtmlFragment };
        }
    }
}
