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
                                    "Projects",
                                    "Cool Stuff",
                                    "Lectures",
                                    "Jobs",
                                    "More"
                                };

        string[] queryName = {
                                 "PLu47uXiGePcbq0M9tCJG9bh8CG8jJGt_P",
                                 "PLdaXzinAA0qR4C_ATm53g3QM6v7xQUGY3",
                                 "PL889C4F3DF29C5C08",
                                 "PLdaXzinAA0qS13dtxvdFEYysfiXyVBdF5",
                                 "PLu47uXiGePcYiAkPWQv6Wumuwq8tAtQx0"
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
