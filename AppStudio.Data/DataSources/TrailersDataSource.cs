using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class TrailersDataSource : IDataSource<YouTubeSchema>
    {
        private const string _url = @"https://gdata.youtube.com/feeds/api/videos?q=2014+movie+trailers&orderby=relevance&start-index=1&max-results=20&safeSearch=strict&format=5&v=2";

        private IEnumerable<YouTubeSchema> _data = null;

        public TrailersDataSource()
        {
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

        public async Task<IEnumerable<YouTubeSchema>> Refresh()
        {
            _data = null;
            return await LoadData();
        }
    }
}
