#define DEBUG_AGENT
using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using System;
using System.Net;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using AppStudio.Data;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppStudio;
using System.IO.IsolatedStorage;


namespace ScheduledTaskAgent1
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        static ScheduledAgent()
        {
            // Subscribe to the managed exception handler
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// Code to execute on Unhandled Exceptions
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>

        #region All required variables
        private List<YouTubeSchema> videosList = new List<YouTubeSchema>();
        YTHelper videoItem;
        string baseUrl = "https://gdata.youtube.com/feeds/api/playlists/";
        string baseEndUrl = "?v=2";
        string _queryString = null;
        string _url = "";
        private ObservableCollection<YouTubeSchema> resultItems;
        private IEnumerable<YouTubeSchema> _data = null;

        private string[] topics = {"PLEF339D927FB7B009",
                                 "PL4CC01EA57955A7C8",
                                 "PLaoaE0FasyziIGLKUi6b_uR6gCtRh0sS6",
                                 "PLB615B6B6241F1679",
                                 "PL74042A9AD18C7EB7"};

        DateTime dt = DateTime.Now;
        int minHour = 17, maxHour = 18;

        #endregion


        // Main method that run when the background agent is invoked
        protected override void OnInvoke(ScheduledTask task)
        {
            int hours = dt.Hour;
            int minute = dt.Minute;

            // If debugging is enabled, launch the agent again in one minute.
#if DEBUG_AGENT
            ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(10));
#endif

            #region Check for the right time to Toast
            if (dt.Hour >= minHour && dt.Hour < maxHour)
            {
                if (dt.Minute >= 0 && dt.Minute <= 30)
                {
                    Random r = new Random();
                    int randomTopic = r.Next(0, topics.Length);
                    _queryString = topics[randomTopic];
                    _url = baseUrl + _queryString + baseEndUrl;
                    fetchData();
                }
            }
            else if (dt.Hour < minHour && dt.Hour > maxHour)
            {
                NotifyComplete();
            }
            #endregion
        }


        #region Background Job to fetch the YouTube data
        private async void fetchData()
        {
            try
            {
                var resultData = await LoadData();
                resultItems = (ObservableCollection<YouTubeSchema>)resultData;
                Random r = new Random();
                int randomValue = r.Next(0, resultItems.Count);
                var finaldata = resultItems[randomValue];
                helper(finaldata);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("No network while executing backgroundAgent " + ex.ToString());
            }
        }

        public async Task<IEnumerable<YouTubeSchema>> LoadData()
        {
            if (_data == null)
            {
                try
                {
                    var youTubeDataProvider = new YouTubeDataProvider(_url);
                    _data = await youTubeDataProvider.Load();
                }
                catch (Exception ex)
                {
                    AppLogs.WriteError("TrailersDataSourceDataSource.LoadData", ex.ToString());
                }
            }
            return _data;
        }

        private void helper(YouTubeSchema item)
        {
            videoItem = new YTHelper { Title = item.Title, Summary = item.Summary, ExternalUrl = item.ExternalUrl, EmbedHtmlFragment = item.EmbedHtmlFragment };
            string[] toastMessage = { "Check this out!", "Today's Video of The Day", "Watch This Now!", "Here is Our Pick" };
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            // SearchInput is a TextBox defined in XAML.

            if (!settings.Contains("videoItemSummary"))
            {
                settings.Add("videoItemSummary", videoItem.Summary);
                settings.Add("videoItemExternalUrl", videoItem.ExternalUrl);
                settings.Add("videoItemEmbedHtmlFragment", videoItem.EmbedHtmlFragment);
            }
            else
            {
                settings["videoItemSummary"] = videoItem.Summary;
                settings["videoItemExternalUrl"] = videoItem.ExternalUrl;
                settings["videoItemEmbedHtmlFragment"] = videoItem.EmbedHtmlFragment;
            }
            settings.Save();

            Random r = new Random();
            // Launch a toast to show that the agent is running.
            // The toast will not be shown if the foreground application is running.
            ShellToast toast = new ShellToast();
            toast.Title = toastMessage[r.Next(0, toastMessage.Length)];
            toast.Content = videoItem.Title;
            toast.NavigationUri = new System.Uri("/Views/YouTubeViewer.xaml", System.UriKind.Relative);
            toast.Show();

            NotifyComplete();
        }
        #endregion

    }
}