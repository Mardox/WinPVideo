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
                                    "Lessons",
                                    "Tricks",
                                    "Competitions",
                                    "Styles",
                                    "More"
                                };

        string[] queryName = {
                                 "PLF7D39B1F2BFF79DF",
                                 "PLP2iDvHJFpM_U2trkhLTJ5jkgS5qLrSzE",
                                 "PL91FC8D5D757D135C",
                                 "PL831747C14F06755C",
                                 "PLuolWeYY_qL1WmdK7t1O7jYgdc7nqw-f0"
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
