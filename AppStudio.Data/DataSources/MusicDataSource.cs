using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class MusicDataSource : IDataSource<YouTubeSchema>
    {
        private const string _url =  @"https://gdata.youtube.com/feeds/api/videos?q=music&orderby=published&start-index=1&max-results=20&safeSearch=strict&format=5&v=2";

        private IEnumerable<YouTubeSchema> _data = null;

        public MusicDataSource()
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
                    AppLogs.WriteError("MusicDataSourceDataSource.LoadData", ex.ToString());
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
