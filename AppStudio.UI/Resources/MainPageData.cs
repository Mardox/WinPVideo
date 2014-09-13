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
                                    "Trailers",
                                    "Music",
                                    "Java",
                                    "iPhone 6",
                                    "Google I/O"
                                };

        string[] queryName = {
                                 "Movie Trailers 2014",
                                 "Music 2014",
                                 "PLDAA5DE54FB5215EC", //Playlist ID
                                 "iphone 6",
                                 "PLOU2XLYxmsIJQe6T9CKafiDm7p_LCCx6F" //Playlist ID
                             };

        public string[] returnCategoryName()
        {
            return categoryName;
        }
        public string[] returnFinalQuery()
        {
            string[] finalQuery = {
                                      categoryType[0] + queryName[0],
                                      categoryType[0] + queryName[1],
                                      categoryType[1] + queryName[2],
                                      categoryType[0] + queryName[3],
                                      categoryType[1] + queryName[4],
                                  };
            return finalQuery;
        }
    }
}
