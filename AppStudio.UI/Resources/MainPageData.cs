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
                                    "Tutorials",
                                    "Lessons",
                                    "Tricks",
                                    "Competetions",
                                    "More"
                                };

        string[] queryName = {
                                 "PL7rMLZSTcHBfrRvFuimfb9QS4DMht-ywW",
                                 "PL_VKUxMTnGJQmipV2De5ZpsQnpbv9Hfdp",
                                 "PL49505D88A4A21F60",
                                 "golf competition 2014",
                                 "PLttrWOrrte2vu8X7jvB5tU3RAsHiWg2LT"
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
