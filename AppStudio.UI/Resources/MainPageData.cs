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
                                 "PL3dYowBrcf5_XtgGD4PKdWTuXnIn2h27C",
                                 "PLBB819A102F2E8D31",
                                 "PLMsnE1dBWcFGce-C92QPYo_5Fy1zBzfw-",
                                 "PLZ7LJejf54QVqj-fe_Lxl6HJiVtnT9z98",
                                 "holi songs"
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
