using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStudio.Resources
{
    public class MainPageData
    {
        public MainPageData() { }
        string[] categoryType = {
                                    "Search",
                                    "Playlist"
                                };

        string[] categoryName = {
                                    "New",
                                    "Old",
                                    "Pop",
                                    "Movie Song",
                                    "More"
                                };

        string[] queryName = {
                                 "PLStyg_2A0Lw4jeFObWq5oB8or0lkInIxH",
                                 "PLfAqY_46pgUOwh4Hdr928lS-l79MWdaxJ",
                                 "PLJjEUxMM8w8JmXadqhPHwR0CHdzAzLcjg",
                                 "PLF7548261947BA6EC",
                                 "PL43F311D825BF4C11"
                             };

        public string[] returnCategoryName()
        {
            return categoryName;
        }
        public string[] returnFinalQuery()
        {
            string[] finalQuery = new string[queryName.Length];
            for (int i = 0; i < queryName.Length; i++)
            {
                if (queryName[i].StartsWith("PL"))
                {
                    finalQuery[i] = categoryType[1] + queryName[i];
                }
                else
                {
                    finalQuery[i] = categoryType[0] + queryName[i];
                }
            }
            return finalQuery;
        }
    }
}
