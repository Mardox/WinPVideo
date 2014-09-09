using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class SearchDataSource : IDataSource<YouTubeSchema>
    {
        private string baseUrl = "https://gdata.youtube.com/feeds/api/videos?q=";
        private string baseEndUrl = "&orderby=published&start-index=1&max-results=20&safeSearch=strict&format=5&v=2";
        private string _queryString = null;
        private string _url = "";

        private IEnumerable<YouTubeSchema> _data = null;

        public SearchDataSource()
        {
        }

        public async Task<IEnumerable<YouTubeSchema>> LoadData()
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("searchData"))
            {
                _queryString = IsolatedStorageSettings.ApplicationSettings["searchData"] as string;
            }

            if (_queryString.Contains(" "))
            {
                _queryString = _queryString.Replace(" ", "+");
            }
            _url = baseUrl + _queryString + baseEndUrl;
            if (_data == null)
            {
                try
                {
                    var youTubeDataProvider = new YouTubeDataProvider(_url);
                    _data = await youTubeDataProvider.Load();
                }
                catch (Exception ex)
                {
                    AppLogs.WriteError("SearchDataSourceDataSource.LoadData", ex.ToString());
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
