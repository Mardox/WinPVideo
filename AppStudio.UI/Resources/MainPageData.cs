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
                                 "PLl-TWbc2VGoY_xHhDwfxgHTLNHaN0nQR_",
                                 "PLE2AE59AE2815C007",
                                 "PLl57O3VXgEuUIqeZZmLy9qm3JnzWydbBV",
                                 "PL43CAC95984379452",
                                 "PL62D660EF9FCA7E84"
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
