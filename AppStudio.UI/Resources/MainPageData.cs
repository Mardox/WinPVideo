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
                                    "Top",
                                    "More"
                                };

        string[] queryName = {
                                 "PL2067F563C9ABC592",
                                 "PL6208108772F8B791",
                                 "PL5B0D01D64C817C33",
                                 "PLddMio_eGRxe9EYZqCrJywZaBYenVu7kT",
                                 "PLFA2E79F30697AC84"
                             };
        public string admobBanner = "ca-app-pub-3230884902788293/2587157996";
        public string adMobInterstetial = "ca-app-pub-3230884902788293/9970823995";
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
