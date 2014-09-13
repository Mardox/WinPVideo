using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class Item2DataSource : IDataSource<YouTubeSchema>
    {
        private string baseUrl = "https://gdata.youtube.com/feeds/api/videos?q=";
        private string baseEndUrl = "&orderby=relevance&start-index=1&max-results=20&safeSearch=strict&format=5&v=2";

        private string baseUrlPlaylist = "https://gdata.youtube.com/feeds/api/playlists/";
        private string baseEndUrlPlatlist = "?v=2";

        private string _queryString = null;
        private string _url = "";
        private string _finalBaseUrl = null;
        private string _finalBaseEndUrl = null;

        private IEnumerable<YouTubeSchema> _data = null;

        public Item2DataSource()
        {
        }

        public async Task<IEnumerable<YouTubeSchema>> LoadData()
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("Item2Data"))
            {
                _queryString = IsolatedStorageSettings.ApplicationSettings["Item2Data"] as string;
            }
            if (_queryString.Contains("Playlist"))
            {
                _queryString = _queryString.Remove(0, 8);
                _finalBaseUrl = baseUrlPlaylist;
                _finalBaseEndUrl = baseEndUrlPlatlist;
            }
            else if (_queryString.Contains("Search"))
            {
                _queryString = _queryString.Remove(0, 6);
                _finalBaseUrl = baseUrl;
                _finalBaseEndUrl = baseEndUrl;
            }

            if (_queryString.Contains(" "))
            {
                _queryString = _queryString.Replace(" ", "+");
            }
            _url = _finalBaseUrl + _queryString + _finalBaseEndUrl;
            if (_data == null)
            {
                try
                {
                    var youTubeDataProvider = new YouTubeDataProvider(_url);
                    _data = await youTubeDataProvider.Load();
                }
                catch (Exception ex)
                {
                    AppLogs.WriteError("Item2DataSource.LoadData", ex.ToString());
                }
            }
            return _data;
        }

        public async Task<IEnumerable<YouTubeSchema>> Refresh()
        {
            _data = null;
            return await LoadData();
        }
    }
}
