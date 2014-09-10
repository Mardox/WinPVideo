#define DEBUG_AGENT
using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using System;
using MyToolkit.Multimedia;
using System.Net;
using System.IO;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Collections.Generic;


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
        private bool ranOnce = false;
        private YoutubeVideo video;
        private List<YoutubeVideo> videosList = new List<YoutubeVideo>();
        YoutubeVideo finalVideo;
        protected override void OnInvoke(ScheduledTask task)
        {
            //TODO: Add code to perform your task in background
            DateTime dt = DateTime.Now;
            int hours = dt.Hour;
            int minute = dt.Minute;
            string baseUrl = "https://gdata.youtube.com/feeds/api/videos?q=";
            string baseEndUrl = "&orderby=published&start-index=1&max-results=20&safeSearch=strict&format=5&v=2";
            string _queryString = null;
            string _url = "";

            string[] topics = { "Apple",
                                "Microsoft",
                              "Google",
                              "Facebook",
                              "Twitter",
                              "Android",
                              "Samsung",
                              "Sony",
                              "iOS",
                              "Windows"};
            
            if (hours <= 20 && hours >= 15)
            {
               
                    Random r = new Random();
                    int randomTopic = r.Next(0, 10);
                    _queryString = topics[3];
                    _url = baseUrl + _queryString + baseEndUrl;



                    GetData(_url);
                    //int randomVideo = r.Next(1, 19); 
                
            }
            // If debugging is enabled, launch the agent again in one minute.
#if DEBUG_AGENT
  ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(10));
#endif
            
        }

        //RSS 
        private void GetData(string url)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadStringAsync(new System.Uri(url));
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
        }


        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else
            {
                GetYoutubeVideos(e.Result);
            }
        }


        //Get Youtube Channel 
        private void GetYoutubeVideos(string feedXML)
        {
            try
            {
                StringReader stringReader = new StringReader(feedXML);
                XmlReader xmlReader = XmlReader.Create(stringReader);
                SyndicationFeed feed = SyndicationFeed.Load(xmlReader);

                
                foreach (SyndicationItem item in feed.Items)
                {
                    video = new YoutubeVideo();

                    video.VideoUrl = item.Links[0].Uri.ToString();
                    string a = video.VideoUrl.Remove(0, 32);
                    video.VideoId = a.Substring(0, 11);
                    video.Title = item.Title.Text;
                    //video.PubDate = item.PublishDate.DateTime;

                    //video.ImageUrl = YouTube.GetThumbnailUri(video.VideoId, YouTubeThumbnailSize.Large);

                    videosList.Add(video);
                }
                finalVideo = videosList[2];

                string toastMessage = "Check out this new video";
                // Launch a toast to show that the agent is running.
                // The toast will not be shown if the foreground application is running.
                ShellToast toast = new ShellToast();
                toast.Title = video.Title;
                toast.Content = toastMessage;
                toast.NavigationUri = new System.Uri("/Views/DetailsPage.xaml?VideoId=" + finalVideo.VideoId, System.UriKind.Relative);
                toast.Show();
                NotifyComplete();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        } 
    }
}