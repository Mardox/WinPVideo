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
                                    "Popular",
                                    "Trailers",
                                    "More"
                                };

        string[] queryName = {
                                 "PL63E3D69CC5BECAC5",
                                 "PLE55BA6424F0A6B2C",
                                 "PLg1rwoOvapSDkBFsCJu65YhXJCKf-Q4eB",
                                 "Myanmar Movie Trailers",
                                 "PL3276C1D7400A9965"
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
